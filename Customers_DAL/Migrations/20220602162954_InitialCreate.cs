using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Customers_DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.CreateTable(
            //     name: "Branch",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         Descript = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
            //         Address = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Branch", x => x.Id);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "DayOff",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         Date_ = table.Column<DateTime>(type: "date", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_DayOff", x => x.Id);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "PossibleTime",
            //     columns: table => new
            //     {
            //         Time = table.Column<TimeSpan>(type: "time", nullable: false),
            //         Available = table.Column<bool>(type: "bit", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK__Possible__8E79CB0049844667", x => x.Time);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "Service_",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         Name_ = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
            //         Duration = table.Column<int>(type: "int", nullable: true),
            //         Price = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
            //         Available = table.Column<bool>(type: "bit", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Service_", x => x.Id);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "User_",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         FirstName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
            //         LastName = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
            //         Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_User_", x => x.Id);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "Customer",
            //     columns: table => new
            //     {
            //         UserId = table.Column<int>(type: "int", nullable: false),
            //         VisitsNum = table.Column<int>(type: "int", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Customer_User", x => x.UserId);
            //         table.ForeignKey(
            //             name: "FK_Customer_User",
            //             column: x => x.UserId,
            //             principalTable: "User_",
            //             principalColumn: "Id");
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "Employee",
            //     columns: table => new
            //     {
            //         UserId = table.Column<int>(type: "int", nullable: false),
            //         BranchId = table.Column<int>(type: "int", nullable: true),
            //         EmployeeStatusId = table.Column<int>(type: "int", nullable: false),
            //         Address = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
            //         PassportImgPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //         Birthday = table.Column<DateTime>(type: "date", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Employee_User", x => x.UserId);
            //         table.ForeignKey(
            //             name: "FK__Employee__Branch__29572725",
            //             column: x => x.BranchId,
            //             principalTable: "Branch",
            //             principalColumn: "Id");
            //         table.ForeignKey(
            //             name: "FK_Employee_User",
            //             column: x => x.UserId,
            //             principalTable: "User_",
            //             principalColumn: "Id");
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "Barber",
            //     columns: table => new
            //     {
            //         EmployeeUserId = table.Column<int>(type: "int", nullable: false),
            //         ChairNum = table.Column<int>(type: "int", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Barber_User", x => x.EmployeeUserId);
            //         table.ForeignKey(
            //             name: "FK_Barber_User",
            //             column: x => x.EmployeeUserId,
            //             principalTable: "Employee",
            //             principalColumn: "UserId");
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "EmployeeDayOff",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         EmployeeUserId = table.Column<int>(type: "int", nullable: true),
            //         DayOffId = table.Column<int>(type: "int", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_EmployeeDayOff", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK__EmployeeD__DayOf__2F10007B",
            //             column: x => x.DayOffId,
            //             principalTable: "DayOff",
            //             principalColumn: "Id");
            //         table.ForeignKey(
            //             name: "FK__EmployeeD__Emplo__2E1BDC42",
            //             column: x => x.EmployeeUserId,
            //             principalTable: "Employee",
            //             principalColumn: "UserId");
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "Appointment",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         BarberUserId = table.Column<int>(type: "int", nullable: false),
            //         CustomerUserId = table.Column<int>(type: "int", nullable: false),
            //         AppointmentStatusId = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
            //         AppDate = table.Column<DateTime>(type: "date", nullable: false),
            //         BeginTime = table.Column<TimeSpan>(type: "time", nullable: false),
            //         EndTime = table.Column<TimeSpan>(type: "time", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Appointment", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK__Appointme__Barbe__3D5E1FD2",
            //             column: x => x.BarberUserId,
            //             principalTable: "Barber",
            //             principalColumn: "EmployeeUserId",
            //             onDelete: ReferentialAction.Cascade);
            //         table.ForeignKey(
            //             name: "FK__Appointme__Custo__3E52440B",
            //             column: x => x.CustomerUserId,
            //             principalTable: "Customer",
            //             principalColumn: "UserId",
            //             onDelete: ReferentialAction.Cascade);
            //     });
            //
            // migrationBuilder.CreateTable(
            //     name: "AppointmentService",
            //     columns: table => new
            //     {
            //         Id = table.Column<int>(type: "int", nullable: false)
            //             .Annotation("SqlServer:Identity", "1, 1"),
            //         AppointmentId = table.Column<int>(type: "int", nullable: true),
            //         ServiceId = table.Column<int>(type: "int", nullable: true)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AppointmentService", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK__Appointme__Appoi__412EB0B6",
            //             column: x => x.AppointmentId,
            //             principalTable: "Appointment",
            //             principalColumn: "Id");
            //         table.ForeignKey(
            //             name: "FK__Appointme__Servi__4222D4EF",
            //             column: x => x.ServiceId,
            //             principalTable: "Service_",
            //             principalColumn: "Id");
            //     });
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_Appointment_BarberUserId",
            //     table: "Appointment",
            //     column: "BarberUserId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_Appointment_CustomerUserId",
            //     table: "Appointment",
            //     column: "CustomerUserId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_AppointmentService_AppointmentId",
            //     table: "AppointmentService",
            //     column: "AppointmentId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_AppointmentService_ServiceId",
            //     table: "AppointmentService",
            //     column: "ServiceId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_Employee_BranchId",
            //     table: "Employee",
            //     column: "BranchId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_EmployeeDayOff_DayOffId",
            //     table: "EmployeeDayOff",
            //     column: "DayOffId");
            //
            // migrationBuilder.CreateIndex(
            //     name: "IX_EmployeeDayOff_EmployeeUserId",
            //     table: "EmployeeDayOff",
            //     column: "EmployeeUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentService");

            migrationBuilder.DropTable(
                name: "EmployeeDayOff");

            migrationBuilder.DropTable(
                name: "PossibleTime");

            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "Service_");

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
