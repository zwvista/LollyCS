using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LollyCommon;
using ReactiveUI;
using System.Reactive.Linq;

namespace LollyMaui
{
    public partial class UnitBlogPostsPage : ContentPage
    {
        UnitBlogPostViewModel vm;

        public UnitBlogPostsPage()
        {
            InitializeComponent();
            BindingContext = vm = new UnitBlogPostViewModel(AppShell.vmSettings, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void WebView_SwipedLeft(object sender, SwipedEventArgs e) =>
            vm.Next(-1);

        void WebView_SwipedRight(object sender, SwipedEventArgs e) =>
            vm.Next(1);
    }
}