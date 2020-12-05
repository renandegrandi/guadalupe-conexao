using Guadalupe.Conexao.App.Model;
using Guadalupe.Conexao.App.Repository;

namespace Guadalupe.Conexao.App.Service
{
    public class SessionService : ISessionService
    {
        public User Autenticated { get; private set; }
        public bool IsAutenticated { 
            get 
            {
                return Autenticated != null;
            }
        }

        public SessionService(IUserRepository userRepository)
        {
            Autenticated = userRepository.GetAsync()
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        public User GetUser()
        {
            return Autenticated;
        }
        public void SetUser(User user)
        {
            Autenticated = user;
        }
    }
}
