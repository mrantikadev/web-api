using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
        : base(options)
        {
            
        }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Stock)
                .WithMany(s => s.Comments)
                .HasForeignKey(c => c.StockId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
