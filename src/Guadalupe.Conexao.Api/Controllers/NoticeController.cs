using AutoMapper;
using Guadalupe.Conexao.Api.Domain;
using Guadalupe.Conexao.Api.Models.V1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NoticeController : ControllerBase
    {
        #region Dependencies

        private readonly ILogger<NoticeController> _logger;
        private readonly INoticeRepository _noticeRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public NoticeController(ILogger<NoticeController> logger,
            INoticeRepository noticeRepository,
            IMapper mapper)
        {
            _logger = logger;
            _noticeRepository = noticeRepository;
            _mapper = mapper;
        }

        #endregion

        /// <summary>
        /// Retorna as últimas noticias cadastradas de acordo com a´última data/hora de atualização do usuário.
        /// </summary>
        /// <param name="lastupdated">Data/Hora da última solicitação de notificações.</param>
        [HttpGet("last_updated")]
        public async Task<IActionResult> GetLastNoticeAsync([FromQuery(Name = "last")]DateTime? lastupdated) 
        {
            var notices = await _noticeRepository.GetLastNoticesAsync(lastupdated, HttpContext.RequestAborted);

            var mapped = _mapper.Map<List<NoticeDto>>(notices);

            return Ok(mapped);
        }
    }
}
