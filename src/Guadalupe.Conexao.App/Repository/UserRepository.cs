using Guadalupe.Conexao.App.Model;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.App.Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task<User> GetAsync()
        {
            var user = await Database
                .DB
                .Table<User>()
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);

            if(user != null) 
            {
                user.Person = await Database
                    .DB
                    .Table<Person>()
                    .Where((p) => p.Id == user.IdPerson)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
            }

            return user;
        }
        public async Task SaveAsync(User user)
        {
            await Database
                .DB
                .DeleteAllAsync<User>()
                .ConfigureAwait(false);

            await Database
                .DB
                .InsertOrReplaceAsync(user.Person, typeof(Person))
                .ConfigureAwait(false);

            await Database.DB.InsertAsync(user, typeof(User))
                .ConfigureAwait(false);
        }
        public async Task UpdateAsync(User user) 
        {
            await Database
                .DB
                .UpdateAsync(user);
        }
    }
}
