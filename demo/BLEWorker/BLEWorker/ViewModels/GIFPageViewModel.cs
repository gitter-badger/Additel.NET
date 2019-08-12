using Prism.Navigation;
using System.Collections.ObjectModel;

namespace BLEWorker.ViewModels
{
    public class GIFPageViewModel : BaseViewModel
    {
        public ObservableCollection<GIFViewModel> Items { get; }

        public GIFPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "GIF";

            Items = new ObservableCollection<GIFViewModel>();
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            // BUG: 必须在这里给 Items 赋值，否则 RowHeight 绑定不会生效
            for (int i = 0; i < 5; i++)
            {
                var item = new GIFViewModel(NavigationService);
                Items.Add(item);
            }
        }
    }
}
