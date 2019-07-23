using Android.Content;

namespace Additel.Core
{
    public static class ContextExtensions
    {
        public static float ToPixels(this Context context, double value)
        {
            var scale = context.Resources.DisplayMetrics.Density;
            var converted = value * scale;

            return (float)converted;
        }

        public static double FromPixels(this Context context, float value)
        {
            var scale = context.Resources.DisplayMetrics.Density;
            var converted = value / scale;

            return converted;
        }
    }
}
