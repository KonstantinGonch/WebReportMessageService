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
    [EnableCors]
    [ApiController]
    [Route("api/incident")]
    public class IncidentReportController : ControllerBase
    {
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
    }
}
