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

        public Person Autenticated { get; private set; }

        public IdentityService(IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public async Task<Person> GetAutenticated(CancellationToken cancellationToken)
        {
            if (Autenticated != null) return Autenticated;

            var id = Guid.Parse(_httpContextAccessor.HttpContext.User
                .Claims
                .First((c) => c.Type == "userid")
                .Value);

            Autenticated = await _userRepository.GetByIdAsync(id, cancellationToken);

            _userRepository.UnitOfWork.Attach(Autenticated);

            return Autenticated;
        }
    }
}
