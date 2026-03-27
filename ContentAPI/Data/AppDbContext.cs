using Microsoft.EntityFrameworkCore;
using ContentAPI.Models;


namespace ContentAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TaskBreakdown> TaskBreakdowns { get; set; }
    }
}