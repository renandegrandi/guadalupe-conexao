using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Guadalupe.Conexao.Backoffice.Core;
using Guadalupe.Conexao.Backoffice.Models;
using Guadalupe.Conexao.Backoffice.Repository.ConexaoApi.Models;
using Guadalupe.Conexao.Backoffice.Repository.ConexaoApi.Resource;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Backoffice.Repository
{
    public class NoticeRepository : INoticeRepository
    {
        #region Dependencies

        private readonly INoticeService _noticeService;
        private readonly IMapper _mapper;
        private readonly BlobContainerClient _assetsContainer;

        #endregion

        public NoticeRepository(INoticeService noticeService,
            IMapper mapper,
            IOptions<Config> config)
        {
            _noticeService = noticeService;
            _mapper = mapper;
            _assetsContainer = new BlobContainerClient(config.Value.ConexaoStorage, "assets");
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

        public Task FileUploadAsync(string name, MemoryStream file, string mimetype, CancellationToken cancellationToken)
        {
            BlobClient blob = _assetsContainer.GetBlobClient(name);

            var options = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = mimetype
                }
            };

            return blob.UploadAsync(file, options, cancellationToken);
        }
    }
}
