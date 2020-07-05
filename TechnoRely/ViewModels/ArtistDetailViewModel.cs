using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Prism.Navigation;
using Prism.Services.Dialogs;
using TechnoRely.Models;
using TechnoRely.Services;
using TechnoRely.Services.Storage;
using Xamarin.Essentials;

namespace TechnoRely.ViewModels
{
    public class ArtistDetailViewModel : BaseViewModel
    {
        #region Private fields

        private readonly IRestService _restService;
        private readonly IStorageService _storageService;

        #endregion
        
        public ArtistDetailViewModel(IRestService restService, IStorageService storageService)
        {
            _restService = restService;
            _storageService = storageService;
        }

        #region Public properties

        private ArtistModel _artist;
        public ArtistModel Artist
        {
            get => _artist;
            set => SetProperty(ref _artist, value);
        }
        
        private ObservableCollection<AlbumModel> _albumsSource;
        public ObservableCollection<AlbumModel> AlbumsSource
        {
            get => _albumsSource;
            set => SetProperty(ref _albumsSource, value);
        }

        #endregion

        #region Overrieds

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey(Constants.IsOfflineNavKey) &&
                parameters[Constants.IsOfflineNavKey] is bool offline)
            {
                IsOffline = offline;
            }

            if (parameters.ContainsKey(Constants.DetailArtistNavKey) &&
                parameters[Constants.DetailArtistNavKey] is ArtistModel model)
            {
                Artist = model;
                GetAlbums(Artist.Name);
            }
        }
        
        #endregion

        #region Private methods

        private async Task GetAlbums(string artistName)
        {
            if (IsOffline)
            {
                AlbumsSource = _storageService.Get<ObservableCollection<AlbumModel>>(artistName);
                return;
            }
            
            var albums = await _restService.GetAlbumsAsync(artistName);
            
            if(albums?.Topalbums?.Album == null)
                return;
            
            AlbumsSource = new ObservableCollection<AlbumModel>(albums?.Topalbums?.Album.ToList());
            _storageService.Set(artistName, AlbumsSource);
        }

        #endregion
    }
}
