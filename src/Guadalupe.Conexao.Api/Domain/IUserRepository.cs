using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Api.Domain
{
    public interface IUserRepository
    {
        Task<List<Notice>> GetLastNoticesAsync(int user, DateTime? lastUpdate);
    }
}
