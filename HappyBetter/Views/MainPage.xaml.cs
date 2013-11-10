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

        private void OpenEntryButton_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var entry = button.DataContext;
                if (entry is DateTime)
                {
                    var goToDateTime = (DateTime)entry;
                    EntryPage.Navigate(goToDateTime);
                }
            }
        }
    }
}