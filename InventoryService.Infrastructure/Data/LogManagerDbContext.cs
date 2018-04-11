using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace InventoryService.Infrastructure.Data
{
    public partial class LogManagerDbContext : DbContext
    {
        public virtual DbSet<AppLogs> AppLogs { get; set; }
        public virtual DbSet<AppMessageLog> AppMessageLog { get; set; }
        public virtual DbSet<Log> Log { get; set; }

        public LogManagerDbContext()
        {
            //default constructor...
        }

        public LogManagerDbContext(DbContextOptions<LogManagerDbContext> options) : base(options)
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
                var sqlConnection = configuration.GetConnectionString("LogManagerDb");
                optionsBuilder.UseSqlServer(sqlConnection);
                #endregion

                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer(@"Server=tcp:kbhupinventory.database.windows.net,1433;Database=LogManager;Persist Security Info=False; User ID=kbhupathi;Password=Inventory@9;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppLogs>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Level)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Logger)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Message).IsRequired();

                entity.Property(e => e.Thread)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AppMessageLog>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AppMessageEntryId).IsRequired();

                entity.Property(e => e.ApplicationName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.RequestTimestamp).HasColumnType("datetime");

                entity.Property(e => e.ResponseTimestamp).HasColumnType("datetime");

                entity.Property(e => e.User)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.Property(e => e.Level).HasMaxLength(128);

                entity.Property(e => e.Properties).HasColumnType("xml");
            });
        }
    }
}
