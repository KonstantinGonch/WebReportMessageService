using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReportMessageService
{
    public class IncidentReportPageModel : BasePageModel
    {
        public IEnumerable<IncidentReport> IncidentReports { get; set; }
    }
}
