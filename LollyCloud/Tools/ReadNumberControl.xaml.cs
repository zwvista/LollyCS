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
        private int selectedReadNumberIndex;
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
            selectedReadNumberIndex = vmSettings.SelectedReadNumberIndex;
            ToolBar1.Items.Clear();
            for (int i = 0; i < vmSettings.ReadNumberTypes.Count; i++)
            {
                var b = new RadioButton
                {
                    Content = vmSettings.ReadNumberTypes[i].NAME,
                    GroupName = "READNUMBER",
                    Tag = i,
                };
                b.Click += (o, e) => vmSettings.SelectedReadNumber = vmSettings.ReadNumberTypes[(int)(o as RadioButton).Tag];
                ToolBar1.Items.Add(b);
                if (i == selectedReadNumberIndex)
                    b.IsChecked = true;
            }
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            int i = vmSettings.SelectedReadNumberIndex;
            ViewModel.Text = i == 0 ? ReadNumber.readInJapanese(ViewModel.Number) :
                i == 1 ? ReadNumber.readInNativeKorean(ViewModel.Number) :
                ReadNumber.readInSinoKorean(ViewModel.Number);
        }
    }
}
