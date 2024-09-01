using Microsoft.EntityFrameworkCore;
using SalesOrderManagement.Core.Models.Domain;

namespace SalesOrderManagement.Infrastructure.Data
{
    public class SalesOrderDbContext : DbContext
    {
        public SalesOrderDbContext(DbContextOptions<SalesOrderDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderLines)
                .WithOne()
                .HasForeignKey(ol => ol.OrderId);

            base.OnModelCreating(modelBuilder);
        }
    }
}