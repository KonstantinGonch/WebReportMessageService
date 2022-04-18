using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReportMessageService
{
    public class AppDataContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<NetworkResource> NetworkResources { get; set; }
        public DbSet<Threat> Threats { get; set; }
        public DbSet<ScanJobResult> ScanJobResults { get; set; }
        public DbSet<ScanJobSettings> ScanJobSettings { get; set; }
        public DbSet<MonitorAbonent> MonitorAbonents { get; set; }
        public DbSet<MonitorMeasurement> MonitorMeasurements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=82.146.56.114;user=external;password=superFinashka;database=Ural;", new MySqlServerVersion(new Version(8, 0, 11)));
        }
    }
}
