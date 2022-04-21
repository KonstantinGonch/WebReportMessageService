using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReportMessageService.Controllers
{
    [ApiController]
    [Route("api/message")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;
        private const int _pageSize = 8;

        public MessageController(ILogger<MessageController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("list")]
        public  MessageListPageModel Get(int pageNumber)
        {
            using (var dbContext = new AppDataContext())
            {
                var pageMessages = dbContext.Messages.OrderByDescending(m => m.MessageDate).Skip((pageNumber-1) * _pageSize).Take(_pageSize).ToList();
                var totalPages = (int)Math.Ceiling(dbContext.Messages.Count() / (double)_pageSize);
                return new MessageListPageModel { Messages = pageMessages, TotalPages = totalPages, PageNumber = pageNumber };
            }
        }

        [HttpPost]
        [Route("save")]
        public async void Save(Message message)
        {
            using (var dbContext = new AppDataContext())
            {
                message.MessageDate = DateTime.Now;
                dbContext.Add(message);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
