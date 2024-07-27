using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

using CryptoDataWpf.Infrastructure.APIs;

namespace CryptoDataWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceCollection services = new ServiceCollection();

            //services.AddScoped<IAPIInteractionService, CoinCapService>();

            services.AddHttpClient("CoinCap", client =>
            {
                client.BaseAddress = new Uri("https://api.coincap.io/");
            });

            base.OnStartup(e);
        }

    }

}
