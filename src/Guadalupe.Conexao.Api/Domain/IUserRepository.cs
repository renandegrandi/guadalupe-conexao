using Guadalupe.Conexao.Api.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Api.Domain
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<Notice>> GetLastNoticesAsync(Guid user, DateTime? lastUpdate, CancellationToken cancellationToken);
        Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<User> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
        Task<Person> GetPersonByEmailAsync(string email, CancellationToken cancellationToken);
        Task SaveRefreshTokenAsync(Guid user, string refreshToken, CancellationToken cancellationToken);
        Task AddAsync(User user, CancellationToken cancellationToken);
    }
}
