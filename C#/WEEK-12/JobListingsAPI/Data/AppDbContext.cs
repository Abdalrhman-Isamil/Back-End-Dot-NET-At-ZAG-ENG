using JobListingsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JobListingsAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<JobListing> JobListings => Set<JobListing>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobListing>(entity =>
            {
                // Salary stored as a fixed-point decimal
                entity.Property(j => j.Salary).HasColumnType("decimal(18,2)");

                // PostedAt defaults to current UTC time at the DB level
                entity.Property(j => j.PostedAt).HasDefaultValueSql("datetime('now')");
            });
        }
    }
}
