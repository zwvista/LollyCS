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
    /// WordsLangDetailDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsLangDetailDlg : Window
    {
        WordsLangDetailViewModel vmDetail;
        public WordsLangDetailDlg(Window owner, WordsLangViewModel vm, MLangWord item)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            tbWord.Focus();
            Owner = owner;
            vmDetail = new WordsLangDetailViewModel(vm, item);
            DataContext = vmDetail.ItemEdit;
            dgWords.DataContext = vmDetail.vmSingleWord;
        }
    }
}
