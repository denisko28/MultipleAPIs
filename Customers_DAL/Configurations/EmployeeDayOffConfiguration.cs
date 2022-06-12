using Customers_DAL.Entities;
using Customers_DAL.Seeding.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers_DAL.Configurations
{
    public class EmployeeDayOffConfiguration : IEntityTypeConfiguration<EmployeeDayOff>
    {
        public void Configure(EntityTypeBuilder<EmployeeDayOff> builder)
        {
            builder.ToTable("EmployeeDayOff");

            builder.HasOne(d => d.DayOff)
                .WithMany(p => p.EmployeeDayOffs)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasForeignKey(d => d.DayOffId)
                .HasConstraintName("FK__EmployeeD__DayOf__2F10007B");

            builder.HasOne(d => d.EmployeeUser)
                .WithMany(p => p.EmployeeDayOffs)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(d => d.EmployeeUserId)
                .HasConstraintName("FK__EmployeeD__Emplo__2E1BDC42");
            
            new EmployeeDayOffsSeeder().Seed(builder);
        }
    }
}