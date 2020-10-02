using Hardcodet.Wpf.Util;
using LollyCommon;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace LollyCloud
{
    /// <summary>
    /// PhrasesReviewControl.xaml の相互作用ロジック
    /// </summary>
    public partial class PhrasesReviewControl : UserControl, ILollySettings
    {
        public PhrasesReviewViewModel vm { get; set; }
        public SettingsViewModel vmSettings => vm.vmSettings;

        public PhrasesReviewControl()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            OnSettingsChanged();
        }

        public async Task OnSettingsChanged()
        {
            DataContext = vm = new PhrasesReviewViewModel(MainWindow.vmSettings, needCopy: true, () =>
            {
                tbPhraseInput.Focus();
                if (vm.HasNext && vm.IsSpeaking)
                    App.Speak(vm.vmSettings, vm.CurrentPhrase);
            });
            btnNewTest_Click(null, null);
        }
        async void btnNewTest_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new ReviewOptionsDlg(Window.GetWindow(this), vm.Options);
            if (dlg.ShowDialog() == true)
                await vm.NewTest();
        }

        void btnCheck_Click(object sender, RoutedEventArgs e) =>
            vm.Check();

        void tbPhraseInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return) return;
            vm.Check();
        }
    }
}
