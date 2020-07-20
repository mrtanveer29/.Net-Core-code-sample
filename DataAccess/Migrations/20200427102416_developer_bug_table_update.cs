using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EmployeeManagementCore.Migrations
{
    public partial class developer_bug_table_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "componets");

            migrationBuilder.AddColumn<int>(
                name: "solve_count",
                table: "developer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "source",
                table: "bugs",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "components",
                columns: table => new
                {
                    component_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    component_name = table.Column<string>(maxLength: 255, nullable: true),
                    developers_worked = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_components", x => x.component_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "components");

            migrationBuilder.DropColumn(
                name: "solve_count",
                table: "developer");

            migrationBuilder.DropColumn(
                name: "source",
                table: "bugs");

            migrationBuilder.CreateTable(
                name: "componets",
                columns: table => new
                {
                    component_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    component_name = table.Column<string>(maxLength: 255, nullable: true),
                    developers_worked = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_componets", x => x.component_id);
                });
        }
    }
}
