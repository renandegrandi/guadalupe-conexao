using AutoMapper;
using Guadalupe.Conexao.Api.Domain;
using Guadalupe.Conexao.Api.Infrastructure.Config;
using Guadalupe.Conexao.Api.Infrastructure.Data;
using Guadalupe.Conexao.Api.Models.V1;
using Guadalupe.Conexao.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Dependencies

        private readonly IUserRepository _userRepository;
        private readonly AuthenticationConfig _authentication;
        private readonly IMapper _mapper;
        private readonly ISmtpService _smtpService;
        private readonly IIdentityService _identityService;

        #endregion

        #region Constructor

        public UserController(IUserRepository userRepository,
            IOptions<AuthenticationConfig> autenticationConfig,
            IMapper mapper,
            ISmtpService smtpService,
            IIdentityService identityService)
        {
            _userRepository = userRepository;
            _authentication = autenticationConfig.Value;
            _mapper = mapper;
            _smtpService = smtpService;
            _identityService = identityService;
        }

        #endregion

        #region Private Methods

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authentication.Jwt.SymmetricKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Person.Email.ToString()),
                    new Claim(ClaimTypes.Email, user.Person.Email.ToString()),
                    new Claim("userid", user.Person.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private Task SendAuthenticationCodeByEmailAsync(User user) 
        {
            //TODO: Implementar pattern para templates de e-mail na aplicação.

            var template = @"<!DOCTYPE html>
<html>
<head>
    <meta charset='utf - 8' />
    <title>{ASSUNTO_EMAIL}</title>
</head>
<body>
    <p> Seu código de acesso é: {CODIGO_ACESSO}.</p>
</body>
</html>";

            var body = template;

            var subject = "[Guadalupe.Conexão] Código de Acesso.";

            body = body.Replace("{ASSUNTO_EMAIL}", subject);
            body = body.Replace("{CODIGO_ACESSO}", user.CodeAccess);

            return _smtpService.SendAsync(user.Person.Email, subject, body);
        }

        #endregion

        /// <summary>
        /// Responsável por gerar um novo código de acesso e enviar para o e-mail que foi solicitado.
        /// A função irá verificar se o email já está cadastrado na nossa base de dados se não encontrar irá gerar um novo registro de usuário e pessoa, disparando um novo código a cada requisição para o e-mail do solicitante.
        /// </summary>
        /// <param name="email">Email que deseja receber um novo código de acesso.</param>
        [HttpPut("{email}/send_code")]
        public async Task<IActionResult> SendNewCodeByEmailAsync([FromRoute(Name = "email"), EmailAddress] string email)
        {
            var user = await _userRepository.GetByEmailAsync(email, HttpContext.RequestAborted)
                .ConfigureAwait(false);

            var newUser = user == null;

            var unitOfWork = _userRepository.UnitOfWork;

            if (newUser)
            {
                var person = await _userRepository.GetPersonByEmailAsync(email, HttpContext.RequestAborted)
                    .ConfigureAwait(false);

                if (person == null)
                    person = new Person(email);
                else
                    unitOfWork.Attach(person);

                user = new User(person);

                unitOfWork.Add(user);
            }
            else
            {
                unitOfWork.Attach(user);
                user.RegenerateCodeAccess();
            }

            await unitOfWork.CommitAsync(HttpContext.RequestAborted)
                .ConfigureAwait(false);

            await SendAuthenticationCodeByEmailAsync(user)
                .ConfigureAwait(false);

            return NoContent();
        }

        /// <summary>
        /// Método responsável por gerar um token de acesso para a aplicação.
        /// </summary>
        [HttpPost("token")]
        public async Task<IActionResult> AuthenticationAsync([FromBody] AuthenticationDto authentication)
        {
            User user = null;

            switch (authentication.GrantType)
            {
                case GrantTypes.password:

                    user = await _userRepository.GetByEmailAsync(authentication.Username, HttpContext.RequestAborted);

                    if (user == null)
                    {
                        return Unauthorized(new AuthenticationErroDto
                        {
                            Erro = OAuthError.invalid_client,
                            Descricao = $"O Email: {authentication.Username}, não está cadastrado!"
                        });
                    }

                    var codigoInvalido = !user.CodeAccess.Equals(authentication.Password.ToUpper());

                    if (codigoInvalido)
                    {
                        return Unauthorized(new AuthenticationErroDto
                        {
                            Erro = OAuthError.invalid_client,
                            Descricao = $"O Código informado está invalido!"
                        });
                    }


                    //TODO: Realizar a implementação de um time-out para expirar códigos muito antigos.

                    break;
                case GrantTypes.refresh_token:
                    user = await _userRepository.GetByRefreshTokenAsync(authentication.RefreshToken, HttpContext.RequestAborted)
                        .ConfigureAwait(false);

                    if (user == null)
                    {
                        return Unauthorized(new AuthenticationErroDto
                        {
                            Erro = OAuthError.invalid_request,
                            Descricao = "Código de autenticação invalido!"
                        });
                    }

                    break;
                default:
                    return Unauthorized(new AuthenticationErroDto
                    {
                        Erro = OAuthError.unsupported_grant_type,
                        Descricao = "grant_type não permitido pela aplicação!"
                    });
            }

            var accessToken = GenerateToken(user);

            var refreshToken = Guid.NewGuid().ToString();

            await _userRepository.SaveRefreshTokenAsync(user.Id, refreshToken, HttpContext.RequestAborted);

            var expiresIn = DateTime.Now.AddHours(2).Millisecond.ToString();

            var personMapping = _mapper.Map<PersonDto>(user.Person);

            return Ok(new AuthenticationTokenDto
            {
                AccessToken = accessToken,
                ExpiresIn = expiresIn,
                RefreshToken = refreshToken,
                UserInfo = personMapping
            });
        }

        /// <summary>
        /// Método reponsável por registrar e/ou alterar o token do fcm.
        /// </summary>
        [HttpPut("fcm_token")]
        [Authorize]
        public async Task<IActionResult> RegisterFirebaseTokenAsync([FromBody] FirebaseTokenDto fcmToken) 
        {
            var user = await _identityService.GetAutenticatedUserAsync(HttpContext.RequestAborted);

            user.ChangeFcmToken(fcmToken.Token);

            var unitOfWork = ((ConexaoContext)_userRepository.UnitOfWork);

            unitOfWork.Attach(user);

            await _userRepository.UnitOfWork.CommitAsync(HttpContext.RequestAborted);

            return NoContent();
        }
    }
}
