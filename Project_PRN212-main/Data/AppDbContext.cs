using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project_PRN212.Models;

namespace Project_PRN212.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Store> Stores { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<ProductionPlan> ProductionPlans { get; set; }
        public DbSet<ProductionOrder> ProductionOrders { get; set; }
        public DbSet<ProductionPlanDetail> ProductionPlanDetails { get; set; }
        public DbSet<FinishedDish> FinishedDishes { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipmentDetail> ShipmentDetails { get; set; }
        public DbSet<StoreReceipt> StoreReceipts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();

                optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique constraint on Username
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Unique constraint on StoreReceipts.ShipmentId
            modelBuilder.Entity<StoreReceipt>()
                .HasIndex(r => r.ShipmentId)
                .IsUnique();

            // ── SEED DATA ──────────────────────────────────────────────

            modelBuilder.Entity<Store>().HasData(
                new Store { StoreId = 1, StoreName = "Franchise Thu Duc", Address = "Thu Duc, TP.HCM", Phone = "0901111111", ManagerName = "Nguyen Van A" },
                new Store { StoreId = 2, StoreName = "Franchise Go Vap", Address = "Go Vap, TP.HCM", Phone = "0902222222", ManagerName = "Tran Thi B" },
                new Store { StoreId = 3, StoreName = "Franchise District 1", Address = "Quan 1, TP.HCM", Phone = "0903333333", ManagerName = "Le Van C" },
                new Store { StoreId = 4, StoreName = "Franchise Binh Thanh", Address = "Binh Thanh, TP.HCM", Phone = "0904444444", ManagerName = "Pham Thi D" }
            );

            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Username = "admin", Password = "123", FullName = "System Admin", Role = "admin", StoreId = null },
                new User { UserId = 2, Username = "kitchen01", Password = "123", FullName = "Kitchen Staff 01", Role = "kitchen", StoreId = null },
                new User { UserId = 3, Username = "kitchen02", Password = "123", FullName = "Kitchen Staff 02", Role = "kitchen", StoreId = null },
                new User { UserId = 4, Username = "store_thuduc", Password = "123", FullName = "Store User Thu Duc", Role = "store", StoreId = 1 },
                new User { UserId = 5, Username = "store_govap", Password = "123", FullName = "Store User Go Vap", Role = "store", StoreId = 2 },
                new User { UserId = 6, Username = "store_q1", Password = "123", FullName = "Store User Q1", Role = "store", StoreId = 3 },
                new User { UserId = 7, Username = "store_bt", Password = "123", FullName = "Store User Binh Thanh", Role = "store", StoreId = 4 }
            );

            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient { IngredientId = 1, IngredientName = "Thịt gà", Unit = "kg", QuantityInStock = 100, ImportPrice = 120000 },
                new Ingredient { IngredientId = 2, IngredientName = "Rau xà lách", Unit = "kg", QuantityInStock = 50, ImportPrice = 40000 },
                new Ingredient { IngredientId = 3, IngredientName = "Bột chiên giòn", Unit = "kg", QuantityInStock = 30, ImportPrice = 35000 },
                new Ingredient { IngredientId = 4, IngredientName = "Sốt mayonnaise", Unit = "lít", QuantityInStock = 20, ImportPrice = 60000 },
                new Ingredient { IngredientId = 5, IngredientName = "Cơm trắng", Unit = "kg", QuantityInStock = 80, ImportPrice = 20000 },
                new Ingredient { IngredientId = 6, IngredientName = "Dưa leo", Unit = "kg", QuantityInStock = 40, ImportPrice = 15000 },
                new Ingredient { IngredientId = 7, IngredientName = "Trứng gà", Unit = "quả", QuantityInStock = 200, ImportPrice = 3000 },
                new Ingredient { IngredientId = 8, IngredientName = "Nước tương", Unit = "lít", QuantityInStock = 25, ImportPrice = 50000 }
            );

        }
    }
}
