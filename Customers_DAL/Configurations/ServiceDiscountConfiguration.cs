using Customers_DAL.Entities;
using Customers_DAL.Seeding.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers_DAL.Configurations
{
    public class ServiceDiscountConfiguration : IEntityTypeConfiguration<ServiceDiscount>
    {
        public void Configure(EntityTypeBuilder<ServiceDiscount> builder)
        {
            builder.ToTable("ServiceDiscount");

            builder.HasOne(d => d.Service)
                .WithMany(p => p.ServiceDiscounts)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__ServiceDi__Servi__398D8EEE");
            
            builder.HasOne(d => d.Branch)
                .WithMany(p => p.ServiceDiscounts)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("FK__ServiceDi__Branc__3A81B327");
            
            new ServiceDiscountSeeder().Seed(builder);
        }
    }
}