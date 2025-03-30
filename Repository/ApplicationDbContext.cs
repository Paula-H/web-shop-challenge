using Microsoft.EntityFrameworkCore;
using Domain.Entity;

namespace Repository
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserCouponMapping> UserCouponMappings { get; set; }
        public DbSet<OrderProductMapping> OrderProductMappings { get; set; }
        public DbSet<OrderCouponMapping> OrderCouponMappings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderCouponMapping>()
                .HasKey(x => new { x.OrderId, x.CouponId });

            modelBuilder.Entity<OrderProductMapping>()
                .HasKey(x => new { x.OrderId, x.ProductId });

            modelBuilder.Entity<UserCouponMapping>()
                .HasKey(x => new { x.UserId, x.CouponId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
