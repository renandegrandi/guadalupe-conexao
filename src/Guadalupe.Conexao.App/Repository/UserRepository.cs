using Guadalupe.Conexao.App.Model;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.App.Repository
{
    public class UserRepository : IUserRepository
    {
        public Task<User> GetAsync()
        {
            return Database
                .DB
                .Table<User>()
                .FirstOrDefaultAsync();
        }
        public async Task SaveAsync(User user)
        {
            await Database
                .DB
                .DeleteAllAsync<User>()
                .ConfigureAwait(false);

            await Database.DB.InsertAsync(user, typeof(User))
                .ConfigureAwait(false);
        }
    }
}
