using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace EmployeeManagementCore.Migrations
{
    public partial class delete_bugs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Priorities");

            migrationBuilder.DropTable(
                name: "Severities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bugs",
                table: "bugs");

            migrationBuilder.DropColumn(
                name: "bugtitle",
                table: "bugs");

            migrationBuilder.DropColumn(
                name: "component",
                table: "bugs");

            migrationBuilder.DropColumn(
                name: "hardware",
                table: "bugs");

            migrationBuilder.DropColumn(
                name: "product",
                table: "bugs");

            migrationBuilder.RenameTable(
                name: "bugs",
                newName: "Bugs");

            migrationBuilder.RenameColumn(
                name: "summary",
                table: "Bugs",
                newName: "Summary");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Bugs",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "source",
                table: "Bugs",
                newName: "Source");

            migrationBuilder.RenameColumn(
                name: "severity",
                table: "Bugs",
                newName: "Severity");

            migrationBuilder.RenameColumn(
                name: "resolution",
                table: "Bugs",
                newName: "Resolution");

            migrationBuilder.RenameColumn(
                name: "priority",
                table: "Bugs",
                newName: "Priority");

            migrationBuilder.RenameColumn(
                name: "keywords",
                table: "Bugs",
                newName: "KeyWords");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Bugs",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "createdtime",
                table: "Bugs",
                newName: "CreatedTime");

            migrationBuilder.RenameColumn(
                name: "createddate",
                table: "Bugs",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "changed",
                table: "Bugs",
                newName: "Changed");

            migrationBuilder.RenameColumn(
                name: "assignee",
                table: "Bugs",
                newName: "Assignee");

            migrationBuilder.AlterColumn<string>(
                name: "Source",
                table: "Bugs",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Resolution",
                table: "Bugs",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bugs",
                table: "Bugs",
                column: "BugId");

            migrationBuilder.CreateTable(
                name: "hardwares",
                columns: table => new
                {
                    hardware_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    hardware_name = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hardwares", x => x.hardware_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hardwares");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bugs",
                table: "Bugs");

            migrationBuilder.RenameTable(
                name: "Bugs",
                newName: "bugs");

            migrationBuilder.RenameColumn(
                name: "Summary",
                table: "bugs",
                newName: "summary");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "bugs",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Source",
                table: "bugs",
                newName: "source");

            migrationBuilder.RenameColumn(
                name: "Severity",
                table: "bugs",
                newName: "severity");

            migrationBuilder.RenameColumn(
                name: "Resolution",
                table: "bugs",
                newName: "resolution");

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
                name: "CreatedTime",
                table: "bugs",
                newName: "createdtime");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "bugs",
                newName: "createddate");

            migrationBuilder.RenameColumn(
                name: "Changed",
                table: "bugs",
                newName: "changed");

            migrationBuilder.RenameColumn(
                name: "Assignee",
                table: "bugs",
                newName: "assignee");

            migrationBuilder.AlterColumn<string>(
                name: "source",
                table: "bugs",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "resolution",
                table: "bugs",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bugtitle",
                table: "bugs",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "component",
                table: "bugs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "hardware",
                table: "bugs",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "product",
                table: "bugs",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_bugs",
                table: "bugs",
                column: "BugId");

            migrationBuilder.CreateTable(
                name: "Priorities",
                columns: table => new
                {
                    PriorityId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PriorityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Priorities", x => x.PriorityId);
                });

            migrationBuilder.CreateTable(
                name: "Severities",
                columns: table => new
                {
                    SeverityId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    SeverityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Severities", x => x.SeverityId);
                });
        }
    }
}
