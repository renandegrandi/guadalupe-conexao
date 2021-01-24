using Guadalupe.Conexao.Api.Core.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Api.Domain
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken);
        Task<User> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
        Task<User> GetByPersonIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Person> GetPersonByEmailAsync(string email, CancellationToken cancellationToken);
        Task<Person> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task SaveRefreshTokenAsync(Guid user, string refreshToken, CancellationToken cancellationToken);
    }
}
