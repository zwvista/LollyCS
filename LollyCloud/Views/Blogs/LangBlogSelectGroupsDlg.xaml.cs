using LollyCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LollyCloud
{
    /// <summary>
    /// LangBlogSelectGroupsDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class LangBlogSelectGroupsDlg : Window
    {
        public SettingsViewModel vmSettings;
        LangBlogSelectGroupsViewModel vm;
        public LangBlogSelectGroupsDlg()
        {
            InitializeComponent();
            // https://stackoverflow.com/questions/339620/how-do-i-remove-minimize-and-maximize-from-a-resizable-window-in-wpf
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = vm = new LangBlogSelectGroupsViewModel(vmSettings);
        }

    }
}
