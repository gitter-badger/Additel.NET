using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using Xamarin.Forms.Platform.WPF;

using SwitchView = Additel.Forms.Controls.SwitchView;
using SwitchViewRenderer = Additel.Forms.Renderers.SwitchViewRenderer;

[assembly: ExportRenderer(typeof(SwitchView), typeof(SwitchViewRenderer))]
