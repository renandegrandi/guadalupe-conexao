using Guadalupe.Conexao.Backoffice.Repository.ConexaoApi.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Backoffice.Repository.ConexaoApi.Resource
{
    public interface INoticeService
    {
        Task<Guid> AdicionarAsync(NoticeCreateDto notice, CancellationToken cancellationToken);
        Task<PaginatedResult<NoticeDto>> GetAsync(string title, int index, int size, CancellationToken cancellationToken);
        Task<NoticeDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
