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
    /// TextbooksDetailDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class TextbooksDetailDlg : Window
    {
        TextbooksDetailViewModel vmDetail;
        public MTextbook Item { get; set; }
        public TextbooksDetailDlg(Window owner, MTextbook item, TextbooksViewModel vm)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            tbTextbookName.Focus();
            Owner = owner;
            vmDetail = new TextbooksDetailViewModel(Item = item, vm);
            DataContext = vmDetail.ItemEdit;
            tbLangName.Text = vmDetail.LANGNAME;
        }
    }
}
