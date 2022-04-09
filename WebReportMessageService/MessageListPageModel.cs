using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReportMessageService
{
    public class MessageListPageModel : BasePageModel
    {
        public IEnumerable<Message> Messages { get; set; }
    }
}
