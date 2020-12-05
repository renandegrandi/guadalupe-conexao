using Guadalupe.Conexao.App.Model;

namespace Guadalupe.Conexao.App.Service
{
    public interface ISessionService
    {
        bool IsAutenticated { get; }
        void SetUser(User user);
        User GetUser();
    }
}
