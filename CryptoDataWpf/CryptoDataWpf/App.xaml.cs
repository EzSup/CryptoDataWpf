using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using CryptoDataWpf.Application.Interfaces.Services;
using CryptoDataWpf.Infrastructure.APIs;
using CryptoDataWpf.ViewModels;
using CryptoDataWpf.Pages;
using CryptoDataWpf.Application.Services;

namespace CryptoDataWpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private ServiceProvider _serviceProvider;

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAPIInteractionService, CoinCapService>();
            services.AddScoped<ICurrencyCalculationsService, CurrencyCalculationsService>();

            services.AddHttpClient("CoinCap", client =>
            {
                client.BaseAddress = new Uri("https://api.coincap.io/");
            });

            services.AddSingleton<CurrencyViewModel>();
            services.AddScoped<TopListViewModel>();
            services.AddSingleton<TopList, TopList>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<ExchangesViewModel>();
            services.AddSingleton<Exchanges>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceCollection services = new ServiceCollection();

            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
            
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);

        }

    }

}
