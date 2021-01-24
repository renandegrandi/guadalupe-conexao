using Guadalupe.Conexao.Backoffice.Core;
using Guadalupe.Conexao.Backoffice.Models;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Backoffice.Repository
{
    public interface INoticeRepository
    {
        Task<PaginatorViewModel<NoticeViewModel>> GetPaginatedAsync(string title, int index, CancellationToken cancellationToken);
        Task FileUploadAsync(string name, MemoryStream file, string mimetype, CancellationToken cancellationToken);
        Task<Guid> AddAsync(NoticeViewModel notice, CancellationToken cancellationToken);
        Task<NoticeViewModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
