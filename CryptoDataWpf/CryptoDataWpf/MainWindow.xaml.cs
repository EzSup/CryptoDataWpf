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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SwitchThemeClick(object sender, RoutedEventArgs e)
        {
            string themeName = (sender as MenuItem).Tag.ToString();

            AppTheme.ChangeTheme(new Uri($"Themes/{themeName}.xaml", uriKind: UriKind.Relative));
        }

    }
}