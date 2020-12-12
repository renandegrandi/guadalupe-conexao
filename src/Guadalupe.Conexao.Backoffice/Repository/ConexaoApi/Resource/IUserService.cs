using Guadalupe.Conexao.Backoffice.Repository.ConexaoApi.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Backoffice.Repository.ConexaoApi.Resource
{
    public interface IUserService
    {
        Task<AuthenticationTokenDto> AuthenticationAsync(AuthenticationDto authentication, CancellationToken cancellationToken);
    }
}
