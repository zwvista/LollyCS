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

        void btnEditTransform_Click(object sender, RoutedEventArgs e)
        {
            var tag = (string)((Button)sender).Tag;
            var o = vmDetail.ItemEdit;
            var dlg = new TransformEditDlg(this, o.TRANSFORM, tag == "1" ? o.TEMPLATE : o.TEMPLATE2, o.URL);
            if (dlg.ShowDialog() == true)
            {
                o.TRANSFORM = dlg.TRANSFORM;
                if (tag == "1")
                    o.TEMPLATE = dlg.TEMPLATE;
                else
                    o.TEMPLATE2 = dlg.TEMPLATE;
            }
        }

        async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            await vmDetail.OnOK();
            DialogResult = true;
        }
    }
}
