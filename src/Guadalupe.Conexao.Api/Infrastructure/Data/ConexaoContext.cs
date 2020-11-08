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

        #endregion

        public Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
        public Task<int> CommitAsync()
        {
            return base.SaveChangesAsync();
        }
        void IUnitOfWork.Attach(object input)
        {
            this.Attach(input);
        }
        void IUnitOfWork.Add(object input) 
        {
            this.Add(input);
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.ApplyConfiguration(new Configurations.Person());
            modelBuilder.ApplyConfiguration(new Configurations.User());
        }


    }
}
