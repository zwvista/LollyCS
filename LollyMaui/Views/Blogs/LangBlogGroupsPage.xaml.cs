using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LollyCommon;

namespace LollyMaui
{
    public partial class LangBlogGroupsPage : ContentPage
    {
        LangBlogGroupsViewModel vm;

        public LangBlogGroupsPage()
        {
            InitializeComponent();
            BindingContext = vm = new LangBlogGroupsViewModel(AppShell.vmSettings, false);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async Task Edit(MLangBlogGroup item) =>
            await Shell.Current.GoToModalAsync(nameof(LangBlogGroupsDetailPage), item);

        async Task ShowPosts(MLangBlogGroup item)
        {
            vm.SelectedGroupItem = item;
            await Shell.Current.GoToAsync(nameof(LangBlogPostsListPage), vm);
        }

        async void OnItemTapped(object sender, EventArgs e)
        {
            var item = (MLangBlogGroup)((TappedEventArgs)e).Parameter;
            await Edit(item);
        }

        async void OnEditSwipeItemInvoked(object sender, EventArgs e)
        {
            var item = (MLangBlogGroup)((SwipeItem)sender).BindingContext;
            await Edit(item);
        }

        async void OnMoreSwipeItemInvoked(object sender, EventArgs e)
        {
            var item = (MLangBlogGroup)((SwipeItem)sender).BindingContext;
            var a = await DisplayActionSheet("More", "Cancel", null, "Edit", "Browse Web Page");
            switch (a)
            {
                case "Edit":
                    await Edit(item);
                    break;
                case "Show Posts":
                    await ShowPosts(item);
                    break;
            }
        }

        async void IconButton_Clicked(object sender, EventArgs e)
        {
            var item = (MLangBlogGroup)((ImageButton)sender).BindingContext;
            await ShowPosts(item);
        }
    }
}