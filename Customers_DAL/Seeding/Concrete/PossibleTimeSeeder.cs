using System;
using System.Collections.Generic;
using Customers_DAL.Entities;
using Customers_DAL.Seeding.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers_DAL.Seeding.Concrete
{
    public class PossibleTimeSeeder : ISeeder<PossibleTime>
    {   
        public void Seed(EntityTypeBuilder<PossibleTime> builder)
        {
            List<PossibleTime> possibleTime = new();
            var initTime = TimeSpan.Parse("08:00:00");
            var incrementTime = new TimeSpan(0, 0, 15, 0);

            for (var i = 0; i < 61; i++)
            {
                possibleTime.Add(new PossibleTime{ Time = initTime, Available = true});
                initTime = initTime.Add(incrementTime);
            }

            builder.HasData(possibleTime);
        }
    }
}