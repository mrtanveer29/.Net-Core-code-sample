using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EmployeeManagementCore.Migrations
{
    public partial class Restructute_Bug_developer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginViewModel");

            migrationBuilder.DropColumn(
                name: "total_component",
                table: "Developer");

            migrationBuilder.DropColumn(
                name: "total_product",
                table: "Developer");

            migrationBuilder.DropColumn(
                name: "FixerTeamId",
                table: "Bugs");

            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "Bugs",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "Developers",
                table: "skills",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Components",
                table: "Developer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Platforms",
                table: "Developer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Products",
                table: "Developer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Component",
                table: "Bugs",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeyWords",
                table: "Bugs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Product",
                table: "Bugs",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "version",
                table: "Bugs",
                type: "decimal(6,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "AssignedDevelopers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DeveloperId = table.Column<int>(nullable: false),
                    BugId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignedDevelopers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssignedDevelopers");

            migrationBuilder.DropColumn(
                name: "Developers",
                table: "skills");

            migrationBuilder.DropColumn(
                name: "Components",
                table: "Developer");

            migrationBuilder.DropColumn(
                name: "Platforms",
                table: "Developer");

            migrationBuilder.DropColumn(
                name: "Products",
                table: "Developer");

            migrationBuilder.DropColumn(
                name: "KeyWords",
                table: "Bugs");

            migrationBuilder.DropColumn(
                name: "Product",
                table: "Bugs");

            migrationBuilder.DropColumn(
                name: "version",
                table: "Bugs");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Bugs",
                newName: "Subject");

            migrationBuilder.AddColumn<int>(
                name: "total_component",
                table: "Developer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "total_product",
                table: "Developer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Component",
                table: "Bugs",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FixerTeamId",
                table: "Bugs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "LoginViewModel",
                columns: table => new
                {
                    UserName = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    RememberMe = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginViewModel", x => x.UserName);
                });
        }
    }
}
