using Customers_DAL.Entities;
using Customers_DAL.Seeding.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers_DAL.Configurations
{
    public class AppointmentServiceConfiguration : IEntityTypeConfiguration<AppointmentService>
    {
        public void Configure(EntityTypeBuilder<AppointmentService> builder)
        {
            builder.ToTable("AppointmentService");

            builder.HasOne(d => d.Appointment)
                .WithMany(p => p.AppointmentServices)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__Appointme__Appoi__412EB0B6");

            new AppointmentServicesSeeder().Seed(builder);
        }
    }
}