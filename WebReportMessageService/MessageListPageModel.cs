using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReportMessageService
{
    public class MessageListPageModel
    {
        public IEnumerable<Message> Messages { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
    }
}
