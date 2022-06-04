using Customers_DAL.Entities;
using Customers_DAL.Seeding.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers_DAL.Configurations
{
    public class DayOffConfiguration : IEntityTypeConfiguration<DayOff>
    {
        public void Configure(EntityTypeBuilder<DayOff> builder)
        {
            builder.ToTable("DayOff");

            builder.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("Date_");
            
            new DayOffsSeeder().Seed(builder);
        }
    }
}