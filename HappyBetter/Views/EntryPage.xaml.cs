using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using HappyBetter.Models;
using Microsoft.Phone.Controls;

namespace HappyBetter.Views
{
    public partial class EntryPage : PhoneApplicationPage
    {
        private static DateTime? _dataDateTimeKey;

        public EntryPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (_dataDateTimeKey != null)
            {
                App.ViewModelLocator.EntryPage.IsLoading = true;
                App.ViewModelLocator.EntryPage.GetData(_dataDateTimeKey.Value);
                App.ViewModelLocator.EntryPage.IsLoading = false;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            _dataDateTimeKey = null;
            App.ViewModelLocator.EntryPage.SaveEntry();
        }

        public static Boolean Navigate(DateTime dataDateTimeKey)
        {
            _dataDateTimeKey = dataDateTimeKey;
            return App.RootFrame.Navigate(new Uri("/Views/EntryPage.xaml", UriKind.Relative));
        }

        #region Click Events

        #endregion

        private void TextBoxUpdateBindingSource_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
        }
    }
}