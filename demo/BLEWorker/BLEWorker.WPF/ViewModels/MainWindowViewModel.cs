using Prism.Commands;
using System.Collections.ObjectModel;

namespace BLEWorker.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ObservableCollection<GIFViewModel> Items { get; }

        public MainWindowViewModel()
        {
            Title = "BLE Worker";
            Items = new ObservableCollection<GIFViewModel>()
            {
                new GIFViewModel(),
                new GIFViewModel(),
                new GIFViewModel(),
                new GIFViewModel(),
                new GIFViewModel()
            };
        }

        private DelegateCommand _controlItemsCommand;
        public DelegateCommand ControlItemsCommand =>
            _controlItemsCommand ?? (_controlItemsCommand = new DelegateCommand(ExecuteControlItemsCommand));

        void ExecuteControlItemsCommand()
        {
            if (Items.Count != 0)
            {
                Items.Clear();
            }
            else
            {
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
                Items.Add(new GIFViewModel());
            }
        }
    }
}
