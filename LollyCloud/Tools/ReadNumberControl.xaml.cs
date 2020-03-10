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
    }
}
