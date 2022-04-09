using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReportMessageService
{
    public class Threat
    {
        public long Id { get; set; }
        public DateTime DateAppeared { get; set; }
        public string ThreatMessage { get; set; }
    }
}
