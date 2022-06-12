using Customers_DAL.Entities;
using Customers_DAL.Seeding.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers_DAL.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointment");

            builder.Property(e => e.AppointmentStatusId).HasDefaultValue(1);
                
            builder.Property(e => e.AppDate).HasColumnType("date");

            builder.HasOne(d => d.Barber)
                .WithMany(p => p.Appointments)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(d => d.BarberUserId)
                .HasConstraintName("FK__Appointme__Barbe__3D5E1FD2");

            builder.HasOne(d => d.Customer)
                .WithMany(p => p.Appointments)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(d => d.CustomerUserId)
                .HasConstraintName("FK__Appointme__Custo__3E52440B");
            
            new AppointmentsSeeder().Seed(builder);
        }
    }
}