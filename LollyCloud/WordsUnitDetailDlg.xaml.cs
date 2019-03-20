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

using LollyShared;

namespace LollyCloud
{
    /// <summary>
    /// WordsUnitDetailDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsUnitDetailDlg : Window
    {
        public MUnitWord item;
        public WordsUnitDetailDlg()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = item;
        }
    }
}
