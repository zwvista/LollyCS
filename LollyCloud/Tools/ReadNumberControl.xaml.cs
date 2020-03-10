using LollyShared;
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
    public partial class ReadNumberControl : ReactiveUserControl<ReadNumberViewModel>, ILollySettings
    {
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
            foreach (var o in vmSettings.ReadNumberType)
            {
                var btn = new RadioButton
                {
                    Content = o.NAME,
                    GroupName = "READNUMBER",
                };
                btn.SetBinding(RadioButton.IsCheckedProperty, new Binding("Type")
                {
                    Converter = new EnumBooleanConverter(),
                    ConverterParameter = ((ReadNumberType)o.CODE).ToString(),
                });
                ToolBar1.Items.Add(btn);
            }
        }
    }
}
