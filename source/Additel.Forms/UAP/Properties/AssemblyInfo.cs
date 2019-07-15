using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Xamarin.Forms.Platform.UWP;

using Switch = Additel.Forms.Controls.Switch;
using SwitchRenderer = Additel.Forms.Renderers.SwitchRenderer;

[assembly: ExportRenderer(typeof(Switch), typeof(SwitchRenderer))]
