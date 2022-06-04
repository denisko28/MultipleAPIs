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
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK__Appointme__Appoi__412EB0B6");

            builder.HasOne(d => d.Service)
                .WithMany(p => p.AppointmentServices)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__Appointme__Servi__4222D4EF");
            
            new AppointmentServicesSeeder().Seed(builder);
        }
    }
}