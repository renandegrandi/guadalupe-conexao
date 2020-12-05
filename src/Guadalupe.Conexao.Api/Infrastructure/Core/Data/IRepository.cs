using Guadalupe.Conexao.Api.Core.DomainObject;
using System;

namespace Guadalupe.Conexao.Api.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
