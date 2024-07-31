using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebStore.Entities;

namespace WebStore.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=projectDb;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC0792556493");

            entity.ToTable("Category");

            entity.Property(e => e.Alias).HasMaxLength(50);
            entity.Property(e => e.CreatedOnLocal).HasColumnType("datetime");
            entity.Property(e => e.CreatedOnUtc).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PreviewImage).IsUnicode(false);
            entity.Property(e => e.UpdatedOnLocal).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOnUtc).HasColumnType("datetime");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC07B4B207A1");

            entity.ToTable("Product");

            entity.Property(e => e.Alias).HasMaxLength(50);
            entity.Property(e => e.CreatedOnLocal).HasColumnType("datetime");
            entity.Property(e => e.CreatedOnUtc).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ProductionDateOnLocal).HasColumnType("datetime");
            entity.Property(e => e.ProductionDateOnUtc).HasColumnType("datetime");
            entity.Property(e => e.UnitDescription).HasMaxLength(50);
            entity.Property(e => e.UpdatedOnLocal).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOnUtc).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__Categor__2E1BDC42");

            entity.HasOne(d => d.Vendor).WithMany(p => p.Products)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__VendorI__2F10007B");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vendor__3214EC0708707E4E");

            entity.ToTable("Vendor");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.ContactPerson).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Logo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
