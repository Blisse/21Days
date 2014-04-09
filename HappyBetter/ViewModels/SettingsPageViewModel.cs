using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyBetter.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the MainPageViewModel class.
        /// </summary>
        public SettingsPageViewModel()
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

            IsLoading = false;
        }

        #endregion
    }
}
