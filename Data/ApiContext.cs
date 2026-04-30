using Microsoft.EntityFrameworkCore;
using TASKFLOWAPI.Models;

namespace TASKFLOWAPI.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .Property(u => u.UserRole)
                .HasConversion<string>();
        }
    }    
}