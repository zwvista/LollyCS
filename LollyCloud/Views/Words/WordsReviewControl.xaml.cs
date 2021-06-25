using Dragablz;
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
    /// WordsReviewControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsReviewControl : WordsBaseControl
    {
        public WordsReviewViewModel vm { get; set; }
        protected override WordsBaseViewModel vmWords => vm;
        public override SettingsViewModel vmSettings => vm.vmSettings;
        protected override ToolBar ToolBarDictBase => ToolBarDict;
        protected override TabablzControl tcDictsBase => tcDicts;

        public WordsReviewControl()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            OnSettingsChanged();
        }

        public override async Task OnSettingsChanged()
        {
            DataContext = vm = new WordsReviewViewModel(MainWindow.vmSettings, needCopy: true, () =>
            {
                tbWordInput.Focus();
                if (vm.HasCurrent && vm.IsSpeaking)
                    App.Speak(vm.vmSettings, vm.CurrentWord);
                if (!vm.IsTestMode)
                    dgWords_SelectionChanged(null, null);
            });
            await base.OnSettingsChanged();
            btnNewTest_Click(null, null);
        }

        async void btnNewTest_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new ReviewOptionsDlg(Window.GetWindow(this), vm.Options);
            if (dlg.ShowDialog() == true)
                await vm.NewTest();
        }

        async void btnCheck_Click(object sender, RoutedEventArgs e) =>
            await vm.Check(sender == btnCheckNext);

        async void tbWordInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && !(vm.IsTestMode && string.IsNullOrEmpty(vm.WordInputString)))
                await vm.Check(true);
        }

        void btnSpeak_Click(object sender, RoutedEventArgs e)
        {
            if (vm.HasCurrent)
                App.Speak(vm.vmSettings, vm.CurrentWord);
        }
        public override async Task GetPhrases(int wordid) { }
    }
}
