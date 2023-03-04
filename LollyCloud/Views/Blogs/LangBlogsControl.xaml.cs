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
                //if (sender == dgGroups)
                //    await vm.Update((MPattern)item);
                //else
                //    await vmWP.UpdatePatternWebPage((MPatternWebPage)item);
            });
        void dgGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        void dgGroups_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
        void miAddGroup_Click(object sender, RoutedEventArgs e)
        {
        }
        void miEditGroup_Click(object sender, RoutedEventArgs e)
        {
        }
        void miDeleteGroup_Click(object sender, RoutedEventArgs e)
        {
        }
        void miAddBlog_Click(object sender, RoutedEventArgs e)
        {
        }
        void miEditBlog_Click(object sender, RoutedEventArgs e)
        {
        }
        void miDeleteBlog_Click(object sender, RoutedEventArgs e)
        {
        }
        void dgBlogs_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
