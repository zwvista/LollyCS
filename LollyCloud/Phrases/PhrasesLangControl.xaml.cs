using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using LollyShared;

namespace LollyCloud
{
    /// <summary>
    /// PhrasesLangControl.xaml の相互作用ロジック
    /// </summary>
    public partial class PhrasesLangControl : UserControl, ILollySettings
    {
        public SettingsViewModel vmSettings => MainWindow.vmSettings;
        public PhrasesLangViewModel vm { get; set; }
        string selectedPhrase = "";

        public PhrasesLangControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        void dgPhrases_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var row = dgPhrases.SelectedIndex;
            if (row == -1) return;
            selectedPhrase = vm.Items[row].PHRASE;
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
            vm.Items.Add(dlg.itemOriginal);
        }

        async void dgPhrases_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var item = vm.Items[e.Row.GetIndex()];
                await vm.Update(item);
            }
        }

        async void btnRefresh_Click(object sender, RoutedEventArgs e) => await OnSettingsChanged();

        public async Task OnSettingsChanged()
        {
            vm = await PhrasesLangViewModel.CreateAsync(vmSettings);
            dgPhrases.ItemsSource = vm.Items;
        }

        async void miDelete_Click(object sender, RoutedEventArgs e)
        {
            var row = dgPhrases.SelectedIndex;
            if (row == -1) return;
            var item = vm.Items[row];
            await vm.Delete(item.ID);
        }

        void miCopy_Click(object sender, RoutedEventArgs e) => Clipboard.SetText(selectedPhrase);

        void miGoogle_Click(object sender, RoutedEventArgs e) => CommonApi.GoogleString(selectedPhrase);
    }
}
