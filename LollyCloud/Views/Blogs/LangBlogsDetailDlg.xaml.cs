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
    /// LangBlogsDetailDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class LangBlogsDetailDlg : Window
    {
        LangBlogsDetailViewModel vmDetail;
        public MLangBlogPost Item { get; set; }
        public LangBlogsDetailDlg(Window owner, MLangBlogPost item, LangBlogsViewModel vm)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            tbTitle.Focus();
            Owner = owner;
            vmDetail = new LangBlogsDetailViewModel(Item = item, vm);
            DataContext = vmDetail.ItemEdit;
            tbLangName.Text = vmDetail.LANGNAME;
        }
    }
}
