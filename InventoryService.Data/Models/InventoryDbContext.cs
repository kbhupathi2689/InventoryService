using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace InventoryService.Data.Models
{
    public partial class InventoryDbContext : DbContext
    {
        public virtual DbSet<Domain> Domain { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductSubCategory> ProductSubCategory { get; set; }
        public virtual DbSet<ProductType> ProductType { get; set; }
        public virtual DbSet<InventoryView> InventoryView { get; set; }
        public virtual DbSet<ProductsView> ProductsView { get; set; }

        public InventoryDbContext()
        {
            //default constructor...
        }

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #region Read connection string from appsettings.json and assign it to optionsbuilder.sqlserver
                string dir = AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin"));
                IConfigurationRoot configuration = new ConfigurationBuilder()
                                  .SetBasePath(dir)
                                  .AddJsonFile("appsettings.json")
                                  .Build();
                var sqlConnection = configuration.GetConnectionString("InventoryDb");
                optionsBuilder.UseSqlServer(sqlConnection);
                #endregion

                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer(@"Server=tcp:kbhupinventory.database.windows.net,1433;Database=Inventory;Persist Security Info=False; User ID=kbhupathi;Password=Inventory@9;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdatedUser).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.Property(e => e.InventoryId).HasColumnName("InventoryID");

                entity.Property(e => e.Bin).IsRequired();

                entity.Property(e => e.DomainId).HasColumnName("DomainID");

                entity.Property(e => e.InventoryAddress).IsRequired();

                entity.Property(e => e.InventoryName).IsRequired();

                entity.Property(e => e.InventoryPrimaryContact).IsRequired();

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdatedUser).IsRequired();

                entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ShelfLocation).IsRequired();

                entity.HasOne(d => d.Domain)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.DomainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventory__Domai__6477ECF3");

                entity.HasOne(d => d.Manufacturer)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.ManufacturerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventory__Manuf__5CD6CB2B");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Inventory)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Inventory__Produ__5DCAEF64");
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.Property(e => e.ManufacturerId).HasColumnName("ManufacturerID");

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdatedUser).IsRequired();

                entity.Property(e => e.ManufacturerAddress).IsRequired();

                entity.Property(e => e.ManufacturerContact).IsRequired();

                entity.Property(e => e.ManufacturerName).IsRequired();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdatedUser).IsRequired();

                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.Property(e => e.ProductDiscontinuedDate).HasColumnType("datetime");

                entity.Property(e => e.ProductName).IsRequired();

                entity.Property(e => e.ProductNumber).IsRequired();

                entity.Property(e => e.ProductReleasedOn).HasColumnType("datetime");

                entity.Property(e => e.ProductSellEndDate).HasColumnType("datetime");

                entity.Property(e => e.ProductSellStartDate).HasColumnType("datetime");

                entity.Property(e => e.ProductSellingPrice).HasColumnType("money");

                entity.Property(e => e.ProductSubCategoryId).HasColumnName("ProductSubCategoryID");

                entity.Property(e => e.ProductTypeId).HasColumnName("ProductTypeID");

                entity.Property(e => e.StandardCost).HasColumnType("money");

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Product__59063A47");

                entity.HasOne(d => d.ProductSubCategory)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductSubCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Product__59FA5E80");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ProductTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__Product__5812160E");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdatedUser).IsRequired();

                entity.Property(e => e.ProductCategoryDesc).IsRequired();

                entity.Property(e => e.ProductCategoryName).IsRequired();

                entity.Property(e => e.ProductTypeId).HasColumnName("ProductTypeID");

                entity.HasOne(d => d.ProductType)
                    .WithMany(p => p.ProductCategory)
                    .HasForeignKey(d => d.ProductTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductCa__Produ__619B8048");
            });

            modelBuilder.Entity<ProductSubCategory>(entity =>
            {
                entity.Property(e => e.ProductSubCategoryId).HasColumnName("ProductSubCategoryID");

                entity.Property(e => e.DomainId).HasColumnName("DomainID");

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdatedUser).IsRequired();

                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategoryID");

                entity.Property(e => e.ProductSubCategoryName).IsRequired();

                entity.Property(e => e.SupportedPlatform).IsRequired();

                entity.HasOne(d => d.Domain)
                    .WithMany(p => p.ProductSubCategory)
                    .HasForeignKey(d => d.DomainId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductSu__Domai__6383C8BA");

                entity.HasOne(d => d.ProductCategory)
                    .WithMany(p => p.ProductSubCategory)
                    .HasForeignKey(d => d.ProductCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductSu__Produ__5535A963");
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.Property(e => e.ProductTypeId).HasColumnName("ProductTypeID");

                entity.Property(e => e.LastUpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdatedUser).IsRequired();

                entity.Property(e => e.ProductTypeDesc).IsRequired();

                entity.Property(e => e.ProductTypeName).IsRequired();
            });

            modelBuilder.Entity<InventoryView>(entity => { entity.HasKey(e => e.ID); });

            modelBuilder.Entity<ProductsView>(entity => { entity.HasKey(e => e.ID); });
        }
    }
}
