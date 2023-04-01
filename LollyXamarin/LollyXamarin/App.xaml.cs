using System;
using LollyXamarin.Services;
using System.Threading.Tasks;
using LollyCommon;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace LollyXamarin
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            Plugin.Iconize.Iconize
                .With(new Plugin.Iconize.Fonts.EntypoPlusModule())
                .With(new Plugin.Iconize.Fonts.FontAwesomeRegularModule())
                .With(new Plugin.Iconize.Fonts.FontAwesomeBrandsModule())
                .With(new Plugin.Iconize.Fonts.FontAwesomeSolidModule())
                .With(new Plugin.Iconize.Fonts.IoniconsModule())
                .With(new Plugin.Iconize.Fonts.JamIconsModule())
                .With(new Plugin.Iconize.Fonts.MaterialModule())
                .With(new Plugin.Iconize.Fonts.MeteoconsModule())
                .With(new Plugin.Iconize.Fonts.SimpleLineIconsModule())
                .With(new Plugin.Iconize.Fonts.TypiconsModule())
                .With(new Plugin.Iconize.Fonts.WeatherIconsModule());

            DependencyService.Register<MockDataStore>();
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

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
