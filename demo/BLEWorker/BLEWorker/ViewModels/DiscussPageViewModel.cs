using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLEWorker.ViewModels
{
    public class DiscussPageViewModel : BaseViewModel
    {
        public DiscussPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "讨论";
        }
    }
}
