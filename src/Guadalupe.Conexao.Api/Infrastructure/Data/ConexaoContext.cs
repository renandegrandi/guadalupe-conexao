using Guadalupe.Conexao.Api.Core.Data;
using Guadalupe.Conexao.Api.Domain;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Api.Infrastructure.Data
{
    public class ConexaoContext: DbContext, IUnitOfWork
    {
        #region Constructor

        public ConexaoContext(DbContextOptions<ConexaoContext> options) : base(options)
        {

        }

        #endregion

        #region Properties

        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<User> User { get; set; }

        public Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        public Task<int> CommitAsync()
        {
            return base.SaveChangesAsync();
        }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.ApplyConfiguration(new Configurations.Person());
            modelBuilder.ApplyConfiguration(new Configurations.User());
        }

    }
}
