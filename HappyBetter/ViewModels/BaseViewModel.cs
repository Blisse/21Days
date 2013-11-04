using GalaSoft.MvvmLight;

namespace HappyBetter.ViewModels
{
    public class BaseViewModel: ViewModelBase 
    {
        /// <summary>
        /// The <see cref="IsLoading" /> property's name.
        /// </summary>
        public const string IsLoadingPropertyName = "IsLoading";

        private bool _isLoading = false;

        /// <summary>
        /// Sets and gets the IsLoading property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }

            set
            {
                if (_isLoading == value)
                {
                    return;
                }

                RaisePropertyChanging(IsLoadingPropertyName);
                _isLoading = value;
                RaisePropertyChanged(IsLoadingPropertyName);
            }
        }
    }
}
