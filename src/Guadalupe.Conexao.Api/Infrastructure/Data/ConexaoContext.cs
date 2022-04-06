using Guadalupe.Conexao.Api.Core.Data;
using Guadalupe.Conexao.Api.Core.DomainObject;
using Guadalupe.Conexao.Api.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Guadalupe.Conexao.Api.Infrastructure.Data
{
    sealed class ConexaoContext: DbContext, IUnitOfWork
    {
        public DbSet<Person> Person { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Notice> Notice { get; set; }

        public ConexaoContext(DbContextOptions<ConexaoContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.Person());
            modelBuilder.ApplyConfiguration(new Configurations.User());
            modelBuilder.ApplyConfiguration(new Configurations.Notice());
        }
        private void SetModifiedPropertiesOnCommit() 
        {
            var entries = ChangeTracker.Entries();

            var paraIncluir = entries.Where((e) => e.Properties.Any((p) => p.Metadata.Name == nameof(Entity.Registration)));
            var paraAtualizar = entries.Where((e) => e.Properties.Any((p) => p.Metadata.Name == nameof(Entity.Modification)) && e.State == EntityState.Modified);
            var uniqueIdModificado = entries.Where((e) => e.Properties.Any((p) => p.Metadata.Name == nameof(Entity.Id)) && e.State == EntityState.Modified);

            foreach (var item in paraIncluir.Where((e) => e.State == EntityState.Added))
                item.Property(nameof(Entity.Registration)).CurrentValue = DateTime.Now;

            foreach (var item in paraIncluir.Where((e) => e.State == EntityState.Modified))
                item.Property(nameof(Entity.Registration)).IsModified = false;

            foreach (var item in paraAtualizar)
                item.Property(nameof(Entity.Modification)).CurrentValue = DateTime.Now;

            foreach (var item in uniqueIdModificado)
                item.Property(nameof(Entity.Id)).IsModified = false;
        }
        public Task<int> CommitAsync(CancellationToken cancellationToken)
        {
            SetModifiedPropertiesOnCommit();

            return base.SaveChangesAsync(cancellationToken);
        }
        void IUnitOfWork.Attach(object input)
        {
            this.Attach(input);
        }
        void IUnitOfWork.Add(object input) 
        {
            this.Add(input);
        }
        
    }
}
