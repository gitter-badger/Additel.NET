using Prism.Mvvm;

namespace BLEWorker.ViewModels
{
    public class BaseViewModel : BindableBase
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}
