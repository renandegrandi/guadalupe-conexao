using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Api.Core.Data
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken);
        Task<int> CommitAsync();
    }
}
