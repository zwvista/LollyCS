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
        TransformSourceControl sourceCtrl;
        TransformResultControl resultCtrl;
        TransformInterimControl interimCtrl;
        TransformTemplateControl templateCtrl;

        public TransformEditDlg(Window owner, string transform, string template, string url)
        {
            InitializeComponent();
            //SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            Owner = owner;
            vm = new TransformEditViewModel(transform, template, url);
            DataContext = vm;
            tcTranformResult.DataContext = this;
            templateCtrl = new TransformTemplateControl(vm);
            sourceCtrl = new TransformSourceControl(vm);
            resultCtrl = new TransformResultControl(vm);
            interimCtrl = new TransformInterimControl(vm);
            Tabs.Add(new ActionTabItem { Header = "Source", Content = sourceCtrl });
            Tabs.Add(new ActionTabItem { Header = "Result", Content = resultCtrl });
            Tabs.Add(new ActionTabItem { Header = "Interim", Content = interimCtrl });
            Tabs.Add(new ActionTabItem { Header = "Template", Content = templateCtrl });
        }
        void EditTransformItem(MTransformItem item)
        {
            vm.IsEditing = false;
            dgTransform.CancelEdit();
            var dlg = new TransformItemEditDlg(this, item);
            dlg.ShowDialog();
        }
        void dgTransform_RowDoubleClick(object sender, MouseButtonEventArgs e) =>
            EditTransformItem((MTransformItem)((DataGridRow)sender).Item);
        void miAdd_Click(object sender, RoutedEventArgs e)
        {
            var item = vm.NewTransformItem();
            var dlg = new TransformItemEditDlg(this, item);
            if (dlg.ShowDialog() == true)
                vm.Add(item);
        }
        void miEdit_Click(object sender, RoutedEventArgs e)
        {
            var item = (MTransformItem)dgTransform.SelectedItem;
            if (item != null) EditTransformItem(item);
        }
        void miDelete_Click(object sender, RoutedEventArgs e)
        {
            var item = (MTransformItem)dgTransform.SelectedItem;
            if (item != null) vm.Delete(item);
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
            DialogResult = true;
        }
    }
}
