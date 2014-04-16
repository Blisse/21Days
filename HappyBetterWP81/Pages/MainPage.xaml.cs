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
        private readonly CommandBar _addTodayAppBar = new CommandBar();
        private readonly CommandBar _addDateOnlyAppBar = new CommandBar();

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private void MainPagePivot_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pivot = sender as Pivot;
            if (pivot != null)
            {

            }
        }

        private void AddTodayAppBarButton_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void AddDatePickerFlyout_OnDatePicked(DatePickerFlyout sender, DatePickedEventArgs args)
        {

        }
    }
}
