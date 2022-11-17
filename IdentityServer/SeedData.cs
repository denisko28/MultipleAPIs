using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using IdentityServer.Data;
using IdentityServer.Helpers;
using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace IdentityServer
{
    public static class SeedData
    {
        private static readonly List<(ApplicationUser, string, string, int?)> users = new()
        {
            (
                new ApplicationUser
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
                UserRoles.Admin,
                null
            ),
            (
                new ApplicationUser
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
                UserRoles.Manager,
                1
            ),
            (
                new ApplicationUser
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
                UserRoles.Barber,
                1
            ),
            (
                new ApplicationUser
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
                UserRoles.Barber,
                1
            ),
            (
                new ApplicationUser
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
                UserRoles.Barber,
                1
            ),
            (
                new ApplicationUser
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
                UserRoles.Barber,
                1
            ),
            (
                new ApplicationUser
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
                UserRoles.Manager,
                2
            ),
            (
                new ApplicationUser
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
                UserRoles.Barber,
                2
            ),
            (
                new ApplicationUser
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
                UserRoles.Barber,
                2
            ),
            (
                new ApplicationUser
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
                UserRoles.Barber,
                2
            ),
            (
                new ApplicationUser
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
                UserRoles.Manager,
                3
            ),
            (
                new ApplicationUser
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
                UserRoles.Barber,
                3
            ),
            (
                new ApplicationUser
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
                UserRoles.Barber,
                3
            ),
            (
                new ApplicationUser
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
                UserRoles.Barber,
                3
            ),
            (
                new ApplicationUser
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
                UserRoles.Barber,
                3
            ),

            (
                new ApplicationUser
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
                UserRoles.Customer,
                null
            ),
            (
                new ApplicationUser
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
                UserRoles.Customer,
                null
            ),
            (
                new ApplicationUser
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
                UserRoles.Customer,
                null
            ),
            (
                new ApplicationUser
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
                UserRoles.Customer,
                null
            ),
            (
                new ApplicationUser
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
                UserRoles.Customer,
                null
            ),
            (
                new ApplicationUser
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
                UserRoles.Customer,
                null
            ),
            (
                new ApplicationUser
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
                UserRoles.Customer,
                null
            ),
            (
                new ApplicationUser
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
                UserRoles.Customer,
                null
            ),
            (
                new ApplicationUser
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
                UserRoles.Customer,
                null
            ),
            (
                new ApplicationUser
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
                UserRoles.Customer,
                null
            ),
            (
                new ApplicationUser
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
                UserRoles.Customer,
                null
            ),
            (
                new ApplicationUser
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
                UserRoles.Customer,
                null
            ),
            (
                new ApplicationUser
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
                UserRoles.Customer,
                null
            ),
            (
                new ApplicationUser
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
                UserRoles.Customer,
                null
            ),
            (
                new ApplicationUser
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
                UserRoles.Customer,
                null
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

        public static void Seed(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            using var serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.Migrate();

            SeedRoles(scope);
            SeedUsers(scope);
        }

        private static void SeedRoles(IServiceScope scope)
        {
            var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

            foreach (var role in roles)
            {
                var result = roleMgr.CreateAsync(role).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
            }
        }

        private static void SeedUsers(IServiceScope scope)
        {
            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            foreach (var (user, password, roleName, branchId) in users)
            {
                user.Email = user.UserName;
                user.NormalizedEmail = user.Email.ToUpper();
                user.EmailConfirmed = true;
                user.NormalizedUserName = user.UserName.ToUpper();
                var result = userMgr.CreateAsync(user, password).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddToRoleAsync(user, roleName).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                result = userMgr.AddClaimsAsync(user, new Claim[]
                {
                    new(CustomJwtClaimTypes.UserId, user.Id.ToString()),
                    new(CustomJwtClaimTypes.Email, user.UserName),
                    new(CustomJwtClaimTypes.FirstName, user.FirstName),
                    new(CustomJwtClaimTypes.LastName, user.LastName),
                    new(CustomJwtClaimTypes.BranchId, branchId.ToString() ?? string.Empty),
                    new(CustomJwtClaimTypes.Role, roleName)
                }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                Log.Debug("Users were created");
            }
        }
    }
}