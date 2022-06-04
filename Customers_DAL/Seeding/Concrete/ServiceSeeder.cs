using System;
using System.Collections.Generic;
using Customers_DAL.Entities;
using Customers_DAL.Seeding.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers_DAL.Seeding.Concrete
{
    public class ServicesSeeder : ISeeder<Service>
    {
        private static readonly List<Service> services = new()
        {
            new Service
            {
                Id = 1,
                Name = "Стрижка",
                Duration = 60,
                Price = 300,
                Available = true
            },
            new Service
            {
                Id = 2,
                Name = "Стрижка з бородою",
                Duration = 90,
                Price = 450,
                Available = true
            },
            new Service
            {
                Id = 3,
                Name = "Голова - камуфляж сивини",
                Duration = 30,
                Price = 200,
                Available = true
            },
            new Service
            {
                Id = 4,
                Name = "Борода - камуфляж сивини",
                Duration = 30,
                Price = 150,
                Available = true
            },
            new Service
            {
                Id = 5,
                Name = "Дитяча стрижка",
                Duration = 45,
                Price = 200,
                Available = true
            },
            new Service
            {
                Id = 6,
                Name = "Укладка",
                Duration = 15,
                Price = 100,
                Available = true
            },
            new Service
            {
                Id = 7,
                Name = "Королівське гоління",
                Duration = 15,
                Price = 250,
                Available = true
            },
            new Service
            {
                Id = 8,
                Name = "Видалення волосся воском",
                Duration = 15,
                Price = 100,
                Available = true
            },
            new Service
            {
                Id = 9,
                Name = "Чистка лиця",
                Duration = 75,
                Price = 400,
                Available = false
            }
        };
        
        public void Seed(EntityTypeBuilder<Service> builder)
        {
            builder.HasData(services);
        }
    }
}