using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReportMessageService.Controllers
{
    [ApiController]
    [Route("api/monitorAbonent")]
    public class MonitorAbonentController : ControllerBase
    {
        private readonly ILogger<NetworkResourceController> _logger;
        public MonitorAbonentController(ILogger<NetworkResourceController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("register")]
        public async Task<long> RegisterAbonent(string hostName)
        {
            using (var dbContext = new AppDataContext())
            {
                var existingAbonent = dbContext.MonitorAbonents.FirstOrDefault(abonent => abonent.HostName.ToLower() == hostName.ToLower());
                if (existingAbonent == null)
                {
                    existingAbonent = new MonitorAbonent { HostName = hostName };
                    dbContext.MonitorAbonents.Add(existingAbonent);
                    await dbContext.SaveChangesAsync();
                }
                return existingAbonent.Id;
            }
        }
    }
}
