using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyBetterWP81.Managers;
using HappyBetterWP81.Models;

namespace HappyBetterWP81.ViewModels.Pages
{
    public class MainPageViewModel : BaseViewModel
    {

        #region Public Methods

        public void GetData()
        {
            IsLoading = true;

            Dictionary = DataManager.Instance.GetDailyEntriesData();
            UpdateDatesList(Dictionary);

            IsLoading = false;
        }

        public Boolean ClearData()
        {
            IsLoading = true;

            if (!DataManager.Instance.ClearDailyEntriesData())
            {
                IsLoading = false;
                return false;
            }

            Dictionary = DataManager.Instance.GetDailyEntriesData();
            UpdateDatesList(Dictionary);

            if (!DataManager.Instance.SaveDailyEntriesData(Dictionary))
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

            if (!DataManager.Instance.SaveDailyEntriesData(Dictionary))
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

            if (!DataManager.Instance.SaveDailyEntriesData(Dictionary))
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
                RaisePropertyChanged();

                if (DatesListChanged != null)
                {
                    DatesListChanged(this, null);
                }
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
                RaisePropertyChanged();
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

                _datesList = value;
                RaisePropertyChanged();
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
        public EventHandler DatesListChanged;

        #endregion
    }
}
