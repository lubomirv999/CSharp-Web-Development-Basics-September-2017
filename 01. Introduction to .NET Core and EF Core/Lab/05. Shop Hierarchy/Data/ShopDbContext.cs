using Microsoft.EntityFrameworkCore;

public class ShopDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    public DbSet<Salesman> Salesmen { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<Review> Reviews { get; set; }

    public DbSet<Item> Items { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlServer("Server=localhost;Database=Shop;Integrated Security=True;");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .Entity<Customer>()
            .HasOne(c => c.Salesman)
            .WithMany(s => s.Customers)
            .HasForeignKey(c => c.SalesmanId);

        builder
            .Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId);

        builder
            .Entity<Review>()
            .HasOne(r => r.Customer)
            .WithMany(c => c.Reviews)
            .HasForeignKey(r => r.CustomerId);

        builder
            .Entity<ItemOrder>()
            .HasKey(io => new { io.ItemId, io.OrderId });

        builder
            .Entity<Item>()
            .HasMany(i => i.Orders)
            .WithOne(io => io.Item)
            .HasForeignKey(i => i.ItemId);

        builder
            .Entity<Order>()
            .HasMany(o => o.Items)
            .WithOne(io => io.Order)
            .HasForeignKey(o => o.OrderId);

        builder
            .Entity<Item>()
            .HasMany(i => i.Reviews)
            .WithOne(r => r.Item)
            .HasForeignKey(r => r.ItemId);
    }
}