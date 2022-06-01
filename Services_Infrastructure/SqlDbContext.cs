using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services_Domain.Entities;

namespace Services_Infrastructure
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext()
        {
        }

        public SqlDbContext(DbContextOptions<SqlDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<ServiceDiscount> ServiceDiscounts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("MSSQLConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Latin1_General_100_CI_AS_SC_UTF8");

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service_");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .HasColumnName("Name_");

                entity.Property(e => e.Price).HasColumnType("decimal(6, 2)");
            });

            modelBuilder.Entity<ServiceDiscount>(entity =>
            {
                entity.ToTable("ServiceDiscount");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServiceDiscounts)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK__ServiceDi__Servi__398D8EEE");
                
                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.ServiceDiscounts)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__ServiceDi__Branc__3A81B327");
            });

            //OnModelCreatingPartial(modelBuilder);
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
