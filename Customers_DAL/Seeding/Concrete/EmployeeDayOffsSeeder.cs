using System;
using System.Collections.Generic;
using Customers_DAL.Entities;
using Customers_DAL.Seeding.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers_DAL.Seeding.Concrete
{
    public class EmployeeDayOffsSeeder : ISeeder<EmployeeDayOff>
    {
        private static readonly List<EmployeeDayOff> employeeDayOffs = new()
        {
            new EmployeeDayOff
            {
                Id = 1,
                EmployeeUserId = 3,
                DayOffId = 1
            },
            new EmployeeDayOff
            {
                Id = 2,
                EmployeeUserId = 9,
                DayOffId = 2
            },
            new EmployeeDayOff
            {
                Id = 3,
                EmployeeUserId = 14,
                DayOffId = 3
            },
            new EmployeeDayOff
            {
                Id = 4,
                EmployeeUserId = 15,
                DayOffId = 4
            }
        };
        
        public void Seed(EntityTypeBuilder<EmployeeDayOff> builder)
        {
            builder.HasData(employeeDayOffs);
        }
    }
}