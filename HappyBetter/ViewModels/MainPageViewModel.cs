using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public void GetData()
        {
            Dictionary = DataManager.Instance.GetDailyEntriesData() ?? new Dictionary<DateTime, DailyEntry>();

            DatesList = new ObservableCollection<DateTime>(Dictionary.Select(x => x.Key));

            if (Dictionary.Count == 0)
            {
                AddToDatesList(DateTime.Now.Date);
            }
        }

        public Boolean AddToDatesList(DateTime enterDateTime)
        {
            Dictionary = DataManager.Instance.GetDailyEntriesData() ?? new Dictionary<DateTime, DailyEntry>();

            if (Dictionary.ContainsKey(enterDateTime))
            {
                RaiseAddAlreadyAddedDate(this, enterDateTime);
                return false;
            }

            var dailyEntry = new DailyEntry(enterDateTime);
            Dictionary.Add(enterDateTime, dailyEntry);

            if (DataManager.Instance.SaveDailyEntriesData(Dictionary))
            {
                DatesList.Add(enterDateTime);
                return true;
            }

            return false;
        }

        public Boolean DeleteFromDatesList(DateTime deleteDateTime)
        {
            Dictionary = DataManager.Instance.GetDailyEntriesData();

            if (Dictionary == null || !Dictionary.ContainsKey(deleteDateTime))
            {
                return false;
            }

            Dictionary.Remove(deleteDateTime);

            if (DataManager.Instance.SaveDailyEntriesData(Dictionary))
            {
                DatesList.Remove(deleteDateTime);
                return true;
            }

            return false;
        }

        private void RaiseAddAlreadyAddedDate(object sender, DateTime dateTime)
        {
            if (AddAlreadyAddedDate != null)
            {
                AddAlreadyAddedDate(sender, dateTime);
            }
        }

        public EventHandler<DateTime> AddAlreadyAddedDate;

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

                RaisePropertyChanging(DatesListPropertyName);
                _datesList = value;
                RaisePropertyChanged(DatesListPropertyName);
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


    }
}
