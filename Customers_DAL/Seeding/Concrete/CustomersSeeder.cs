using System;
using System.Collections.Generic;
using Customers_DAL.Entities;
using Customers_DAL.Seeding.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers_DAL.Seeding.Concrete
{
    public class CustomersSeeder : ISeeder<Customer>
    {
        private static readonly List<Customer> customers = new()
        {
            new Customer
            {
                UserId = 16,
                VisitsNum = 0 
            },
            new Customer
            {
                UserId = 17,
                VisitsNum = 0 
            },
            new Customer
            {
                UserId = 18,
                VisitsNum = 0 
            },
            new Customer
            {
                UserId = 19,
                VisitsNum = 0 
            },
            new Customer
            {
                UserId = 20,
                VisitsNum = 0 
            },
            new Customer
            {
                UserId = 21,
                VisitsNum = 0 
            },
            new Customer
            {
                UserId = 22,
                VisitsNum = 0 
            },
            new Customer
            {
                UserId = 23,
                VisitsNum = 0 
            },
            new Customer
            {
                UserId = 24,
                VisitsNum = 0 
            },
            new Customer
            {
                UserId = 25,
                VisitsNum = 0 
            },
            new Customer
            {
                UserId = 26,
                VisitsNum = 0 
            },
            new Customer
            {
                UserId = 27,
                VisitsNum = 0 
            },
            new Customer
            {
                UserId = 28,
                VisitsNum = 0 
            },
            new Customer
            {
                UserId = 29,
                VisitsNum = 0 
            },
            new Customer
            {
                UserId = 30,
                VisitsNum = 0 
            }
        };
        
        public void Seed(EntityTypeBuilder<Customer> builder)
        {
            builder.HasData(customers);
        }
    }
}