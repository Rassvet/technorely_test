using Acr.UserDialogs;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using TechnoRely.Services;
using TechnoRely.Services.Storage;
using TechnoRely.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TechnoRely
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
            InitializeComponent();
        }
        
        #region Overrieds

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            #region Register navigation pages

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<PopularArtistsView>();
            containerRegistry.RegisterForNavigation<ArtistDetailView>();

            #endregion

            #region Register Services

            containerRegistry.RegisterSingleton<IRestService, RestService>();
            containerRegistry.RegisterSingleton<IStorageService, StorageService>();

            #endregion

            #region Register instance

            containerRegistry.RegisterInstance(UserDialogs.Instance);

            #endregion
        }

        protected override async void OnInitialized()
        {
            MainPage = new NavigationPage(new Page());
            await NavigationService.NavigateAsync(nameof(PopularArtistsView));
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        #endregion
    }
}
