﻿using CryptoDataWpf.ViewModels;
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
using CryptoDataWpf.Pages;
using CryptoDataWpf.Application.Interfaces.Services;

namespace CryptoDataWpf
{
    public partial class MainWindow : Window
    {

        public MainWindow(MainWindowViewModel viewModel, TopList firstPage)
        {
            viewModel.CurrentView = firstPage;
            this.DataContext = viewModel;
            InitializeComponent();
        }               

        private void SwitchThemeClick(object sender, RoutedEventArgs e)
        {
            string themeName = (sender as MenuItem).Tag.ToString();
            SetTheme(themeName);
        }

        private void SetLanguageClick(object sender, RoutedEventArgs e)
        {
            var tag = (sender as MenuItem).Tag.ToString();
            SetLang(tag);
        }        

        private void SetTheme(string themeName)
        {
            AppTheme.ChangeTheme(new Uri($"Resources/Themes/{themeName}.xaml", uriKind: UriKind.Relative));
        }

        private void SetLang(string lang)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(lang);

            System.Windows.Application.Current.Resources.Clear();
            ResourceDictionary resdict = new()
            {
                Source = new Uri($"Resources/Localizations/Dictionary-{lang}.xaml", UriKind.Relative)
            };
            System.Windows.Application.Current.Resources.MergedDictionaries.Add(resdict);
        }

        // themes and localization options showing
        private void ListViewItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var listViewItem = sender as ListViewItem;
            if (listViewItem != null)
            {
                var contextMenu = listViewItem.ContextMenu;
                if (contextMenu != null)
                {
                    contextMenu.IsOpen = true;
                }
            }
        }

        // servicing code to redirect links in browser
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            var sInfo = new System.Diagnostics.ProcessStartInfo(e.Uri.AbsoluteUri)
            {
                UseShellExecute = true,
            };
            System.Diagnostics.Process.Start(sInfo);
            e.Handled = true;
        }

    }
}