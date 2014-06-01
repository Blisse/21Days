using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using HappyBetterWP81.Models;

namespace HappyBetterWP81.ViewModels.Pages
{
    public class HomePageViewModel : BasePageViewModel
    {
        #region Properties

        private ObservableCollection<DateTime> _monthEntries = new ObservableCollection<DateTime>();

        /// <summary>
        /// Sets and gets the MonthEntries property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<DateTime> MonthEntries
        {
            get
            {
                return _monthEntries;
            }

            set
            {
                if (_monthEntries == value)
                {
                    return;
                }

                _monthEntries = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<DateTime> _daysOfMonthEntries = new ObservableCollection<DateTime>();

        /// <summary>
        /// Sets and gets the DaysOfMonthEntries property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<DateTime> DaysOfMonthEntries
        {
            get
            {
                return _daysOfMonthEntries;
            }

            set
            {
                if (_daysOfMonthEntries == value)
                {
                    return;
                }

                _daysOfMonthEntries = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand<DateTime> FilterDaysOfMonthCommand { get; private set; }

        public RelayCommand SetMonthsCommand { get; private set; }

        private readonly HbDataSource _hbDataSource;

        #endregion

        #region Private Methods

        #endregion

        public HomePageViewModel()
        {
            FilterDaysOfMonthCommand = new RelayCommand<DateTime>(FilterDaysOfMonthByDateTime);
            SetMonthsCommand = new RelayCommand(SetMonths);

            _hbDataSource = new HbDataSource();
        }

        public override void GetData()
        {
            SetMonths();
        }

        public void SetMonths()
        {
            var allEntries = _hbDataSource.GetDayEntriesSorted();
            var allDates = allEntries.Select(entry => entry.EntryDay);
            var allMonths = allDates.GroupBy(date => date.Month).Select(group => group.First());

            MonthEntries = new ObservableCollection<DateTime>(allMonths);
        }

        public void FilterDaysOfMonthByDateTime(DateTime monthDateTime)
        {
            var allEntries = _hbDataSource.GetDayEntries();
            var filterMonth = monthDateTime.Month;
            var filterYear = monthDateTime.Year;
            var filteredEntries = allEntries.Where(entry => entry.EntryDay.Month == filterMonth
                                                        && entry.EntryDay.Year == filterYear);
            var filteredDays = filteredEntries.Select(entry => entry.EntryDay);

            DaysOfMonthEntries = new ObservableCollection<DateTime>(filteredDays);
        }
    }
}
