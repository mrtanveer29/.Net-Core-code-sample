using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagementCore.Migrations
{
    public partial class is_indexed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_indexed",
                table: "developer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_indexed",
                table: "developer");
        }
    }
}
