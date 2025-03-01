using Android.App;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;
using Serilog;

namespace MicroApp.Android;

[Activity(
    Label = "MicroApp.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        Log.Logger = new LoggerConfiguration().WriteTo.AndroidLog().CreateLogger();
        return base.CustomizeAppBuilder(builder)
            .WithInterFont();
    }
}