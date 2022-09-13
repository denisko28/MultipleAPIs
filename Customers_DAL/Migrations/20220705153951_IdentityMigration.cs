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
                    Address = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
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
                    Name_ = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
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
                    FirstName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
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
                name: "ServiceDiscount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    DiscountSize = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceDiscount", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ServiceDi__Branc__3A81B327",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__ServiceDi__Servi__398D8EEE",
                        column: x => x.ServiceId,
                        principalTable: "Service_",
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_User",
                        column: x => x.UserId,
                        principalTable: "User_",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    { 1, 0, null, "22aff58f-3002-4142-a5c1-f7ada188d19f", "User1@gmail.com", true, "Петро", "Василенко", true, null, "USER1@GMAIL.COM", null, "AQAAAAEAACcQAAAAEPhyWSSpu1VQC5HQx2piOTBK5DpKtyU2HVcIHBUYeWtmAO/ydUCII1ylQustTsJNng==", null, false, "cb695984-28ea-4490-8b09-9afa0eb7cc63", false, "User1@gmail.com" },
                    { 2, 0, null, "6b3e2c6b-46d0-4d83-bf29-eaefa2a107dd", "User2@outlook.com", true, "Іван", "Григоренко", true, null, "USER2@OUTLOOK.COM", null, "AQAAAAEAACcQAAAAEHKy9oHywFE3qeMj1UXjL2DaS2Tkb5pzwZgIcb7zd2eriQl+YRJJRZE54CTqUQtupg==", null, false, "647a3498-c0b8-4c22-a7e6-6b67e5da6947", false, "User2@outlook.com" },
                    { 3, 0, null, "956c030e-a6e5-4f18-802e-2162352b6cbe", "User3@gmail.com", true, "Олександр", "Шевченко", true, null, "USER3@GMAIL.COM", null, "AQAAAAEAACcQAAAAEPSQgKVtJYfB1rwxhfd1ULIGDvrZ/M6lgKDR9UaC/8shXbk5Uo6ji+XnjZQwiiJOsg==", null, false, "ab2bf23e-177a-4fb2-9efa-6700d609e02e", false, "User3@gmail.com" }
                });

            migrationBuilder.InsertData(
                table: "User_",
                columns: new[] { "Id", "AccessFailedCount", "Avatar", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 4, 0, null, "cd445aae-5106-4050-8d75-8ae4714545c6", "User4@outlook.com", true, "Роман", "Добровольський", true, null, "USER4@OUTLOOK.COM", null, "AQAAAAEAACcQAAAAEMxAzq2N8QwWV3GXgy1AsF48zrWQjOXnI5nZp+e9DRjOW0febz3x5AMPqMAcQb2tWQ==", null, false, "7fe3632d-a15c-4577-8f94-be63bdb2e4e1", false, "User4@outlook.com" },
                    { 5, 0, null, "4d333fe1-9092-427c-9c60-6a27bfe6c670", "User5@outlook.com", true, "Степан", "Петришко", true, null, "USER5@OUTLOOK.COM", null, "AQAAAAEAACcQAAAAEPuR+4XVdyzvbMRgyi6ygeSFlCubWpRFb5QLxIStOe3T2RcJHxDaoLTP6Npxjv7Xdw==", null, false, "3ffb8eee-26f9-4814-a5b5-1412e373cf77", false, "User5@outlook.com" },
                    { 6, 0, null, "7c7cb3a8-e591-4164-84b4-f48f976f1c86", "User6@gmail.com", true, "Світлана", "Петришко", true, null, "USER6@GMAIL.COM", null, "AQAAAAEAACcQAAAAEJVW2HPmtMWeY0AAH/r5qUBhhO1zipJi02M9wPeVdhLzZpQO4lcmHk+9UALuRHCKQA==", null, false, "65c80871-b4d2-4691-afdd-2c5af3562a78", false, "User6@gmail.com" },
                    { 7, 0, null, "63ee0d3b-e81e-45ce-a158-3121abef03f3", "User7@yahoo.com", true, "Богдан", "Ящук", true, null, "USER7@YAHOO.COM", null, "AQAAAAEAACcQAAAAECnZMRzSlPd2hVPN7eNm5terRiCJQVuULSTWRfkJr7Bo0sTgLiv5x6OwQ1rQfg9Xbw==", null, false, "e260ecbb-8baa-4b66-a13b-28b729c425f2", false, "User7@yahoo.com" },
                    { 8, 0, null, "7c55dba4-a4d4-48d0-b087-cca59df57ce8", "User8@outlook.com", true, "Валентина", "Генко", true, null, "USER8@OUTLOOK.COM", null, "AQAAAAEAACcQAAAAEAqSqb3bJvByacodj6EujLXhDuydpMX5PKbXyLbIawgnVIFheZnsNN+U9g3MA+uIsA==", null, false, "e2765c9d-7167-4da1-bb09-ead05fa828cf", false, "User8@outlook.com" },
                    { 9, 0, null, "cf8859f2-62f2-4eeb-9de6-987cec08aad7", "User9@gmail.com", true, "Андрій", "Івашко", true, null, "USER9@GMAIL.COM", null, "AQAAAAEAACcQAAAAEHKNmU9qn6MZ1iSqVZ/YZcWpXU0G4i6CKUNqVWSf0cBehVf23Nde4Z+te26NUieflA==", null, false, "f8d3e583-740c-475c-a8d7-78e52c301fe6", false, "User9@gmail.com" },
                    { 10, 0, null, "240c084d-a308-46f2-8176-dac439129421", "User10@gmail.com", true, "Олександр", "Ванченко", true, null, "USER10@GMAIL.COM", null, "AQAAAAEAACcQAAAAENO7qcEruKtDg2RGjG+NCuEWwqU/TsE7+BYPUfrKpFxkYYTiIalq9ytrpXG68XGeWw==", null, false, "b3795669-849c-4c86-9b52-043edeff91f5", false, "User10@gmail.com" },
                    { 11, 0, null, "9e992370-2a71-47ec-952e-c5bd4a6a4919", "User11@ukr.net", true, "Володимир", "Михайлішин", true, null, "USER11@UKR.NET", null, "AQAAAAEAACcQAAAAEGVFi6LLPOap/+30DYosyXy41XmqszNVhLmjirtVqzYY5270vX81fUxaA7y/R8ygcg==", null, false, "ec6de2af-33ff-4eb5-b36a-ef662c41bffb", false, "User11@ukr.net" },
                    { 12, 0, null, "795e5409-6ecd-4e42-a7b1-470d2a7c1940", "User12@outlook.com", true, "Станіслав", "Жолудь", true, null, "USER12@OUTLOOK.COM", null, "AQAAAAEAACcQAAAAEFHFyGb1VkuJgKLnd2LpJQC3ykf29qaAbC6VBFUae8XzWqpfk8x9WoINag/VjT1Ehg==", null, false, "5b0c15af-fc6f-4acb-a872-a901b5b0d44d", false, "User12@outlook.com" },
                    { 13, 0, null, "4ba13ab4-5d91-4350-aae6-8e20accb8a44", "User13@gmail.com", true, "Микола", "Лисенко", true, null, "USER13@GMAIL.COM", null, "AQAAAAEAACcQAAAAEBd7Z0AiHbWZDT7Z8Q3LRYi2XCpA4EZNDJ3bgq2RJmq73PshZCVHD78S6ZUT/+pULQ==", null, false, "184a8056-328c-4ebc-a9de-2c004c2d5ed3", false, "User13@gmail.com" },
                    { 14, 0, null, "000a9674-cf71-44bc-8511-77766ded2c52", "User14@outlook.com", true, "Дмитро", "Жовнірчук", true, null, "USER14@OUTLOOK.COM", null, "AQAAAAEAACcQAAAAEDLwkBxBAD310TagRhJe6auE2vqg1QDvKNZVvSY1w9moqnQ0wBEUiHWDlUwrbAyAmA==", null, false, "a29732ea-e98b-45e5-8a45-14bd66b81ed9", false, "User14@outlook.com" },
                    { 15, 0, null, "a6ed564d-67b2-4650-8b65-5a1fc9f7fcce", "User15@ukr.net", true, "Валентин", "Федоренко", true, null, "USER15@UKR.NET", null, "AQAAAAEAACcQAAAAEIxPeStJoF00s57dsG81EdkQHT+WX/m788nhE1X2TDF7+E8ZC/y5WaMeuWMVsldVqQ==", null, false, "e8cac407-1a68-48fb-9abd-78792dd432c3", false, "User15@ukr.net" },
                    { 16, 0, null, "e3fdaf71-1b88-487b-b69d-ab6b4a48c798", "User16@gmail.com", true, "Віталій", "Свистун", true, null, "USER16@GMAIL.COM", null, "AQAAAAEAACcQAAAAEF+DK8C4+dDzEUAuFiQgaHzheQ+j/PatyYCTOa0Jkw6zJEmdS5CwiWxPi9g9n2qR/g==", null, false, "5f83b2c3-a121-46a9-9e08-87587aa0f2fc", false, "User16@gmail.com" },
                    { 17, 0, null, "0ab6df61-bb21-4ba9-a78b-3ddf0dc2459e", "User17@gmail.com", true, "Інокентій", "Фірташ", true, null, "USER17@GMAIL.COM", null, "AQAAAAEAACcQAAAAEI17+flfuSM3gtMJBLqVm8NbGJjnZvC/vZL0qMqdqo8rfjOgVVRhAI1Aj5525xVICw==", null, false, "ec90fada-6d5c-4762-b190-d2a40734fcbc", false, "User17@gmail.com" },
                    { 18, 0, null, "88e2238f-9eba-4b84-86b4-199fe8d0c179", "User18@gmail.com", true, "Ярослав", "Татарчук", true, null, "USER18@GMAIL.COM", null, "AQAAAAEAACcQAAAAEAqIgapskl9ypTCNLXY1PF8R+RKa4Nyel3WK5Qgtv2WV0CuBLPXPfXRbufsmifjSDQ==", null, false, "760141d1-e949-49be-8372-d0d7828a66ef", false, "User18@gmail.com" },
                    { 19, 0, null, "f6e0a420-f30d-42cd-a03a-8f99514763eb", "User19@ukr.net", true, "Йосиф", "Дмитренко", true, null, "USER19@UKR.NET", null, "AQAAAAEAACcQAAAAEEnhLkT3zSA6NXNCxxYCR7rkBa5Ryxr1NF1pyDNlgNVgTuyXBBCZDROMwd/Z/58Y1w==", null, false, "fc93cf72-c960-4aa6-9ff1-2b2eed41a635", false, "User19@ukr.net" },
                    { 20, 0, null, "b44f2723-201b-442b-a351-2176486a7ac8", "User20@ukr.net", true, "Констянтин", "Шарапенко", true, null, "USER20@UKR.NET", null, "AQAAAAEAACcQAAAAEApew7DDbSHRO7rNqzElhbKTracyhjvANLUUyu6QK5Z6AzZr7At1XaN6URBI/U705Q==", null, false, "e0aae1c3-ebc1-44ac-b43a-4c8cd7649502", false, "User20@ukr.net" },
                    { 21, 0, null, "8ab5ebd7-9ae7-43f7-ac0a-3a76388fff75", "User21@outlook.com", true, "Олег", "Притула", true, null, "USER21@OUTLOOK.COM", null, "AQAAAAEAACcQAAAAEJ601x1DlkjQ/E0ODspcxMCX6RDbV2bIOSSCRMGdssRY/91R3sJSmRcFvHVgeyqjWQ==", null, false, "72491de4-03f1-4902-8b8a-c19ec1b69f44", false, "User21@outlook.com" },
                    { 22, 0, null, "d250c0bf-a685-4052-b6ee-2692b6423ee9", "User22@gmail.com", true, "Анатолій", "Назаренко", true, null, "USER22@GMAIL.COM", null, "AQAAAAEAACcQAAAAEMeuo2wqwJUWEkMiHJueLbGnmbFERq4PR13W9BFVOCAZRgHNbyOXVlrhqoSlPOdgXA==", null, false, "3dd7b7c2-57e5-4469-a17b-3afa705d468b", false, "User22@gmail.com" },
                    { 23, 0, null, "039a07bc-1315-43ce-835c-1466371859b6", "User23@ukr.net", true, "Микола", "Вакуленко", true, null, "USER23@UKR.NET", null, "AQAAAAEAACcQAAAAEE5yp/iIVw+fQmu4Zy3qEKJBPGeEv+LoZKg0w78gU+mGd2jmfokn0fS+efpx5w5Cmw==", null, false, "76441594-db81-420f-9441-f013dca8c10f", false, "User23@ukr.net" },
                    { 24, 0, null, "f582ac18-a619-41d0-86cf-4a9345aef27f", "User24@outlook.com", true, "Степан", "Барабаш", true, null, "USER24@OUTLOOK.COM", null, "AQAAAAEAACcQAAAAEH3U997CnpqhD6Nuoot9uT2bF8OqzJ3dWSNSvUbDNgjYbMNHx72LWxPZ69ws0uLWWQ==", null, false, "d2511534-fb4d-4708-b4c1-ee8fff5569ab", false, "User24@outlook.com" },
                    { 25, 0, null, "325bad3a-d658-413d-b1ef-a1ac797620aa", "User25@gmail.com", true, "Денис", "Ярема", true, null, "USER25@GMAIL.COM", null, "AQAAAAEAACcQAAAAEO15keNZzI7fZm/WTl4VHfPyfhIKbUX/W+1JNkQCGwmUedDJxoZTGNj3W6TPJWOMtw==", null, false, "e1c35900-ef03-4d9f-a3e6-73e9ae44b725", false, "User25@gmail.com" },
                    { 26, 0, null, "46f5db17-d558-4e50-aacc-1a15ca7646cc", "User26@ukr.net", true, "Олег", "Таралевич", true, null, "USER26@UKR.NET", null, "AQAAAAEAACcQAAAAEANp/hqiuWByIbUo1yr37lIBQy3N6C/a9P5rtxTb3STWpt5FntFhiTO6Gta/FE49cQ==", null, false, "649a5586-d0ca-4098-ab77-c91c25e5aff5", false, "User26@ukr.net" },
                    { 27, 0, null, "d5039ea4-58cb-439a-a484-754be97c6b50", "User27@gmail.com", true, "Сергій", "Іващук", true, null, "USER27@GMAIL.COM", null, "AQAAAAEAACcQAAAAEJdXZbYEB8hb0C/U7QHz9C4q2CwSMWfTNpAPxA4Pp2h39SZwsibx2JKajkE3/1aInQ==", null, false, "e41fd558-b0f3-41ae-b51d-7170362b24f7", false, "User27@gmail.com" },
                    { 28, 0, null, "9f68e5fd-0a3e-46e0-8ebb-7d3da0cd3345", "User28@yahoo.com", true, "Михайло", "Компанієць", true, null, "USER28@YAHOO.COM", null, "AQAAAAEAACcQAAAAEKw2/ukfAQedyJGPPivTc8Nr+u+wkRAYAxmEuWiAk8yNWXnY+eWRtsYcyWszw40QtQ==", null, false, "68bb914f-9148-47ec-9ef8-034e28360804", false, "User28@yahoo.com" },
                    { 29, 0, null, "803c9132-be08-4a1e-811f-f3be6de7f226", "User29@outlook.com", true, "Андрій", "Іващук", true, null, "USER29@OUTLOOK.COM", null, "AQAAAAEAACcQAAAAEPAH29tzslexE+NFRoVE3zrCz5vM2cNkGUlrkj8hubwWvNewNim3GkD9ko8Lg0jy9g==", null, false, "cdf592ca-7ba9-41b3-9a5c-082f8c6242d5", false, "User29@outlook.com" },
                    { 30, 0, null, "302807c0-de0b-4b73-9a0c-e2679bf010c1", "User30@gmail.com", true, "Назар", "Мельник", true, null, "USER30@GMAIL.COM", null, "AQAAAAEAACcQAAAAEFxfmRJhDl3Z/3VsK+n3/2UfBfCXWP3sn9bN7HGQT8wBh4ry+3U69fICc/MsYwYtWQ==", null, false, "04dbac00-ad68-4ea3-b365-204af4331059", false, "User30@gmail.com" }
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
                table: "ServiceDiscount",
                columns: new[] { "Id", "BranchId", "DiscountSize", "ServiceId" },
                values: new object[,]
                {
                    { 1, 2, 20, 5 },
                    { 2, 1, 15, 3 }
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
                name: "IX_ServiceDiscount_BranchId",
                table: "ServiceDiscount",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDiscount_ServiceId",
                table: "ServiceDiscount",
                column: "ServiceId");

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
                name: "ServiceDiscount");

            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "DayOff");

            migrationBuilder.DropTable(
                name: "Service_");

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
