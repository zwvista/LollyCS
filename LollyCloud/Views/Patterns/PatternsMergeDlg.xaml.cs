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
    /// PatternsMergeDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class PatternsMergeDlg : Window
    {
        PatternsMergeViewModelWPF vm;
        public PatternsMergeDlg(Window owner, List<MPattern> items)
        {
            InitializeComponent();
            //SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            Owner = owner;
            DataContext = vm = new PatternsMergeViewModelWPF(items);
        }
    }
}
