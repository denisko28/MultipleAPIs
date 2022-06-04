using System;
using System.Collections.Generic;
using Customers_DAL.Entities;
using Customers_DAL.Seeding.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers_DAL.Seeding.Concrete
{
    public class EmployeesSeeder : ISeeder<Employee>
    {
        private static readonly List<Employee> employees = new()
        {
            new Employee
            {
                UserId = 1,
                BranchId = 1,
                EmployeeStatusId = 1,
                Address = "Бульвар незалежності 12-А, Київ, Київська область",
                PassportImgPath = "",
                Birthday = DateTime.Parse("1995-02-04")
            },
            new Employee
            {
                UserId = 2,
                BranchId = 1,
                EmployeeStatusId = 2,
                Address = "вул. Золотоворітська 18, Київ, Київська область",
                PassportImgPath = "",
                Birthday = DateTime.Parse("1998-09-18")
            },
            new Employee
            {
                UserId = 3,
                BranchId = 1,
                EmployeeStatusId = 3,
                Address = "вул. Дарвіна, Київ, Київська область",
                PassportImgPath = "",
                Birthday = DateTime.Parse("1994-11-09")
            },
            new Employee
            {
                UserId = 4,
                BranchId = 1,
                EmployeeStatusId = 3,
                Address = "вул. Січових Стрільців, Київ, Київська область",
                PassportImgPath = "",
                Birthday = DateTime.Parse("1997-01-16")
            },
            new Employee
            {
                UserId = 5,
                BranchId = 1,
                EmployeeStatusId = 3,
                Address = "вул. Івана Богуна, Київ, Київська область",
                PassportImgPath = "",
                Birthday = DateTime.Parse("2000-04-21")
            },
            new Employee
            {
                UserId = 6,
                BranchId = 1,
                EmployeeStatusId = 3,
                Address = "вул. Татарська, Петропавлівська Борщагівка, Київська область",
                PassportImgPath = "",
                Birthday = DateTime.Parse("1993-08-22")
            },
            new Employee
            {
                UserId = 7,
                BranchId = 2,
                EmployeeStatusId = 2,
                Address = "вул. Університетська, Чернівці, Чернівецька область",
                PassportImgPath = "",
                Birthday = DateTime.Parse("1993-04-27")
            },
            new Employee
            {
                UserId = 8,
                BranchId = 2,
                EmployeeStatusId = 3,
                Address = "вул. Поштова, Чернівці, Чернівецька область",
                PassportImgPath = "",
                Birthday = DateTime.Parse("1992-02-02")
            },
            new Employee
            {
                UserId = 9,
                BranchId = 2,
                EmployeeStatusId = 3,
                Address = "вул. Богдана Хмельницького, Чернівці, Чернівецька область",
                PassportImgPath = "",
                Birthday = DateTime.Parse("1995-08-07")
            },
            new Employee
            {
                UserId = 10,
                BranchId = 2,
                EmployeeStatusId = 3,
                Address = "вул. Селятинська, Чернівці, Чернівецька область",
                PassportImgPath = "",
                Birthday = DateTime.Parse("1998-03-24")
            },
            new Employee
            {
                UserId = 11,
                BranchId = 3,
                EmployeeStatusId = 2,
                Address = "вул. Михайлівська, Львів, Львівська область",
                PassportImgPath = "",
                Birthday = DateTime.Parse("1990-09-12")
            },
            new Employee
            {
                UserId = 12,
                BranchId = 3,
                EmployeeStatusId = 3,
                Address = "вул. Лесі Українки, Львів, Львівська область",
                PassportImgPath = "",
                Birthday = DateTime.Parse("1998-04-29")
            },
            new Employee
            {
                UserId = 13,
                BranchId = 3,
                EmployeeStatusId = 3,
                Address = "вул. Вірменська, Львів, Львівська область",
                PassportImgPath = "",
                Birthday = DateTime.Parse("1991-01-14")
            },
            new Employee
            {
                UserId = 14,
                BranchId = 3,
                EmployeeStatusId = 3,
                Address = "вул. Шолом-Алейхема, Львів, Львівська область",
                PassportImgPath = "",
                Birthday = DateTime.Parse("1996-05-02")
            },
            new Employee
            {
                UserId = 15,
                BranchId = 3,
                EmployeeStatusId = 3,
                Address = "вул. Горлівська, Львів, Львівська область",
                PassportImgPath = "",
                Birthday = DateTime.Parse("1996-11-03")
            },
        };
        
        public void Seed(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData(employees);
        }
    }
}