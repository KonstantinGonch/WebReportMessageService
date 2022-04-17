using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace WebReportMessageService.Jobs
{
    public class ScanJob : IHostedService, IDisposable
    {
        private const int _pingDelay = 10;
        private ScanJobSettings _settings = null;
        private Ping _pinger = null;

        public ScanJob()
        {
            using (var dbContext = new AppDataContext())
            {
                _settings = dbContext.ScanJobSettings.FirstOrDefault();
                _pinger = new Ping();
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    if (_settings != null)
                    {
                        await PerformScanning();
                        await Task.Delay(TimeSpan.FromMinutes(_settings.JobRestartMinutes));
                    }
                    else
                    {
                        await Task.Delay(TimeSpan.FromMinutes(60));
                    }
                    using (var dbContext = new AppDataContext())
                    {
                        _settings = dbContext.ScanJobSettings.FirstOrDefault();
                    }
                }
            });

            return Task.CompletedTask;
        }

        private async Task PerformScanning()
        {
            if (_settings != null)
            {
                var successPings = 0;
                var failedResources = new List<string>();

                using (var dbContext = new AppDataContext())
                {
                    var resources = dbContext.NetworkResources;
                    foreach(var networkResource in resources)
                    {
                        var success = false;
                        for (int i = 0; i < _settings.PingRetries; i++)
                        {
                            try
                            {
                                if (IPAddress.TryParse(networkResource.IpAddress, out IPAddress ipAddress))
                                {
                                    var reply = _pinger.Send(ipAddress);
                                    if (reply.Status == IPStatus.Success)
                                    {
                                        success = true;
                                        successPings += 1;
                                        break;
                                    }
                                }
                                else
                                {
                                    var webRequest = (HttpWebRequest)WebRequest.Create(networkResource.IpAddress);
                                    var webResponse = (HttpWebResponse)webRequest.GetResponse();
                                    if (webResponse.StatusCode == HttpStatusCode.OK)
                                    {
                                        success = true;
                                        successPings += 1;
                                        break;
                                    }
                                }
                                Thread.Sleep(_pingDelay * 1000);
                            }
                            catch (Exception ex)
                            {
                                Thread.Sleep(_pingDelay * 1000);
                            }
                        }

                        if (!success)
                        {
                            failedResources.Add(networkResource.IpAddress);
                        }
                    }

                    var scanDate = DateTime.Now;
                    var scanResult = new ScanJobResult
                    {
                        ScanDate = scanDate,
                        PlanNextScan = scanDate.AddMinutes(_settings.JobRestartMinutes),
                        SuccessScanned = successPings,
                        TotalResources = resources.Count()
                    };

                    if (failedResources.Count >= _settings.PingFailureThreat)
                    {
                        var threat = new Threat
                        {
                            DateAppeared = scanDate,
                            ThreatMessage = $"Выявлена ошибка сканирования. Недоступные ресурсы: {string.Join("; ", failedResources)}"
                        };
                        dbContext.Threats.Add(threat);
                        await dbContext.SaveChangesAsync();
                        scanResult.ThreatId = threat.Id;
                    }
                    dbContext.ScanJobResults.Add(scanResult);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _pinger.Dispose();
        }
    }
}
