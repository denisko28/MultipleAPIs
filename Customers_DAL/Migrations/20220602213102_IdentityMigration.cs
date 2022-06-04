using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Customers_DAL.Migrations
{
    public partial class IdentityMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descript = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DayOff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date_ = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayOff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PossibleTime",
                columns: table => new
                {
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Possible__8E79CB0049844667", x => x.Time);
                });

            migrationBuilder.CreateTable(
                name: "Service_",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_ = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    Available = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service_", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User_",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_User__UserId",
                        column: x => x.UserId,
                        principalTable: "User_",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_User__UserId",
                        column: x => x.UserId,
                        principalTable: "User_",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_User__UserId",
                        column: x => x.UserId,
                        principalTable: "User_",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_User__UserId",
                        column: x => x.UserId,
                        principalTable: "User_",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    VisitsNum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Customer_User",
                        column: x => x.UserId,
                        principalTable: "User_",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    EmployeeStatusId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    PassportImgPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee_User", x => x.UserId);
                    table.ForeignKey(
                        name: "FK__Employee__Branch__29572725",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Employee_User",
                        column: x => x.UserId,
                        principalTable: "User_",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Barber",
                columns: table => new
                {
                    EmployeeUserId = table.Column<int>(type: "int", nullable: false),
                    ChairNum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barber_User", x => x.EmployeeUserId);
                    table.ForeignKey(
                        name: "FK_Barber_User",
                        column: x => x.EmployeeUserId,
                        principalTable: "Employee",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDayOff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeUserId = table.Column<int>(type: "int", nullable: true),
                    DayOffId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDayOff", x => x.Id);
                    table.ForeignKey(
                        name: "FK__EmployeeD__DayOf__2F10007B",
                        column: x => x.DayOffId,
                        principalTable: "DayOff",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__EmployeeD__Emplo__2E1BDC42",
                        column: x => x.EmployeeUserId,
                        principalTable: "Employee",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BarberUserId = table.Column<int>(type: "int", nullable: false),
                    CustomerUserId = table.Column<int>(type: "int", nullable: false),
                    AppointmentStatusId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    AppDate = table.Column<DateTime>(type: "date", nullable: false),
                    BeginTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Appointme__Barbe__3D5E1FD2",
                        column: x => x.BarberUserId,
                        principalTable: "Barber",
                        principalColumn: "EmployeeUserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Appointme__Custo__3E52440B",
                        column: x => x.CustomerUserId,
                        principalTable: "Customer",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentService",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentId = table.Column<int>(type: "int", nullable: true),
                    ServiceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentService", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Appointme__Appoi__412EB0B6",
                        column: x => x.AppointmentId,
                        principalTable: "Appointment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__Appointme__Servi__4222D4EF",
                        column: x => x.ServiceId,
                        principalTable: "Service_",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "1", "Admin", "Admin" },
                    { 2, "2", "Manager", "Manager" },
                    { 3, "3", "Barber", "Barber" },
                    { 4, "4", "Customer", "Customer" }
                });

            migrationBuilder.InsertData(
                table: "Branch",
                columns: new[] { "Id", "Address", "Descript" },
                values: new object[,]
                {
                    { 1, "вул. Банкова 12, Київ, Київська область", "Barbershop Lodon(1)" },
                    { 2, "вул. Героїв майдану 55, Чернівці, Чернівецька область", "Barbershop Lodon(2)" },
                    { 3, "вул. Степана Бандери 2-А, Львів, Львівська область", "Barbershop Lodon(3)" }
                });

            migrationBuilder.InsertData(
                table: "DayOff",
                columns: new[] { "Id", "Date_" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 3, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2022, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(2022, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(2022, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "PossibleTime",
                columns: new[] { "Time", "Available" },
                values: new object[,]
                {
                    { new TimeSpan(0, 8, 0, 0, 0), true },
                    { new TimeSpan(0, 8, 15, 0, 0), true },
                    { new TimeSpan(0, 8, 30, 0, 0), true },
                    { new TimeSpan(0, 8, 45, 0, 0), true },
                    { new TimeSpan(0, 9, 0, 0, 0), true },
                    { new TimeSpan(0, 9, 15, 0, 0), true },
                    { new TimeSpan(0, 9, 30, 0, 0), true },
                    { new TimeSpan(0, 9, 45, 0, 0), true },
                    { new TimeSpan(0, 10, 0, 0, 0), true },
                    { new TimeSpan(0, 10, 15, 0, 0), true },
                    { new TimeSpan(0, 10, 30, 0, 0), true },
                    { new TimeSpan(0, 10, 45, 0, 0), true },
                    { new TimeSpan(0, 11, 0, 0, 0), true },
                    { new TimeSpan(0, 11, 15, 0, 0), true },
                    { new TimeSpan(0, 11, 30, 0, 0), true },
                    { new TimeSpan(0, 11, 45, 0, 0), true },
                    { new TimeSpan(0, 12, 0, 0, 0), true },
                    { new TimeSpan(0, 12, 15, 0, 0), true },
                    { new TimeSpan(0, 12, 30, 0, 0), true },
                    { new TimeSpan(0, 12, 45, 0, 0), true },
                    { new TimeSpan(0, 13, 0, 0, 0), true },
                    { new TimeSpan(0, 13, 15, 0, 0), true },
                    { new TimeSpan(0, 13, 30, 0, 0), true },
                    { new TimeSpan(0, 13, 45, 0, 0), true },
                    { new TimeSpan(0, 14, 0, 0, 0), true },
                    { new TimeSpan(0, 14, 15, 0, 0), true },
                    { new TimeSpan(0, 14, 30, 0, 0), true },
                    { new TimeSpan(0, 14, 45, 0, 0), true },
                    { new TimeSpan(0, 15, 0, 0, 0), true },
                    { new TimeSpan(0, 15, 15, 0, 0), true },
                    { new TimeSpan(0, 15, 30, 0, 0), true }
                });

            migrationBuilder.InsertData(
                table: "PossibleTime",
                columns: new[] { "Time", "Available" },
                values: new object[,]
                {
                    { new TimeSpan(0, 15, 45, 0, 0), true },
                    { new TimeSpan(0, 16, 0, 0, 0), true },
                    { new TimeSpan(0, 16, 15, 0, 0), true },
                    { new TimeSpan(0, 16, 30, 0, 0), true },
                    { new TimeSpan(0, 16, 45, 0, 0), true },
                    { new TimeSpan(0, 17, 0, 0, 0), true },
                    { new TimeSpan(0, 17, 15, 0, 0), true },
                    { new TimeSpan(0, 17, 30, 0, 0), true },
                    { new TimeSpan(0, 17, 45, 0, 0), true },
                    { new TimeSpan(0, 18, 0, 0, 0), true },
                    { new TimeSpan(0, 18, 15, 0, 0), true },
                    { new TimeSpan(0, 18, 30, 0, 0), true },
                    { new TimeSpan(0, 18, 45, 0, 0), true },
                    { new TimeSpan(0, 19, 0, 0, 0), true },
                    { new TimeSpan(0, 19, 15, 0, 0), true },
                    { new TimeSpan(0, 19, 30, 0, 0), true },
                    { new TimeSpan(0, 19, 45, 0, 0), true },
                    { new TimeSpan(0, 20, 0, 0, 0), true },
                    { new TimeSpan(0, 20, 15, 0, 0), true },
                    { new TimeSpan(0, 20, 30, 0, 0), true },
                    { new TimeSpan(0, 20, 45, 0, 0), true },
                    { new TimeSpan(0, 21, 0, 0, 0), true },
                    { new TimeSpan(0, 21, 15, 0, 0), true },
                    { new TimeSpan(0, 21, 30, 0, 0), true },
                    { new TimeSpan(0, 21, 45, 0, 0), true },
                    { new TimeSpan(0, 22, 0, 0, 0), true },
                    { new TimeSpan(0, 22, 15, 0, 0), true },
                    { new TimeSpan(0, 22, 30, 0, 0), true },
                    { new TimeSpan(0, 22, 45, 0, 0), true },
                    { new TimeSpan(0, 23, 0, 0, 0), true }
                });

            migrationBuilder.InsertData(
                table: "Service_",
                columns: new[] { "Id", "Available", "Duration", "Name_", "Price" },
                values: new object[,]
                {
                    { 1, true, 60, "Стрижка", 300m },
                    { 2, true, 90, "Стрижка з бородою", 450m },
                    { 3, true, 30, "Голова - камуфляж сивини", 200m },
                    { 4, true, 30, "Борода - камуфляж сивини", 150m },
                    { 5, true, 45, "Дитяча стрижка", 200m },
                    { 6, true, 15, "Укладка", 100m },
                    { 7, true, 15, "Королівське гоління", 250m },
                    { 8, true, 15, "Видалення волосся воском", 100m },
                    { 9, false, 75, "Чистка лиця", 400m }
                });

            migrationBuilder.InsertData(
                table: "User_",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, null, "1e1ac0bc-c031-496d-bbd6-b8e140a3b7c3", null, false, "Петро", "Василенко", false, null, null, null, "AQAAAAEAACcQAAAAEK/alR94SuUsYKT8Q/R1x7QKyUvT0fLxB7NP3aK6tN0zSCwmoULvTmyJYp3aORefJQ==", null, false, null, false, "User1" },
                    { 2, 0, null, "7d8850fb-2c8c-4c77-b03f-1f9dfb8b51cd", null, false, "Іван", "Григоренко", false, null, null, null, "AQAAAAEAACcQAAAAEJgppkv1g0qNUf5zxqKMUn3MUeyxyEHeWEcknsdMb3OTRJSh78qL9tlYRfdAic2onQ==", null, false, null, false, "User2" },
                    { 3, 0, null, "5ba3a136-dc0d-4a68-ad57-197e87532df2", null, false, "Олександр", "Шевченко", false, null, null, null, "AQAAAAEAACcQAAAAEBIxIri67xQE6m6yMMi0/XW3dkOOLnpEHhQq/hkaCYT8dfgezsVGPfWYTYzmJ4K6pg==", null, false, null, false, "User3" }
                });

            migrationBuilder.InsertData(
                table: "User_",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 4, 0, null, "4d25dc5e-4713-4523-82a1-caffc5cc0740", null, false, "Роман", "Добровольський", false, null, null, null, "AQAAAAEAACcQAAAAECk9CPGf5l4bhtd/d3234WGlDOa9pSLafkzaLz9DTXCb/fhLwCajdzo3JdyUQPM8sg==", null, false, null, false, "User4" },
                    { 5, 0, null, "00ad5769-7f6d-4564-8e10-8e04da9a0b14", null, false, "Степан", "Петришко", false, null, null, null, "AQAAAAEAACcQAAAAEGnDDJgEz3dO/3XMLxwfuL0XSRnFNE9447gHudKiL/Fu8EOaKcMbgXNKx4sObSUTNw==", null, false, null, false, "User5" },
                    { 6, 0, null, "7de145c7-ef98-4715-8505-20510a39fc2d", null, false, "Світлана", "Петришко", false, null, null, null, "AQAAAAEAACcQAAAAEHxS1u8LK9QBRg9YH559451bwv9td1Wa47JrSAM3QmGM9rvEF3rbaEUNRvWVEfHPDw==", null, false, null, false, "User6" },
                    { 7, 0, null, "a92b87ca-8b1d-4637-806f-c826ee10db83", null, false, "Богдан", "Ящук", false, null, null, null, "AQAAAAEAACcQAAAAEKWePaNjxn5mfAwlzhTGaXIMsITC3x0WLcjnvKGN/DHDPDE7zdZcYYW0YrVVEeaOBg==", null, false, null, false, "User7" },
                    { 8, 0, null, "f2d19e9f-3ed4-4fc7-ab0c-24e102caca6e", null, false, "Валентина", "Генко", false, null, null, null, "AQAAAAEAACcQAAAAELpIzoXFJ7Po3S8DtvnQYzmZ7TNrkc4h0fd1fPVCuZLgPPKwH2TpUIi0uSdPfzRoLA==", null, false, null, false, "User8" },
                    { 9, 0, null, "af9373c3-a7a4-49b5-90d6-789497cc8f62", null, false, "Андрій", "Івашко", false, null, null, null, "AQAAAAEAACcQAAAAELXkvyUD4zg9yVRjM0t4i64yhgQ4yQifyloGFhnvdNA/52aBjxioHoV9JovPtxuAxA==", null, false, null, false, "User9" },
                    { 10, 0, null, "92ffd833-e836-43b5-8798-c17bdbe9f7a5", null, false, "Олександр", "Ванченко", false, null, null, null, "AQAAAAEAACcQAAAAEFPMelDkTBGliyDWeRx3nGXzEALrWuLX6qcPB4Ra/W/VdEKHVRQVxvz9jhT0hPBGmA==", null, false, null, false, "User10" },
                    { 11, 0, null, "0b0307bd-8f80-43bd-be22-15985f0804cc", null, false, "Володимир", "Михайлішин", false, null, null, null, "AQAAAAEAACcQAAAAEMvHu7p6M+ZtNvICWVCVzpiqKTMDaUZOXs4bVJVSFuy+O0acZexrQ1IR00kpoWZFlQ==", null, false, null, false, "User11" },
                    { 12, 0, null, "14c8c2e1-4f04-42bb-a4d3-6e225e3e87a9", null, false, "Станіслав", "Жолудь", false, null, null, null, "AQAAAAEAACcQAAAAEKc4I6OLoO/9l9GBBJ6DgHUgbTvt4ZxUxEiTcQ20ftzlu9k/UUYG1/VDHLBJetTmHg==", null, false, null, false, "User12" },
                    { 13, 0, null, "7b51afc0-0805-42f0-a104-a3a9c022b8a5", null, false, "Микола", "Лисенко", false, null, null, null, "AQAAAAEAACcQAAAAEKLhPwYIwtfXKISBWbogzaXRdsTTMU4gZ+tCjBEvXfiAZru0W1JeiwcfwUwifFcnWw==", null, false, null, false, "User13" },
                    { 14, 0, null, "2595aa40-e2e7-4b92-8758-efd788037a5a", null, false, "Дмитро", "Жовнірчук", false, null, null, null, "AQAAAAEAACcQAAAAEJ63H3gxCfAphLIcuucP8lgr/G/fBVMvw3Hhk7pfy9yMQBLAnPVXb9H4wDbkM1wTVg==", null, false, null, false, "User14" },
                    { 15, 0, null, "033ab118-1d9c-41bf-ac93-bb9fc9413902", null, false, "Валентин", "Федоренко", false, null, null, null, "AQAAAAEAACcQAAAAELDDSxcIF7bwrcGNi7fYThxggtIdVmHsgOrfDG9frPqfG+eE3Cp6G7Ldut46iuz8cA==", null, false, null, false, "User15" },
                    { 16, 0, null, "009bc5e4-c155-4049-90fc-497656b7decf", null, false, "Віталій", "Свистун", false, null, null, null, "AQAAAAEAACcQAAAAEAbyOYQdWE9Z+DIrDgpjszTumHATDDIIQIp0UVOpCJMl/TJXJWL8fhm5ZABY4NFyfw==", null, false, null, false, "User16" },
                    { 17, 0, null, "a033a3c2-60ac-4892-913c-9fd8e27fe58b", null, false, "Інокентій", "Фірташ", false, null, null, null, "AQAAAAEAACcQAAAAEN1fNpTAvFy2QHVWrzZ9jPXMa1CNIp2Ib+a7u3HkSvWz/1yAVQWUdruKD62ltr7TEQ==", null, false, null, false, "User17" },
                    { 18, 0, null, "aee2722b-68c0-45e7-9af3-9286d775161e", null, false, "Ярослав", "Татарчук", false, null, null, null, "AQAAAAEAACcQAAAAEIx8ZEKMIat1qmy7ug4vMPc52isyIpRCr0YhdDvY6jqSEfZmxlgI8xI5dBik7e1zCg==", null, false, null, false, "User18" },
                    { 19, 0, null, "b89c03a7-331a-429c-baa6-071ecb7a22db", null, false, "Йосиф", "Дмитренко", false, null, null, null, "AQAAAAEAACcQAAAAEBGjT7jEM405I3BknFlnDdwKa62BLguC4agmNJLcBEVv2mqEHX/g8lgo6INwFoTFyQ==", null, false, null, false, "User19" },
                    { 20, 0, null, "9d21e332-0102-4abf-a224-0a1b3faf8520", null, false, "Констянтин", "Шарапенко", false, null, null, null, "AQAAAAEAACcQAAAAEMJWMdKrImT1sUyBrMqYGFVUZgLxWo/pnSVIg+Ld87wDnjkcQ13b0usZ30w/UnMyfg==", null, false, null, false, "User20" },
                    { 21, 0, null, "a4f8f146-3cc8-4cb0-b9d0-32e50174e3fe", null, false, "Олег", "Притула", false, null, null, null, "AQAAAAEAACcQAAAAEKxAg28Gtu/DwGKHz3jbAi38tnC9Oy3LBc6HSAqk4ZWSAAPfuCDj74c4ehO3UiU6OQ==", null, false, null, false, "User21" },
                    { 22, 0, null, "9b416149-018b-42e2-9830-58553318709f", null, false, "Анатолій", "Назаренко", false, null, null, null, "AQAAAAEAACcQAAAAEPVmmU1H+KSPSzXhYx5oF6CzcDfwpNzKm7DJVHydUoqF8axZVqWUIGv+j+1G5uFzGA==", null, false, null, false, "User22" },
                    { 23, 0, null, "cd355748-4d6f-4a39-b53a-d7ad72508f2e", null, false, "Микола", "Вакуленко", false, null, null, null, "AQAAAAEAACcQAAAAEKMUgN8m0Q9SoApBxEmz6yQrrLnRodHPQ9Bp9YQCZXedUKGlBqRY8W1b/ynZF2zWjg==", null, false, null, false, "User23" },
                    { 24, 0, null, "c625edff-4c6d-4e7f-aeec-1099769cc7a2", null, false, "Степан", "Барабаш", false, null, null, null, "AQAAAAEAACcQAAAAECC3Xzztna60GTac5VfF1VRklHzAlm1BvMx5i+mgzFnem2W9YhlbbLWDwQMwjwo9IA==", null, false, null, false, "User24" },
                    { 25, 0, null, "be88073f-5080-463b-8276-ab31270b96df", null, false, "Денис", "Ярема", false, null, null, null, "AQAAAAEAACcQAAAAELrUUlIdNM2yC3LLr7xd3SmQcc/j4wixvKlqUfge99Ltes9yEYUNAeHRPnZjQmeeyA==", null, false, null, false, "User25" },
                    { 26, 0, null, "a1d010d7-71bc-4fbe-b76b-afded2a9779e", null, false, "Олег", "Таралевич", false, null, null, null, "AQAAAAEAACcQAAAAEPxrDl7WCV2iCeO2nCCv10gHS29onWHjJ5bVqCZiRV+oEJytjN1wIdshf3babGV0sA==", null, false, null, false, "User26" },
                    { 27, 0, null, "26c39e9d-f8f7-4b2d-9e6b-0ac243a90106", null, false, "Сергій", "Іващук", false, null, null, null, "AQAAAAEAACcQAAAAEINeZCLI7aodwOvG8pQ7JtJIFj9iH5l9vyKEfLtW6lmUVYqoz4CYTcoy7/lWoSPd6A==", null, false, null, false, "User27" },
                    { 28, 0, null, "3882a793-a6ba-4fb0-9515-373fca8a565d", null, false, "Михайло", "Компанієць", false, null, null, null, "AQAAAAEAACcQAAAAEEafAuHMTwnfVJuLFK9cBJhKct6BBf+aKX9595EH0CaGWUjDPOTmlPUSDx5LIKyXsw==", null, false, null, false, "User28" },
                    { 29, 0, null, "38b2c5cc-f33d-46c2-bd84-d9f408102d50", null, false, "Андрій", "Іващук", false, null, null, null, "AQAAAAEAACcQAAAAEJ75oK2+Q0ELx9MR37vBF+ybrpq83AWnkSpgcXOZbYEYmmS7EOAucZMwyia0VpOPhA==", null, false, null, false, "User29" },
                    { 30, 0, null, "45a6abcf-9332-4999-9a11-c6e312b8e1ce", null, false, "Назар", "Мельник", false, null, null, null, "AQAAAAEAACcQAAAAEOUuoxNa+kklao5euNqBqa5uW5Z04Om8kx8Nc1pF99K77JtAkU8Hv6g7FedcZ6iIFA==", null, false, null, false, "User30" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 3, 4 },
                    { 3, 5 },
                    { 3, 6 },
                    { 2, 7 },
                    { 3, 8 },
                    { 3, 9 },
                    { 3, 10 },
                    { 2, 11 },
                    { 3, 12 },
                    { 3, 13 },
                    { 3, 14 },
                    { 3, 15 },
                    { 4, 16 },
                    { 4, 17 },
                    { 4, 18 },
                    { 4, 19 },
                    { 4, 20 },
                    { 4, 21 },
                    { 4, 22 },
                    { 4, 23 },
                    { 4, 24 },
                    { 4, 25 },
                    { 4, 26 },
                    { 4, 27 },
                    { 4, 28 },
                    { 4, 29 },
                    { 4, 30 }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "UserId", "VisitsNum" },
                values: new object[,]
                {
                    { 16, 0 },
                    { 17, 0 },
                    { 18, 0 },
                    { 19, 0 },
                    { 20, 0 },
                    { 21, 0 },
                    { 22, 0 },
                    { 23, 0 },
                    { 24, 0 },
                    { 25, 0 },
                    { 26, 0 },
                    { 27, 0 }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "UserId", "VisitsNum" },
                values: new object[,]
                {
                    { 28, 0 },
                    { 29, 0 },
                    { 30, 0 }
                });

            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "UserId", "Address", "Birthday", "BranchId", "EmployeeStatusId", "PassportImgPath" },
                values: new object[,]
                {
                    { 1, "Бульвар незалежності 12-А, Київ, Київська область", new DateTime(1995, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "" },
                    { 2, "вул. Золотоворітська 18, Київ, Київська область", new DateTime(1998, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 2, "" },
                    { 3, "вул. Дарвіна, Київ, Київська область", new DateTime(1994, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, "" },
                    { 4, "вул. Січових Стрільців, Київ, Київська область", new DateTime(1997, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, "" },
                    { 5, "вул. Івана Богуна, Київ, Київська область", new DateTime(2000, 4, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, "" },
                    { 6, "вул. Татарська, Петропавлівська Борщагівка, Київська область", new DateTime(1993, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 3, "" },
                    { 7, "вул. Університетська, Чернівці, Чернівецька область", new DateTime(1993, 4, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, "" },
                    { 8, "вул. Поштова, Чернівці, Чернівецька область", new DateTime(1992, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3, "" },
                    { 9, "вул. Богдана Хмельницького, Чернівці, Чернівецька область", new DateTime(1995, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3, "" },
                    { 10, "вул. Селятинська, Чернівці, Чернівецька область", new DateTime(1998, 3, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3, "" },
                    { 11, "вул. Михайлівська, Львів, Львівська область", new DateTime(1990, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2, "" },
                    { 12, "вул. Лесі Українки, Львів, Львівська область", new DateTime(1998, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, "" },
                    { 13, "вул. Вірменська, Львів, Львівська область", new DateTime(1991, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, "" },
                    { 14, "вул. Шолом-Алейхема, Львів, Львівська область", new DateTime(1996, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, "" },
                    { 15, "вул. Горлівська, Львів, Львівська область", new DateTime(1996, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, "" }
                });

            migrationBuilder.InsertData(
                table: "Barber",
                columns: new[] { "EmployeeUserId", "ChairNum" },
                values: new object[,]
                {
                    { 3, 1 },
                    { 4, 2 },
                    { 5, 3 },
                    { 6, 4 },
                    { 8, 1 },
                    { 9, 2 },
                    { 10, 3 },
                    { 12, 1 },
                    { 13, 2 },
                    { 14, 3 },
                    { 15, 4 }
                });

            migrationBuilder.InsertData(
                table: "EmployeeDayOff",
                columns: new[] { "Id", "DayOffId", "EmployeeUserId" },
                values: new object[,]
                {
                    { 1, 1, 3 },
                    { 2, 2, 9 },
                    { 3, 3, 14 },
                    { 4, 4, 15 }
                });

            migrationBuilder.InsertData(
                table: "Appointment",
                columns: new[] { "Id", "AppDate", "AppointmentStatusId", "BarberUserId", "BeginTime", "CustomerUserId", "EndTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4, new TimeSpan(0, 12, 0, 0, 0), 19, new TimeSpan(0, 13, 0, 0, 0) },
                    { 2, new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 9, new TimeSpan(0, 16, 0, 0, 0), 16, new TimeSpan(0, 17, 30, 0, 0) },
                    { 3, new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3, new TimeSpan(0, 17, 0, 0, 0), 18, new TimeSpan(0, 18, 30, 0, 0) },
                    { 4, new DateTime(2022, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 6, new TimeSpan(0, 13, 30, 0, 0), 22, new TimeSpan(0, 15, 15, 0, 0) },
                    { 5, new DateTime(2022, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 8, new TimeSpan(0, 16, 15, 0, 0), 17, new TimeSpan(0, 17, 15, 0, 0) },
                    { 6, new DateTime(2022, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5, new TimeSpan(0, 10, 0, 0, 0), 21, new TimeSpan(0, 10, 15, 0, 0) },
                    { 7, new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 6, new TimeSpan(0, 15, 0, 0, 0), 23, new TimeSpan(0, 15, 45, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "AppointmentService",
                columns: new[] { "Id", "AppointmentId", "ServiceId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 2, 3 },
                    { 4, 3, 2 },
                    { 5, 4, 1 },
                    { 6, 4, 4 },
                    { 7, 4, 8 },
                    { 8, 5, 1 },
                    { 9, 6, 6 },
                    { 10, 7, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_BarberUserId",
                table: "Appointment",
                column: "BarberUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_CustomerUserId",
                table: "Appointment",
                column: "CustomerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentService_AppointmentId",
                table: "AppointmentService",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentService_ServiceId",
                table: "AppointmentService",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_BranchId",
                table: "Employee",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDayOff_DayOffId",
                table: "EmployeeDayOff",
                column: "DayOffId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDayOff_EmployeeUserId",
                table: "EmployeeDayOff",
                column: "EmployeeUserId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "User_",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "User_",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentService");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EmployeeDayOff");

            migrationBuilder.DropTable(
                name: "PossibleTime");

            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "Service_");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "DayOff");

            migrationBuilder.DropTable(
                name: "Barber");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Branch");

            migrationBuilder.DropTable(
                name: "User_");
        }
    }
}
