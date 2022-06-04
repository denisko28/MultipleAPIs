using System;
using System.Collections.Generic;
using Customers_DAL.Entities;
using Customers_DAL.Seeding.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers_DAL.Seeding.Concrete
{
    public class BranchesSeeder : ISeeder<Branch>
    {
        private static readonly List<Branch> branches = new()
        {
            new Branch
            {
                Id = 1,
                Descript = "Barbershop Lodon(1)",
                Address = "вул. Банкова 12, Київ, Київська область"
            },
            new Branch
            {
                Id = 2,
                Descript = "Barbershop Lodon(2)",
                Address = "вул. Героїв майдану 55, Чернівці, Чернівецька область"
            },
            new Branch
            {
                Id = 3,
                Descript = "Barbershop Lodon(3)",
                Address = "вул. Степана Бандери 2-А, Львів, Львівська область"
            },
        };
        
        public void Seed(EntityTypeBuilder<Branch> builder)
        {
            builder.HasData(branches);
        }
    }
}