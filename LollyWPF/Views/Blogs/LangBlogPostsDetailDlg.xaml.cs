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


namespace LollyWPF
{
    /// <summary>
    /// LangBlogPostsDetailDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class LangBlogPostsDetailDlg : Window
    {
        LangBlogPostsDetailViewModel vmDetail;
        public MLangBlogPost ItemPost { get; set; }
        public MLangBlogGroup? ItemGroup { get; }
        public LangBlogPostsDetailDlg(Window owner, MLangBlogPost item, LangBlogViewModel vm, MLangBlogGroup? itemGroup = null)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            tbTitle.Focus();
            Owner = owner;
            vmDetail = new LangBlogPostsDetailViewModel(ItemPost = item, vm, ItemGroup = itemGroup);
            DataContext = vmDetail.ItemEdit;
            tbGroup.DataContext = ItemGroup;
            tbLangName.Text = vmDetail.LANGNAME;
        }
    }
}
