using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UIWorker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewsPage : ContentPage
    {
        public ViewsPage()
        {
            InitializeComponent();
        }

        private async void OnSwitchViewButtonClicked(object sender, EventArgs e)
        {
            var view = new SwitchViewPage();
            await Navigation.PushAsync(view);
        }

        private async void OnGIFViewButtonClicked(object sender, EventArgs e)
        {
            var view = new GIFViewPage();
            await Navigation.PushAsync(view);
        }
    }
}