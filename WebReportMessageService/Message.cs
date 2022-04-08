using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReportMessageService
{
    public class Message
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public MessageType MessageType { get; set; }
        public DateTime MessageDate { get; set; }
    }
}
