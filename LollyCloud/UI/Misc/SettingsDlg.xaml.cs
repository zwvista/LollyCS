using LollyShared;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LollyCloud
{
    public enum SettingsDlgResult { ApplyToAll, ApplyToCurrent, ApplyToNone };
    /// <summary>
    /// SettingsDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class SettingsDlg : Window
    {
        SettingsViewModel vm = new SettingsViewModel();
        public SettingsDlgResult Result { get; private set; }

        public SettingsDlg()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            DataContext = vm;
            // https://stackoverflow.com/questions/339620/how-do-i-remove-minimize-and-maximize-from-a-resizable-window-in-wpf
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            vm.OnUpdateToType += (o, e) => cbToTypes_SelectionChanged(o, null);
            Init();
        }

        async Task Init() => await vm.GetData();

        async void cbLanguages_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            await vm.SetSelectedLang(vm.SelectedLang);

        async void cbVoices_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            await vm.UpdateVoice();

        async void cbDictsReference_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            await vm.UpdateDictItem();

        async void cbDictsNote_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            await vm.UpdateDictNote();

        async void cbDictsTranslation_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            await vm.UpdateDictTranslation();

        async void cbTextbooks_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            await vm.UpdateTextbook();

        async void cbUnitFrom_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            await vm.UpdateUnitFrom();

        async void cbPartFrom_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            await vm.UpdatePartFrom();

        async void cbToTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vm.Units == null) return;
            var b = vm.ToType == UnitPartToType.To;
            cbUnitTo.IsEnabled = b;
            cbPartTo.IsEnabled = b && !vm.IsSinglePart;
            btnPrevious.IsEnabled = !b;
            btnNext.IsEnabled = !b;
            var b2 = vm.ToType != UnitPartToType.Unit;
            var t = !b2 ? "Unit" : "Part";
            btnPrevious.Content = "Previous " + t;
            btnNext.Content = "Next " + t;
            cbPartFrom.IsEnabled = b2 && !vm.IsSinglePart;
            await vm.UpdateToType();
        }

        async void cbUnitTo_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            await vm.UpdateUnitTo();

        async void cbPartTo_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            await vm.UpdatePartTo();

        async void btnPrevious_Click(object sender, RoutedEventArgs e) =>
            await vm.PreviousUnitPart();

        async void btnNext_Click(object sender, RoutedEventArgs e) =>
            await vm.NextUnitPart();

        void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Result = (SettingsDlgResult)(sender as Button).Tag;
            this.Close();
        }

    }
}
