using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyBetterWP81.Models;

namespace HappyBetterWP81.ViewModels.Pages
{
    public class SelectDatePageViewModel : BasePageViewModel
    {
        private ObservableCollection<DateTime> _availableDatesList = new ObservableCollection<DateTime>();

        /// <summary>
        /// Sets and gets the AvailableDatesList property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<DateTime> AvailableDatesList
        {
            get
            {
                return _availableDatesList;
            }

            set
            {
                if (_availableDatesList == value)
                {
                    return;
                }

                _availableDatesList = value;
                RaisePropertyChanged();
            }
        }

        public DateTimeOffset MinimumYearOffset
        {
            get
            {
                var minimumYear = DateTimeOffset.UtcNow.AddYears(-5);
                return minimumYear;
            }
        }

        public DateTimeOffset MaximumYearOffset
        {
            get
            {
                var maximumYear = DateTimeOffset.UtcNow.AddYears(5);
                return maximumYear;
            }
        }

        private readonly HbDataSource _hbDataSource;

        public SelectDatePageViewModel()
        {
            _hbDataSource = new HbDataSource();
        }

        public override void GetData()
        {

        }

        public void SetDates()
        {
            var chosenDates = _hbDataSource.GetDayEntriesSorted();

        }
    }
}
