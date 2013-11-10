using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

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
            App.ViewModelLocator.MainPage.AddAlreadyAddedDate += OnAddAlreadyAddedDate;
            App.ViewModelLocator.MainPage.IsLoading = true;
            App.ViewModelLocator.MainPage.GetData();
            App.ViewModelLocator.MainPage.IsLoading = false;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            App.ViewModelLocator.MainPage.AddAlreadyAddedDate -= OnAddAlreadyAddedDate;
        }

        private void OnAddAlreadyAddedDate(object sender, DateTime dateTime)
        {
            Dispatcher.BeginInvoke(() => CustomMessageBoxes.DateAlreadyExistsMessageBox.Show(dateTime));
        }

        private void AddDate_OnClick(object sender, RoutedEventArgs e)
        {
            App.ViewModelLocator.MainPage.IsLoading = true;
            if (HiddenDatePicker != null && HiddenDatePicker.Value != null)
            {
                App.ViewModelLocator.MainPage.AddToDatesList((DateTime)HiddenDatePicker.Value);
            }
            App.ViewModelLocator.MainPage.IsLoading = false;
        }

        private void AddTodayAppBar_OnClick(object sender, EventArgs e)
        {
            App.ViewModelLocator.MainPage.IsLoading = true;
            App.ViewModelLocator.MainPage.AddToDatesList(DateTime.Now.Date);
            App.ViewModelLocator.MainPage.IsLoading = false;
        }

        private void OpenDatePickerAppBar_OnClick(object sender, EventArgs e)
        {
            if (HiddenDatePickerGrid.Height < 1.0)
            {
                ShowDatePickerGrid.Begin();
            }
            else if (HiddenDatePickerGrid.Height > 79.0)
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

        private void MainPagePivot_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pivot = sender as Pivot;

            if (pivot != null)
            {
                if (HiddenDatePickerGrid.Height > 1.0)
                {
                    HideDatePickerGrid.Begin();
                }
                switch (pivot.SelectedIndex)
                {
                    case 0:
                        if (App.ViewModelLocator.MainPage.DatesList.Contains(DateTime.Now.Date))
                        {
                            ApplicationBar = Resources["DatesApplicationBar"] as ApplicationBar;
                        }
                        else
                        {
                            ApplicationBar = Resources["DatesNoTodayApplicationBar"] as ApplicationBar;
                        }
                        break;
                    case 1:
                        ApplicationBar = Resources["StatusApplicationBar"] as ApplicationBar;
                        break;
                    case 2:
                        ApplicationBar = Resources["InfoApplicationBar"] as ApplicationBar;
                        break;
                }
            }
        }
    }
}