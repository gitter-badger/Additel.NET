using Prism;
using Prism.Ioc;
using BLEWorker.ViewModels;
using BLEWorker.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BLEWorker
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var name = "MainPage?createTab=DevicesPage&createTab=DiscussPage&createTab=SettingsPage";
            await NavigationService.NavigateAsync(name);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<DevicesPage, DevicesPageViewModel>();
            containerRegistry.RegisterForNavigation<DevicePage, DevicePageViewModel>();
            containerRegistry.RegisterForNavigation<DiscussPage, DiscussPageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
        }
    }
}
