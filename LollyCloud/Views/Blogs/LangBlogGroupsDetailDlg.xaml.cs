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
    /// LangBlogGroupsDetailDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class LangBlogGroupsDetailDlg : Window
    {
        LangBlogGroupsDetailViewModel vmDetail;
        public MLangBlogGroup Item { get; set; }
        public LangBlogGroupsDetailDlg(Window owner, MLangBlogGroup item, LangBlogsViewModel vm)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            tbGroup.Focus();
            Owner = owner;
            vmDetail = new LangBlogGroupsDetailViewModel(Item = item, vm);
            DataContext = vmDetail.ItemEdit;
            tbLangName.Text = vmDetail.LANGNAME;
        }
    }
}
