using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Guadalupe.Conexao.Api.Infrastructure.Data.Configurations
{
    public class Person : IEntityTypeConfiguration<Domain.Person>
    {
        public void Configure(EntityTypeBuilder<Domain.Person> builder)
        {
            builder
                .ToTable("person")
                .HasKey("id");

            builder.Property((b) => b.Id)
                .ValueGeneratedNever()
                .IsRequired()
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasColumnName("id");

            builder.Property((b) => b.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasMaxLength(255);

            builder.Property((b) => b.Nome)
                .HasColumnName("nome")
                .HasMaxLength(255);

            builder.Property((b) => b.Registration)
                .IsRequired()
                .HasDefaultValueSql("getdate()")
                .HasColumnType("datetime")
                .HasColumnName("registration_date");

            builder.Property((b) => b.Modification)
                .HasColumnType("datetime")
                .HasColumnName("modification_date");

            builder.Property((b) => b.Removal)
                .HasColumnType("datetime")
                .HasColumnName("removal_date");

        }
    }
}
