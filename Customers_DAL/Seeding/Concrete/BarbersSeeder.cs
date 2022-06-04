using System;
using System.Collections.Generic;
using Customers_DAL.Entities;
using Customers_DAL.Seeding.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers_DAL.Seeding.Concrete
{
    public class BarbersSeeder : ISeeder<Barber>
    {
        private static readonly List<Barber> barbers = new()
        {
            new Barber
            {
                EmployeeUserId = 3,
                ChairNum = 1
            },
            new Barber
            {
                EmployeeUserId = 4,
                ChairNum = 2
            },
            new Barber
            {
                EmployeeUserId = 5,
                ChairNum = 3
            },
            new Barber
            {
                EmployeeUserId = 6,
                ChairNum = 4
            },
            new Barber
            {
                EmployeeUserId = 8,
                ChairNum = 1
            },
            new Barber
            {
                EmployeeUserId = 9,
                ChairNum = 2
            },
            new Barber
            {
                EmployeeUserId = 10,
                ChairNum = 3
            },
            new Barber
            {
                EmployeeUserId = 12,
                ChairNum = 1
            },
            new Barber
            {
                EmployeeUserId = 13,
                ChairNum = 2
            },
            new Barber
            {
                EmployeeUserId = 14,
                ChairNum = 3
            },
            new Barber
            {
                EmployeeUserId = 15,
                ChairNum = 4
            },
        };
        
        public void Seed(EntityTypeBuilder<Barber> builder)
        {
            builder.HasData(barbers);
        }
    }
}