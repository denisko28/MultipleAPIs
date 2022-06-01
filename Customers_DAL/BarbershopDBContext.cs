using System;
using Customers_DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Customers_DAL
{
    public partial class BarbershopDbContext : DbContext
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
        public virtual DbSet<PossibleTime> PossibleTimes { get; set; } = null!;
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

                entity.Property(e => e.AppointmentStatusId).HasDefaultValue(1);
                
                entity.Property(e => e.AppDate).HasColumnType("date");

                entity.HasOne(d => d.Barber)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.BarberUserId)
                    .HasConstraintName("FK__Appointme__Barbe__3D5E1FD2");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.CustomerUserId)
                    .HasConstraintName("FK__Appointme__Custo__3E52440B");
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
                entity.HasKey(e => e.EmployeeUserId)
                    .HasName("PK_Barber_User");

                entity.ToTable("Barber");

                entity.Property(e => e.EmployeeUserId).ValueGeneratedNever();

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.Barber)
                    .HasForeignKey<Barber>(d => d.EmployeeUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Barber_User");
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.ToTable("Branch");

                entity.Property(e => e.Address).HasMaxLength(80);

                entity.Property(e => e.Descript).HasMaxLength(20);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_Customer_User");

                entity.ToTable("Customer");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Customer)
                    .HasForeignKey<Customer>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_User");
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
                entity.HasKey(e => e.UserId)
                    .HasName("PK_Employee_User");

                entity.ToTable("Employee");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(80);

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__Employee__Branch__29572725");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Employee)
                    .HasForeignKey<Employee>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_User");
            });

            modelBuilder.Entity<EmployeeDayOff>(entity =>
            {
                entity.ToTable("EmployeeDayOff");

                entity.HasOne(d => d.DayOff)
                    .WithMany(p => p.EmployeeDayOffs)
                    .OnDelete(DeleteBehavior.ClientCascade)
                    .HasForeignKey(d => d.DayOffId)
                    .HasConstraintName("FK__EmployeeD__DayOf__2F10007B");

                entity.HasOne(d => d.EmployeeUser)
                    .WithMany(p => p.EmployeeDayOffs)
                    .OnDelete(DeleteBehavior.ClientCascade)
                    .HasForeignKey(d => d.EmployeeUserId)
                    .HasConstraintName("FK__EmployeeD__Emplo__2E1BDC42");
            });

            modelBuilder.Entity<PossibleTime>(entity =>
            {
                entity.HasKey(e => e.Time)
                    .HasName("PK__Possible__8E79CB0049844667");

                entity.ToTable("PossibleTime");
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

                entity.Property(e => e.FirstName).HasMaxLength(15);

                entity.Property(e => e.LastName).HasMaxLength(15);
            });

            //OnModelCreatingPartial(modelBuilder);
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
