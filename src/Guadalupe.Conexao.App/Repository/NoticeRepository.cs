using Guadalupe.Conexao.App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.App.Repository
{
    sealed class NoticeRepository : INoticeRepository
    {
        public async Task<List<Notice>> GetAsync()
        {
            var notices = await Database
                .DB
                .Table<Notice>()
                .OrderByDescending((q) => q.Posted)
                .ToListAsync()
                .ConfigureAwait(false);

            if (notices.Any()) 
            {
                var idPersons = notices
                    .GroupBy((p) => p.IdPostedBy)
                    .Select((p) => p.Key)
                    .ToArray();

                var persons = await Database
                    .DB
                    .Table<Person>()
                    .Where((q) => idPersons.Contains(q.Id))
                    .ToListAsync()
                    .ConfigureAwait(false);

                foreach (var notice in notices)
                {
                    notice.PostedBy = persons
                        .FirstOrDefault((p) => p.Id == notice.IdPostedBy);
                }
            }

            return notices;
        }

        public Task<List<Notice>> GetAsync(Guid[] ids)
        {
            if (!ids.Any())
                return Task.FromResult(new List<Notice>());

            return Database.DB.Table<Notice>()
                .Where((n) => ids.Contains(n.Id))
                .ToListAsync();
        }

        public async Task InsertAsync(List<Notice> notices)
        {
            var persons = notices.Select((p) => p.PostedBy).ToArray();

            foreach (var person in persons)
            {
                await Database.DB.InsertOrReplaceAsync(person, typeof(Person))
                    .ConfigureAwait(false);
            }

            if(notices.Any())
                await Database.DB.InsertAllAsync(notices, typeof(Notice), true)
                    .ConfigureAwait(false);
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
