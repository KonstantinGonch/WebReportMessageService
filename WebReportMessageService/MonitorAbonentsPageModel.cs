using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReportMessageService
{
    public class MonitorAbonentsPageModel : BasePageModel
    {
        public IEnumerable<MonitorAbonentExtendedInfo> MonitorAbonents { get; set; }
    }
}
