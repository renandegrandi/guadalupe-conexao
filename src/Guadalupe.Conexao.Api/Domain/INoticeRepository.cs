using Guadalupe.Conexao.Api.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Api.Domain
{
    public interface INoticeRepository : IRepository<Notice>
    {
        Task<List<Notice>> GetLastNoticesAsync(DateTime? lastUpdate, CancellationToken cancellationToken);
    }
}
