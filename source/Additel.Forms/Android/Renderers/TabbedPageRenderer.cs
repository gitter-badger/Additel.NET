using Additel.Core;
using Additel.Forms.PlatformConfiguration.AndroidSpecific;
using Android.Content;
using Android.Support.Design.BottomNavigation;
using Android.Support.Design.Widget;
using System;
using System.Collections.Specialized;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using XFTabbedPageRenderer = Xamarin.Forms.Platform.Android.AppCompat.TabbedPageRenderer;

namespace Additel.Forms.Renderers
{
    public class TabbedPageRenderer : XFTabbedPageRenderer
    {
        private readonly FieldInfo _bnvfi;
        private bool _disposed;

        private IPageController PageController
            => Element;

        private bool IsBottomPlacement
            => Element == null
            ? false
            : Element.OnThisPlatform().GetToolbarPlacement() == ToolbarPlacement.Bottom;

        private bool IsBNVAnimationEnabled
            => Element == null
            ? true
            : Element.OnThisPlatform().GetIsBNVAnimationEnabled();

        public TabbedPageRenderer(Context context)
            : base(context)
        {
            _bnvfi = GetType().BaseType.GetField("_bottomNavigationView", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        private void OnChildrenCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            => TryModifyBottomNavigationView();

        private void TryModifyBottomNavigationView()
        {
            if (!IsBottomPlacement ||
                IsBNVAnimationEnabled ||
                !(_bnvfi.GetValue(this) is BottomNavigationView bnv))
                return;

            bnv.ItemHorizontalTranslationEnabled = false;
            bnv.LabelVisibilityMode = LabelVisibilityMode.LabelVisibilityLabeled;
            bnv.DisableScale();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TabbedPage> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                ((IPageController)e.OldElement).InternalChildren.CollectionChanged -= OnChildrenCollectionChanged;
            }
            if (e.NewElement != null)
            {
                TryModifyBottomNavigationView();
                ((IPageController)e.NewElement).InternalChildren.CollectionChanged += OnChildrenCollectionChanged;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (Element != null)
                    {
                        PageController.InternalChildren.CollectionChanged -= OnChildrenCollectionChanged;
                    }
                }

                _disposed = true;
            }

            base.Dispose(disposing);
        }
    }
}
