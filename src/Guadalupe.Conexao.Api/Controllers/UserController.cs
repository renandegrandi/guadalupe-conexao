using AutoMapper;
using Guadalupe.Conexao.Api.Config;
using Guadalupe.Conexao.Api.Domain;
using Guadalupe.Conexao.Api.Models.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
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

        public string GenerateToken(User user)
        {
            var key = Encoding.ASCII.GetBytes(_authentication.Jwt.SymmetricKey);

            var expires = DateTime.UtcNow.AddHours(2);

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature);

            var claims = new Claim[] {
                new Claim(ClaimTypes.Name, user.Email)
            };

            var now = DateTime.Now;

            var jwt = new JwtSecurityToken(null, _authentication.Jwt.Audience, claims, now, expires, signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        #endregion

        [HttpPost]
        [Route("/token")]
        public async Task<IActionResult> AuthenticationAsync([FromBody]AuthenticationDto authentication)
        {
            User user = null;

            switch (authentication.GrantType)
            {
                case GrantTypes.password:
                    user = await _userRepository.GetByEmailAsync(authentication.Username);

                    if (user == null) 
                    {
                        return Unauthorized(new AuthenticationErroDto
                        {
                            Erro = OAuthError.invalid_client,
                            Descricao = "Email ou código invalido!"
                        });
                    }

                    break;
                case GrantTypes.refresh_token:
                    user = await _userRepository.GetByRefreshTokenAsync(authentication.RefreshToken);

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

            await _userRepository.SaveRefreshTokenAsync(user.Id, refreshToken);

            return Ok(new AuthenticationTokenDto { 
                AccessToken = accessToken,
                ExpiresIn = TimeSpan.FromHours(2).ToString(),
                RefreshToken = refreshToken,
            });
        }

        /// <summary>
        /// Retorna as últimas noticias para o usuário de acordo com a data da última solicitação de atualização.
        /// </summary>
        /// <param name="lastUpdate">Data da última solicitação de atualização de noticias</param>
        [HttpGet]
        [Authorize]
        [Route("notices")]
        public async Task<IActionResult> GetNoticesByLastUpdateAsync([FromQuery(Name = "last_update")] DateTime? lastUpdate)
        {
            Guid user = Guid.NewGuid();

            var notices = await _userRepository.GetLastNoticesAsync(user, lastUpdate);

            var noticesMapped = _mapper.Map<List<NoticeDto>>(notices);

            return Ok(noticesMapped);
        }
    }
}
