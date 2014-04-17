using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace HappyBetterWP81.Pages
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            App.Locator.MainPage.GetData();
        }

        private void MainPagePivot_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddTodayAppBarButton_OnClick(object sender, RoutedEventArgs e)
        {
            App.Locator.MainPage.AddToDatesList(DateTime.Now.Date);
        }

        private void AddDatePickerFlyout_OnDatePicked(DatePickerFlyout sender, DatePickedEventArgs args)
        {
            App.Locator.MainPage.AddToDatesList(sender.Date.DateTime);
        }

        private void DateListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DatesSemanticZoom.IsZoomedInViewActive = !DatesSemanticZoom.IsZoomedInViewActive;
        }

        private void MonthGridView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DatesSemanticZoom.IsZoomedInViewActive = !DatesSemanticZoom.IsZoomedInViewActive;
        }
    }
}
