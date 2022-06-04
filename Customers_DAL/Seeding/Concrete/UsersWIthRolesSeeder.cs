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
                    UserName = "User1",
                    FirstName = "Петро",
                    LastName = "Василенко"
                },
                "User%password1",
                UserRoles.Admin
            ),
            (
                new User
                {
                    Id = 2,
                    UserName = "User2",
                    FirstName = "Іван",
                    LastName = "Григоренко"
                },
                "User%password2",
                UserRoles.Manager
            ),
            (
                new User
                {
                    Id = 3,
                    UserName = "User3",
                    FirstName = "Олександр",
                    LastName = "Шевченко",
                },
                "User%password3",
                UserRoles.Barber
            ),
            (
                new User
                {
                    Id = 4,
                    UserName = "User4",
                    FirstName = "Роман",
                    LastName = "Добровольський"
                },
                "User%password4",
                UserRoles.Barber
            ),
            (
                new User
                {
                    Id = 5,
                    UserName = "User5",
                    FirstName = "Степан",
                    LastName = "Петришко"
                },
                "User%password5",
                UserRoles.Barber
            ),
            (
                new User
                {
                    Id = 6,
                    UserName = "User6",
                    FirstName = "Світлана",
                    LastName = "Петришко",
                },
                "User%password6",
                UserRoles.Barber
            ),
            (
                new User
                {
                    Id = 7,
                    UserName = "User7",
                    FirstName = "Богдан",
                    LastName = "Ящук"
                },
                "User%password7",
                UserRoles.Manager
            ),
            (
                new User
                {
                    Id = 8,
                    UserName = "User8",
                    FirstName = "Валентина",
                    LastName = "Генко",
                },
                "User%password8",
                UserRoles.Barber
            ),
            (
                new User
                {
                    Id = 9,
                    UserName = "User9",
                    FirstName = "Андрій",
                    LastName = "Івашко"
                },
                "User%password9",
                UserRoles.Barber
            ),
            (
                new User
                {
                    Id = 10,
                    UserName = "User10",
                    FirstName = "Олександр",
                    LastName = "Ванченко"
                },
                "User%password10",
                UserRoles.Barber
            ),
            (
                new User
                {
                    Id = 11,
                    UserName = "User11",
                    FirstName = "Володимир",
                    LastName = "Михайлішин"
                },
                "User%password11",
                UserRoles.Manager
            ),
            (
                new User
                {
                    Id = 12,
                    UserName = "User12",
                    FirstName = "Станіслав",
                    LastName = "Жолудь"
                },
                "User%password12",
                UserRoles.Barber
            ),
            (
                new User
                {
                    Id = 13,
                    UserName = "User13",
                    FirstName = "Микола",
                    LastName = "Лисенко"
                },
                "User%password13",
                UserRoles.Barber
            ),
            (
                new User
                {
                    Id = 14,
                    UserName = "User14",
                    FirstName = "Дмитро",
                    LastName = "Жовнірчук"
                },
                "User%password14",
                UserRoles.Barber
            ),
            (
                new User
                {
                    Id = 15,
                    UserName = "User15",
                    FirstName = "Валентин",
                    LastName = "Федоренко"
                },
                "User%password15",
                UserRoles.Barber
            ),
            
            (
                new User
                {
                    Id = 16,
                    UserName = "User16",
                    FirstName = "Віталій",
                    LastName = "Свистун"
                },
                "User%password16",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 17,
                    UserName = "User17",
                    FirstName = "Інокентій",
                    LastName = "Фірташ"
                },
                "User%password17",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 18,
                    UserName = "User18",
                    FirstName = "Ярослав",
                    LastName = "Татарчук"
                },
                "User%password18",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 19,
                    UserName = "User19",
                    FirstName = "Йосиф",
                    LastName = "Дмитренко"
                },
                "User%password19",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 20,
                    UserName = "User20",
                    FirstName = "Констянтин",
                    LastName = "Шарапенко"
                },
                "User%password20",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 21,
                    UserName = "User21",
                    FirstName = "Олег",
                    LastName = "Притула"
                },
                "User%password21",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 22,
                    UserName = "User22",
                    FirstName = "Анатолій",
                    LastName = "Назаренко"
                },
                "User%password22",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 23,
                    UserName = "User23",
                    FirstName = "Микола",
                    LastName = "Вакуленко"
                },
                "User%password23",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 24,
                    UserName = "User24",
                    FirstName = "Степан",
                    LastName = "Барабаш"
                },
                "User%password24",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 25,
                    UserName = "User25",
                    FirstName = "Денис",
                    LastName = "Ярема"
                },
                "User%password25",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 26,
                    UserName = "User26",
                    FirstName = "Олег",
                    LastName = "Таралевич"
                },
                "User%password26",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 27,
                    UserName = "User27",
                    FirstName = "Сергій",
                    LastName = "Іващук"
                },
                "User%password27",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 28,
                    UserName = "User28",
                    FirstName = "Михайло",
                    LastName = "Компанієць"
                },
                "User%password28",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 29,
                    UserName = "User29",
                    FirstName = "Андрій",
                    LastName = "Іващук"
                },
                "User%password29",
                UserRoles.Customer
            ),
            (
                new User
                {
                    Id = 30,
                    UserName = "User30",
                    FirstName = "Назар",
                    LastName = "Мельник"
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
            }
            
            builder.Entity<IdentityUserRole<int>>().HasData(userRoles);
        }
    }
}