using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReportMessageService.Controllers
{
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
        }
    }
}
