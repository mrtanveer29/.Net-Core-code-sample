using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EmployeeManagementCore.Migrations
{
    public partial class profile_matching_config : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "profile_match_config",
                columns: table => new
                {
                    profile_match_config_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    severity = table.Column<string>(maxLength: 50, nullable: true),
                    no_of_sed = table.Column<int>(nullable: false),
                    no_of_ned = table.Column<int>(nullable: false),
                    no_of_fd = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profile_match_config", x => x.profile_match_config_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "profile_match_config");
        }
    }
}
