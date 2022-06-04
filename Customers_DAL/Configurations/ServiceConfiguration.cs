using Customers_DAL.Entities;
using Customers_DAL.Seeding.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers_DAL.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Service_");

            builder.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("Name_");

            builder.Property(e => e.Price).HasColumnType("decimal(6, 2)");
            
            new ServicesSeeder().Seed(builder);
        }
    }
}