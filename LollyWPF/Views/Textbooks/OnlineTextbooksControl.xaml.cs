using Hardcodet.Wpf.Util;
using LollyCommon;
using ReactiveUI;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace LollyWPF
{
    /// <summary>
    /// OnlineTextbooksControl.xaml の相互作用ロジック
    /// </summary>
    public partial class OnlineTextbooksControl : UserControl, ILollySettings
    {
        public OnlineTextbooksViewModel vm { get; set; } = null!;
        public SettingsViewModel vmSettings => vm.vmSettings;
        public OnlineTextbooksControl()
        {
            InitializeComponent();
            _ = OnSettingsChanged();
        }

        public async Task OnSettingsChanged()
        {
            DataContext = vm = new OnlineTextbooksViewModel(MainWindow.vmSettings, needCopy: true);
        }

        public void btnRefresh_Click(object sender, RoutedEventArgs e) => vm.Reload();

        void dgOnlineTextbooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (MOnlineTextbook)dgOnlineTextbooks.SelectedItem;
            if (item == null) return;
            wbWebPage.Load(item.URL);
        }

    }
}
