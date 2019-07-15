using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using Xamarin.Forms.Platform.WPF;

using Switch = Additel.Forms.Controls.Switch;
using SwitchRenderer = Additel.Forms.Renderers.SwitchRenderer;

[assembly: ExportRenderer(typeof(Switch), typeof(SwitchRenderer))]
