using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Api.Core.Data
{
    public interface IUnitOfWork
    {
        void Attach(object input);
        void Add(object input);
        Task<int> CommitAsync(CancellationToken cancellationToken);
    }
}
