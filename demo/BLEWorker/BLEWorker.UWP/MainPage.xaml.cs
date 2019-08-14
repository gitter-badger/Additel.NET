using Xamarin.Forms.Platform.UWP;

// https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x804 上介绍了“空白页”项模板

namespace BLEWorker
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : WindowsPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            var initializer = new Initializer();
            var application = new CoreApplication(initializer);
            LoadApplication(application);
        }
    }
}
