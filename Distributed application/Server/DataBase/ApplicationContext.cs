using Microsoft.EntityFrameworkCore;
using Server.DataBase.Entity;

namespace Server.DataBase
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Logs> Logs { get; set; }
        public DbSet <Items> Items { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure your entities here
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.UserID);
                entity.Property(u => u.Username).IsRequired();
                entity.Property(u => u.Password).IsRequired();
                entity.HasMany(u => u.Items)
                      .WithOne(i => i.User)
                      .HasForeignKey(i => i.UserID);
                entity.HasMany(u => u.Logs)
                      .WithOne(l => l.User)
                      .HasForeignKey(l => l.UserID);
            });

            // Configure Items entity
            modelBuilder.Entity<Items>(entity =>
            {
                entity.ToTable("Items");
                entity.HasKey(i => i.ItemID);
                entity.Property(i => i.Name).IsRequired();
                entity.Property(i => i.Quantity).IsRequired();
                entity.Property(i => i.PricePerUnit).IsRequired();
                entity.Property(i => i.TotalPrice);
                entity.HasOne(i => i.User)
                      .WithMany(u => u.Items)
                      .HasForeignKey(i => i.UserID);
            });

            // Configure Logs entity
            modelBuilder.Entity<Logs>(entity =>
            {
                entity.ToTable("Logs");
                entity.HasKey(l => l.Id);
                entity.Property(l => l.UserID).IsRequired();
                entity.Property(l => l.DateTime).IsRequired();
                entity.Property(l => l.Command).IsRequired();
                entity.Property(l => l.Result).IsRequired();
                entity.HasOne(l => l.User)
                      .WithMany(u => u.Logs)
                      .HasForeignKey(l => l.UserID);
            });

        }
    }
}
