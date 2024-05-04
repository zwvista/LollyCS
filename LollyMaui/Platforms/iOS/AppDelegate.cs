using Foundation;

namespace LollyMaui
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
        // https://stackoverflow.com/questions/78218327/got-a-sigabrt-while-executing-native-code-when-i-tap-in-an-entry
    }
}
