using Guadalupe.Conexao.Api.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Api.Services
{
    public interface IIdentityService
    {
        Task<Person> GetAutenticatedPersonAsync(CancellationToken cancellationToken);
        Task<User> GetAutenticatedUserAsync(CancellationToken cancellationToken);
    }
}
