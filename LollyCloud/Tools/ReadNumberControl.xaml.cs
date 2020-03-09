using LollyShared;
using ReactiveUI;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// BlogControl.xaml の相互作用ロジック
    /// </summary>
    public partial class ReadNumberControl : UserControl, IViewFor<ReadNumberViewModel>, ILollySettings
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
            .Register(nameof(ViewModel), typeof(ReadNumberViewModel), typeof(ReadNumberControl));
        public ReadNumberViewModel ViewModel
        {
            get => (ReadNumberViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }
        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (ReadNumberViewModel)value;
        }
        private SettingsViewModel vmSettings => ViewModel.vmSettings;

        public ReadNumberControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        public async Task OnSettingsChanged()
        {
            ViewModel = new ReadNumberViewModel(MainWindow.vmSettings, true);
            DataContext = ViewModel;
            ToolBar1.Items.Clear();
            foreach (var o in vmSettings.ReadNumberTypes)
            {
                var b = new RadioButton
                {
                    Content = o.NAME,
                    GroupName = "READNUMBER",
                    Tag = o.CODE,
                };
                b.Click += (o2, e) => vmSettings.USREADNUMBERID = (int)((RadioButton)o2).Tag;
                ToolBar1.Items.Add(b);
                if (o.CODE == vmSettings.USREADNUMBERID)
                    b.IsChecked = true;
            }
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            int i = vmSettings.USREADNUMBERID;
            ViewModel.Text = i == 1 ? ReadNumberService.readInJapanese(ViewModel.Number) :
                i == 2 ? ReadNumberService.readInNativeKorean(ViewModel.Number) :
                ReadNumberService.readInSinoKorean(ViewModel.Number);
        }
    }
}
