using Guadalupe.Conexao.App.Model;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.App.Repository
{
    public interface IUserRepository 
    {
        Task SaveAsync(User user);
        Task<User> GetAsync();

        Task UpdateAsync(User user);
    }
}
