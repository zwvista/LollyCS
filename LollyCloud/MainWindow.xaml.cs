using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using LollyShared;

namespace LollyCloud
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        SettingsViewModel vmSettings = new SettingsViewModel();
        WordsUnitViewModel vmWords;

        public MainWindow()
        {
            InitializeComponent();
            // https://stackoverflow.com/questions/3145511/how-to-set-the-default-font-for-a-wpf-application
            Style = (Style)FindResource(typeof(Window));
            // Can't call Init().Wait(); here
            Init();
        }

        async Task Init()
        {
            await vmSettings.GetData();
            vmWords = await WordsUnitViewModel.CreateAsync(vmSettings);
            dg.ItemsSource = vmWords.UnitWords;
            for (int i = 0; i < vmSettings.DictItems.Count; i++)
            {
                var b = new RadioButton
                {
                    Content = vmSettings.DictItems[i].DICTNAME,
                    GroupName = "DICT"
                };
                ToolBar1.Items.Add(b);
                if (i == vmSettings.SelectedDictItemIndex)
                    b.IsChecked = true;
            }
        }

        private void Dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
