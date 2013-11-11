﻿using System;
using System.Collections.Specialized;
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
            App.ViewModelLocator.MainPage.DatesList.CollectionChanged += DatesListOnCollectionChanged;

            App.ViewModelLocator.MainPage.GetData();
        }

        private void OnAddAlreadyAddedDate(object sender, DateTime dateTime)
        {
            Dispatcher.BeginInvoke(() => CustomMessageBoxes.DateAlreadyExistsMessageBox.Show(dateTime));
        }

        private void DatesListOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
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
            HideDatePickerGrid.Begin();

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
                    ApplicationBar = Resources["StatusApplicationBar"] as ApplicationBar;
                    break;
                case 2:
                    ApplicationBar = Resources["InfoApplicationBar"] as ApplicationBar;
                    break;
            }
        }
    }
}