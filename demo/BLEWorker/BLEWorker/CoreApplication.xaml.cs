using Prism;
using Prism.Ioc;
using BLEWorker.ViewModels;
using BLEWorker.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.DryIoc;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BLEWorker
{
    public partial class CoreApplication : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public CoreApplication() : this(null) { }

        public CoreApplication(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            var name = "MainPage?createTab=NavigationPage|DevicesPage&createTab=NavigationPage|DiscussPage&createTab=SettingsPage";
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
            containerRegistry.RegisterForNavigation<GIFPage, GIFPageViewModel>();
        }
    }
}
