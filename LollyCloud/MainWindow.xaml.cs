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
        public static SettingsViewModel vmSettings = new SettingsViewModel();

        public MainWindow()
        {
            InitializeComponent();
            // https://stackoverflow.com/questions/3145511/how-to-set-the-default-font-for-a-wpf-application
            Style = (Style)FindResource(typeof(Window));
            Init();
        }

        async void Init() => await vmSettings.GetData();

        void miSettings_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SettingsDlg();
            dlg.ShowDialog();
        }

        void miWordsUnit_Click(object sender, RoutedEventArgs e)
        {
            tcMain.Items.Add(new TabItem { Header = "Words in Unit", Content = new WordsUnitControl() });
        }
    }
}
