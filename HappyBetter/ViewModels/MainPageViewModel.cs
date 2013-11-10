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
                AddToDatesList(DateTime.Now);
            }
        }

        public Boolean AddToDatesList(DateTime enterDateTime)
        {
            Dictionary = DataManager.Instance.GetDailyEntriesData() ?? new Dictionary<DateTime, DailyEntry>();

            if (Dictionary.ContainsKey(enterDateTime))
            {
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

        public Dictionary<DateTime, DailyEntry> Dictionary { get; set; }

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
    }
}
