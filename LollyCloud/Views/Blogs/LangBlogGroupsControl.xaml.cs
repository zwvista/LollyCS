using CefSharp;
using LollyCommon;
using ReactiveUI;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace LollyCloud
{
    /// <summary>
    /// LangBlogGroupsControl.xaml の相互作用ロジック
    /// </summary>
    public partial class LangBlogGroupsControl : UserControl, ILollySettings
    {
        string originalText = "";
        LangBlogGroupsViewModel vm;
        BlogPostEditService editService = new BlogPostEditService();
        LangBlogPostContentDataStore contentDS = new LangBlogPostContentDataStore();

        public LangBlogGroupsControl()
        {
            InitializeComponent();
            // Disable image loading
            // wbBlog.BrowserSettings.ImageLoading = CefState.Disabled;
            OnSettingsChanged();
        }

        public async Task OnSettingsChanged()
        {
            DataContext = vm = new LangBlogGroupsViewModel(MainWindow.vmSettings, true);
            vm.WhenAnyValue(x => x.PostContent).Subscribe(v => wbBlog.LoadLargeHtml(editService.MarkedToHtml(v, "\n")));
        }
        void OnBeginEdit(object sender, DataGridBeginningEditEventArgs e) =>
            originalText = DataGridHelper.OnBeginEditCell(e);
        void OnEndEdit(object sender, DataGridCellEditEndingEventArgs e) =>
            DataGridHelper.OnEndEditCell(sender, e, originalText, null, null, async item =>
            {
                if (sender == dgGroups)
                    await vm.UpdateGroup((MLangBlogGroup)item);
                else
                    await vm.UpdateBlog((MLangBlogPost)item);
            });
        void dgGroups_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            miEditGroup_Click(sender, null);
        }
        void miAddGroup_Click(object sender, RoutedEventArgs e)
        {
            dgGroups.CancelEdit();
            var dlg = new LangBlogGroupsDetailDlg(Window.GetWindow(this), vm.NewGroup(), vm);
            dlg.ShowDialog();
        }
        void miEditGroup_Click(object sender, RoutedEventArgs e)
        {
            dgGroups.CancelEdit();
            var dlg = new LangBlogGroupsDetailDlg(Window.GetWindow(this), vm.SelectedGroupItem, vm);
            dlg.ShowDialog();
        }
        void miDeleteGroup_Click(object sender, RoutedEventArgs e)
        {
        }
        void miAddBlog_Click(object sender, RoutedEventArgs e)
        {
            //dgPosts.CancelEdit();
            //var dlg = new LangBlogGroupsDetailDlg(Window.GetWindow(this), vm.NewGroup(), vm);
            //dlg.ShowDialog();
        }
        void miEditBlog_Click(object sender, RoutedEventArgs e)
        {
            //dgPosts.CancelEdit();
            //var dlg = new LangBlogGroupsDetailDlg(Window.GetWindow(this), vm.SelectedPostItem, vm);
            //dlg.ShowDialog();
        }
        async void miEditBlogContent_Click(object sender, RoutedEventArgs e)
        {
            var w = (MainWindow)Window.GetWindow(this);
            var itemBlog = await contentDS.GetDataById(vm.SelectedPostItem.ID);
            w.AddBlogPostEditTab("Language Blog", itemBlog);
        }
        void miDeleteBlog_Click(object sender, RoutedEventArgs e)
        {
        }
        void dgPosts_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            miEditBlog_Click(sender, null);
        }
    }
}
