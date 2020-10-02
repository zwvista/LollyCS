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
    /// PatternsWebPageDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class PatternsWebPageDlg : Window
    {
        PatternsWebPageViewModel vmDetail;
        public PatternsWebPageDlg(Window owner, PatternsViewModelWPF vm, MPatternWebPage item)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            tbTitle.Focus();
            Owner = owner;
            vmDetail = new PatternsWebPageViewModel(vm, item);
            DataContext = vmDetail.ItemEdit;
            btnExisting.IsEnabled = btnNew.IsEnabled = vmDetail.ItemEdit.ID == 0;
        }

        void btnNew_Click(object sender, RoutedEventArgs e)
        {
            vmDetail.ItemEdit.WEBPAGEID = 0;
        }

        void btnExisting_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new WebPageSelectDlg();
            dlg.Owner = this;
            if (dlg.ShowDialog() == true)
            {
                var o = dlg.VM.SelectedWebPage;
                vmDetail.ItemEdit.WEBPAGEID = o.ID;
                //vmDetail.ItemEdit.TITLE = o.TITLE;
                //vmDetail.ItemEdit.URL = o.URL;
                tbTitle.Text = o.TITLE;
                tbURL.Text = o.URL;
            }
        }
    }
}
