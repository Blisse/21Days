using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using HappyBetter.Models;


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
            App.ViewModelLocator.MainPage.DatesListChanged += DatesListOnCollectionChanged;

            App.ViewModelLocator.MainPage.GetData();

            Loaded += delegate
            {
                if (!App.StorageManager.ContainsKey(StorageKeys.NotFirstTimeOpened))
                {
                    App.StorageManager.AddOrUpdateValue(StorageKeys.NotFirstTimeOpened, true);
                    MainPagePivot.SelectedIndex = 2;
                }
            };
        }

        public static Boolean Navigate()
        {
            return App.RootFrame.Navigate(new Uri("/Views/MainPage.xaml", UriKind.Relative));
        }

        private void OnAddAlreadyAddedDate(object sender, DateTime dateTime)
        {
            Dispatcher.BeginInvoke(() => CustomMessageBoxes.DateAlreadyExistsMessageBox.Show(dateTime));
        }

        private void DatesListOnCollectionChanged(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(() => UpdateApplicationBar(MainPagePivot));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            App.ViewModelLocator.MainPage.AddAlreadyAddedDate -= OnAddAlreadyAddedDate;
            App.ViewModelLocator.MainPage.DatesList.CollectionChanged -= DatesListOnCollectionChanged;
        }

        private void AddDate_OnClick(object sender, RoutedEventArgs e)
        {
            if (HiddenDatePicker != null && HiddenDatePicker.Value != null)
            {
                App.ViewModelLocator.MainPage.AddToDatesList((DateTime)HiddenDatePicker.Value);
            }
        }

        private void AddTodayAppBar_OnClick(object sender, EventArgs e)
        {
            App.ViewModelLocator.MainPage.AddToDatesList(DateTime.Now.Date);
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
                EntryPage.Navigate((DateTime)menuItem.DataContext);
            }
        }

        private void DeleteEntryMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            if (menuItem != null && menuItem.DataContext is DateTime)
            {
                App.ViewModelLocator.MainPage.DeleteFromDatesList((DateTime)menuItem.DataContext);
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
            if (HiddenDatePickerGrid.Height > 79.0)
            {
                HideDatePickerGrid.Begin();
            }

            if (sender as Pivot != null)
            {
                UpdateApplicationBar(sender as Pivot);
            }
        }

        private void UpdateApplicationBar(Pivot pivot)
        {
            switch (pivot.SelectedIndex)
            {
                case 0:
                    if (App.ViewModelLocator.MainPage.DatesList.Contains(DateTime.Now.Date))
                    {
                        ApplicationBar = Resources["DatesNoTodayApplicationBar"] as ApplicationBar;
                    }
                    else
                    {
                        ApplicationBar = Resources["DatesApplicationBar"] as ApplicationBar;
                    }
                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                    ApplicationBar = Resources["NormalApplicationBar"] as ApplicationBar;
                    break;
            }
        }

        private void DeleteAllDatesAppBarMenuItem_Click(object sender, EventArgs e)
        {
            var result =
                MessageBox.Show(
                    "Are you sure you want to delete all the data in this app? This action cannot be undone.", "Confirm Delete",
                    MessageBoxButton.OKCancel);

            switch (result)
            {
                case MessageBoxResult.OK:
                    App.ViewModelLocator.MainPage.ClearData();
                    break;
            }
            
        }
    }
}