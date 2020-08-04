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
    /// PatternsSplitDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class PatternsSplitDlg : Window
    {
        PatternsDetailViewModel vmDetail;
        public MPattern Item { get; set; }
        public PatternsSplitDlg(Window owner, MPattern item, PatternsViewModel vm)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            tbPattern.Focus();
            Owner = owner;
            vmDetail = new PatternsDetailViewModel(Item = item, vm);
            DataContext = vmDetail.ItemEdit;
        }
    }
}
