using Guadalupe.Conexao.Api.Core.Data;
using Guadalupe.Conexao.Api.Domain;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Api.Infrastructure.Data.Repositories
{
    sealed class UserRepository : IUserRepository
    {
        #region Dependencies

        private readonly ConexaoContext _context;

        #endregion

        #region Constructor

        public UserRepository(ConexaoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        #endregion

        public void Dispose()
        {
            _context?.Dispose();
        }

        public Task AddAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<Notice>> GetLastNoticesAsync(Guid user, DateTime? lastUpdate, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetPersonByEmailAsync(string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SaveRefreshTokenAsync(Guid user, string refreshToken, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
