using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReportMessageService.Controllers
{
    [ApiController]
    [Route("api/scanJob")]
    public class ScanJobController : ControllerBase
    {
        private readonly ILogger<ScanJobController> _logger;
        private const int _pageSize = 10;

        public ScanJobController(ILogger<ScanJobController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("list")]
        public ScanJobResultListPageModel Get(int pageNumber)
        {
            using (var dbContext = new AppDataContext())
            {
                var scanJobResults = dbContext.ScanJobResults.OrderByDescending(m => m.Id).Skip((pageNumber - 1) * _pageSize).Take(_pageSize).ToList();
                var totalPages = (int)Math.Ceiling(dbContext.ScanJobResults.Count() / (double)_pageSize);
                return new ScanJobResultListPageModel { ScanJobResults = scanJobResults, TotalPages = totalPages, PageNumber = pageNumber };
            }
        }
    }
}
