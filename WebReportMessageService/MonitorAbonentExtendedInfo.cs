using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReportMessageService
{
    public class MonitorAbonentExtendedInfo : MonitorAbonent
    {
        public DateTime LastSync { get; set; }
        public int AttentionEvents { get; set; }
        public MonitorAbonentExtendedInfo(MonitorAbonent abonent)
        {
            Id = abonent.Id;
            HostName = abonent.HostName;
        }
    }
}
