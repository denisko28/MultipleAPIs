using System;
using System.Collections.Generic;
using Customers_DAL.Entities;
using Customers_DAL.Seeding.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers_DAL.Seeding.Concrete
{
    public class AppointmentsSeeder : ISeeder<Appointment>
    {
        private static readonly List<Appointment> appointments = new()
        {
            new Appointment()
            {
                Id = 1,
                BarberUserId = 4,
                CustomerUserId = 19,
                AppointmentStatusId = 3,
                BranchId = 1,
                AppDate = DateTime.Parse("2022-02-27"),
                BeginTime = TimeSpan.Parse("12:00:00"),
                EndTime = TimeSpan.Parse("13:00:00")
            },
            new Appointment()
            {
                Id = 2,
                BarberUserId = 9,
                CustomerUserId = 16,
                AppointmentStatusId = 2,
                BranchId = 2,
                AppDate = DateTime.Parse("2022-03-01"),
                BeginTime = TimeSpan.Parse("16:00:00"),
                EndTime = TimeSpan.Parse("17:30:00")
            },
            new Appointment()
            {
                Id = 3,
                BarberUserId = 3,
                CustomerUserId = 18,
                AppointmentStatusId = 2,
                BranchId = 1,
                AppDate = DateTime.Parse("2022-03-01"),
                BeginTime = TimeSpan.Parse("17:00:00"),
                EndTime = TimeSpan.Parse("18:30:00")
            },
            new Appointment()
            {
                Id = 4,
                BarberUserId = 6,
                CustomerUserId = 22,
                AppointmentStatusId = 1,
                BranchId = 1,
                AppDate = DateTime.Parse("2022-03-08"),
                BeginTime = TimeSpan.Parse("13:30:00"),
                EndTime = TimeSpan.Parse("15:15:00")
            },
            new Appointment()
            {
                Id = 5,
                BarberUserId = 8,
                CustomerUserId = 17,
                AppointmentStatusId = 2,
                BranchId = 2,
                AppDate = DateTime.Parse("2022-03-12"),
                BeginTime = TimeSpan.Parse("16:15:00"),
                EndTime = TimeSpan.Parse("17:15:00")
            },
            new Appointment()
            {
                Id = 6,
                BarberUserId = 5,
                CustomerUserId = 21,
                AppointmentStatusId = 1,
                BranchId = 1,
                AppDate = DateTime.Parse("2022-03-10"),
                BeginTime = TimeSpan.Parse("10:00:00"),
                EndTime = TimeSpan.Parse("10:15:00")
            },
            new Appointment()
            {
                Id = 7,
                BarberUserId = 6,
                CustomerUserId = 23,
                AppointmentStatusId = 2,
                BranchId = 1,
                AppDate = DateTime.Parse("2022-03-15"),
                BeginTime = TimeSpan.Parse("15:00:00"),
                EndTime = TimeSpan.Parse("15:45:00")
            }
        };
        
        public void Seed(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasData(appointments);
        }
    }
}