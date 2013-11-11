using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class MainPageViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the MainPageViewModel class.
        /// </summary>
        public MainPageViewModel()
        {
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {

            }
        }

        #region Public Methods

        public void GetData()
        {
            IsLoading = true;

            Dictionary = DataManager.Instance.GetDailyEntriesData();
            UpdateDatesList(Dictionary);

            IsLoading = false;
        }

        public Boolean AddToDatesList(DateTime enterDateTime)
        {
            IsLoading = true;
            Dictionary = DataManager.Instance.GetDailyEntriesData();

            if (Dictionary.ContainsKey(enterDateTime))
            {
                if (AddAlreadyAddedDate != null)
                {
                    AddAlreadyAddedDate(this, enterDateTime);
                }

                IsLoading = false;
                return false;
            }

            var dailyEntry = new DailyEntry(enterDateTime);
            Dictionary.Add(enterDateTime, dailyEntry);
            UpdateDatesList(Dictionary);

            if (DataManager.Instance.SaveDailyEntriesData(Dictionary))
            {
                if (ErrorOccurred != null)
                {
                    ErrorOccurred(this, null);
                }

                IsLoading = false;
                return false;
            }

            IsLoading = false;
            return true;
        }

        public Boolean DeleteFromDatesList(DateTime deleteDateTime)
        {
            IsLoading = true;
            Dictionary = DataManager.Instance.GetDailyEntriesData();

            if (!Dictionary.ContainsKey(deleteDateTime))
            {
                if (DeleteNotExistingDate != null)
                {
                    DeleteNotExistingDate(this, deleteDateTime);
                }

                IsLoading = false;
                return false;
            }

            Dictionary.Remove(deleteDateTime);
            UpdateDatesList(Dictionary);

            if (DataManager.Instance.SaveDailyEntriesData(Dictionary))
            {
                if (ErrorOccurred != null)
                {
                    ErrorOccurred(this, null);
                }

                IsLoading = false;
                return false;
            }

            IsLoading = false;
            return true;
        }

        #endregion

        #region Private Methods

        private Boolean UpdateDatesList(Dictionary<DateTime, DailyEntry> dailyEntries)
        {
            if (dailyEntries != null)
            {
                DatesList = new ObservableCollection<DateTime>(dailyEntries.Select(x => x.Key).OrderBy(o => o));
                RaisePropertyChanged(String.Empty);
                return true;
            }

            return false;
        }

        #endregion

        #region Properties

        private Dictionary<DateTime, DailyEntry> _dictionary; 
        public Dictionary<DateTime, DailyEntry> Dictionary
        {
            get { return _dictionary; }
            set
            {
                _dictionary = value;
                RaisePropertyChanged(String.Empty);
            }
        }

        /// <summary>
        /// The <see cref="DatesList" /> property's name.
        /// </summary>
        public const string DatesListPropertyName = "DatesList";
        private ObservableCollection<DateTime> _datesList = new ObservableCollection<DateTime>();

        /// <summary>
        /// Sets and gets the QuickiesList property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<DateTime> DatesList
        {
            get
            {
                return _datesList;
            }

            set
            {
                if (_datesList == value)
                {
                    return;
                }

                RaisePropertyChanging(String.Empty);
                _datesList = value;
                RaisePropertyChanged(String.Empty);
            }
        }


        public int NumberOfEntries
        {
            get { return Dictionary.Count; }
        }

        public int NumberOfCompletedEntries
        {
            get { return Dictionary.Count(x => x.Value.IsCompleted); }
        }

        public int NumberOfConsecutiveEntries
        {
            get
            {
                var dates = Dictionary.Select(x => x.Value.EntryDateTimeKey).OrderBy(x => x).ToList();
                if (!dates.Any())
                {
                    return 0;
                }

                var currentConsecutiveStreak = 1;
                var maxConsecutive = 1;
                for (var i = 1; i < dates.Count(); i++)
                {
                    if ((dates[i] - dates[i - 1]).TotalDays <= 1.1)
                    {
                        currentConsecutiveStreak++;
                    }
                    else
                    {
                        currentConsecutiveStreak = 1;
                    }

                    if (currentConsecutiveStreak >= maxConsecutive)
                    {
                        maxConsecutive = currentConsecutiveStreak;
                    }
                }
                return maxConsecutive;
            }
        }

        public int NumberOfConsecutiveCompletedEntries
        {
            get
            {
                var dates = Dictionary.Where(x => x.Value.IsCompleted).Select(x => x.Value.EntryDateTimeKey).OrderBy(x => x).ToList();
                if (!dates.Any())
                {
                    return 0;
                }

                var currentConsecutiveStreak = 1;
                var maxConsecutive = 1;
                for (var i = 1; i < dates.Count(); i++)
                {
                    if ((dates[i] - dates[i - 1]).TotalDays <= 1.1)
                    {
                        currentConsecutiveStreak++;
                    }
                    else
                    {
                        currentConsecutiveStreak = 1;
                    }

                    if (currentConsecutiveStreak >= maxConsecutive)
                    {
                        maxConsecutive = currentConsecutiveStreak;
                    }
                }
                return maxConsecutive;
            }
        }

        public Boolean IsTodayCompleted
        {
            get
            {
                if (Dictionary.ContainsKey(DateTime.Now.Date))
                {
                    if (Dictionary[DateTime.Now.Date].IsCompleted)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        #endregion

        #region EventHandlers

        public EventHandler<DateTime> AddAlreadyAddedDate;
        public EventHandler<DateTime> DeleteNotExistingDate;
        public EventHandler ErrorOccurred;

        #endregion
    }
}
