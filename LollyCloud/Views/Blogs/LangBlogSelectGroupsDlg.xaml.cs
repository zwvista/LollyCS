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
        LangBlogSelectGroupsViewModel vm;
        public LangBlogSelectGroupsDlg(Window owner, SettingsViewModel vmSettings, MLangBlogPost item)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            Owner = owner;
            DataContext = vm = new LangBlogSelectGroupsViewModel(vmSettings, item);
        }
    }
}
