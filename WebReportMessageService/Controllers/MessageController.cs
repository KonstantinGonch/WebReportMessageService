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

        public MessageController(ILogger<MessageController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("list")]
        public  IEnumerable<Message> Get()
        {
            using (var dbContext = new AppDataContext())
            {
                return dbContext.Messages.ToList();
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
