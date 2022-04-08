using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReportMessageService
{
    public class NetworkResourceListPageModel
    {
        public IEnumerable<NetworkResource> NetworkResources { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
    }
}
