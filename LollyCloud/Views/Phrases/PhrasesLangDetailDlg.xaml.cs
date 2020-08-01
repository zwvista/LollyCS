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
    /// PhrasesLangDetailDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class PhrasesLangDetailDlg : Window
    {
        PhrasesLangDetailViewModel vmDetail;
        public MLangPhrase Item;
        public PhrasesLangDetailDlg(Window owner, MLangPhrase item, PhrasesLangViewModel vm)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            tbPhrase.Focus();
            Owner = owner;
            vmDetail = new PhrasesLangDetailViewModel(Item = item, vm);
            DataContext = vmDetail.ItemEdit;
            dgPhrases.DataContext = vmDetail.vmSinglePhrase;
        }
    }
}
