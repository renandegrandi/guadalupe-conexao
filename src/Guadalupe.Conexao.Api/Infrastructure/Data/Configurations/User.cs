using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Guadalupe.Conexao.Api.Infrastructure.Data.Configurations
{
    public class User : IEntityTypeConfiguration<Domain.User>
    {
        public void Configure(EntityTypeBuilder<Domain.User> builder)
        {
            builder
                .ToTable("user")
                .HasKey((b) => b.Id);

            builder.Property((b) => b.Id)
                .ValueGeneratedNever()
                .IsRequired()
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasColumnName("id");

            builder.Property((b) => b.CodeAccess)
                .HasMaxLength(100)
                .HasColumnName("code_access");

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
