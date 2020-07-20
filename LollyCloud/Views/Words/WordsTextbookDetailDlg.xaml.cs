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
    /// WordsTextbookDetailDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsTextbookDetailDlg : Window
    {
        WordsUnitDetailViewModel vmDetail;
        public MUnitWord Item { get; set; }
        public WordsTextbookDetailDlg(Window owner, MUnitWord item, WordsUnitViewModel vm)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            tbWord.Focus();
            Owner = owner;
            vmDetail = new WordsUnitDetailViewModel(Item = item, vm);
            DataContext = vmDetail.ItemEdit;
            dgWords.DataContext = vmDetail.vmSingleWord;
        }

        async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            await vmDetail.OnOK();
            DialogResult = true;
        }
    }
}
