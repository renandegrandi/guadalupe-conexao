using Guadalupe.Conexao.Api.Core.Data;
using Guadalupe.Conexao.Api.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Api.Infrastructure.Data.Repositories
{
    sealed class NoticeRepository : INoticeRepository
    {
        #region Depencies

        private readonly ConexaoContext _context;

        #endregion

        #region Constructor

        public NoticeRepository(ConexaoContext context)
        {
            _context = context;
        }

        #endregion

        #region Properties

        public IUnitOfWork UnitOfWork => _context;

        #endregion

        public void Dispose()
        {
            _context?.Dispose();
        }

        public Task<List<Notice>> GetLastNoticesAsync(DateTime? lastUpdate, CancellationToken cancellationToken)
        {
            var query = _context
                .Notice
                .AsNoTracking()
                .Include((n) => n.PostedBy)
                .AsQueryable();

            if (lastUpdate.HasValue)
                query = query.Where((n) => n.Registration > lastUpdate || n.Modification > lastUpdate || n.Removal >= lastUpdate);

            return query.ToListAsync(cancellationToken);
        }
    }
}
