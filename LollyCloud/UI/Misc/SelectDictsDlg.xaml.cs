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
    /// SelectDictsDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class SelectDictsDlg : Window
    {
        public SelectDictsDlg()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            DataContext = this;
            // https://stackoverflow.com/questions/339620/how-do-i-remove-minimize-and-maximize-from-a-resizable-window-in-wpf
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
        }

        async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        void btnCancel_Click(object sender, RoutedEventArgs e) => Close();
    }
}
