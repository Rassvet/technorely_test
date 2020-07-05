using Prism.Mvvm;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace TechnoRely.Views
{
    public class BaseContentView : ContentPage
    {
        public BaseContentView()
        {
            On<iOS>().SetUseSafeArea(true);
            ViewModelLocator.SetAutowireViewModel(this, true);
        }
    }
}