using System;
using System.Collections.Generic;
using System.Linq;
using Customers_DAL.Entities;
using Customers_DAL.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Customers_DAL.Seeding.Concrete
{
    public class UsersWithRolesSeeder
    {
        private static readonly List<(User, string, string)> users = new()
        {
            (
                new User
                {
                    Id = 1,
                    UserName = "User1@gmail.com",
                    FirstName = "Петро",
                    LastName = "Василенко",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password1",
                UserRoles.Admin
            ),
            (
                new User
                {
                    Id = 2,
                    UserName = "User2@outlook.com",
                    FirstName = "Іван",
                    LastName = "Григоренко",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password2",
                UserRoles.Manager
            ),
            (
                new User
                {
                    Id = 3,
                    UserName = "User3@gmail.com",
                    FirstName = "Олександр",
                    LastName = "Шевченко",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password3",
                UserRoles.Barber
            ),
            (
                new User
                {
                    Id = 4,
                    UserName = "User4@outlook.com",
                    FirstName = "Роман",
                    LastName = "Добровольський",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password4",
                UserRoles.Barber
            ),
            (
                new User
                {
                    Id = 5,
                    UserName = "User5@outlook.com",
                    FirstName = "Степан",
                    LastName = "Петришко",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password5",
                UserRoles.Barber
            ),
            (
                new User
                {
                    Id = 6,
                    UserName = "User6@gmail.com",
                    FirstName = "Світлана",
                    LastName = "Петришко",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password6",
                UserRoles.Barber
            ),
            (
                new User
                {
                    Id = 7,
                    UserName = "User7@yahoo.com",
                    FirstName = "Богдан",
                    LastName = "Ящук",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password7",
                UserRoles.Manager
            ),
            (
                new User
                {
                    Id = 8,
                    UserName = "User8@outlook.com",
                    FirstName = "Валентина",
                    LastName = "Генко",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password8",
                UserRoles.Barber
            ),
            (
                new User
                {
                    Id = 9,
                    UserName = "User9@gmail.com",
                    FirstName = "Андрій",
                    LastName = "Івашко",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password9",
                UserRoles.Barber
            ),
            (
                new User
                {
                    Id = 10,
                    UserName = "User10@gmail.com",
                    FirstName = "Олександр",
                    LastName = "Ванченко",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password10",
                UserRoles.Barber
            ),
            (
                new User
                {
                    Id = 11,
                    UserName = "User11@ukr.net",
                    FirstName = "Володимир",
                    LastName = "Михайлішин",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password11",
                UserRoles.Manager
            ),
            (
                new User
                {
                    Id = 12,
                    UserName = "User12@outlook.com",
                    FirstName = "Станіслав",
                    LastName = "Жолудь",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password12",
                UserRoles.Barber
            ),
            (
                new User
                {
                    Id = 13,
                    UserName = "User13@gmail.com",
                    FirstName = "Микола",
                    LastName = "Лисенко",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password13",
                UserRoles.Barber
            ),
            (
                new User
                {
                    Id = 14,
                    UserName = "User14@outlook.com",
                    FirstName = "Дмитро",
                    LastName = "Жовнірчук",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password14",
                UserRoles.Barber
            ),
            (
                new User
                {
                    Id = 15,
                    UserName = "User15@ukr.net",
                    FirstName = "Валентин",
                    LastName = "Федоренко",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password15",
                UserRoles.Barber
            ),
            
            (
                new User
                {
                    Id = 16,
                    UserName = "User16@gmail.com",
                    FirstName = "Віталій",
                    LastName = "Свистун",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password16",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 17,
                    UserName = "User17@gmail.com",
                    FirstName = "Інокентій",
                    LastName = "Фірташ",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password17",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 18,
                    UserName = "User18@gmail.com",
                    FirstName = "Ярослав",
                    LastName = "Татарчук",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password18",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 19,
                    UserName = "User19@ukr.net",
                    FirstName = "Йосиф",
                    LastName = "Дмитренко",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password19",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 20,
                    UserName = "User20@ukr.net",
                    FirstName = "Констянтин",
                    LastName = "Шарапенко",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password20",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 21,
                    UserName = "User21@outlook.com",
                    FirstName = "Олег",
                    LastName = "Притула",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password21",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 22,
                    UserName = "User22@gmail.com",
                    FirstName = "Анатолій",
                    LastName = "Назаренко",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password22",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 23,
                    UserName = "User23@ukr.net",
                    FirstName = "Микола",
                    LastName = "Вакуленко",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password23",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 24,
                    UserName = "User24@outlook.com",
                    FirstName = "Степан",
                    LastName = "Барабаш",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password24",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 25,
                    UserName = "User25@gmail.com",
                    FirstName = "Денис",
                    LastName = "Ярема",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password25",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 26,
                    UserName = "User26@ukr.net",
                    FirstName = "Олег",
                    LastName = "Таралевич",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password26",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 27,
                    UserName = "User27@gmail.com",
                    FirstName = "Сергій",
                    LastName = "Іващук",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password27",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 28,
                    UserName = "User28@yahoo.com",
                    FirstName = "Михайло",
                    LastName = "Компанієць",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password28",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 29,
                    UserName = "User29@outlook.com",
                    FirstName = "Андрій",
                    LastName = "Іващук",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password29",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 30,
                    UserName = "User30@gmail.com",
                    FirstName = "Назар",
                    LastName = "Мельник",
                    EmailConfirmed = true,
                    LockoutEnabled = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                "User%password30",
                UserRoles.Customer
            ),
        };
        
        private static readonly List<IdentityRole<int>> roles = new()
        {
            new IdentityRole<int>
            {
                Id = 1,
                Name = UserRoles.Admin,
                ConcurrencyStamp = "1",
                NormalizedName = UserRoles.Admin
            },
            new IdentityRole<int>
            {
                Id = 2,
                Name = UserRoles.Manager,
                ConcurrencyStamp = "2",
                NormalizedName = UserRoles.Manager
            },
            new IdentityRole<int>
            {
                Id = 3,
                Name = UserRoles.Barber,
                ConcurrencyStamp = "3",
                NormalizedName = UserRoles.Barber
            },
            new IdentityRole<int>
            {
                Id = 4,
                Name = UserRoles.Customer,
                ConcurrencyStamp = "4",
                NormalizedName = UserRoles.Customer
            }
        };
        
        public static void Seed(ModelBuilder builder)
        {
            SeedUsers(builder);
            
            SeedRoles(builder);
            
            SeedUserRoles(builder);
        }
        
        private static void SeedUsers(ModelBuilder builder)
        {
            foreach (var (user, password, roleName) in users)
            {
                user.PasswordHash = new PasswordHasher<User>().HashPassword(user, password);
                builder.Entity<User>().HasData(user);
            }
        }
        
        private static void SeedRoles(ModelBuilder builder) => builder.Entity<IdentityRole<int>>().HasData(roles);
        
        private static void SeedUserRoles(ModelBuilder builder)
        {
            var userRoles = new List<IdentityUserRole<int>>();
            foreach (var (user, password, roleName) in users)
            {
                var roleId = roles.Select(_ => _).FirstOrDefault(role => role.Name == roleName)!.Id;
                userRoles.Add(new IdentityUserRole<int>{ RoleId = roleId, UserId = user.Id});
                user.Email = user.UserName;
                user.NormalizedEmail = user.UserName.ToUpper();
            }
            
            builder.Entity<IdentityUserRole<int>>().HasData(userRoles);
        }
    }
}