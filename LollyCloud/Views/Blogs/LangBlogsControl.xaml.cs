using CefSharp;
using LollyCommon;
using ReactiveUI;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// LangBlogsControl.xaml の相互作用ロジック
    /// </summary>
    public partial class LangBlogsControl : UserControl, ILollySettings
    {
        BlogEditViewModel vm;

        public LangBlogsControl()
        {
            InitializeComponent();
            // Disable image loading
            // wbBlog.BrowserSettings.ImageLoading = CefState.Disabled;
            OnSettingsChanged();
        }

        public async Task OnSettingsChanged()
        {
            DataContext = vm = new BlogEditViewModel(MainWindow.vmSettings, true);
        }

    }
}
