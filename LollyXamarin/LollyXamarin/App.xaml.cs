using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LollyXamarin.Services;
using LollyXamarin.Views;

namespace LollyXamarin
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            Plugin.Iconize.Iconize
                      .With(new Plugin.Iconize.Fonts.FontAwesomeRegularModule())
                      .With(new Plugin.Iconize.Fonts.FontAwesomeBrandsModule())
                      .With(new Plugin.Iconize.Fonts.FontAwesomeSolidModule());

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
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
