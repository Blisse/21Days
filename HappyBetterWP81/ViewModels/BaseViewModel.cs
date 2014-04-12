using GalaSoft.MvvmLight;

namespace HappyBetterWP81.ViewModels
{
    public class BaseViewModel : ViewModelBase
    {
        public BaseViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (_isLoading == value)
                {
                    return;
                }
                _isLoading = value;
                RaisePropertyChanged();
            }
        }
    }
}