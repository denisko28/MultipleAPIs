using Customers_DAL.Entities;
using Customers_DAL.Seeding.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers_DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User_");

            builder.Property(e => e.FirstName).HasMaxLength(15);

            builder.Property(e => e.LastName).HasMaxLength(15);
        }
    }
}