using AutoMapper;
using Guadalupe.Conexao.Api.Domain;
using Guadalupe.Conexao.Api.Models.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        #endregion

        #region Constructor

        public UserController(IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        #endregion

        /// <summary>
        /// Retorna as últimas noticias para o usuário de acordo com a data da última solicitação de atualização.
        /// </summary>
        /// <param name="lastUpdate">Data da última solicitação de atualização de noticias</param>
        [HttpGet]
        [Authorize]
        [Route("notices")]
        public async Task<IActionResult> GetNoticesByLastUpdateAsync([FromQuery(Name = "last_update")] DateTime? lastUpdate) 
        {
            var user = 1;

            var notices = await _userRepository.GetLastNoticesAsync(user, lastUpdate);

            var noticesMapped = _mapper.Map<List<NoticeDto>>(notices);

            return Ok(noticesMapped);
        }
    }
}
