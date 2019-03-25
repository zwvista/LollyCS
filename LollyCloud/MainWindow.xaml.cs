using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ActionTabViewModal vmd;

        public MainWindow()
        {
            InitializeComponent();
            // https://stackoverflow.com/questions/3145511/how-to-set-the-default-font-for-a-wpf-application
            Style = (Style)FindResource(typeof(Window));
            // https://stackoverflow.com/questions/43528152/how-to-close-tab-with-a-close-button-in-wpf
            // Initialize viewModel
            vmd = new ActionTabViewModal();
            // Bind the xaml TabControl to view model tabs
            tcMain.ItemsSource = vmd.Tabs;

            Init();
        }

        async void Init() => await vmSettings.GetData();

        async void miSettings_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new SettingsDlg();
            dlg.Owner = this;
            dlg.ShowDialog();
            await vmSettings.GetData();
            foreach (var t in vmd.Tabs)
                (t.Content as ILollySettings)?.OnSettingChanged();
        }

        void miWordsUnit_Click(object sender, RoutedEventArgs e)
        {
            vmd.Tabs.Add(new ActionTabItem { Header = "Words in Unit", Content = new WordsUnitControl() });
            tcMain.SelectedIndex = tcMain.Items.Count - 1;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // This event will be thrown when on a close image clicked
            vmd.Tabs.RemoveAt(tcMain.SelectedIndex);
        }
    }
    // This class will be the Tab in the TabControl
    public class ActionTabItem
    {
        // This will be the text in the tab control
        public string Header { get; set; }
        // This will be the content of the tab control It is a UserControl whits you need to create manually
        public UserControl Content { get; set; }
    }
    /// view model for the TabControl To bind on
    public class ActionTabViewModal
    {
        // These Are the tabs that will be bound to the TabControl 
        public ObservableCollection<ActionTabItem> Tabs { get; set; }

        public ActionTabViewModal()
        {
            Tabs = new ObservableCollection<ActionTabItem>();
        }
    }
}
