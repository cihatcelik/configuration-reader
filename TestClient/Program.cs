using System;
using System.Threading.Tasks;
using System.Timers;
using ConfigurationReader;
using RedisCache.CacheService;
using RedisCache.CacheService.Redis;

namespace TestClient
{
    internal class Program
    {
        private static System.Timers.Timer aTimer;
        static string connectionString = "mongodb://127.0.0.1:27017";
        static ConfigurationReader.ConfigurationReader reader = new ConfigurationReader.ConfigurationReader("Service-A", connectionString, 20000);

        static async Task Main(string[] args)
        {

            aTimer = new System.Timers.Timer(2000);

            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            Console.ReadLine();
        }


        private static async void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            try
            {
                int p = await reader.GetValue<int>("MaxItemCount");
                Console.WriteLine("maxItemCount " + p.ToString() + " " + DateTime.Now.ToLongTimeString());
            }
            catch
            {
                Console.WriteLine("Config Bulunamadı");
            }
        }
    }
}
