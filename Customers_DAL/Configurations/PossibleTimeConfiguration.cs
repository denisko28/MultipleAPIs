using Customers_DAL.Entities;
using Customers_DAL.Seeding.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers_DAL.Configurations
{
    public class PossibleTimeConfiguration : IEntityTypeConfiguration<PossibleTime>
    {
        public void Configure(EntityTypeBuilder<PossibleTime> builder)
        {
            builder.HasKey(e => e.Time)
                .HasName("PK__Possible__8E79CB0049844667");

            builder.ToTable("PossibleTime");
            
            new PossibleTimeSeeder().Seed(builder);
        }
    }
}