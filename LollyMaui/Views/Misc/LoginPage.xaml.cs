using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LollyCommon;

namespace LollyMaui
{
    public partial class LoginPage : ContentPage
    {
        LoginViewModel vm = new LoginViewModel();
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = vm;
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            CommonApi.UserId = await vm.Login();
            if (string.IsNullOrEmpty(CommonApi.UserId))
                await DisplayAlert("Wrong username or password!", "Login", "OK");
            else
            {
                MauiCommon.SaveUserId();
                _ = Task.Run(async () =>
                {
                    await AppShell.vmSettings.GetData();
                });
                await Shell.Current.GoToAsync($"//{nameof(SearchPage)}");
            }
        }
    }
}