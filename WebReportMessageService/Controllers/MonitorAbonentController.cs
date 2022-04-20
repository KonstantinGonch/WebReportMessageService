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
        private int _pageSize = 5;
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

        [HttpPost]
        [Route("saveMeasurement")]
        public async Task AcceptMeasurement(MonitorMeasurement monitorMeasurement)
        {
            monitorMeasurement.Date = DateTime.Now;
            using (var dbContext = new AppDataContext())
            {
                dbContext.MonitorMeasurements.Add(monitorMeasurement);
                await dbContext.SaveChangesAsync();
            }
        }

        [HttpGet]
        [Route("list")]
        public MonitorAbonentsPageModel Get(int pageNumber)
        {
            using (var dbContext = new AppDataContext())
            {
                var pageAbonents = dbContext.MonitorAbonents.Skip((pageNumber - 1) * _pageSize).Take(_pageSize).ToList();
                var totalPages = (int)Math.Ceiling(dbContext.MonitorAbonents.Count() / (double)_pageSize);
                var pageAbonentInfos = new List<MonitorAbonentExtendedInfo>();
                foreach (var abonent in pageAbonents)
                {
                    pageAbonentInfos.Add(BuildExtendedModel(abonent, dbContext));
                }
                return new MonitorAbonentsPageModel { MonitorAbonents = pageAbonentInfos, TotalPages = totalPages, PageNumber = pageNumber };
            }
        }

        [HttpGet]
        [Route("actualMeasurements")]
        public IEnumerable<MonitorMeasurement> GetLastMeasurements(long abonentId)
        {
            using (var dbContext = new AppDataContext())
            {
                return dbContext.MonitorMeasurements
                    .Where(m => m.MonitorAbonentId == abonentId && m.Date > DateTime.Now.AddDays(-7)).ToList();
            }
        }

        private MonitorAbonentExtendedInfo BuildExtendedModel(MonitorAbonent abonent, AppDataContext ctx)
        {
            var extendedAbonentModel = new MonitorAbonentExtendedInfo(abonent);
            var lastSync = ctx.MonitorMeasurements.Where(m => m.MonitorAbonentId == abonent.Id).OrderByDescending(m => m.Id).FirstOrDefault();
            extendedAbonentModel.LastSync = lastSync.Date;
            extendedAbonentModel.AttentionEvents = GetPossibleTroubles(extendedAbonentModel, ctx);
            return extendedAbonentModel;
        }

        private int GetPossibleTroubles(MonitorAbonentExtendedInfo abonent, AppDataContext ctx)
        {
            var lastMeasurements = ctx.MonitorMeasurements.Where(m => m.MonitorAbonentId == abonent.Id && m.Date >= DateTime.Now.AddDays(-7));
            var threatIds = lastMeasurements.Where(measurement => measurement.ThreatId > 0).Select(measurement => measurement.ThreatId).ToList();
            return ctx.Threats.Count(th => threatIds.Contains(th.Id));
        }
    }
}
