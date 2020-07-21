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
    /// PhrasesReviewControl.xaml の相互作用ロジック
    /// </summary>
    public partial class PhrasesReviewControl : UserControl, ILollySettings
    {
        public PhrasesReviewViewModel vm { get; set; }
        public SettingsViewModel vmSettings => vm.vmSettings;
        DispatcherTimer _timer;

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
            _timer?.Stop();
            vm = new PhrasesReviewViewModel(MainWindow.vmSettings, needCopy: true);
            btnNewTest_Click(null, null);
        }
        void DoTest()
        {
            var b = vm.HasNext;
            lblIndex.Visibility = b ? Visibility.Visible : Visibility.Hidden;
            lblCorrect.Visibility = Visibility.Hidden;
            lblIncorrect.Visibility = Visibility.Hidden;
            btnCheck.IsEnabled = b;
            lblPhraseTarget.Content = vm.CurrentPhrase;
            lblTranslation.Content = vm.CurrentItem?.TRANSLATION ?? "";
            lblPhraseTarget.Visibility = !vm.IsTestMode ? Visibility.Visible : Visibility.Hidden;
            tbPhraseInput.Text = "";
            tbPhraseInput.Focus();
            if (b)
            {
                lblIndex.Content = $"{vm.Index + 1}/{vm.Count}";
                //if isspeaking {
                //    synth.startspeaking(vm.currentword);
                //}
            }
            else if (vm.Options.Mode == ReviewMode.ReviewAuto)
                _timer.Stop();
        }

        void btnNewTest_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new ReviewOptionsDlg(Window.GetWindow(this), vm.Options);
            if (dlg.ShowDialog() == true)
            {
                vm.NewTest();
                DoTest();
                btnCheck.Content = vm.IsTestMode ? "Check" : "Next";
                if (vm.Options.Mode == ReviewMode.ReviewAuto)
                {
                    _timer = new DispatcherTimer();
                    _timer.Interval = new TimeSpan(0, 0, 3);
                    _timer.Tick += (s, e2) => btnCheck_Click(sender, e);
                    _timer.Start();
                }
                else
                    _timer?.Stop();
            }
        }

        void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            if (!vm.IsTestMode)
            {
                vm.Next();
                DoTest();
            }
            else if (!lblCorrect.IsVisible && lblIncorrect.IsVisible)
            {
                tbPhraseInput.Text = vmSettings.AutoCorrectInput(tbPhraseInput.Text);
                lblPhraseTarget.Visibility = Visibility.Hidden;
                if (tbPhraseInput.Text == vm.CurrentPhrase)
                    lblCorrect.Visibility = Visibility.Visible;
                else
                    lblIncorrect.Visibility = Visibility.Visible;
                btnCheck.Content = "Next";
                vm.Check(tbPhraseInput.Text);
            }
            else
            {
                vm.Next();
                DoTest();
                btnCheck.Content = "Check";
            }
        }

        void tbPhraseInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return) return;
            btnCheck_Click(sender, null);
        }

        void chkSpeak_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
