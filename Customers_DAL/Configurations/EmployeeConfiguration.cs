using Customers_DAL.Entities;
using Customers_DAL.Seeding.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers_DAL.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.UserId)
                .HasName("PK_Employee_User");

            builder.ToTable("Employee");

            builder.Property(e => e.UserId).ValueGeneratedNever();

            builder.Property(e => e.Address).HasMaxLength(80);

            builder.Property(e => e.Birthday).HasColumnType("date");

            builder.HasOne(d => d.Branch)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("FK__Employee__Branch__29572725");

            builder.HasOne(d => d.User)
                .WithOne(p => p.Employee)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey<Employee>(d => d.UserId)
                .HasConstraintName("FK_Employee_User");
            
            new EmployeesSeeder().Seed(builder);
        }
    }
}