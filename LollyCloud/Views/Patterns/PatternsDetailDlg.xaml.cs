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
    /// PatternsDetailDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class PatternsDetailDlg : Window
    {
        PatternsDetailViewModel vmDetail;
        public PatternsDetailDlg(Window owner, PatternsViewModel vm, MPattern item)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            tbPattern.Focus();
            Owner = owner;
            vmDetail = new PatternsDetailViewModel(vm, item);
            DataContext = vmDetail.ItemEdit;
        }
    }
}
