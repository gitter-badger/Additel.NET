using Xamarin.Forms;

using XFTabbedPage = Xamarin.Forms.TabbedPage;

namespace Additel.Forms.PlatformConfiguration.AndroidSpecific
{
    using Android = Xamarin.Forms.PlatformConfiguration.Android;

    public static class TabbedPage
    {
        public static bool GetIsBNVAnimationEnabled(BindableObject obj)
            => (bool)obj.GetValue(IsBNVAnimationEnabledProperty);

        public static void SetIsBNVAnimationEnabled(BindableObject obj, bool value)
            => obj.SetValue(IsBNVAnimationEnabledProperty, value);

        // Using a BindableProperty as the backing store for IsBNVAnimationEnabled.  This enables animation, styling, binding, etc...
        public static readonly BindableProperty IsBNVAnimationEnabledProperty
            = BindableProperty.CreateAttached(
                "IsBNVAnimationEnabled",
                typeof(bool),
                typeof(TabbedPage),
                true);

        public static bool GetIsBNVAnimationEnabled(this IPlatformElementConfiguration<Android, XFTabbedPage> config)
            => GetIsBNVAnimationEnabled(config.Element);

        public static IPlatformElementConfiguration<Android, XFTabbedPage> SetIsBNVAnimationEnabled(this IPlatformElementConfiguration<Android, XFTabbedPage> config, bool value)
        {
            SetIsBNVAnimationEnabled(config.Element, value);
            return config;
        }
    }
}
