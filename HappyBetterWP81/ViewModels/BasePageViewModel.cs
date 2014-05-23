using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace HappyBetterWP81.ViewModels
{
    public abstract class BasePageViewModel : ViewModelBase
    {
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

        public abstract void GetData();
    }
}