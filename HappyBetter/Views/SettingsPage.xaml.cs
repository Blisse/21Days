using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using HappyBetter.Models;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace HappyBetter.Views
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        public static void Navigate()
        {
            App.RootFrame.Navigate(new Uri("/Views/SettingsPage.xaml", UriKind.Relative));
        }

        private void ClearAllData_OnClick(object sender, RoutedEventArgs e)
        {
            var result =
                MessageBox.Show(
                    "Are you sure you want to delete all the data in this app? This action cannot be undone.", "Confirm Delete",
                    MessageBoxButton.OKCancel);

            switch (result)
            {
                case MessageBoxResult.OK:
                    App.StorageManager.DeleteKey(StorageKeys.DailyEntriesDataStorageKey);
                    break;
            }
            
        }
    }
}