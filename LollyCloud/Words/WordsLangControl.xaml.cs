﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
using mshtml;

namespace LollyCloud
{
    /// <summary>
    /// WordsLangControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsLangControl : UserControl, ILollySettings
    {
        public SettingsViewModel vmSettings => MainWindow.vmSettings;
        public WordsLangViewModel vm { get; set; }
        DictWebBrowserStatus status = DictWebBrowserStatus.Ready;
        int selectedDictItemIndex;
        string selectedWord = "";

        public WordsLangControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        void dgWords_SelectionChanged(object sender, SelectionChangedEventArgs e) => SearchDict(null, null);

        void SearchDict(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton)
                selectedDictItemIndex = (int)(sender as RadioButton).Tag;
            var row = dgWords.SelectedIndex;
            if (row == -1) return;
            selectedWord = vm.Items[row].WORD;
            SearchWord(selectedWord);
        }

        async void SearchWord(string word)
        {
            status = DictWebBrowserStatus.Ready;
            var item = vmSettings.DictItems[selectedDictItemIndex];
            if (item.DICTNAME.StartsWith("Custom"))
            {
                var str = vmSettings.DictHtml(word, item.DictIDs.ToList());
                wbDict.NavigateToString(str);
            }
            else
            {
                var item2 = vmSettings.DictsMean.First(o => o.DICTNAME == item.DICTNAME);
                var url = item2.UrlString(word, vmSettings.AutoCorrects.ToList());
                if (item2.DICTTYPENAME == "OFFLINE")
                {
                    wbDict.Navigate("about:blank");
                    var html = await vmSettings.client.GetStringAsync(url);
                    var str = item2.HtmlString(html, word);
                    wbDict.Navigate(str);
                }
                else
                {
                    wbDict.Navigate(url);
                    if (item2.DICTTYPENAME == "OFFLINE-ONLINE")
                        status = DictWebBrowserStatus.Navigating;
                }
            }
        }

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        void dgWords_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var dlg = new WordsLangDetailDlg();
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            dlg.Owner = Window.GetWindow(this);
            dlg.itemOriginal = (sender as DataGridRow).Item as MLangWord;
            dlg.vm = vm;
            dlg.ShowDialog();
        }

        void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new WordsLangDetailDlg();
            dlg.Owner = Window.GetWindow(this);
            dlg.itemOriginal = vm.NewLangWord();
            dlg.vm = vm;
            dlg.ShowDialog();
            vm.Items.Add(dlg.itemOriginal);
        }

        async void dgWords_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var item = vm.Items[e.Row.GetIndex()];
                await vm.Update(item);
            }
        }

        void wbDict_Navigated(object sender, NavigationEventArgs e) => wbDict.SetSilent(true);

        void wbDict_LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (status != DictWebBrowserStatus.Navigating) return;
            var item = vmSettings.DictItems[selectedDictItemIndex];
            var item2 = vmSettings.DictsMean.FirstOrDefault(o => o.DICTNAME == item.DICTNAME);
            var doc = (HTMLDocument)wbDict.Document;
            var html = doc.documentElement.outerHTML;
            var str = item2.HtmlString(html, selectedWord);
            status = DictWebBrowserStatus.Ready;
            wbDict.NavigateToString(str);
        }

        async void btnRefresh_Click(object sender, RoutedEventArgs e) => await OnSettingsChanged();

        public async Task OnSettingsChanged()
        {
            vm = await WordsLangViewModel.CreateAsync(vmSettings);
            selectedDictItemIndex = vmSettings.SelectedDictItemIndex;
            dgWords.ItemsSource = vm.Items;
            ToolBar1.Items.Clear();
            for (int i = 0; i < vmSettings.DictItems.Count; i++)
            {
                var b = new RadioButton
                {
                    Content = vmSettings.DictItems[i].DICTNAME,
                    GroupName = "DICT",
                    Tag = i,
                };
                b.Click += SearchDict;
                ToolBar1.Items.Add(b);
                if (i == selectedDictItemIndex)
                    b.IsChecked = true;
            }
        }

        async void miDelete_Click(object sender, RoutedEventArgs e)
        {
            var row = dgWords.SelectedIndex;
            if (row == -1) return;
            var item = vm.Items[row];
            await vm.Delete(item.ID);
        }

        void miCopy_Click(object sender, RoutedEventArgs e) => Clipboard.SetText(selectedWord);

        void miGoogle_Click(object sender, RoutedEventArgs e) => CommonApi.GoogleString(selectedWord);

        async void tbNewWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return || string.IsNullOrEmpty(vm.NewWord)) return;
            var item = vm.NewLangWord();
            item.WORD = vmSettings.AutoCorrect(vm.NewWord);
            vm.NewWord = "";
            item.ID = await vm.Create(item);
            vm.Items.Add(item);
        }

        async Task ChangeLevel(int delta)
        {
            var row = dgWords.SelectedIndex;
            if (row == -1) return;
            var item = vm.Items[row];
            var newLevel = item.LEVEL + delta;
            if (newLevel != 0 && !vmSettings.USLEVELCOLORS.ContainsKey(newLevel)) return;
            item.LEVEL = newLevel;
            await vmSettings.UpdateLevel(item.ID, item.LEVEL);
        }

        async void dgWords_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Alt && (e.SystemKey == Key.Up || e.SystemKey == Key.Down))
            {
                await ChangeLevel(e.SystemKey == Key.Up ? 1 : -1);
                e.Handled = true;
            }
        }
    }
}
