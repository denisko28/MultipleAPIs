using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Customers_DAL.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Descript = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.Id);
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
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
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
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    AppDate = table.Column<DateTime>(type: "date", nullable: false),
                    BeginTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Appointme__Custo__3E52440B",
                        column: x => x.CustomerUserId,
                        principalTable: "Customer",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__Branch",
                        column: x => x.BranchId,
                        principalTable: "Branch",
                        principalColumn: "Id",
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
                    { 27, 0 },
                    { 28, 0 },
                    { 29, 0 },
                    { 30, 0 }
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
                    { new TimeSpan(0, 13, 45, 0, 0), true }
                });

            migrationBuilder.InsertData(
                table: "PossibleTime",
                columns: new[] { "Time", "Available" },
                values: new object[,]
                {
                    { new TimeSpan(0, 14, 0, 0, 0), true },
                    { new TimeSpan(0, 14, 15, 0, 0), true },
                    { new TimeSpan(0, 14, 30, 0, 0), true },
                    { new TimeSpan(0, 14, 45, 0, 0), true },
                    { new TimeSpan(0, 15, 0, 0, 0), true },
                    { new TimeSpan(0, 15, 15, 0, 0), true },
                    { new TimeSpan(0, 15, 30, 0, 0), true },
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
                table: "Service",
                columns: new[] { "Id", "Available", "Duration", "Name", "Price" },
                values: new object[,]
                {
                    { 1, true, 60, "Стрижка", 300m },
                    { 2, true, 90, "Стрижка з бородою", 450m },
                    { 3, true, 30, "Голова - камуфляж сивини", 200m },
                    { 4, true, 30, "Борода - камуфляж сивини", 150m },
                    { 5, true, 45, "Дитяча стрижка", 200m }
                });

            migrationBuilder.InsertData(
                table: "Service",
                columns: new[] { "Id", "Available", "Duration", "Name", "Price" },
                values: new object[,]
                {
                    { 6, true, 15, "Укладка", 100m },
                    { 7, true, 15, "Королівське гоління", 250m },
                    { 8, true, 15, "Видалення волосся воском", 100m },
                    { 9, false, 75, "Чистка лиця", 400m }
                });

            migrationBuilder.InsertData(
                table: "Appointment",
                columns: new[] { "Id", "AppDate", "AppointmentStatusId", "BarberUserId", "BeginTime", "BranchId", "CustomerUserId", "EndTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 2, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4, new TimeSpan(0, 12, 0, 0, 0), 1, 19, new TimeSpan(0, 13, 0, 0, 0) },
                    { 2, new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 9, new TimeSpan(0, 16, 0, 0, 0), 2, 16, new TimeSpan(0, 17, 30, 0, 0) },
                    { 3, new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3, new TimeSpan(0, 17, 0, 0, 0), 1, 18, new TimeSpan(0, 18, 30, 0, 0) },
                    { 4, new DateTime(2022, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 6, new TimeSpan(0, 13, 30, 0, 0), 1, 22, new TimeSpan(0, 15, 15, 0, 0) },
                    { 5, new DateTime(2022, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 8, new TimeSpan(0, 16, 15, 0, 0), 2, 17, new TimeSpan(0, 17, 15, 0, 0) },
                    { 6, new DateTime(2022, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5, new TimeSpan(0, 10, 0, 0, 0), 1, 21, new TimeSpan(0, 10, 15, 0, 0) },
                    { 7, new DateTime(2022, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 6, new TimeSpan(0, 15, 0, 0, 0), 1, 23, new TimeSpan(0, 15, 45, 0, 0) }
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
                name: "IX_Appointment_BranchId",
                table: "Appointment",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_CustomerUserId",
                table: "Appointment",
                column: "CustomerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentService_AppointmentId",
                table: "AppointmentService",
                column: "AppointmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentService");

            migrationBuilder.DropTable(
                name: "PossibleTime");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Branch");
        }
    }
}
