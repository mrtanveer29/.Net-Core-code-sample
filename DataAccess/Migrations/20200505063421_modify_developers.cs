using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagementCore.Migrations
{
    public partial class modify_developers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "components",
                table: "developer");

            migrationBuilder.DropColumn(
                name: "platforms",
                table: "developer");

            migrationBuilder.DropColumn(
                name: "products",
                table: "developer");

            migrationBuilder.DropColumn(
                name: "solve_count",
                table: "developer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "components",
                table: "developer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "platforms",
                table: "developer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "products",
                table: "developer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "solve_count",
                table: "developer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
