using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MonitorConsoleApp
{
    class Program
    {
        const string SERVICE_URL = "http://localhost:5000";
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            Console.WriteLine("Введите имя хоста");
            var hostName = Console.ReadLine();
            Console.WriteLine("Подключение к серверу...");

            var values = new Dictionary<string, string>
                {
                    { "hostName", hostName },
                };

            var content = new StringContent(JsonConvert.SerializeObject(values), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(SERVICE_URL + "/api/monitorAbonent/register?hostName="+hostName, content);

            var responseString = await response.Content.ReadAsStringAsync();

            long tickCountMs = Environment.TickCount;
            var uptime = TimeSpan.FromMilliseconds(tickCountMs);
            var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total"); ;
            var ramCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");

            Console.ReadKey();
        }
    }
}
