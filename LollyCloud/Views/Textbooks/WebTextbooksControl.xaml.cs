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

namespace LollyCloud
{
    /// <summary>
    /// WebTextbooksControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WebTextbooksControl : UserControl, ILollySettings
    {
        public WebTextbooksViewModel vm { get; set; }
        string originalText = "";
        public WebTextbooksControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        public async Task OnSettingsChanged()
        {
            DataContext = vm = new WebTextbooksViewModel(MainWindow.vmSettings, needCopy: true);
        }

        public void btnRefresh_Click(object sender, RoutedEventArgs e) => vm.Reload();

        void dgWebTextbooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (MWebTextbook)dgWebTextbooks.SelectedItem;
            if (item == null) return;
            wbWebPage.Load(item.URL);
        }

    }
}
