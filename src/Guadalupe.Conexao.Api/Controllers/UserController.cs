using AutoMapper;
using Guadalupe.Conexao.Api.Config;
using Guadalupe.Conexao.Api.Domain;
using Guadalupe.Conexao.Api.Models.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
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
        private readonly IMapper _mapper;
        private readonly AuthenticationConfig _authentication;

        #endregion

        #region Constructor

        public UserController(IUserRepository userRepository,
            IMapper mapper,
            IOptions<AuthenticationConfig> autenticationConfig)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _authentication = autenticationConfig.Value;
        }

        #endregion

        #region Private Methods

        public string GenerateToken(User user, DateTime date)
        {
            var key = Encoding.ASCII.GetBytes(_authentication.Jwt.SymmetricKey);

            

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature);

            var claims = new Claim[] {
                new Claim(ClaimTypes.Name, user.Person.Email)
            };

            var expires = date.AddHours(2);

            var jwt = new JwtSecurityToken(null, _authentication.Jwt.Audience, claims, date, expires, signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        #endregion

        /// <summary>
        /// Responsável por gerar um novo código de acesso e enviar para o e-mail que foi solicitado.
        /// A função irá verificar se o email já está cadastrado na nossa base de dados se não encontrar irá gerar um novo registro de usuário e pessoa, disparando um novo código a cada requisição para o e-mail do solicitante.
        /// </summary>
        /// <param name="email">Email que deseja receber um novo código de acesso.</param>
        [HttpPut("{email}/send_code")]
        public async Task<IActionResult> SendNewCodeByEmailAsync([FromRoute(Name = "email"), EmailAddress]string email) 
        {
            var user = await _userRepository.GetByEmailAsync(email, HttpContext.RequestAborted);

            var newUser = user == null;

            var unitOfWork = _userRepository.UnitOfWork;

            if (newUser)
            {
                var person = await _userRepository.GetPersonByEmailAsync(email, HttpContext.RequestAborted);

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

            await unitOfWork.CommitAsync(HttpContext.RequestAborted);

            //TODO: Realizar a implementação do envio de e-mail.

            return NoContent();
        }

        /// <summary>
        /// Método responsável por gerar um token de acesso para a aplicação.
        /// </summary>
        [HttpPost("token")]
        public async Task<IActionResult> AuthenticationAsync([FromBody]AuthenticationDto authentication)
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

                    var codigoInvalido = !user.CodeAccess.Equals(authentication.Password);

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
                    user = await _userRepository.GetByRefreshTokenAsync(authentication.RefreshToken, HttpContext.RequestAborted);

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

            var now = DateTime.Now;

            var accessToken = GenerateToken(user, now);

            var refreshToken = Guid.NewGuid().ToString();

            await _userRepository.SaveRefreshTokenAsync(user.Id, refreshToken, HttpContext.RequestAborted);

            var expiresIn = now.AddHours(2).Millisecond.ToString();

            return Ok(new AuthenticationTokenDto { 
                AccessToken = accessToken,
                ExpiresIn = expiresIn,
                RefreshToken = refreshToken,
            });
        }

        /// <summary>
        /// Retorna as últimas noticias para o usuário de acordo com a data da última solicitação de atualização.
        /// </summary>
        /// <param name="lastUpdate">Data da última solicitação de atualização de noticias</param>
        [HttpGet("notices")]
        [Authorize]
        public async Task<IActionResult> GetNoticesByLastUpdateAsync([FromQuery(Name = "last_update")] DateTime? lastUpdate)
        {
            Guid user = Guid.NewGuid();

            var notices = await _userRepository.GetLastNoticesAsync(user, lastUpdate, HttpContext.RequestAborted);

            var noticesMapped = _mapper.Map<List<NoticeDto>>(notices);

            return Ok(noticesMapped);
        }
    }
}
