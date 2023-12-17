using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyFirstBot.Controllers;
using MyFirstBot.Services;
using System.Text;
using Telegram.Bot;

namespace MyFirstBot
{
    internal class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services))
                .UseConsoleLifetime()
                .Build();
            Console.WriteLine("Service is run");

            await host.RunAsync();

            Console.WriteLine("Service is stop");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            AppSettings appSettings = GetAppSettings();
            services.AddSingleton(GetAppSettings());
            services.AddSingleton<IStorage, Memory>();

            services.AddTransient<TextMessageHandler>();
            services.AddTransient<TextMessageControler>();
            services.AddTransient<DefaultMessageControler>();
            services.AddTransient<InlineKeyboardControler>();


            services.AddSingleton<ITelegramBotClient>
                (provider => new TelegramBotClient(appSettings.BotToken));

            services.AddHostedService<Bot>();
        }

        static AppSettings GetAppSettings()
        {
            return new AppSettings()
            {
                BotToken = "6574636027:AAGAv2Ce3XTlEVWY8Db0gGdEZEWPEbYOLQI"
            };
        }
    }
}
