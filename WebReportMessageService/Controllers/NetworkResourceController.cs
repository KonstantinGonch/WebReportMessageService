using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReportMessageService.Controllers
{
    [ApiController]
    [Route("api/networkResource")]
    public class NetworkResourceController : ControllerBase
    {
        private readonly ILogger<NetworkResourceController> _logger;
        private const int _pageSize = 8;

        public NetworkResourceController(ILogger<NetworkResourceController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("list")]
        public NetworkResourceListPageModel Get(int pageNumber)
        {
            using (var dbContext = new AppDataContext())
            {
                var pageResources = dbContext.NetworkResources.OrderByDescending(m => m.Id).Skip((pageNumber - 1) * _pageSize).Take(_pageSize).ToList();
                var totalPages = (int)Math.Ceiling(dbContext.NetworkResources.Count() / (double)_pageSize);
                return new NetworkResourceListPageModel { NetworkResources = pageResources, TotalPages = totalPages, PageNumber = pageNumber };
            }
        }

        [HttpPost]
        [Route("save")]
        public async void Save(NetworkResource networkResource)
        {
            using (var dbContext = new AppDataContext())
            {
                dbContext.Add(networkResource);
                await dbContext.SaveChangesAsync();
            }
        }

        [HttpPost]
        [Route("delete")]
        public async void Delete(NetworkResource resource)
        {
            using (var dbContext = new AppDataContext())
            {
                var record = dbContext.NetworkResources.FirstOrDefault(e => e.Id == resource.Id);
                if (record != null)
                {
                    dbContext.NetworkResources.Remove(record);
                }
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
