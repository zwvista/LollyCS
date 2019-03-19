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

using LollyShared;

namespace LollyCloud
{
    /// <summary>
    /// SettingsDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingsDlg : Window
    {
        SettingsViewModel vm = new SettingsViewModel();

        public SettingsDlg()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            DataContext = vm;
            Init();
        }

        async Task Init() => await vm.GetData();

        async void cbLanguages_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await vm.SetSelectedLang(vm.SelectedLang);
            await vm.UpdateLang();
        }

        async void cbDictItems_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            await vm.UpdateDictItem();

        async void cbDictsNote_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            await vm.UpdateDictNote();

        async void cbTextbooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await vm.UpdateTextbook();
        }

        async void cbUnitFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        async void cbPartFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        void cbToTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        async void cbUnitTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        async void cbPartTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
