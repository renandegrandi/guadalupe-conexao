using Guadalupe.Conexao.Api.Core.Data;
using Guadalupe.Conexao.Api.Domain;
using Guadalupe.Conexao.Api.Models.V1;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Api.Infrastructure.Data.Repositories
{
    sealed class NoticeRepository : INoticeRepository
    {
        #region Depencies

        private readonly ConexaoContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        #endregion

        #region Constructor

        public NoticeRepository(ConexaoContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        #endregion

        #region Properties

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Notice notice)
        {
            _context.Add(notice);
        }

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
            {
                var universalTime = lastUpdate.Value.ToUniversalTime();

                query = query.Where((n) => n.Registration > universalTime ||
                    (n.Modification != null && n.Modification > universalTime) ||
                    (n.Removal != null && n.Removal >= universalTime));
            }

            return query
                .OrderBy((n) => n.Registration)
                .Take(20)
                .ToListAsync(cancellationToken);
        }
        public async Task<string> UploadImageAsync(Guid id, IFormFile image)
        {
            var filePath = Path.Combine(_webHostEnvironment.ContentRootPath,
                "Assets/Imagens", id.ToString());

            using (var stream = File.Create(filePath))
                await image.CopyToAsync(stream);

            return "";
        }
        public async Task<PaginatedResult<Notice>> GetAsync(string title, int index, int size, CancellationToken cancellationToken) 
        {
            var query = _context.Notice
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where((c) => c.Title.Contains(title));

            var totalRegisters = await query
                .CountAsync(cancellationToken);

            var registers = await query
                .OrderByDescending((q) => q.Registration)
                .Skip((index - 1) * size)
                .Take(size)
                .ToListAsync(cancellationToken);

            return new PaginatedResult<Notice>
            {
                TotalRegisters = totalRegisters,
                Registers = registers
            };
        }
        public Task<Notice> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _context
                .Notice
                .AsNoTracking()
                .Include((p) => p.PostedBy)
                .FirstOrDefaultAsync((n) => n.Id == id, cancellationToken);
        }
        public void Remove(Notice notice)
        {
            _context.Remove(notice);
        }
    }
}
