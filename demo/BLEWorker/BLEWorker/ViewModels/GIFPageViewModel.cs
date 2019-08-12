using Additel.Forms;
using Prism.Commands;
using Prism.Navigation;

namespace BLEWorker.ViewModels
{
    public class GIFPageViewModel : BaseViewModel
    {
        private Stretch _stretch;
        public Stretch Stretch
        {
            get { return _stretch; }
            set { SetProperty(ref _stretch, value); }
        }

        public GIFPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "GIF";
        }

        private DelegateCommand _switchStretchCommand;
        public DelegateCommand SwitchStretchCommand =>
            _switchStretchCommand ?? (_switchStretchCommand = new DelegateCommand(ExecuteSwitchStretchCommand));

        void ExecuteSwitchStretchCommand()
        {
            switch (Stretch)
            {
                case Stretch.None:
                    Stretch = Stretch.Fill;
                    break;
                case Stretch.Fill:
                    Stretch = Stretch.Uniform;
                    break;
                case Stretch.Uniform:
                    Stretch = Stretch.UniformToFill;
                    break;
                case Stretch.UniformToFill:
                    Stretch = Stretch.None;
                    break;
            }
        }
    }
}
