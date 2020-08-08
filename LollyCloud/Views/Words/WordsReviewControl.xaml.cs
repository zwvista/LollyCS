using Hardcodet.Wpf.Util;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace LollyCloud
{
    /// <summary>
    /// WordsReviewControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsReviewControl : UserControl, ILollySettings
    {
        public WordsReviewViewModel vm { get; set; }
        public SettingsViewModel vmSettings => vm.vmSettings;

        public WordsReviewControl()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            OnSettingsChanged();
        }

        public async Task OnSettingsChanged()
        {
            vm = new WordsReviewViewModel(MainWindow.vmSettings, needCopy: true, () =>
            {
                tbWordInput.Focus();
                if (vm.HasNext)
                {
                    if (vm.IsSpeaking) {
                        App.Speak(vm.vmSettings, vm.CurrentWord);
                    }
                }
            });
            await vm.NewTest();
        }

        async void btnNewTest_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new ReviewOptionsDlg(Window.GetWindow(this), vm.Options);
            if (dlg.ShowDialog() == true)
            {
                await vm.NewTest();
            }
        }

        async void btnCheck_Click(object sender, RoutedEventArgs e) =>
            await vm.Check();

        async void tbWordInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && !(vm.IsTestMode && string.IsNullOrEmpty(vm.WordInputString)))
                await vm.Check();
        }

        void chkSpeak_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
