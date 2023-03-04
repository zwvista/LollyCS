using CefSharp;
using LollyCommon;
using ReactiveUI;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// LangBlogsControl.xaml の相互作用ロジック
    /// </summary>
    public partial class LangBlogsControl : UserControl, ILollySettings
    {
        string originalText = "";
        LangBlogsViewModel vm;
        BlogEditService editService = new BlogEditService();

        public LangBlogsControl()
        {
            InitializeComponent();
            // Disable image loading
            // wbBlog.BrowserSettings.ImageLoading = CefState.Disabled;
            OnSettingsChanged();
        }

        public async Task OnSettingsChanged()
        {
            DataContext = vm = new LangBlogsViewModel(MainWindow.vmSettings, true);
        }
        void OnBeginEdit(object sender, DataGridBeginningEditEventArgs e) =>
            originalText = DataGridHelper.OnBeginEditCell(e);
        void OnEndEdit(object sender, DataGridCellEditEndingEventArgs e) =>
            DataGridHelper.OnEndEditCell(sender, e, originalText, null, null, async item =>
            {
                if (sender == dgGroups)
                    await vm.UpdateGroup((MLangBlogGroup)item);
                else
                    await vm.UpdateBlog((MLangBlog)item);
            });
        async void dgGroups_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            await vm.OnSelectedGroupChanged();
        void dgGroups_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            miEditGroup_Click(sender, null);
        }
        void miAddGroup_Click(object sender, RoutedEventArgs e)
        {
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
        }
        void miEditBlog_Click(object sender, RoutedEventArgs e)
        {
            dgGroups.CancelEdit();
            var dlg = new LangBlogsDetailDlg(Window.GetWindow(this), vm.SelectedBlogItem, vm);
            dlg.ShowDialog();
        }
        void miDeleteBlog_Click(object sender, RoutedEventArgs e)
        {
        }
        async void dgBlogs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await vm.OnSelectedBlogChanged();
            wbBlog.LoadLargeHtml(editService.MarkedToHtml(vm.BlogContent));
        }
        void dgBlogs_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            miEditBlog_Click(sender, null);
        }
    }
}
