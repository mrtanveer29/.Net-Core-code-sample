using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagementCore.Migrations
{
    public partial class bugs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Bugs",
                table: "Bugs");

            migrationBuilder.RenameTable(
                name: "Bugs",
                newName: "bugs");

            migrationBuilder.RenameColumn(
                name: "Summery",
                table: "bugs",
                newName: "summery");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "bugs",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Saverity",
                table: "bugs",
                newName: "saverity");

            migrationBuilder.RenameColumn(
                name: "Product",
                table: "bugs",
                newName: "product");

            migrationBuilder.RenameColumn(
                name: "Priority",
                table: "bugs",
                newName: "priority");

            migrationBuilder.RenameColumn(
                name: "KeyWords",
                table: "bugs",
                newName: "keywords");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "bugs",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Component",
                table: "bugs",
                newName: "component");

            migrationBuilder.RenameColumn(
                name: "BugTitle",
                table: "bugs",
                newName: "bugtitle");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bugs",
                table: "bugs",
                column: "BugId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_bugs",
                table: "bugs");

            migrationBuilder.RenameTable(
                name: "bugs",
                newName: "Bugs");

            migrationBuilder.RenameColumn(
                name: "summery",
                table: "Bugs",
                newName: "Summery");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Bugs",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "saverity",
                table: "Bugs",
                newName: "Saverity");

            migrationBuilder.RenameColumn(
                name: "product",
                table: "Bugs",
                newName: "Product");

            migrationBuilder.RenameColumn(
                name: "priority",
                table: "Bugs",
                newName: "Priority");

            migrationBuilder.RenameColumn(
                name: "keyWords",
                table: "Bugs",
                newName: "KeyWords");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Bugs",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "component",
                table: "Bugs",
                newName: "Component");

            migrationBuilder.RenameColumn(
                name: "bugtitle",
                table: "Bugs",
                newName: "BugTitle");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bugs",
                table: "Bugs",
                column: "BugId");
        }
    }
}
