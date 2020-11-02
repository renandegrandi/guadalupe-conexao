using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Api.Domain
{
    public interface IUserRepository
    {
        Task<List<Notice>> GetLastNoticesAsync(Guid user, DateTime? lastUpdate);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByRefreshTokenAsync(string refreshToken);
        Task SaveRefreshTokenAsync(Guid user, string refreshToken);
    }
}
