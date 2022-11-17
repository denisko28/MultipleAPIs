using System.ComponentModel.DataAnnotations.Schema;
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
            
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedNever();
            
            builder.HasMany(d => d.Appointments)
                .WithOne(p => p.Branch)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(d => d.BranchId)
                .HasConstraintName("FK__Branch");

            builder.Property(e => e.Address).HasMaxLength(80);

            builder.Property(e => e.Descript).HasMaxLength(20);
            
            new BranchesSeeder().Seed(builder);
        }
    }
}