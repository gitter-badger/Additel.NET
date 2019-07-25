using Additel.Forms.Renderers;
using Xamarin.Forms;

using Switch = Additel.Forms.Controls.Switch;
using SwitchRenderer = Additel.Forms.Renderers.SwitchRenderer;

[assembly: ExportRenderer(typeof(Switch), typeof(SwitchRenderer))]
[assembly: ExportRenderer(typeof(TabbedPage), typeof(TabbedPageRenderer))]
