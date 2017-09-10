using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;

namespace Obsidian
{
    public static class Program
    {
        private static bool keepRunning = true;
        private static IWebHost host;

        public static void Main(string[] args)
        {
            while (keepRunning)
            {
                keepRunning = false;
                host = BuildWebHost(args);
                host.Run();
            }
        }

        public static async void RestartHostAsync()
        {
            keepRunning = true;
            await host.StopAsync();
        }

        private static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .Build();
    }
}
