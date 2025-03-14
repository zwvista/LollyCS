using LollyCommon;

namespace LollyMaui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }
        // https://stackoverflow.com/questions/79262762/net-maui-application-class-mainpage-is-obsolete
        protected override Window CreateWindow(IActivationState? activationState)
        {
            var shell = new AppShell();
            var window = new Window(shell);
            window.Created += (s, e) =>
            {
                CommonApi.UserId = Preferences.Get("userid", "");
                if (string.IsNullOrEmpty(CommonApi.UserId))
                    shell.OnMenuItemClicked(null, null);
                else
                    Task.Run(async () =>
                    {
                        await AppShell.vmSettings.GetData();
                    });
            };
            return window;
        }
    }
}
