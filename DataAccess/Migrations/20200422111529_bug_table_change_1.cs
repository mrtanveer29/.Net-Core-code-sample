using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagementCore.Migrations
{
    public partial class bug_table_change_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Changed",
                table: "bugs",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "bugs",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Changed",
                table: "bugs");

            migrationBuilder.DropColumn(
                name: "id",
                table: "bugs");
        }
    }
}
