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
    public partial class App : System.Windows.Application
    {
        private ServiceProvider _serviceProvider;

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICurrencyDataService, CoinCapService>();
            services.AddScoped<ICurrencyCalculationsService, CurrencyCalculationsService>();
            services.AddScoped<IOhlcService, KuCoinService>();

            // CoinCap API for everything except OHLC charts data 
            services.AddHttpClient("CoinCap", client =>
            {
                client.BaseAddress = new Uri("https://api.coincap.io/");
            });
            // KuCoin API for OHLC charts data 
            services.AddHttpClient("KuCoin", client =>
            {
                client.BaseAddress = new Uri("https://api.kucoin.com/");
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
            this.Dispatcher.UnhandledException += OnDispatcherUnhandledException;
            IServiceCollection services = new ServiceCollection();

            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
            
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);

        }        

        private void OnDispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string errorMessage = string.Format("An unhandled exception occured: {0}", e.Exception.Message);
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

    }

}
