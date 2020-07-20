using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EmployeeManagementCore.Migrations
{
    public partial class developer_mapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComponentDeveloperMappings",
                columns: table => new
                {
                    component_developer_mapping_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    developer_id = table.Column<int>(nullable: false),
                    component_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentDeveloperMappings", x => x.component_developer_mapping_id);
                });

            migrationBuilder.CreateTable(
                name: "HardwareDeveloperMappings",
                columns: table => new
                {
                    hardware_developer_mapping_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    developer_id = table.Column<int>(nullable: false),
                    hardware_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardwareDeveloperMappings", x => x.hardware_developer_mapping_id);
                });

            migrationBuilder.CreateTable(
                name: "ProductDeveloperMappings",
                columns: table => new
                {
                    product_developer_mapping_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    developer_id = table.Column<int>(nullable: false),
                    product_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDeveloperMappings", x => x.product_developer_mapping_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentDeveloperMappings");

            migrationBuilder.DropTable(
                name: "HardwareDeveloperMappings");

            migrationBuilder.DropTable(
                name: "ProductDeveloperMappings");
        }
    }
}
