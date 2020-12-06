using Guadalupe.Conexao.Api.Core.Data;
using Guadalupe.Conexao.Api.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return _context
                .User
                .AsNoTracking()
                .Include((c) => c.Person)
                .Where((c) => c.Person.Email.Equals(email))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task<User> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
        {
            return _context
                .User
                .AsNoTracking()
                .Include((c) => c.Person)
                .Where((c) => c.RefreshToken == refreshToken)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task<Person> GetPersonByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return _context
                .Person
                .AsNoTracking()
                .Where((c) => c.Email.Equals(email))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task SaveRefreshTokenAsync(Guid user, string refreshToken, CancellationToken cancellationToken)
        {
            var @params = new List<SqlParameter> {
                new SqlParameter("@refresh_token", refreshToken),
                new SqlParameter("@id", user)
            };

            return _context.Database.ExecuteSqlRawAsync($"UPDATE [user] SET refresh_token = @refresh_token WHERE id = @id", @params, cancellationToken);
        }
    }
}
