using ReactiveUI;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// BlogControl.xaml の相互作用ロジック
    /// </summary>
    public partial class ReadNumberControl : UserControl, ILollySettings
    {
        ReadNumberViewModel vm;
        private SettingsViewModel vmSettings => vm.vmSettings;

        public ReadNumberControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        public async Task OnSettingsChanged()
        {
            vm = new ReadNumberViewModel(MainWindow.vmSettings, true);
            DataContext = vm;
            ToolBar1.Items.Clear();
            foreach (var o in vmSettings.ReadNumberCodes)
            {
                var btn = new RadioButton
                {
                    Content = o.NAME,
                    GroupName = "READNUMBER",
                };
                btn.SetBinding(RadioButton.IsCheckedProperty, new Binding("Type")
                {
                    Converter = new ComparisonConverter(),
                    ConverterParameter = (ReadNumberType)o.CODE,
                });
                ToolBar1.Items.Add(btn);
            }
        }
    }
}
