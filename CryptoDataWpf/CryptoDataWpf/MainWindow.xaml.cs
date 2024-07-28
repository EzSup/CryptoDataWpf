using CryptoDataWpf.ViewModels;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CryptoDataWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;

        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.InitializeAsync();
        }

        private void SwitchThemeClick(object sender, RoutedEventArgs e)
        {
            string themeName = (sender as MenuItem).Tag.ToString();

            AppTheme.ChangeTheme(new Uri($"Themes/{themeName}.xaml", uriKind: UriKind.Relative));
        }

        private void SetLanguageClick(object sender, RoutedEventArgs e)
        {
            var tag = (sender as MenuItem).Tag.ToString();
            SetLang(tag);
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            var sInfo = new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri)
            {
                UseShellExecute = true,
            };
            System.Diagnostics.Process.Start(sInfo);
            e.Handled = true;
        }

        private void SetLang(string lang)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);

            System.Windows.Application.Current.Resources.MergedDictionaries.Clear();
            ResourceDictionary resdict = new()
            {
                Source = new Uri($"/Localizations/Dictionary-{lang}.xaml", UriKind.Relative)
            };
            System.Windows.Application.Current.Resources.MergedDictionaries.Add(resdict);
        }

        
    }
}