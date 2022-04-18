using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReportMessageService
{
    public class MonitorMeasurement
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public long Uptime { get; set; }
        public int CpuUsage { get; set; }
        public int RamUsage { get; set; }
        public long ThreatId { get; set; }
        public long MonitorAbonentId { get; set; }
    }
}
