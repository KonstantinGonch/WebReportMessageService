using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebReportMessageService.Migrations
{
    public partial class ThreatAndScanMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ScanJobResults",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TotalResources = table.Column<int>(type: "INTEGER", nullable: false),
                    SuccessScanned = table.Column<int>(type: "INTEGER", nullable: false),
                    ScanDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PlanNextScan = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScanJobResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScanJobSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JobRestartMinutes = table.Column<int>(type: "INTEGER", nullable: false),
                    PingRetries = table.Column<int>(type: "INTEGER", nullable: false),
                    PingFailureThreat = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScanJobSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Threats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DateAppeared = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ThreatMessage = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Threats", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScanJobResults");

            migrationBuilder.DropTable(
                name: "ScanJobSettings");

            migrationBuilder.DropTable(
                name: "Threats");
        }
    }
}
