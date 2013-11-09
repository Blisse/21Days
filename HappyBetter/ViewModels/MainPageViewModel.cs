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
            var dictionary = DataManager.Instance.GetDailyEntriesData() ?? new Dictionary<DateTime, DailyEntry>();

            DatesList = new ObservableCollection<DateTime>(dictionary.Select(x => x.Key));

            if (dictionary.Count == 0)
            {
                AddToDatesList(DateTime.Now);
            }
        }

        public Boolean AddToDatesList(DateTime enterDateTime)
        {
            var dictionary = DataManager.Instance.GetDailyEntriesData() ?? new Dictionary<DateTime, DailyEntry>();

            if (dictionary.ContainsKey(enterDateTime))
            {
                return false;
            }

            var dailyEntry = new DailyEntry(enterDateTime);
            dictionary.Add(enterDateTime, dailyEntry);

            if (DataManager.Instance.SaveDailyEntriesData(dictionary))
            {
                DatesList.Add(enterDateTime);
                return true;
            }

            return false;
        }

        public Boolean DeleteFromDatesList(DateTime deleteDateTime)
        {
            var dictionary = DataManager.Instance.GetDailyEntriesData();

            if (dictionary == null || !dictionary.ContainsKey(deleteDateTime))
            {
                return false;
            }

            dictionary.Remove(deleteDateTime);

            if (DataManager.Instance.SaveDailyEntriesData(dictionary))
            {
                DatesList.Remove(deleteDateTime);
                return true;
            }

            return false;
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
    }
}
