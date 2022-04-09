using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReportMessageService.Controllers
{
    [ApiController]
    [Route("api/scanSettings")]
    public class ScanSettingsController : ControllerBase
    {
        private readonly ILogger<ScanSettingsController> _logger;

        public ScanSettingsController(ILogger<ScanSettingsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("settings")]
        public ScanJobSettings Get()
        {
            using (var dbContext = new AppDataContext())
            {
                return dbContext.ScanJobSettings.FirstOrDefault();
            }
        }

        [HttpPost]
        [Route("save")]
        public async void Save(ScanJobSettings settings)
        {
            using (var dbContext = new AppDataContext())
            {
                var existingSettings = dbContext.ScanJobSettings.FirstOrDefault();
                if (existingSettings != null)
                    dbContext.ScanJobSettings.Remove(existingSettings);

                dbContext.Add(settings);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
