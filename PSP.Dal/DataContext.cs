using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PSP.Dal.Configurations;
using PSP.Domain.Aggregates.CategoryAggregate;
using PSP.Domain.Aggregates.ProjectAggregate;
using PSP.Domain.Aggregates.UserAggregate;

namespace PSP.Dal
{

    public class DataContext : IdentityDbContext
    {

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Project> Projects { get; set; }
        
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProjectCommentConfig());
            modelBuilder.ApplyConfiguration(new ProjectInteractionConfig());
            modelBuilder.ApplyConfiguration(new UserProfileConfig());
            modelBuilder.ApplyConfiguration(new CategoryConfig());
            modelBuilder.ApplyConfiguration(new IdentityUserLoginConfig());
            modelBuilder.ApplyConfiguration(new IdentityUserRoleConfig());
            modelBuilder.ApplyConfiguration(new IdentityUserTokenConfig());
        }
    }
}