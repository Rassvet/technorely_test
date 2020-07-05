using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Essentials;

namespace TechnoRely.ViewModels
{
    public class BaseViewModel : BindableBase, IInitialize, INavigationAware
    {
        #region Public properties
        
        private bool _isOffline;
        public bool IsOffline
        {
            get => _isOffline;
            set => SetProperty(ref _isOffline, value);
        }
        
        #endregion
        
        #region Public methods

        public virtual void Initialize(INavigationParameters parameters)
        {
            CheckConnectivity();
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
            Connectivity.ConnectivityChanged -= ConnectivityOnConnectivityChanged;
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
            Connectivity.ConnectivityChanged += ConnectivityOnConnectivityChanged;
        }
        
        #endregion

        #region Private methods

        private void ConnectivityOnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            CheckConnectivity();
        }

        private void CheckConnectivity()
        {
            IsOffline = Connectivity.NetworkAccess == NetworkAccess.None;
        }

        #endregion

    }
}