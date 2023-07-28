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
    /// LangBlogSelectPostsDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class LangBlogSelectPostsDlg : Window
    {
        LangBlogSelectPostsViewModel vm;
        public LangBlogSelectPostsDlg(Window owner, SettingsViewModel vmSettings, MLangBlogGroup item)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            Owner = owner;
            DataContext = vm = new LangBlogSelectPostsViewModel(vmSettings, item);
        }
    }
}
