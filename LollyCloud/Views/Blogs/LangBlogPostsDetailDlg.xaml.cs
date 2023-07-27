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
    /// LangBlogPostsDetailDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class LangBlogPostsDetailDlg : Window
    {
        LangBlogPostsDetailViewModel vmDetail;
        public MLangBlogPost Item { get; set; }
        public LangBlogPostsDetailDlg(Window owner, MLangBlogPost item, LangBlogPostsViewModel vm)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            tbTitle.Focus();
            Owner = owner;
            vmDetail = new LangBlogPostsDetailViewModel(Item = item, vm);
            DataContext = vmDetail.ItemEdit;
            tbLangName.Text = vmDetail.LANGNAME;
        }
    }
}
