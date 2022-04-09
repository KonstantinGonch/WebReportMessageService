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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=AppDb.sqlite");
        }
    }
}
