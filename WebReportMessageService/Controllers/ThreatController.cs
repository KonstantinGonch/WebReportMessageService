using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReportMessageService.Controllers
{
    [ApiController]
    [Route("api/threat")]
    public class ThreatController
    {
        private readonly ILogger<ThreatController> _logger;

        public ThreatController(ILogger<ThreatController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("get")]
        public Threat Get(long id)
        {
            using (var dbContext = new AppDataContext())
            {
                return dbContext.Threats.FirstOrDefault(e => e.Id == id);
            }
        }
    }
}
