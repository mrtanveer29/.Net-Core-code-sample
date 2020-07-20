using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagementCore.Migrations
{
    public partial class bug_table_change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "saverity",
                table: "bugs",
                newName: "severity");

            migrationBuilder.AddColumn<int>(
                name: "assignee",
                table: "bugs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "createddate",
                table: "bugs",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "createdtime",
                table: "bugs",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "hardware",
                table: "bugs",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "resolution",
                table: "bugs",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "assignee",
                table: "bugs");

            migrationBuilder.DropColumn(
                name: "createddate",
                table: "bugs");

            migrationBuilder.DropColumn(
                name: "createdtime",
                table: "bugs");

            migrationBuilder.DropColumn(
                name: "hardware",
                table: "bugs");

            migrationBuilder.DropColumn(
                name: "resolution",
                table: "bugs");

            migrationBuilder.RenameColumn(
                name: "severity",
                table: "bugs",
                newName: "saverity");
        }
    }
}
