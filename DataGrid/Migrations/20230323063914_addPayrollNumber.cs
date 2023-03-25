using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataGrid.Migrations
{
    /// <inheritdoc />
    public partial class addPayrollNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "payroll_number",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "payroll_number",
                table: "Employees");
        }
    }
}
