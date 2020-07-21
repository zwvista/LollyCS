using LollyCloud.ViewModels;
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
    /// WebPageSelectDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class WebPageSelectDlg : Window
    {
        public WebPageSelectViewModel VM { get; } = new WebPageSelectViewModel();
        public WebPageSelectDlg()
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            DataContext = VM;
        }

        void chkWebPage_Click(object sender, RoutedEventArgs e)
        {
            VM.SelectedWebPage = (MWebPage)((RadioButton)sender).DataContext;
        }

        void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
