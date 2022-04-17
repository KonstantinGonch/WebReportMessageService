using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReportMessageService
{
    public class ScanJobResult
    {
        public long Id { get; set; }
        public int TotalResources { get; set; }
        public int SuccessScanned { get; set; }
        public DateTime ScanDate { get; set; }
        public DateTime PlanNextScan { get; set; }
        public long ThreatId { get; set; }
    }
}
