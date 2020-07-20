using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagementCore.Migrations
{
    public partial class bug_table_change_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Changed",
                table: "bugs",
                newName: "changed");

            migrationBuilder.RenameColumn(
                name: "summery",
                table: "bugs",
                newName: "summary");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "changed",
                table: "bugs",
                newName: "Changed");

            migrationBuilder.RenameColumn(
                name: "summary",
                table: "bugs",
                newName: "summery");
        }
    }
}
