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

            builder.Property(e => e.AppDate)
                .HasColumnType("date")
                .HasConversion(v => v, v => 
                    DateTime.SpecifyKind(v, DateTimeKind.Utc));

            builder.HasOne(d => d.Customer)
                .WithMany(p => p.Appointments)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(d => d.CustomerUserId)
                .HasConstraintName("FK__Appointme__Custo__3E52440B");

            new AppointmentsSeeder().Seed(builder);
        }
    }
}