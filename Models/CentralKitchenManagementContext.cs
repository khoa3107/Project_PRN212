using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Project_PRN212.Models;

public partial class CentralKitchenManagementContext : DbContext
{
    public CentralKitchenManagementContext()
    {
    }

    public CentralKitchenManagementContext(DbContextOptions<CentralKitchenManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FinishedDish> FinishedDishes { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<ProductionPlan> ProductionPlans { get; set; }

    public virtual DbSet<ProductionPlanDetail> ProductionPlanDetails { get; set; }

    public virtual DbSet<Shipment> Shipments { get; set; }

    public virtual DbSet<ShipmentDetail> ShipmentDetails { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<StoreReceipt> StoreReceipts { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FinishedDish>(entity =>
        {
            entity.HasKey(e => e.DishId).HasName("PK__Finished__18834F500C72503B");

            entity.Property(e => e.DishName).HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Available");
            entity.Property(e => e.Unit)
                .HasMaxLength(50)
                .HasDefaultValue("portion");

            entity.HasOne(d => d.Plan).WithMany(p => p.FinishedDishes)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__FinishedD__PlanI__5070F446");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.IngredientId).HasName("PK__Ingredie__BEAEB25A41467901");

            entity.Property(e => e.ImportPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IngredientName).HasMaxLength(100);
            entity.Property(e => e.LastUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.QuantityInStock).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Available");
            entity.Property(e => e.Unit).HasMaxLength(50);
        });

        modelBuilder.Entity<ProductionPlan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__Producti__755C22B727269FB2");

            entity.Property(e => e.DishName).HasMaxLength(100);
            entity.Property(e => e.Note).HasMaxLength(255);
            entity.Property(e => e.PlanName).HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Planned");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProductionPlans)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productio__Creat__46E78A0C");
        });

        modelBuilder.Entity<ProductionPlanDetail>(entity =>
        {
            entity.HasKey(e => e.PlanDetailId).HasName("PK__Producti__B7E7452D4792B7A3");

            entity.Property(e => e.RequiredQuantity).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Ingredient).WithMany(p => p.ProductionPlanDetails)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productio__Ingre__4AB81AF0");

            entity.HasOne(d => d.Plan).WithMany(p => p.ProductionPlanDetails)
                .HasForeignKey(d => d.PlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Productio__PlanI__49C3F6B7");
        });

        modelBuilder.Entity<Shipment>(entity =>
        {
            entity.HasKey(e => e.ShipmentId).HasName("PK__Shipment__5CAD37ED3FC1A13F");

            entity.Property(e => e.Note).HasMaxLength(255);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Shipments)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Shipments__Creat__5629CD9C");

            entity.HasOne(d => d.Store).WithMany(p => p.Shipments)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Shipments__Store__5535A963");
        });

        modelBuilder.Entity<ShipmentDetail>(entity =>
        {
            entity.HasKey(e => e.ShipmentDetailId).HasName("PK__Shipment__04714320325E9EA0");

            entity.HasOne(d => d.Dish).WithMany(p => p.ShipmentDetails)
                .HasForeignKey(d => d.DishId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShipmentD__DishI__59FA5E80");

            entity.HasOne(d => d.Shipment).WithMany(p => p.ShipmentDetails)
                .HasForeignKey(d => d.ShipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ShipmentD__Shipm__59063A47");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Stores__3B82F10120663188");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.ManagerName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");
            entity.Property(e => e.StoreName).HasMaxLength(100);
        });

        modelBuilder.Entity<StoreReceipt>(entity =>
        {
            entity.HasKey(e => e.ReceiptId).HasName("PK__StoreRec__CC08C420237D8912");

            entity.HasIndex(e => e.ShipmentId, "UQ__StoreRec__5CAD37ECB79D5983").IsUnique();

            entity.Property(e => e.Note).HasMaxLength(255);
            entity.Property(e => e.ReceiptStatus)
                .HasMaxLength(20)
                .HasDefaultValue("Received");
            entity.Property(e => e.ReceivedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.ReceivedByNavigation).WithMany(p => p.StoreReceipts)
                .HasForeignKey(d => d.ReceivedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StoreRece__Recei__619B8048");

            entity.HasOne(d => d.Shipment).WithOne(p => p.StoreReceipt)
                .HasForeignKey<StoreReceipt>(d => d.ShipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StoreRece__Shipm__60A75C0F");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C8FD48E1B");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E43AA33AAE").IsUnique();

            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Role).HasMaxLength(20);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Active");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Store).WithMany(p => p.Users)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__Users__StoreId__3D5E1FD2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
