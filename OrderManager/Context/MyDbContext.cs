using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using OrderManager.Entities;

namespace OrderManager.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<WorkShop> WorkShops { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__categori__D54EE9B4F0F355C0");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.NameCategory)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("name_category");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__customer__CD65CB8552F31D88");

            entity.ToTable("customers");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("customer_id");
            entity.Property(e => e.Detail)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("detail");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__orders__4659622940C22851");

            entity.ToTable("orders");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("customer_id");
            entity.Property(e => e.Mass).HasColumnName("mass");
            entity.Property(e => e.OrderAt)
                .HasColumnType("date")
                .HasColumnName("order_at");
            entity.Property(e => e.PriceIn).HasColumnName("price_in");
            entity.Property(e => e.PriceOut).HasColumnName("price_out");
            entity.Property(e => e.ShipIn).HasColumnName("ship_in");
            entity.Property(e => e.ShipPrice).HasColumnName("ship_price");
            entity.Property(e => e.TimeOrder).HasColumnName("time_order");
            entity.Property(e => e.TypeWrap).HasColumnName("type_wrap");
            entity.Property(e => e.TotalPrice).HasColumnName("total_price");
            entity.Property(e => e.Volume).HasColumnName("volume");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__orders__customer__4D94879B");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__order_de__3C5A40803EF88C25");

            entity.ToTable("order_detail");

            entity.HasIndex(e => new { e.OrderId, e.ProductId }, "UQ__order_de__022945F7709E800C").IsUnique();

            entity.Property(e => e.OrderDetailId).HasColumnName("order_detail_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__order_det__order__59FA5E80");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__order_det__produ__5AEE82B9");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__products__47027DF59D0B7420");

            entity.ToTable("products");

            entity.Property(e => e.ProductId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("product_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Description)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Photo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("photo");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Mass).HasColumnName("mass");
            entity.Property(e => e.Volume).HasColumnName("volume");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.WorkShopId).HasColumnName("work_shop_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__products__catego__5629CD9C");

            entity.HasOne(d => d.WorkShop).WithMany(p => p.Products)
                .HasForeignKey(d => d.WorkShopId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__products__work_s__5535A963");
        });

        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("PK__request__18D3B90FFED8FDB7");

            entity.ToTable("request");

            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.RequestAt).HasColumnType("date").HasColumnName("request_at");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("customer_id");
            entity.Property(e => e.Detail)
                .HasMaxLength(2000)
                .IsUnicode(false)
                .HasColumnName("detail");
            entity.Property(e => e.Image)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.Customer).WithMany(p => p.Requests)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__request__custome__52593CB8");
        });

        modelBuilder.Entity<WorkShop>(entity =>
        {
            entity.HasKey(e => e.WorkShopId).HasName("PK__work_sho__D641F834BED179FD");

            entity.ToTable("work_shops");

            entity.Property(e => e.WorkShopId).HasColumnName("work_shop_id");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.LinkShop)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("link_shop");
            entity.Property(e => e.NameWorkShop)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name_work_shop");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Wechat)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}