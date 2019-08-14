using Additel.SkiaViews;
using Prism.Commands;

namespace BLEWorker.ViewModels
{
    public class GIFViewModel : BaseViewModel
    {
        private SKStretch _stretch;
        public SKStretch Stretch
        {
            get { return _stretch; }
            set { SetProperty(ref _stretch, value); }
        }

        public GIFViewModel()
        {

        }

        private DelegateCommand _switchStretchCommand;
        public DelegateCommand SwitchStretchCommand =>
            _switchStretchCommand ?? (_switchStretchCommand = new DelegateCommand(ExecuteSwitchStretchCommand));

        void ExecuteSwitchStretchCommand()
        {
            switch (Stretch)
            {
                case SKStretch.None:
                    Stretch = SKStretch.Fill;
                    break;
                case SKStretch.Fill:
                    Stretch = SKStretch.Uniform;
                    break;
                case SKStretch.Uniform:
                    Stretch = SKStretch.UniformToFill;
                    break;
                case SKStretch.UniformToFill:
                    Stretch = SKStretch.None;
                    break;
            }
        }
    }
}
