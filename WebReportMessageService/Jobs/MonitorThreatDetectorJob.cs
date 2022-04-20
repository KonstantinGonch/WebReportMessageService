using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebReportMessageService.Jobs
{
    public class MonitorThreatDetectorJob : IHostedService
    {
        private int DELAY_MINUTES = 10;
        private int CPU_THRESHOLD = 80;
        private int RAM_THRESHOLD = 80;
        private int CPU_AND_RAM_MEASUREMENTS_BATCH = 6;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await PerformScanning();
                    await Task.Delay(TimeSpan.FromMinutes(DELAY_MINUTES));
                }
            });

            return Task.CompletedTask;
        }

        private async Task PerformScanning()
        {
            try
            {
                using (var dbContext = new AppDataContext())
                {

                    var monitorAbonents = dbContext.MonitorAbonents.ToList();
                    foreach (var abonent in monitorAbonents)
                    {
                        var abonentId = abonent.Id;
                        var lastMeasurements = dbContext.MonitorMeasurements.Where(m => m.Date > DateTime.Now.AddDays(-7) && m.MonitorAbonentId == abonentId).OrderBy(m => m.Date).ToList();
                        //Проверка аптаймов
                        if (lastMeasurements.Count() > 1)
                        {
                            for (int i = 0; i < lastMeasurements.Count() - 1; i++)
                            {
                                var prevMeasurement = lastMeasurements.ElementAt(i);
                                var nextMeasurement = lastMeasurements.ElementAt(i + 1);
                                if (prevMeasurement.Uptime > nextMeasurement.Uptime && nextMeasurement.ThreatId == 0)
                                {
                                    var threat = new Threat
                                    {
                                        DateAppeared = DateTime.Now,
                                        ThreatMessage = $"Обнаружен факт перезапуска {abonent.HostName} при синхронизации {nextMeasurement.Date}"
                                    };
                                    dbContext.Threats.Add(threat);
                                    await dbContext.SaveChangesAsync();
                                    nextMeasurement.ThreatId = threat.Id;
                                    await dbContext.SaveChangesAsync();
                                }
                            }
                        }

                        //Проверка использования ЦП и оперативы
                        if (lastMeasurements.Count() > CPU_AND_RAM_MEASUREMENTS_BATCH)
                        {
                            for (int i = 0; i < lastMeasurements.Count() - CPU_AND_RAM_MEASUREMENTS_BATCH; i++)
                            {
                                var cpuRamBatch = lastMeasurements.Skip(i).Take(CPU_AND_RAM_MEASUREMENTS_BATCH);
                                var measurement = lastMeasurements.ElementAt(i);
                                if (measurement.ThreatId == 0)
                                {
                                    if (cpuRamBatch.All(b => b.CpuUsage >= CPU_THRESHOLD))
                                    {
                                        var threat = new Threat
                                        {
                                            DateAppeared = DateTime.Now,
                                            ThreatMessage = $"Обнаружена повышенная нагрузка ЦП (> {CPU_THRESHOLD}%) для {abonent.HostName} при синхронизации {measurement.Date}"
                                        };
                                        dbContext.Threats.Add(threat);
                                        await dbContext.SaveChangesAsync();
                                        foreach (var element in cpuRamBatch)
                                        {
                                            if (element.ThreatId == 0)
                                            {
                                                element.ThreatId = threat.Id;
                                            }
                                        }
                                        measurement.ThreatId = threat.Id;
                                        await dbContext.SaveChangesAsync();
                                    }
                                }

                                if (measurement.ThreatId == 0)
                                {
                                    if (cpuRamBatch.All(b => b.RamUsage >= RAM_THRESHOLD))
                                    {
                                        var threat = new Threat
                                        {
                                            DateAppeared = DateTime.Now,
                                            ThreatMessage = $"Обнаружена повышенная нагрузка RAM (> {RAM_THRESHOLD}%) для {abonent.HostName} при синхронизации {measurement.Date}"
                                        };
                                        dbContext.Threats.Add(threat);
                                        await dbContext.SaveChangesAsync();
                                        foreach (var element in cpuRamBatch)
                                        {
                                            if (element.ThreatId == 0)
                                            {
                                                element.ThreatId = threat.Id;
                                            }
                                        }
                                        measurement.ThreatId = threat.Id;
                                        await dbContext.SaveChangesAsync();
                                    }
                                }
                            }
                        }

                        //Проверка последней синхронизации
                        var lastSync = lastMeasurements.LastOrDefault();
                        if (lastSync != null && lastSync.ThreatId == 0 && lastSync.Date < DateTime.Now.AddHours(-1))
                        {
                            var threat = new Threat
                            {
                                DateAppeared = DateTime.Now,
                                ThreatMessage = $"Отсутствие синхронизации в течение часа для {abonent.HostName} при синхронизации {lastSync.Date}"
                            };
                            dbContext.Threats.Add(threat);
                            await dbContext.SaveChangesAsync();
                            lastSync.ThreatId = threat.Id;
                            await dbContext.SaveChangesAsync();
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {

            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
