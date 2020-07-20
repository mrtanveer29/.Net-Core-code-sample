using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EmployeeManagementCore.Migrations
{
    public partial class excelinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "bug_excel_info_id",
                table: "bugs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "bug_execl_info",
                columns: table => new
                {
                    bug_excel_info_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    bug_excel_info_file_name = table.Column<string>(nullable: true),
                    dumping_date = table.Column<string>(nullable: true),
                    excel_file_dump_status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bug_execl_info", x => x.bug_excel_info_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bug_execl_info");

            migrationBuilder.DropColumn(
                name: "bug_excel_info_id",
                table: "bugs");
        }
    }
}
