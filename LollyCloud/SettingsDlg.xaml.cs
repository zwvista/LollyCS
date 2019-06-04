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
            // https://stackoverflow.com/questions/339620/how-do-i-remove-minimize-and-maximize-from-a-resizable-window-in-wpf
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
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

        async void cbDictsTranslation_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            await vm.UpdateDictTranslation();

        async void cbTextbooks_SelectionChanged(object sender, SelectionChangedEventArgs e) =>
            await vm.UpdateTextbook();

        async void cbUnitFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await UpdateUnitFrom(vm.USUNITFROM, false);
            if (vm.ToType == 0)
                await UpdateSingleUnit();
            else if (vm.ToType == 1 || vm.IsInvalidUnitPart)
                await UpdateUnitPartTo();
        }

        async void cbPartFrom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await UpdatePartFrom(vm.USPARTFROM, false);
            if (vm.ToType == 1 || vm.IsInvalidUnitPart)
                await UpdateUnitPartTo();
        }

        async void cbToTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vm.Units == null) return;
            var b = vm.ToType == 2;
            cbUnitTo.IsEnabled = b;
            cbPartTo.IsEnabled = b && !vm.IsSinglePart;
            btnPrevious.IsEnabled = !b;
            btnNext.IsEnabled = !b;
            cbPartFrom.IsEnabled = vm.ToType != 0 && !vm.IsSinglePart;
            if (vm.ToType == 0)
                await UpdateSingleUnit();
            else if (vm.ToType == 1)
                await UpdateUnitPartTo();
        }

        async void cbUnitTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await UpdateUnitTo(vm.USUNITTO, false);
            if (vm.IsInvalidUnitPart)
                await UpdateUnitPartFrom();
        }

        async void cbPartTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await UpdatePartTo(vm.USPARTTO, false);
            if (vm.IsInvalidUnitPart)
                await UpdateUnitPartFrom();
        }

        async void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (vm.ToType == 0)
            {
                if (vm.USUNITFROM > 1)
                {
                    await UpdateUnitFrom(vm.USUNITFROM - 1);
                    await UpdateUnitTo(vm.USUNITFROM);
                }
            }
            else if (vm.USPARTFROM > 1)
            {
                await UpdatePartFrom(vm.USPARTFROM - 1);
                await UpdateUnitPartTo();
            }
            else if (vm.USUNITFROM > 1)
            {
                await UpdateUnitFrom(vm.USUNITFROM - 1);
                await UpdatePartFrom(vm.PartCount);
                await UpdateUnitPartTo();
            }
        }

        async void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (vm.ToType == 0)
            {
                if (vm.USUNITFROM < vm.UnitCount)
                {
                    await UpdateUnitFrom(vm.USUNITFROM + 1);
                    await UpdateUnitTo(vm.USUNITFROM);
                }
            }
            else if (vm.USPARTFROM < vm.PartCount)
            {
                await UpdatePartFrom(vm.USPARTFROM + 1);
                await UpdateUnitPartTo();
            }
            else if (vm.USUNITFROM < vm.UnitCount)
            {
                await UpdateUnitFrom(vm.USUNITFROM + 1);
                await UpdatePartFrom(1);
                await UpdateUnitPartTo();
            }
        }

        async Task UpdateUnitPartFrom()
        {
            await UpdateUnitFrom(vm.USUNITTO);
            await UpdatePartFrom(vm.USPARTTO);
        }

        async Task UpdateUnitPartTo()
        {
            await UpdateUnitTo(vm.USUNITFROM);
            await UpdatePartTo(vm.USPARTFROM);
        }

        async Task UpdateSingleUnit()
        {
            await UpdateUnitTo(vm.USUNITFROM);
            await UpdatePartFrom(1);
            await UpdatePartTo(vm.PartCount);
        }

        async Task<bool> UpdateUnitFrom(int v, bool check = true)
        {
            if (check && vm.USUNITFROM == v) return false;
            vm.USUNITFROM = v;
            cbUnitFrom.SelectedIndex = vm.Units.FindIndex(o => o.Value == v);
            await vm.UpdateUnitFrom();
            return true;
        }

        async Task<bool> UpdatePartFrom(int v, bool check = true)
        {
            if (check && vm.USPARTFROM == v) return false;
            vm.USPARTFROM = v;
            cbPartFrom.SelectedIndex = vm.Parts.FindIndex(o => o.Value == v);
            await vm.UpdatePartFrom();
            return true;
        }

        async Task<bool> UpdateUnitTo(int v, bool check = true)
        {
            if (check && vm.USUNITTO == v) return false;
            vm.USUNITTO = v;
            cbUnitTo.SelectedIndex = vm.Units.FindIndex(o => o.Value == v);
            await vm.UpdateUnitTo();
            return true;
        }

        async Task<bool> UpdatePartTo(int v, bool check = true)
        {
            if (check && vm.USPARTTO == v) return false;
            vm.USPARTTO = v;
            cbPartTo.SelectedIndex = vm.Parts.FindIndex(o => o.Value == v);
            await vm.UpdatePartTo();
            return true;
        }

        void btnClose_Click(object sender, RoutedEventArgs e) => this.Close();
    }
}
