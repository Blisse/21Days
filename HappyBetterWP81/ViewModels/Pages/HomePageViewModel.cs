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
    public class HomePageViewModel : BasePageViewModel
    {
        #region Properties

        public ObservableCollection<DateTime> MonthEntries { get; set; }
        
        public ObservableCollection<DateTime> DayEntries { get; set; }

        public DateTime SelectedMonth { get; set; }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion

        public HomePageViewModel()
        {
            MonthEntries = new ObservableCollection<DateTime>();
            DayEntries = new ObservableCollection<DateTime>();
        }

        public override void GetData()
        {
            
        }
    }
}
