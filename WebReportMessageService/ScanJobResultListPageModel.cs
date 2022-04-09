using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReportMessageService
{
    public class ScanJobResultListPageModel : BasePageModel
    {
        public IEnumerable<ScanJobResult> ScanJobResults { get; set; }
    }
}
