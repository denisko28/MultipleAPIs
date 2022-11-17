using Customers_DAL.Entities;
using Customers_DAL.Seeding.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers_DAL.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => e.UserId)
                .HasName("PK_Customer_User");

            builder.ToTable("Customer");

            builder.Property(e => e.UserId).ValueGeneratedNever();

            new CustomersSeeder().Seed(builder);
        }
    }
}