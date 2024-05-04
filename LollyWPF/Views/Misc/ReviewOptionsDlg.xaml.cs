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


namespace LollyWPF
{
    /// <summary>
    /// ReviewOptionsDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class ReviewOptionsDlg : Window
    {
        MReviewOptions options;
        MReviewOptions optionsEdit = new MReviewOptions();
        public ReviewOptionsDlg(Window owner, MReviewOptions options)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            Owner = owner;
            this.options = options;
            options.CopyProperties(optionsEdit);
            DataContext = optionsEdit;
        }

        void btnOK_Click(object sender, RoutedEventArgs e)
        {
            optionsEdit.CopyProperties(options);
            DialogResult = true;
        }
    }
}
