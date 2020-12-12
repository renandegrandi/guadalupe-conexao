using Guadalupe.Conexao.Api.Core.Data;
using Guadalupe.Conexao.Api.Models.V1;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Api.Domain
{
    public interface INoticeRepository : IRepository<Notice>
    {
        Task<List<Notice>> GetLastNoticesAsync(DateTime? lastUpdate, CancellationToken cancellationToken);
        Task<string> UploadImageAsync(Guid id, IFormFile image);
        void Add(Notice notice);
        Task<PaginatedResult<Notice>> GetAsync(string title, int index, int size, CancellationToken cancellationToken);
        Task<Notice> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        void Remove(Notice notice);
    }
}
