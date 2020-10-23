using Guadalupe.Conexao.App.Model;
using Guadalupe.Conexao.App.Repository.DTO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.App.Repository
{
    public interface INoticeRepository
    {
        Task<List<NewDto>> GetByDateAsync(DateTime? last, CancellationToken cancellationToken);
        Task<List<Notice>> GetAsync(CancellationToken cancellationToken);
        Task<List<Notice>> GetAsync(Guid[] ids, CancellationToken cancellationToken);
        Task RemoveAsync(Guid[] ids, CancellationToken cancellationToken);
        Task InsertAsync(List<Notice> notices, CancellationToken cancellationToken);
        Task UpdateAsync(List<Notice> notices, CancellationToken cancellationToken);
    }
}
