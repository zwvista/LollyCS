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
        SettingsViewModel vmSettings = new SettingsViewModel();
        WordsUnitViewModel vmWords;
        DictWebBrowserStatus status = DictWebBrowserStatus.Ready;

        public WordsUnitControl()
        {
            InitializeComponent();
            // Can't call Init().Wait(); here
            Init();
        }

        async Task Init()
        {
            await vmSettings.GetData();
            vmWords = await WordsUnitViewModel.CreateAsync(vmSettings);
            dgWords.ItemsSource = vmWords.UnitWords;
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

        private void dgWords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchDict(sender);
        }

        private void SearchDict(object sender)
        {
            var row = dgWords.SelectedIndex;
            SearchWord(vmWords.UnitWords[row].WORD);
        }

        private async void SearchWord(string word)
        {
            status = DictWebBrowserStatus.Ready;
            var item = vmSettings.SelectedDictItem;
            if (item.DICTNAME.StartsWith("Custom"))
            {
                var str = vmSettings.DictHtml(word, item.DictIDs.ToList());
                wbDict.NavigateToString(str);
            }
            else
            {
                var item2 = vmSettings.DictsMean.First(o => o.DICTNAME == item.DICTNAME);
                var url = item2.UrlString(word, vmSettings.AutoCorrects.ToList());
                if (item2.DICTNAME == "OFFLINE")
                {
                    wbDict.Navigate("about:blank");
                    var html = await vmSettings.client.GetStringAsync(url);
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
    }
}
