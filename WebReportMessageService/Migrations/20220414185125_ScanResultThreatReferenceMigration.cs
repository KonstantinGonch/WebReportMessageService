using Microsoft.EntityFrameworkCore.Migrations;

namespace WebReportMessageService.Migrations
{
    public partial class ScanResultThreatReferenceMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ThreatId",
                table: "ScanJobResults",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ThreatId",
                table: "ScanJobResults");
        }
    }
}
