using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Xamarin.Forms.Platform.UWP;

using SwitchView = Additel.Forms.Controls.SwitchView;
using SwitchRenderer = Additel.Forms.Renderers.SwitchRenderer;

[assembly: ExportRenderer(typeof(SwitchView), typeof(SwitchRenderer))]
