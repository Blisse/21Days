using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace HappyBetter.Views
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            App.ViewModelLocator.MainPage.IsLoading = true;
            App.ViewModelLocator.MainPage.GetData();
            App.ViewModelLocator.MainPage.IsLoading = false;
        }

        private void AddDate_OnClick(object sender, RoutedEventArgs e)
        {
            App.ViewModelLocator.MainPage.IsLoading = true;
            App.ViewModelLocator.MainPage.AddToDatesList(DateTime.Now);
            App.ViewModelLocator.MainPage.IsLoading = false;
        }

        private void OpenDateDataButton_OnClick(object sender, RoutedEventArgs e)
        {
            var b = sender as Button;
            if (b != null)
            {
                var dc = b.DataContext;
                if (dc is DateTime)
                {
                    var goToDateTime = (DateTime)dc;
                    EntryPage.Navigate(goToDateTime);
                }
            }
        }

        private void AddTodayAppBar_OnClick(object sender, EventArgs e)
        {
            App.ViewModelLocator.MainPage.IsLoading = true;
            App.ViewModelLocator.MainPage.AddToDatesList(DateTime.Now);
            App.ViewModelLocator.MainPage.IsLoading = false;
        }

        private void MainPagePivot_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OpenDatePickerAppBar_OnClick(object sender, EventArgs e)
        {
            if (HiddenDatePickerGrid.Height < 1.0)
            {
                ShowDatePickerGrid.Begin();
            }
            else if (HiddenDatePickerGrid.Height > 99.0)
            {
                HideDatePickerGrid.Begin();
            }
        }

        private void ViewEntryMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null && menuItem.DataContext is DateTime)
            {
                var goToDateTime = (DateTime)menuItem.DataContext;
                EntryPage.Navigate(goToDateTime);
            }
        }

        private void DeleteEntryMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null && menuItem.DataContext is DateTime)
            {
                var dateTimeToDelete = (DateTime)menuItem.DataContext;
                App.ViewModelLocator.MainPage.DeleteFromDatesList(dateTimeToDelete);
            }
        }
    }
}