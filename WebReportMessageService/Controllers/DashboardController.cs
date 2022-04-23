using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReportMessageService.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        //Линейный график
        [HttpGet]
        [Route("threats")]
        public IEnumerable<ThreatTimeData> GetThreatTimeData()
        {
            using (var dbContext = new AppDataContext())
            {
                var threatData = new List<ThreatTimeData>();
                var threatNumber = 0;
                var dateThreshold = DateTime.Now.AddDays(-7);
                var registeredThreats = dbContext.Threats.Where(t => t.DateAppeared >= dateThreshold).OrderBy(t => t.DateAppeared).ToList();
                var countDate = registeredThreats[0].DateAppeared;
                while (countDate < DateTime.Now)
                {
                    var hourThreats = registeredThreats.Count(t => t.DateAppeared >= countDate && t.DateAppeared <= countDate.AddHours(1));
                    threatNumber += hourThreats;
                    threatData.Add(new ThreatTimeData
                    {
                        Threats = threatNumber,
                        DateTime = countDate
                    });
                    countDate = countDate.AddHours(1);
                }
                return threatData;
            }
        }

        [HttpGet]
        [Route("lastMessages")]
        public IEnumerable<Message> GetLastMessages()
        {
            using (var dbContext = new AppDataContext())
            {
                return dbContext.Messages.OrderByDescending(m => m.MessageDate).Take(3).ToList();
            }
        }

        //Круговая диаграмма
        [HttpGet]
        [Route("scanRate")]
        public ScanRateModel GetScanRate()
        {
            using (var dbContext = new AppDataContext())
            {
                var scanRate = new ScanRateModel
                {
                    GoodScans = dbContext.ScanJobResults.Count(s => s.ThreatId == 0),
                    BadScans = dbContext.ScanJobResults.Count(s => s.ThreatId != 0)
                };
                return scanRate;
            }
        }

        //Круговая диаграмма
        [HttpGet]
        [Route("activeUsers")]
        public MonitorUsersRateModel GetMonitorUserRate()
        {
            using (var dbContext = new AppDataContext())
            {
                return new MonitorUsersRateModel
                {
                    ActiveUsers = dbContext.MonitorMeasurements.Where(m => m.Date >= DateTime.Now.AddHours(-1)).Select(m => m.MonitorAbonentId).Distinct().Count(),
                    InactiveUsers = dbContext.MonitorMeasurements.Where(m => m.Date < DateTime.Now.AddHours(-1)).Select(m => m.MonitorAbonentId).Distinct().Count()
                };
            }
        }

        //Линейный график
        [HttpGet]
        [Route("badMessagesRate")]
        public IEnumerable<BadMessagesTimeData> GetBadMessageData()
        {
            using (var dbContext = new AppDataContext())
            {
                var threatData = new List<BadMessagesTimeData>();
                var threatNumber = 0;
                var registeredMessages = dbContext.Messages.OrderBy(t => t.MessageDate).ToList();
                var countDate = registeredMessages[0].MessageDate;
                while (countDate < DateTime.Now)
                {
                    var hourMessages = registeredMessages.Count(t => t.MessageDate >= countDate && t.MessageDate <= countDate.AddHours(1) && t.MessageType == MessageType.ErrorReport);
                    threatNumber += hourMessages;
                    threatData.Add(new BadMessagesTimeData
                    {
                        Threats = threatNumber,
                        DateTime = countDate
                    });
                    countDate = countDate.AddHours(1);
                }
                return threatData;
            }
        }

    }
}
