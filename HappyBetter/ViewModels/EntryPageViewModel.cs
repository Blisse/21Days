using System;
using System.Collections.Generic;
using System.Linq;
using HappyBetter.Models;

namespace HappyBetter.ViewModels
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class EntryPageViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the EntryPageViewModel class.
        /// </summary>
        public EntryPageViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

            }
        }


        #region Properties


        /// <summary>
        /// The <see cref="DailyEntry" /> property's name.
        /// </summary>
        public const string DailyEntryPropertyName = "DailyEntryPropertyKey";

        private DailyEntry _dailyEntry;

        /// <summary>
        /// Sets and gets the QuickiesList property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DailyEntry DailyEntry
        {
            get
            {
                return _dailyEntry;
            }

            set
            {
                if (_dailyEntry == value)
                {
                    return;
                }

                RaisePropertyChanging(DailyEntryPropertyName);
                _dailyEntry = value;
                RaisePropertyChanged(DailyEntryPropertyName);
            }
        }
        
        #endregion

        #region Public Methods

        public void GetData(DateTime currentDataDateTimeKey)
        {
            var dictionary = DataManager.Instance.GetDailyEntriesData();

            if (dictionary != null && dictionary.ContainsKey(currentDataDateTimeKey))
            {
                DailyEntry = dictionary[currentDataDateTimeKey];
            }
        }

        public void UpdateDailyEntryGratitudeEntriesListItemDescription(int index, String description)
        {
            DailyEntry.GratitudeEntriesList.ElementAt(index).Description = description;
        }

        public void SaveEntry()
        {
            var dictionary = DataManager.Instance.GetDailyEntriesData();
            if (dictionary != null && DailyEntry != null && dictionary.ContainsKey(DailyEntry.EntryDateTimeKey))
            {
                dictionary[DailyEntry.EntryDateTimeKey] = DailyEntry;
                DataManager.Instance.SaveDailyEntriesData(dictionary);
            }
        }

        #endregion

        #region Private Methods

        #endregion
    }
}