using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReportMessageService
{
    public class ScanJobSettings
    {
        public long Id { get; set; }
        public int JobRestartMinutes { get; set; }
        public int PingRetries { get; set; }
        public int PingFailureThreat { get; set; }
    }
}
