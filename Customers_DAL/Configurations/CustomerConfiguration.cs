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

            builder.HasOne(d => d.User)
                .WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_User");
            
            new CustomersSeeder().Seed(builder);
        }
    }
}