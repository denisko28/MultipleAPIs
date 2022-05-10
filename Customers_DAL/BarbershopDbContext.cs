using System;
using Customers_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Customers_DAL
{
    public class BarbershopDbContext : DbContext
    {
        public BarbershopDbContext()
        {
        }

        public BarbershopDbContext(DbContextOptions<BarbershopDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<AppointmentService> AppointmentServices { get; set; } = null!;
        public virtual DbSet<Barber> Barbers { get; set; } = null!;
        public virtual DbSet<Branch> Branches { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<DayOff> DayOffs { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<EmployeeDayOff> EmployeeDayOffs { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

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

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointment");

                entity.Property(e => e.AppDate).HasColumnType("date");

                entity.HasOne(d => d.Barber)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.BarberId)
                    .HasConstraintName("FK__Appointme__Barbe__3D5E1FD2");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Appointme__Custo__3E52440B");
            });

            modelBuilder.Entity<PossibleTime>(entity =>
            {
                entity.ToTable("PossibleTime");
            });

            modelBuilder.Entity<AppointmentService>(entity =>
            {
                entity.ToTable("AppointmentService");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.AppointmentServices)
                    .HasForeignKey(d => d.AppointmentId)
                    .HasConstraintName("FK__Appointme__Appoi__412EB0B6");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.AppointmentServices)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK__Appointme__Servi__4222D4EF");
            });

            modelBuilder.Entity<Barber>(entity =>
            {
                entity.ToTable("Barber");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Barbers)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__Barber__Employee__31EC6D26");
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.ToTable("Branch");

                entity.Property(e => e.Address).HasMaxLength(80);

                entity.Property(e => e.Descript).HasMaxLength(20);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Customer__UserId__34C8D9D1");
            });

            modelBuilder.Entity<DayOff>(entity =>
            {
                entity.ToTable("DayOff");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("Date_");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Address).HasMaxLength(80);

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.PassportImgPath).IsUnicode(false);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__Employee__Branch__29572725");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Employee__UserId__286302EC");
            });

            modelBuilder.Entity<EmployeeDayOff>(entity =>
            {
                entity.ToTable("EmployeeDayOff");

                entity.HasOne(d => d.DayOff)
                    .WithMany(p => p.EmployeeDayOffs)
                    .HasForeignKey(d => d.DayOffId)
                    .HasConstraintName("FK__EmployeeD__DayOf__2F10007B");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.EmployeeDayOffs)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__EmployeeD__Emplo__2E1BDC42");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service_");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .HasColumnName("Name_");

                entity.Property(e => e.Price).HasColumnType("decimal(6, 2)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User_");

                entity.Property(e => e.Avatar).IsUnicode(false);

                entity.Property(e => e.FirstName).HasMaxLength(15);

                entity.Property(e => e.LastName).HasMaxLength(15);
            });

            // OnModelCreatingPartial(modelBuilder);
        }

        // private void OnModelCreatingPartial(ModelBuilder modelBuilder)
        // {
        //     throw new NotImplementedException();
        // }
    }
}
