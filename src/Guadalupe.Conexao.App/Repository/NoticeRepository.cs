using Guadalupe.Conexao.App.Model;
using Guadalupe.Conexao.App.Repository.DTO;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.App.Repository
{
    sealed class NoticeRepository : INoticeRepository
    {
        private static PersonDto missao = new PersonDto
        {
            Id = Guid.NewGuid(),
            Name = "Missão Guadalupe",
            Email = "profile.jpg"
        };

        public NoticeRepository() 
        {
            if (!Database.DB.TableMappings.Any(m => m.MappedType.Name == typeof(Notice).Name))
            {
                var tables = new Task[] 
                {
                    Database.DB.CreateTableAsync<Person>(),
                    Database.DB.CreateTableAsync<Notice>()
                };

                Task.WhenAll(tables)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
            }
        }

        public Task<List<Notice>> GetAsync(CancellationToken cancellationToken)
        {
            return Database.DB.Table<Notice>().ToListAsync();
        }

        public Task<List<Notice>> GetAsync(Guid[] ids, CancellationToken cancellationToken)
        {
            if (!ids.Any())
                return Task.FromResult(new List<Notice>());

            return Database.DB.Table<Notice>()
                .Where((n) => ids.Contains(n.Id))
                .ToListAsync();
        }

        public Task<List<NoticeDto>> GetByDateAsync(DateTime? last, CancellationToken cancellationToken)
        {
            return Task.FromResult(new List<NoticeDto>
            {
                new NoticeDto 
                {
                    Id = Guid.NewGuid(),
                    Message = @"🤩 Bom dia com alegria e muito amor no coração, porque hoje é Dia das Crianças, é Dia de Nossa Senhora Aparecida e é dia de GRUPO DE ORAÇÃO!!!" +
                        @"🙏🏻Nem precisamos dizer que vai ser lindo, né? A convidada dessa noite é a Lidy Souza. Ela vai falar sobre a “Vida Missionária de Maria”, e está preparando tudo com muito carinho. " +
                        @"🙌🏻Esperamos você, às 20h, na Igreja Maceno. O número de pessoas permitido é limitado e será por ordem de chegada. " +
                        @"😉Convide a família e os amigos. A transmissão ao vivo pelo YouTube também continua. " +
                        @"🌐Link: https://bit.ly/3gHcMzL (inscreva-se no nosso canal do YouTube. Link direto na bio.)",
                    Image = "notice.jpg",
                    Posted = DateTime.Now,
                    PostedBy = missao,
                    State = NoticeDto.UserNoticeState.Included
                }
            });
        }

        public Task InsertAsync(List<Notice> notices, CancellationToken cancellationToken)
        {
            return Database.DB.InsertAllAsync(notices, typeof(Notice), true);
        }

        public Task RemoveAsync(Guid[] ids, CancellationToken cancellationToken)
        {
            if (!ids.Any()) return Task.CompletedTask;

            var tasks = new List<Task>();

            foreach (var id in ids)
            {
                tasks.Add(Database.DB.DeleteAsync<Notice>(id));
            }

            return Task.WhenAll(tasks);
        }

        public Task UpdateAsync(List<Notice> notices, CancellationToken cancellationToken)
        {
            return Database.DB.UpdateAllAsync(notices, true);
        }
    }
}
