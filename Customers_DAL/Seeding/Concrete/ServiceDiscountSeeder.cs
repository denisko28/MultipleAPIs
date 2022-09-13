using System;
using System.Collections.Generic;
using Customers_DAL.Entities;
using Customers_DAL.Seeding.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers_DAL.Seeding.Concrete
{
    public class ServiceDiscountSeeder : ISeeder<ServiceDiscount>
    {
        private static readonly List<ServiceDiscount> services = new()
        {
            new ServiceDiscount
            {
                Id = 1,
                ServiceId = 5,
                BranchId = 2,
                DiscountSize = 20
            },
            new ServiceDiscount
            {
                Id = 2,
                ServiceId = 3,
                BranchId = 1,
                DiscountSize = 15
            }
        };
        
        public void Seed(EntityTypeBuilder<ServiceDiscount> builder)
        {
            builder.HasData(services);
        }
    }
}