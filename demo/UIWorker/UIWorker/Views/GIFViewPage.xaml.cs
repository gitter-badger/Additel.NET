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
    public partial class GIFViewPage : ContentPage
    {
        public GIFViewPage()
        {
            InitializeComponent();
        }

        private void OnSwitchButtonClicked(object sender, EventArgs e)
        {
            switch (view.Stretch)
            {
                case Additel.Forms.Stretch.None:
                    view.Stretch = Additel.Forms.Stretch.Fill;
                    break;
                case Additel.Forms.Stretch.Fill:
                    view.Stretch = Additel.Forms.Stretch.Uniform;
                    break;
                case Additel.Forms.Stretch.Uniform:
                    view.Stretch = Additel.Forms.Stretch.UniformToFill;
                    break;
                case Additel.Forms.Stretch.UniformToFill:
                    view.Stretch = Additel.Forms.Stretch.None;
                    break;
            }
        }
    }
}