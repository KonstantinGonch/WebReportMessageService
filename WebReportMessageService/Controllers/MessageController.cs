using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebReportMessageService.Controllers
{
    [ApiController]
    [Route("api/message")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;
        private const int _pageSize = 8;
        private HttpClient httpClient = new HttpClient();

        public MessageController(ILogger<MessageController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("list")]
        public MessageListPageModel Get(int pageNumber)
        {
            using (var dbContext = new AppDataContext())
            {
                var pageMessages = dbContext.Messages.OrderByDescending(m => m.MessageDate).Skip((pageNumber - 1) * _pageSize).Take(_pageSize).ToList();
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

                if (message.MessageType == MessageType.Unformal)
                {
                    try
                    {
                        var measurementContent = new StringContent(JsonConvert.SerializeObject(message), Encoding.UTF8, "application/json");
                        var measurementResponse = await httpClient.PostAsync("http://localhost:5555/predict", measurementContent);
                        var response = await measurementResponse.Content.ReadAsStringAsync();
                        var o = JObject.Parse(response);
                        var result = (string)o["result"];
                        if (int.TryParse(result, out int pred))
                        {
                            if (pred == 1)
                                message.MessageType = MessageType.ErrorReport;
                            else
                                message.MessageType = MessageType.Feedback;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }

                dbContext.Add(message);
                await dbContext.SaveChangesAsync();
            }
        }

        [HttpGet]
        [Route("getAll")]
        public IEnumerable<Message> GetAllMessages()
        {
            using (var dbContext = new AppDataContext())
            {
                return dbContext.Messages.ToList();
            }
        }
    }
}
