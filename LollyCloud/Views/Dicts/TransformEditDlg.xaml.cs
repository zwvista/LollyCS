using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
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
        TransformSourceControl sourceCtrl;
        TransformResultControl resultCtrl;
        TransformInterimControl interimCtrl;
        TransformTemplateControl templateCtrl;

        public TransformEditDlg(Window owner, MDictionaryEdit itemEdit)
        {
            InitializeComponent();
            //SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            Owner = owner;
            vm = new TransformEditViewModel(itemEdit);
            DataContext = vm;
            tcTranform.DataContext = this;
            templateCtrl = new TransformTemplateControl(vm);
            sourceCtrl = new TransformSourceControl(vm);
            resultCtrl = new TransformResultControl(vm);
            interimCtrl = new TransformInterimControl(vm);
            Tabs.Add(new ActionTabItem { Header = "Source", Content = sourceCtrl });
            Tabs.Add(new ActionTabItem { Header = "Result", Content = resultCtrl });
            Tabs.Add(new ActionTabItem { Header = "Interim", Content = interimCtrl });
            Tabs.Add(new ActionTabItem { Header = "Template", Content = templateCtrl });
            // https://stackoverflow.com/questions/50177352/is-there-a-way-to-track-when-reactive-command-finished-its-execution
            Action<ReactiveCommand<Unit, Unit>> f = cmd => cmd.IsExecuting
                .Skip(1) // IsExecuting has an initial value of false.  We can skip that first value
                .Where(isExecuting => !isExecuting) // filter until the executing state becomes false
                .Subscribe(_ => tcTranform.SelectedIndex = 1);
            f(vm.ExecuteTransformCommand);
            f(vm.GetAndTransformCommand);
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
