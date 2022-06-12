using Customers_DAL.Entities;
using Customers_DAL.Seeding.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers_DAL.Configurations
{
    public class BarberConfiguration : IEntityTypeConfiguration<Barber>
    {
        public void Configure(EntityTypeBuilder<Barber> builder)
        {
            builder.HasKey(e => e.EmployeeUserId)
                .HasName("PK_Barber_User");

            builder.ToTable("Barber");

            builder.Property(e => e.EmployeeUserId).ValueGeneratedNever();

            builder.HasOne(d => d.Employee)
                .WithOne(p => p.Barber)
                .OnDelete(DeleteBehavior.ClientNoAction)
                .HasForeignKey<Barber>(d => d.EmployeeUserId)
                .HasConstraintName("FK_Barber_User");
            
            new BarbersSeeder().Seed(builder);
        }
    }
}