using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using HappyBetter.Converters;
using HappyBetter.Models;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace HappyBetter.Views
{
    public partial class EntryPage : PhoneApplicationPage
    {
        private static DateTime? _dataDateTimeKey;
        private EventHandler<DailyEntry> _entryUpdated;

        public EntryPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _entryUpdated += EntryUpdated;
            if (_dataDateTimeKey != null)
            {
                App.ViewModelLocator.EntryPage.IsLoading = true;
                App.ViewModelLocator.EntryPage.GetData(_dataDateTimeKey.Value);
                App.ViewModelLocator.EntryPage.IsLoading = false;
            }
            _entryUpdated(this, App.ViewModelLocator.EntryPage.DailyEntry);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            _entryUpdated -= EntryUpdated;
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

        private void EntryUpdated(object sender, DailyEntry entry)
        {
            Dispatcher.BeginInvoke(() =>
            {
                if (entry != null)
                {
                    EntryDateTimeKeyTextBlock.Foreground =
                        BooleanToEntryHeaderColorConverter.Convert(entry.IsCompleted);
                }

                foreach (PivotItem pivotItem in EntryPagePivot.Items)
                {
                    var header = pivotItem.Header as TextBlock;
                    var index = EntryPagePivot.Items.IndexOf(pivotItem);
                    if (header != null)
                    {
                        SetInactiveEntryPivotItemHeaderColour(header);
                        if (pivotItem == EntryPagePivot.SelectedItem)
                        {
                            SetActiveEntryPivotItemHeaderColour(header, index);
                        }
                    }
                }
            });
        }

        private void TextBoxUpdateBindingSource_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
            _entryUpdated(sender, App.ViewModelLocator.EntryPage.DailyEntry);
        }

        private void SetActiveEntryPivotItemHeaderColour(TextBlock header, int currentIndex)
        {
            bool isCurrentPivotCompleted = false;
            var entry = App.ViewModelLocator.EntryPage.DailyEntry;
            switch (currentIndex)
            {
                case 0: // Gratitude
                    isCurrentPivotCompleted = entry.IsDone(entry.GratitudeEntriesList);
                    break;
                case 1: // Journal
                    isCurrentPivotCompleted = entry.IsDone(entry.JournalEntry);
                    break;
                case 2: // Meditation
                    isCurrentPivotCompleted = entry.IsDone(entry.MeditationEntry);
                    break;
                case 3: // Exercise
                    isCurrentPivotCompleted = entry.IsDone(entry.ExerciseEntry);
                    break;
                case 4: // Kindness
                    isCurrentPivotCompleted = entry.IsDone(entry.ActOfKindnessEntry);
                    break;
            }

            if (isCurrentPivotCompleted)
            {
                header.Foreground = (SolidColorBrush)Application.Current.Resources["ForegroundGreen"];
                header.Opacity = 1;
            }
            else
            {
                header.Foreground = (SolidColorBrush)Application.Current.Resources["ForegroundWhite"];
                header.Opacity = 1;
            }
        }

        private void SetInactiveEntryPivotItemHeaderColour(TextBlock header)
        {
            header.Foreground = (SolidColorBrush)Application.Current.Resources["ForegroundGray"];
            header.Opacity = 0.4;
        }

        private void EntryPagePivot_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pivot = sender as Pivot;

            if (pivot != null)
            {
                var currentIndex = pivot.SelectedIndex;
                if (pivot.SelectedIndex == 0)
                {
                    ApplicationBar = Resources["GratitudesApplicationBar"] as ApplicationBar;
                }
                else
                {
                    ApplicationBar = Resources["NormalAppBar"] as ApplicationBar;
                }

                if (e.RemovedItems != null && e.RemovedItems.Count > 0 && e.RemovedItems[0] as PivotItem != null)
                {
                    var lastPivotItem = e.RemovedItems[0] as PivotItem;
                    var header = lastPivotItem.Header as TextBlock;
                    if (header != null)
                    {
                        SetInactiveEntryPivotItemHeaderColour(header);
                    }
                }

                if (e.AddedItems != null && e.AddedItems.Count > 0 && e.AddedItems[0] as PivotItem != null)
                {
                    var currentPivotItem = e.AddedItems[0] as PivotItem;
                    var header = currentPivotItem.Header as TextBlock;

                    if (header != null)
                    {
                        SetInactiveEntryPivotItemHeaderColour(header);
                        if (currentPivotItem == pivot.SelectedItem)
                        {
                            SetActiveEntryPivotItemHeaderColour(header, currentIndex);
                        }
                    }
                }


            }


        }

    }
}