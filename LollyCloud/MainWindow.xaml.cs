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
            // Can't call Init().Wait(); here
            Init();
        }

        async Task Init()
        {
            await vmSettings.GetData();
            vmWords = await WordsUnitViewModel.CreateAsync(vmSettings);
        }
    }
}
