﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReportMessageService
{
    public class NetworkResourceListPageModel : BasePageModel
    {
        public IEnumerable<NetworkResource> NetworkResources { get; set; }
    }
}
