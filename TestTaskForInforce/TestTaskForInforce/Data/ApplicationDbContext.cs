using Microsoft.EntityFrameworkCore;
using TestTaskForInforce.Data.Entities;
using TestTaskForInforce.Data.EntityConfigurations;

namespace TestTaskForInforce.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<RoleEntity> Roles { get; set; } = null!;

        public DbSet<UserEntity> Users { get; set; } = null!;

        public DbSet<UrlEntity> Urls { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new RoleEntityTypeConfiguration());
            builder.ApplyConfiguration(new UserEntityTypeConfiguration());
            builder.ApplyConfiguration(new UrlEntityTypeConfiguration());
        }
    }
}
