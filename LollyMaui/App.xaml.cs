using LollyCommon;

namespace LollyMaui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            var shell = MainPage as AppShell;

            CommonApi.UserId = Preferences.Get("userid", "");
            if (string.IsNullOrEmpty(CommonApi.UserId))
                shell.OnMenuItemClicked(null, null);
            else
                Task.Run(async () =>
                {
                    await AppShell.vmSettings.GetData();
                });
        }
    }
}
