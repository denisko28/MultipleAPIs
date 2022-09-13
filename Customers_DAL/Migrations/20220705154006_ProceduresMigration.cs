using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Customers_DAL.Migrations
{
    public partial class ProceduresMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var getAllProc = @"CREATE PROC GetAllProc
			                    @Table_Name sysname
		                    AS
		                    BEGIN
			                    DECLARE @DynamicSQL nvarchar(50);
			                    SET @DynamicSQL = 'SELECT * FROM ' + @Table_Name;
			                    EXEC (@DynamicSQL);
		                    END";
            
            var getByIdProc = @"CREATE PROC GetByIdProc
								@Table_Name sysname, @Id INT 
							AS
							BEGIN
								DECLARE @DynamicSQL nvarchar(70);
								SET @DynamicSQL = 'SELECT * FROM ' + @Table_Name + ' WHERE Id=' + CONVERT(nvarchar(10), @Id);
								EXEC (@DynamicSQL);
							END";

            var insertProc = @"CREATE PROC InsertProc
								@Table_Name sysname, @Params nvarchar(80), @Values nvarchar(120)
							AS
							BEGIN
								DECLARE @DynamicSQL nvarchar(250);
								SET @DynamicSQL = 'INSERT INTO ' + @Table_Name + ' ('+ @Params +') VALUES('+ @Values +'); SELECT SCOPE_IDENTITY();';
								EXEC (@DynamicSQL);
							END";

            var deleteByIdProc = @"CREATE PROC DeleteByIdProc
								@Table_Name sysname, @Id INT 
							AS
							BEGIN
								DECLARE @DynamicSQL nvarchar(70);
								SET @DynamicSQL = 'DELETE FROM ' + @Table_Name + ' WHERE Id=' + CONVERT(VARCHAR(10), @Id);
								EXEC (@DynamicSQL);
							END";

            var addEmployeeProc = @"CREATE PROC AddEmployeeProc
								@UserId int,
								@BranchId int,
								@EmloyeeStatusId int,
								@Address nvarchar(80),
								@PassportCopy nchar(40),
								@Birthday date
							AS
							BEGIN
								INSERT INTO Employee VALUES (@UserId, @BranchId, @EmloyeeStatusId, @Address, @PassportCopy, @Birthday)
							END";

            migrationBuilder.Sql(getAllProc);
            migrationBuilder.Sql(getByIdProc);
            migrationBuilder.Sql(insertProc);
            migrationBuilder.Sql(deleteByIdProc);
            migrationBuilder.Sql(addEmployeeProc);
        }
    }
}
