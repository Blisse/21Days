using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyBetterWP81.Models;

namespace HappyBetterWP81.ViewModels.Pages
{
    public class MainPageViewModel : BaseViewModel
    {
        #region Public Methods

        public async Task GetData()
        {
            IsLoading = true;

            DatesList = new ObservableCollection<DateTime>
            {
                DateTime.UtcNow.AddDays(-5),
                DateTime.UtcNow.AddDays(-3),
                DateTime.UtcNow.AddDays(-15),
                DateTime.UtcNow.AddDays(-2),
                DateTime.UtcNow.AddDays(-35),
                DateTime.UtcNow.AddDays(-55),
                DateTime.UtcNow.AddDays(-105),
                DateTime.UtcNow.AddDays(-4)
            };

            IsLoading = false;
        }

        #endregion



        private ObservableCollection<DateTime> _datesList;

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
        

    }
}
