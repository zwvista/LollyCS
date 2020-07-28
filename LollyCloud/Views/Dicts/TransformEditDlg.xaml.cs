using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
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
    /// TransformEditDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class TransformEditDlg : Window
    {
        TransformEditViewModel vm;
        public ObservableCollection<ActionTabItem> Tabs { get; } = new ObservableCollection<ActionTabItem>();
        public ActionInterTabClient ActionInterTabClient { get; } = new ActionInterTabClient();
        public string TRANSFORM => vm.TRANSFORM;
        public string TEMPLATE => vm.TEMPLATE;
        TransformTemplateControl templateCtrl;
        TransformSourceControl sourceCtrl;
        TransformResultControl resultCtrl;
        TransformDetailControl detailCtrl;

        public TransformEditDlg(Window owner, string transform, string template)
        {
            InitializeComponent();
            //SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            Owner = owner;
            vm = new TransformEditViewModel(transform, template);
            DataContext = vm;
            tcTranformResult.DataContext = this;
            templateCtrl = new TransformTemplateControl(vm);
            sourceCtrl = new TransformSourceControl(vm);
            resultCtrl = new TransformResultControl(vm);
            detailCtrl = new TransformDetailControl(vm);
            Tabs.Add(new ActionTabItem { Header = "Template", Content = templateCtrl });
            Tabs.Add(new ActionTabItem { Header = "Source", Content = sourceCtrl });
            Tabs.Add(new ActionTabItem { Header = "Result", Content = resultCtrl });
            Tabs.Add(new ActionTabItem { Header = "Detail", Content = detailCtrl });
        }
        void dgTransform_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            vm.IsEditing = false;
            dgTransform.CancelEdit();
            var dlg = new TransformItemEditDlg(this, (MTransformItem)((DataGridRow)sender).Item);
            dlg.ShowDialog();
        }

        void OnBeginEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            vm.IsEditing = true;
        }
        void OnEndEdit(object sender, DataGridCellEditEndingEventArgs e)
        {
            vm.IsEditing = false;
        }

        void btnOK_Click(object sender, RoutedEventArgs e)
        {
            vm.OnOK();
            DialogResult = true;
        }
    }
}
