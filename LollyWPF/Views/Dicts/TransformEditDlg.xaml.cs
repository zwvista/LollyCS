using LollyCommon;
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
        TransformEditViewModelWPF vm;
        public ObservableCollection<ActionTabItem> Tabs { get; } = new ObservableCollection<ActionTabItem>();
        public ActionInterTabClient ActionInterTabClient { get; } = new ActionInterTabClient();
        TransformSourceControl sourceCtrl;
        TransformResultControl resultCtrl;
        TransformIntermediateControl intermediateCtrl;
        TransformTemplateControl templateCtrl;

        public TransformEditDlg(Window owner, SettingsViewModel vmSettings, MDictionaryEdit itemEdit)
        {
            InitializeComponent();
            //SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            Owner = owner;
            DataContext = vm = new TransformEditViewModelWPF(vmSettings, itemEdit);
            tcTranform.DataContext = this;
            templateCtrl = new TransformTemplateControl(vm);
            sourceCtrl = new TransformSourceControl(vm);
            resultCtrl = new TransformResultControl(vm);
            intermediateCtrl = new TransformIntermediateControl(vm);
            Tabs.Add(new ActionTabItem { Header = "Source", Content = sourceCtrl });
            Tabs.Add(new ActionTabItem { Header = "Result", Content = resultCtrl });
            Tabs.Add(new ActionTabItem { Header = "Intermediate", Content = intermediateCtrl });
            Tabs.Add(new ActionTabItem { Header = "Template", Content = templateCtrl });
            Action<ReactiveCommand<Unit, Unit>> f = cmd => cmd.WhenFinishedExecuting().Subscribe(_ => tcTranform.SelectedIndex = 1);
            f(vm.ExecuteTransformCommand);
            f(vm.GetAndTransformCommand);
        }
        void EditTransformItem(MTransformItem item)
        {
            dgTransform.CancelEdit();
            var dlg = new TransformItemEditDlg(this, item);
            dlg.ShowDialog();
        }
        void dgTransform_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ((DataGridRow)sender).Item as MTransformItem;
            if (item != null)
                EditTransformItem(item);
        }
    }
}
