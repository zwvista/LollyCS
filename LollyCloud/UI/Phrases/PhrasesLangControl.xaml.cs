﻿using LollyShared;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// PhrasesLangControl.xaml の相互作用ロジック
    /// </summary>
    public partial class PhrasesLangControl : PhrasesBaseControl
    {
        public PhrasesLangViewModel vm { get; set; }
        public override SettingsViewModel vmSettings => vm.vmSettings;
        public override DataGrid dgPhrasesBase => dgPhrases;
        public override MPhraseInterface ItemForRow(int row) => vm.PhraseItems[row];

        public PhrasesLangControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgPhrases_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dlg = new PhrasesLangDetailDlg();
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            dlg.Owner = Window.GetWindow(this);
            dlg.itemOriginal = (sender as DataGridRow).Item as MLangPhrase;
            dlg.vm = vm;
            dlg.ShowDialog();
        }

        void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new PhrasesLangDetailDlg();
            dlg.Owner = Window.GetWindow(this);
            dlg.itemOriginal = vm.NewLangPhrase();
            dlg.vm = vm;
            dlg.ShowDialog();
            vm.PhraseItems.Add(dlg.itemOriginal);
        }

        void OnBeginEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            originalText = ((TextBlock)e.EditingEventArgs.Source).Text;
        }

        async void OnEndEdit(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var text = ((TextBox)e.EditingElement).Text;
                if (text != originalText)
                {
                    var item = vm.PhraseItems[e.Row.GetIndex()];
                    await vm.Update(item);
                }
                dgPhrases.CancelEdit(DataGridEditingUnit.Row);
            }
        }

        public override async Task OnSettingsChanged()
        {
            vm = new PhrasesLangViewModel(MainWindow.vmSettings, needCopy: true);
            DataContext = vm;
            await base.OnSettingsChanged();
        }

        async void miDelete_Click(object sender, RoutedEventArgs e)
        {
            var row = dgPhrases.SelectedIndex;
            if (row == -1) return;
            var item = vm.PhraseItems[row];
            await vm.Delete(item.ID);
        }
    }
}