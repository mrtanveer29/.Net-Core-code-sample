using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagementCore.Migrations
{
    public partial class extra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "current_workload",
                table: "developer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "current_workload",
                table: "developer");
        }
    }
}
