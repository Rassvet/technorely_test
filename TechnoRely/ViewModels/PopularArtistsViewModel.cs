using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Newtonsoft.Json;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using TechnoRely.Models;
using TechnoRely.Services;
using TechnoRely.Services.Storage;
using TechnoRely.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TechnoRely.ViewModels
{
    public class PopularArtistsViewModel : BaseViewModel
    {
        #region Private fields

        private readonly IRestService _restService;
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _pageDialogService;
        private readonly IUserDialogs _userDialogs;
        private readonly IStorageService _storageService;
        
        #endregion
        public PopularArtistsViewModel(IRestService restService, INavigationService navigationService, IPageDialogService pageDialogService, IUserDialogs userDialogs, IStorageService storageService)
        {
            _restService = restService;
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _userDialogs = userDialogs;
            _storageService = storageService;
        }

        #region Public properties

        private string _selectedCountry;
        public string SelectedCountry
        {
            get => _selectedCountry;
            set => SetProperty(ref _selectedCountry, value);
        }
        
        private List<string> _countryList;
        public List<string> CountryList
        {
            get => _countryList;
            set => SetProperty(ref _countryList, value);
        }
        
        private ObservableCollection<ArtistModel> _artistsSource;
        public ObservableCollection<ArtistModel> ArtistsSource
        {
            get => _artistsSource;
            set => SetProperty(ref _artistsSource, value);
        }
        
        private ArtistModel _selectedArtistModel;
        public ArtistModel SelectedArtistModel
        {
            get => _selectedArtistModel;
            set
            {
                SetProperty(ref _selectedArtistModel, value);
                if(_selectedArtistModel == null)
                    return;
                
                NavigateToDetail(_selectedArtistModel);
            }
        }
        
        private Color _navbarColor;
        public Color NavbarColor
        {
            get => _navbarColor;
            set => SetProperty(ref _navbarColor, value);
        }
        
        private bool _isSwitcherVisible;
        public bool IsSwitcherVisible
        {
            get => _isSwitcherVisible;
            set => SetProperty(ref _isSwitcherVisible, value);
        }
        
        private bool _isToggle;
        public bool IsToggle
        {
            get => _isToggle;
            set
            {
                SetProperty(ref _isToggle, value);
                
                if(Connectivity.NetworkAccess != NetworkAccess.None)
                    IsOffline = !value;
            }
        }

        #endregion

        #region Command

        private ICommand _selectCountryCommand;
        public ICommand SelectCountryCommand =>
                    _selectCountryCommand ?? (_selectCountryCommand = new Command(async () => await OnSelectCountryAsync()));
        
        private async Task OnSelectCountryAsync()
        {
            var answer = await _pageDialogService.DisplayActionSheetAsync ("Choose a country", "Cancel", null, "Ukraine", "Netherlands", "Portugal");
            
            if(answer == SelectedCountry || answer == null || answer == "Cancel")
                return;

            SelectedCountry = answer;
            
            _userDialogs.ShowLoading();
            
            await GetArtists(SelectedCountry);
            
            _userDialogs.HideLoading();
        }

        #endregion

        #region Overrides

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
            
            IsToggle = true;
            #if DEBUG
            NavbarColor = Color.Red;
            IsSwitcherVisible = true;
            #elif RELEASE
            NavbarColor = Color.Blue;
            #endif
            
            CountryList = new List<string>()
            {
                "Ukraine",
                "Netherlands",
                "Portugal"
            };
            SelectedCountry = CountryList[0];

            GetArtists(SelectedCountry);
        }
        
        #endregion

        #region Private methods

        private async Task GetArtists(string selectedCountry)
        {
            SelectedArtistModel = null;

            if (IsOffline || !IsToggle)
            {
                LoadFromStorage(selectedCountry);
                return;
            }

            var artists = await _restService.GetArtistsAsync(selectedCountry);

            if (artists?.TopArtists?.Artist == null)
                ArtistsSource.Clear();
            else
            {
                ArtistsSource = new ObservableCollection<ArtistModel>(artists.TopArtists.Artist.ToList());
                _storageService.Set(selectedCountry, ArtistsSource);
            }
        }

        private void LoadFromStorage(string selectedCountry)
        {
            ArtistsSource = _storageService.Get<ObservableCollection<ArtistModel>>(selectedCountry);
        }

        private async void NavigateToDetail(ArtistModel selectedArtistModel)
        {
            var param = CreateNavDetailParam(selectedArtistModel);
            
            await _navigationService.NavigateAsync(nameof(ArtistDetailView), param);
        }

        private NavigationParameters CreateNavDetailParam(ArtistModel selectedArtistModel)
        {
            return new NavigationParameters()
            {
                {Constants.DetailArtistNavKey, selectedArtistModel},
                {Constants.IsOfflineNavKey, IsOffline}
            };
        }

        #endregion
    }
}