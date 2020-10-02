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
    /// TransformItemEditDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class TransformItemEditDlg : Window
    {
        TransformItemEditViewModel vm;
        public MTransformItem Item { get; set; }
        public TransformItemEditDlg(Window owner, MTransformItem item)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            Owner = owner;
            vm = new TransformItemEditViewModel(Item = item);
            DataContext = vm.ItemEdit;
        }

        async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            vm.OnOK();
            DialogResult = true;
        }
    }
}
