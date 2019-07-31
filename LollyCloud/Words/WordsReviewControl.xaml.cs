using Hardcodet.Wpf.Util;
using LollyShared;
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
        DispatcherTimer _timer;

        public WordsReviewControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        public async Task OnSettingsChanged()
        {
            vm = await WordsReviewViewModel.CreateAsync(MainWindow.vmSettings, needCopy: true);
        }

        async Task DoTest()
        {
            var b = vm.HasNext;
            lblIndex.Visibility = b ? Visibility.Visible : Visibility.Hidden;
            lblCorrect.Visibility = Visibility.Hidden;
            lblIncorrect.Visibility = Visibility.Hidden;
            lblAccuracy.Visibility = vm.IsTestMode && b ? Visibility.Visible : Visibility.Hidden;
            btnCheck.IsEnabled = b;
            lblWordTarget.Content = vm.CurrentWord;
            lblNote.Content = vm.CurrentItem?.NOTE ?? "";
            lblWordTarget.Visibility = !vm.IsTestMode ? Visibility.Visible : Visibility.Hidden;
            lblNote.Visibility = !vm.IsTestMode ? Visibility.Visible : Visibility.Hidden;
            tbTranslation.Text = "";
            tbWordInput.Text = "";
            tbWordInput.Focus();
            if (b)
            {
                lblIndex.Content = $"{vm.Index + 1}/{vm.Count}";
                lblAccuracy.Content = vm.CurrentItem.ACCURACY;
                //if isspeaking {
                //    synth.startspeaking(vm.currentword);
                //}
                tbTranslation.Text = await vm.GetTranslation();
            }
            else if (vm.Options.Mode == ReviewMode.ReviewAuto)
                _timer.Stop();
        }

        async void btnNewTest_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new ReviewOptionsDlg();
            dlg.Owner = UIHelpers.TryFindParent<Window>(this);
            dlg.optionsOriginal = vm.Options;
            if (dlg.ShowDialog() == true)
            {
                await vm.NewTest();
                await DoTest();
                btnCheck.Content = vm.IsTestMode ? "Check" : "Next";
                if (vm.Options.Mode == ReviewMode.ReviewAuto)
                {
                    _timer = new DispatcherTimer();
                    _timer.Interval = new TimeSpan(0, 0, 3);
                    _timer.Tick += (s, e2) => btnCheck_Click(sender, e);
                    _timer.Start();
                }
            }
        }

        async void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            if (!vm.IsTestMode)
            {
                vm.Next();
                await DoTest();
            }
            else if (!lblCorrect.IsVisible && lblIncorrect.IsVisible)
            {
                tbWordInput.Text = vmSettings.AutoCorrectInput(tbWordInput.Text);
                lblWordTarget.Visibility = Visibility.Hidden;
                lblNote.Visibility = Visibility.Hidden;
                if (tbWordInput.Text == vm.CurrentWord)
                    lblCorrect.Visibility = Visibility.Visible;
                else
                    lblIncorrect.Visibility = Visibility.Visible;
                btnCheck.Content = "Next";
                await vm.Check(tbWordInput.Text);
            }
            else
            {
                vm.Next();
                await DoTest();
                btnCheck.Content = "Check";
            }
        }

        void tbWordInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return) return;
            btnCheck_Click(sender, null);
        }

        void chkSpeak_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
