using Prism.Navigation;

namespace BLEWorker.ViewModels
{
    public class SettingsPageViewModel : BaseViewModel
    {
        public SettingsPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "设置";
        }
    }
}
