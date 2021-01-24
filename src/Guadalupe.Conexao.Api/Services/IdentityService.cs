using Guadalupe.Conexao.Api.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Api.Services
{
    public class IdentityService : IIdentityService
    {
        #region Dependencies

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        #endregion

        public Person PersonAutenticated { get; private set; }
        public User UserAutenticated { get; private set; }

        public IdentityService(IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public async Task<Person> GetAutenticatedPersonAsync(CancellationToken cancellationToken)
        {
            if (PersonAutenticated != null) return PersonAutenticated;

            var id = Guid.Parse(_httpContextAccessor.HttpContext.User
                .Claims
                .First((c) => c.Type == "userid")
                .Value);

            PersonAutenticated = await _userRepository.GetByIdAsync(id, cancellationToken);

            _userRepository.UnitOfWork.Attach(PersonAutenticated);

            return PersonAutenticated;
        }

        public async Task<User> GetAutenticatedUserAsync(CancellationToken cancellationToken)
        {
            if (UserAutenticated != null) return UserAutenticated;

            var person = await GetAutenticatedPersonAsync(cancellationToken);

            UserAutenticated = await _userRepository.GetByPersonIdAsync(person.Id, cancellationToken);

            return UserAutenticated;
        }
    }
}
