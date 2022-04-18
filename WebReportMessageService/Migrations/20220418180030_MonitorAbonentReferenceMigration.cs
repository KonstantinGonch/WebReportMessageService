using Microsoft.EntityFrameworkCore.Migrations;

namespace WebReportMessageService.Migrations
{
    public partial class MonitorAbonentReferenceMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MonitorAbonentId",
                table: "MonitorMeasurements",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonitorAbonentId",
                table: "MonitorMeasurements");
        }
    }
}
