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
    /// WordsUnitControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsUnitControl : UserControl
    {
        WordsUnitViewModel vm;
        DictWebBrowserStatus status = DictWebBrowserStatus.Ready;
        int selectedDictItemIndex;

        public WordsUnitControl()
        {
            InitializeComponent();
            // Can't call Init().Wait(); here
            Init();
        }

        async Task Init()
        {
            vm = await WordsUnitViewModel.CreateAsync(MainWindow.vmSettings);
            selectedDictItemIndex = vm.vmSettings.SelectedDictItemIndex;
            dgWords.ItemsSource = vm.UnitWords;
            for (int i = 0; i < vm.vmSettings.DictItems.Count; i++)
            {
                var b = new RadioButton
                {
                    Content = vm.vmSettings.DictItems[i].DICTNAME,
                    GroupName = "DICT",
                    Tag = i,
                };
                b.Click += SearchDict;
                ToolBar1.Items.Add(b);
                if (i == selectedDictItemIndex)
                    b.IsChecked = true;
            }
            wbDict.SetSilent(true);
        }

        void dgWords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchDict(null, null);
        }

        void SearchDict(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton)
                selectedDictItemIndex = (int)(sender as RadioButton).Tag;
            var row = dgWords.SelectedIndex;
            SearchWord(vm.UnitWords[row].WORD);
        }

        async void SearchWord(string word)
        {
            status = DictWebBrowserStatus.Ready;
            var item = vm.vmSettings.DictItems[selectedDictItemIndex];
            if (item.DICTNAME.StartsWith("Custom"))
            {
                var str = vm.vmSettings.DictHtml(word, item.DictIDs.ToList());
                wbDict.NavigateToString(str);
            }
            else
            {
                var item2 = vm.vmSettings.DictsMean.First(o => o.DICTNAME == item.DICTNAME);
                var url = item2.UrlString(word, vm.vmSettings.AutoCorrects.ToList());
                if (item2.DICTNAME == "OFFLINE")
                {
                    wbDict.Navigate("about:blank");
                    var html = await vm.vmSettings.client.GetStringAsync(url);
                    var str = item2.HtmlString(html, word);
                    wbDict.Navigate(str);
                }
                else
                {
                    wbDict.Navigate(url);
                    if (item2.DICTNAME == "OFFLINE-ONLINE")
                        status = DictWebBrowserStatus.Navigating;
                }
            }
        }

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgWords_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dlg = new WordsUnitDetailDlg();
            dlg.item = (sender as DataGridRow).Item as MUnitWord;
            dlg.ShowDialog();
        }
    }
}
