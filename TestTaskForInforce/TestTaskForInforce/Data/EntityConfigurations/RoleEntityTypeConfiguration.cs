using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestTaskForInforce.Data.Entities;

namespace TestTaskForInforce.Data.EntityConfigurations
{
    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable("Role");

            builder.HasKey(h => h.Id);
            builder.Property(p => p.Name).IsRequired();
        }
    }
}
