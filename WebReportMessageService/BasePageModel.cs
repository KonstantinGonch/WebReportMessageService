using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReportMessageService
{
    public abstract class BasePageModel
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
    }
}
