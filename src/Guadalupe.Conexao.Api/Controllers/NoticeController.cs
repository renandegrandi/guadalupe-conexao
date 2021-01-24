using AutoMapper;
using Guadalupe.Conexao.Api.Domain;
using Guadalupe.Conexao.Api.Models.V1;
using Guadalupe.Conexao.Api.Services;
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
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;

        #endregion

        #region Constructor

        public NoticeController(ILogger<NoticeController> logger,
            INoticeRepository noticeRepository,
            IIdentityService identityService,
            IMapper mapper)
        {
            _logger = logger;
            _noticeRepository = noticeRepository;
            _identityService = identityService;
            _mapper = mapper;
        }

        #endregion

        /// <summary>
        /// Retorna as últimas noticias cadastradas de acordo com a´última data/hora de atualização do usuário.
        /// </summary>
        /// <param name="dataHora">Data/Hora da última solicitação de notificações.</param>
        [HttpGet("last_updated")]
        public async Task<IActionResult> GetLastNoticeAsync([FromQuery(Name = "data_hora")]DateTime? dataHora) 
        {
            var notices = await _noticeRepository.GetLastNoticesAsync(dataHora, HttpContext.RequestAborted);

            var mapped = _mapper.Map<List<NoticeDto>>(notices);

            return Ok(mapped);
        }

        /// <summary>
        /// Permite incluir uma noticia.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] NoticeCreateDto noticeCreate) 
        {
            var notice = _mapper.Map<Notice>(noticeCreate);

            var autenticated = await _identityService.GetAutenticatedPersonAsync(HttpContext.RequestAborted);

            notice
                .AddPostedBy(autenticated);

            _noticeRepository.Add(notice);

            await _noticeRepository.UnitOfWork.CommitAsync(HttpContext.RequestAborted);

            return Created($"api/notice/{notice.Id}", notice.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string title, int index, int size) 
        {
            if (index == 0) index = 1;

            var result = await _noticeRepository.GetAsync(title, index, size, HttpContext.RequestAborted);

            var mappedRegisters = _mapper.Map<List<NoticeDto>>(result.Registers);

            Response.Headers.Add("X-Total-Count", result.TotalRegisters.ToString());

            return Ok(mappedRegisters);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id) 
        {
            var notice = await _noticeRepository.GetByIdAsync(id, HttpContext.RequestAborted);

            if (notice == null)
                return BadRequest();

            _noticeRepository.Remove(notice);

            await _noticeRepository.UnitOfWork.CommitAsync(HttpContext.RequestAborted);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id) 
        {
            var notice = await _noticeRepository.GetByIdAsync(id, HttpContext.RequestAborted);

            if (notice == null)
                return BadRequest();

            var mapped = _mapper.Map<NoticeDto>(notice);

            return Ok(mapped);
        }
    }
}
