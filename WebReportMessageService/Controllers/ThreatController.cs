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

        [HttpGet]
        [Route("monitorAbonentThreats")]
        public IEnumerable<Threat> GetMonitorAbonentThreats(long id)
        {
            using (var dbContext = new AppDataContext())
            {
                var threatIds = dbContext.MonitorMeasurements.Where(m => m.MonitorAbonentId == id && m.ThreatId > 0 && m.Date > DateTime.Now.AddDays(-7))
                    .Select(m => m.ThreatId).Distinct();

                return dbContext.Threats.Where(t => threatIds.Contains(t.Id)).ToList();
            }
        }
    }
}
