using Guadalupe.Conexao.App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.App.Repository
{
    sealed class NoticeRepository : INoticeRepository
    {
        public Task<List<Notice>> GetAsync()
        {
            return Database
                .DB
                .Table<Notice>()
                .ToListAsync();
        }

        public Task<List<Notice>> GetAsync(Guid[] ids)
        {
            if (!ids.Any())
                return Task.FromResult(new List<Notice>());

            return Database.DB.Table<Notice>()
                .Where((n) => ids.Contains(n.Id))
                .ToListAsync();
        }

        public Task InsertAsync(List<Notice> notices)
        {
            return Database.DB.InsertAllAsync(notices, typeof(Notice), true);
        }

        public Task RemoveAsync(Guid[] ids)
        {
            if (!ids.Any()) return Task.CompletedTask;

            var tasks = new List<Task>();

            foreach (var id in ids)
            {
                tasks.Add(Database.DB.DeleteAsync<Notice>(id));
            }

            return Task.WhenAll(tasks);
        }

        public Task UpdateAsync(List<Notice> notices)
        {
            return Database.DB.UpdateAllAsync(notices, true);
        }
    }
}
