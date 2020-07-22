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
    /// DictsDetailDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class DictsDetailDlg : Window
    {
        DictsDetailViewModel vmDetail;
        public MDictionary Item { get; set; }
        public DictsDetailDlg(Window owner, MDictionary item, DictsViewModel vm)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            tbDictName.Focus();
            Owner = owner;
            vmDetail = new DictsDetailViewModel(Item = item, vm);
            DataContext = vmDetail.ItemEdit;
            cbLangTo.ItemsSource = vm.vmSettings.Languages;
            cbDictType.ItemsSource = vm.vmSettings.DictCodes;
        }

        async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            await vmDetail.OnOK();
            DialogResult = true;
        }
    }
}
