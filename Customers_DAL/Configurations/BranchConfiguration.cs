using Customers_DAL.Entities;
using Customers_DAL.Seeding.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers_DAL.Configurations
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("Branch");

            builder.Property(e => e.Address).HasMaxLength(80);

            builder.Property(e => e.Descript).HasMaxLength(20);
            
            new BranchesSeeder().Seed(builder);
        }
    }
}