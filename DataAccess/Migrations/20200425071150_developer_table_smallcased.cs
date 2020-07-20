using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagementCore.Migrations
{
    public partial class developer_table_smallcased : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Developer",
                table: "Developer");

            migrationBuilder.RenameTable(
                name: "Developer",
                newName: "developer");

            migrationBuilder.RenameColumn(
                name: "Skills",
                table: "developer",
                newName: "skills");

            migrationBuilder.RenameColumn(
                name: "Products",
                table: "developer",
                newName: "products");

            migrationBuilder.RenameColumn(
                name: "Platforms",
                table: "developer",
                newName: "platforms");

            migrationBuilder.RenameColumn(
                name: "Components",
                table: "developer",
                newName: "components");

            migrationBuilder.AddColumn<string>(
                name: "previousworks",
                table: "developer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_developer",
                table: "developer",
                column: "developer_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_developer",
                table: "developer");

            migrationBuilder.DropColumn(
                name: "previousworks",
                table: "developer");

            migrationBuilder.RenameTable(
                name: "developer",
                newName: "Developer");

            migrationBuilder.RenameColumn(
                name: "skills",
                table: "Developer",
                newName: "Skills");

            migrationBuilder.RenameColumn(
                name: "products",
                table: "Developer",
                newName: "Products");

            migrationBuilder.RenameColumn(
                name: "platforms",
                table: "Developer",
                newName: "Platforms");

            migrationBuilder.RenameColumn(
                name: "components",
                table: "Developer",
                newName: "Components");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Developer",
                table: "Developer",
                column: "developer_id");
        }
    }
}
