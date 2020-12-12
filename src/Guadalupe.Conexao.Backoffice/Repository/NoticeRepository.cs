using AutoMapper;
using Guadalupe.Conexao.Backoffice.Core;
using Guadalupe.Conexao.Backoffice.Models;
using Guadalupe.Conexao.Backoffice.Repository.ConexaoApi.Models;
using Guadalupe.Conexao.Backoffice.Repository.ConexaoApi.Resource;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Backoffice.Repository
{
    public class NoticeRepository : INoticeRepository
    {
        #region Dependencies

        private readonly INoticeService _noticeService;
        private readonly IMapper _mapper;

        #endregion

        public NoticeRepository(INoticeService noticeService,
            IMapper mapper)
        {
            _noticeService = noticeService;
            _mapper = mapper;
        }

        public Task<Guid> AddAsync(NoticeViewModel notice, CancellationToken cancellationToken)
        {
            var noticeCreateDto = _mapper.Map<NoticeCreateDto>(notice);

            return _noticeService.AdicionarAsync(noticeCreateDto, cancellationToken);
        }

        public async Task<PaginatorViewModel<NoticeViewModel>> GetPaginatedAsync(string title, int index, CancellationToken cancellationToken)
        {
            var api = await _noticeService.GetAsync(title, index, 6, cancellationToken);

            var mappedRegister = _mapper.Map<List<NoticeViewModel>>(api.Registers);

            return new PaginatorViewModel<NoticeViewModel>
            {
                Index = index,
                Registers = mappedRegister,
                Total = api.TotalRegisters,
                Search = title
            };
        }

        public async Task<NoticeViewModel> GetByIdAsync(Guid id, CancellationToken cancellationToken) 
        {
            var api = await _noticeService.GetByIdAsync(id, cancellationToken);

            var mapped = _mapper.Map<NoticeViewModel>(api);

            return mapped;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken) 
        {
            await _noticeService.DeleteAsync(id, cancellationToken);
        }
    }
}
