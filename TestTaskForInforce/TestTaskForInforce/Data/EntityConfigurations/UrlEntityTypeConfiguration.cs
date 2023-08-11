using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskForInforce.Data.Entities;

namespace TestTaskForInforce.Data.EntityConfigurations
{
    public class UrlEntityTypeConfiguration : IEntityTypeConfiguration<UrlEntity>
    {
        public void Configure(EntityTypeBuilder<UrlEntity> builder)
        {
            builder.ToTable("Url");

            builder.HasKey(h => h.Id);
            builder.Property(p => p.BaseUrl).IsRequired();
            builder.Property(p => p.ShortenedUrl).IsRequired();

            builder.Property(p => p.CreatedAt).HasColumnType("date");
        }
    }
}
