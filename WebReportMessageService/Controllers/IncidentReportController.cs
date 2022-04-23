using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebReportMessageService.Controllers
{
    [ApiController]
    [Route("api/incident")]
    public class IncidentReportController : ControllerBase
    {
        private int _pageSize = 8;
        [HttpPost]
        [Route("save")]
        public async void Save(IncidentReport report)
        {
            using (var dbContext = new AppDataContext())
            {
                report.FixationAt = DateTime.Now;
                dbContext.IncidentReports.Add(report);
                await dbContext.SaveChangesAsync();
            }

            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("testovtesttestovich13@gmail.com", "superFinashka"),
                    EnableSsl = true
                };
                var message = $"Содержание: {report.Description}; Составлено: {report.FixationAt}; Вектор атаки: {report.AttackVector.ToString()}; Тип атаки: {report.TypeOfAttack.ToString()}; Необходимость содействия: {report.AssistanceNeeded.ToString()}";
                client.Send("testovtesttestovich13@gmail.com", "goncharovkostya.1997@gmail.com", "Зарегистрировано новое обращение", message);
            }
            catch (Exception ex)
            {

            }
        }

        [HttpGet]
        [Route("get")]
        public IEnumerable<IncidentReport> Get()
        {
            using (var dbContext = new AppDataContext())
            {
                return dbContext.IncidentReports.ToList();
            }
        }

        [HttpGet]
        [Route("list")]
        public IncidentReportPageModel List(int pageNumber)
        {
            using (var dbContext = new AppDataContext())
            {
                var pageReports = dbContext.IncidentReports.OrderByDescending(m => m.Id).Skip((pageNumber - 1) * _pageSize).Take(_pageSize).ToList();
                var totalPages = (int)Math.Ceiling(dbContext.IncidentReports.Count() / (double)_pageSize);
                return new IncidentReportPageModel { IncidentReports = pageReports, TotalPages = totalPages, PageNumber = pageNumber };
            }
        }
    }
}
