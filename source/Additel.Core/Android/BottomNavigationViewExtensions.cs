using Android.Runtime;
using Android.Support.Design.Internal;
using Android.Support.Design.Widget;
using Android.Widget;

namespace Additel.Core
{
    public static class BottomNavigationViewExtensions
    {
        public static void SetLabelVisibilityMode(this BottomNavigationView bnv, int mode)
        {
            var bnmv = (BottomNavigationMenuView)bnv.GetChildAt(0);
            using (var field = bnmv.Class.GetDeclaredField("labelVisibilityMode"))
            {
                field.Accessible = true;
                field.SetInt(bnmv, mode);
                field.Accessible = false;
            }
        }

        public static void SetShifting(this BottomNavigationView bnv, bool shifting)
        {
            var bnmv = (BottomNavigationMenuView)bnv.GetChildAt(0);
            for (int i = 0; i < bnmv.ChildCount; i++)
            {
                var bniv = (BottomNavigationItemView)bnmv.GetChildAt(i);
                bniv.SetShifting(shifting);
                bniv.SetChecked(bniv.ItemData.IsChecked);
            }
        }

        public static void DisableScale(this BottomNavigationView bnv)
        {
            var bnmv = (BottomNavigationMenuView)bnv.GetChildAt(0);
            var fontScale = bnv.Resources.DisplayMetrics.ScaledDensity;
            for (int i = 0; i < bnmv.ChildCount; i++)
            {
                var bniv = (BottomNavigationItemView)bnmv.GetChildAt(i);
                using (var field0 = bniv.Class.GetDeclaredField("largeLabel"))
                using (var field1 = bniv.Class.GetDeclaredField("smallLabel"))
                using (var field2 = bniv.Class.GetDeclaredField("shiftAmount"))
                using (var field3 = bniv.Class.GetDeclaredField("scaleUpFactor"))
                using (var field4 = bniv.Class.GetDeclaredField("scaleDownFactor"))
                {
                    field0.Accessible = true;
                    field1.Accessible = true;
                    field2.Accessible = true;
                    field3.Accessible = true;
                    field4.Accessible = true;

                    var tvLarge = field0.Get(bniv).JavaCast<TextView>();
                    var tvSmall = field1.Get(bniv).JavaCast<TextView>();
                    tvLarge.TextSize = tvSmall.TextSize / fontScale;
                    field2.Set(bniv, 0);
                    field3.Set(bniv, 1F);
                    field4.Set(bniv, 1F);

                    field0.Accessible = false;
                    field1.Accessible = false;
                    field2.Accessible = false;
                    field3.Accessible = false;
                    field4.Accessible = false;
                }
                bniv.SetChecked(bniv.ItemData.IsChecked);
            }
        }
    }
}
