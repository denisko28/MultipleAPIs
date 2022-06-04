using System;
using System.Collections.Generic;
using Customers_DAL.Entities;
using Customers_DAL.Seeding.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers_DAL.Seeding.Concrete
{
    public class AppointmentServicesSeeder : ISeeder<AppointmentService>
    {
        private static readonly List<AppointmentService> appointmentServices = new()
        {
            new AppointmentService
            {
                Id = 1,
                AppointmentId = 1,
                ServiceId = 1 
            },
            new AppointmentService
            {
                Id = 2,
                AppointmentId = 2,
                ServiceId = 1 
            },
            new AppointmentService
            {
                Id = 3,
                AppointmentId = 2,
                ServiceId = 3
            },
            new AppointmentService
            {
                Id = 4,
                AppointmentId = 3,
                ServiceId = 2 
            },
            new AppointmentService
            {
                Id = 5,
                AppointmentId = 4,
                ServiceId = 1
            },
            new AppointmentService
            {
                Id = 6,
                AppointmentId = 4,
                ServiceId = 4
            },
            new AppointmentService
            {
                Id = 7,
                AppointmentId = 4,
                ServiceId = 8
            },
            new AppointmentService
            {
                Id = 8,
                AppointmentId = 5,
                ServiceId = 1
            },
            new AppointmentService
            {
                Id = 9,
                AppointmentId = 6,
                ServiceId = 6
            },
            new AppointmentService
            {
                Id = 10,
                AppointmentId = 7,
                ServiceId = 5
            }
        };
        
        public void Seed(EntityTypeBuilder<AppointmentService> builder)
        {
            builder.HasData(appointmentServices);
        }
    }
}