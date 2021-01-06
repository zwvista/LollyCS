using LollyCommon;
using ReactiveUI;
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
    /// WordsUnitBatchAddDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsUnitBatchAddDlg : Window
    {
        WordsUnitBatchAddViewModel vmBatch;
        public WordsUnitBatchAddDlg(Window owner, WordsUnitViewModel vm)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            tbWords.Focus();
            Owner = owner;
            vmBatch = new WordsUnitBatchAddViewModel(vm);
            DataContext = vmBatch.ItemEdit;
        }
    }
}
