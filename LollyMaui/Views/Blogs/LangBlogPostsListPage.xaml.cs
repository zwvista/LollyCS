using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LollyCommon;

namespace LollyMaui
{
    public partial class LangBlogPostsListPage : ContentPage, IPageNavigate
    {
        LangBlogGroupsViewModel vm = null!;

        public LangBlogPostsListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public void OnPageNavigated(object navigationData)
        {
            vm = (LangBlogGroupsViewModel)navigationData;
            BindingContext = vm;
        }

        async Task Edit(MLangBlogPost item) =>
            await Shell.Current.GoToModalAsync(nameof(LangBlogPostsDetailPage), item);

        async Task ShowContent(MLangBlogPost item)
        {
            vm.SelectedPostItem = item;
            var index = vm.PostItems.IndexOf(item);
            var (start, end) = CommonApi.GetPreferredRangeFromArray(index, vm.PostItems.Count, 50);
            var items = vm.PostItems.ToList().Slice(start, end);
            await Shell.Current.GoToAsync(nameof(LangBlogPostsContentPage), new LangBlogPostsContentViewModel(vm, items, index));
        }

        async void OnItemTapped(object sender, EventArgs e)
        {
            var item = (MLangBlogPost)((TappedEventArgs)e).Parameter;
            await Edit(item);
        }

        async void OnEditSwipeItemInvoked(object sender, EventArgs e)
        {
            var item = (MLangBlogPost)((SwipeItem)sender).BindingContext;
            await Edit(item);
        }

        async void OnMoreSwipeItemInvoked(object sender, EventArgs e)
        {
            var item = (MLangBlogPost)((SwipeItem)sender).BindingContext;
            var a = await DisplayActionSheet("More", "Cancel", null, "Edit", "Show Content");
            switch (a)
            {
                case "Edit":
                    await Edit(item);
                    break;
                case "Show Content":
                    await ShowContent(item);
                    break;
            }
        }

        async void IconButton_Clicked(object sender, EventArgs e)
        {
            var item = (MLangBlogPost)((ImageButton)sender).BindingContext;
            await ShowContent(item);
        }
    }
}