using System;
using System.Collections.Generic;
using Customers_DAL.Entities;
using Customers_DAL.Seeding.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers_DAL.Seeding.Concrete
{
    public class DayOffsSeeder : ISeeder<DayOff>
    {
        private static readonly List<DayOff> dayOffs = new()
        {
            new DayOff
            {
                Id = 1,
                Date = DateTime.Parse("2022-03-27")
            },
            new DayOff
            {
                Id = 2,
                Date = DateTime.Parse("2022-03-08")
            },
            new DayOff
            {
                Id = 3,
                Date = DateTime.Parse("2022-04-01")
            },
            new DayOff
            {
                Id = 4,
                Date = DateTime.Parse("2022-03-12")
            },
        };
        
        public void Seed(EntityTypeBuilder<DayOff> builder)
        {
            builder.HasData(dayOffs);
        }
    }
}