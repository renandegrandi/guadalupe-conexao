using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Guadalupe.Conexao.Api.Infrastructure.Data.Configurations
{
    public class Notice : IEntityTypeConfiguration<Domain.Notice>
    {
        public void Configure(EntityTypeBuilder<Domain.Notice> builder)
        {
            builder
                .ToTable("notice")
                .HasKey((b) => b.Id);

            builder.Property((b) => b.Id)
                .ValueGeneratedNever()
                .IsRequired()
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasColumnName("id");

            builder.Property((b) => b.Title)
                .IsUnicode(false)
                .IsRequired()
                .HasColumnName("title");

            builder.Property((b) => b.Message)
                .IsUnicode(false)
                .IsRequired()
                .HasColumnName("message");

            builder.Property((b) => b.Image)
                .IsUnicode(false)
                .HasColumnName("image_url");

            builder.Property((b) => b.IdPostedBy)
                .HasColumnName("id_posted_by")
                .IsRequired();

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

            builder.HasOne((b) => b.PostedBy)
                .WithMany()
                .HasForeignKey((b) => b.IdPostedBy);
        }
    }
}
