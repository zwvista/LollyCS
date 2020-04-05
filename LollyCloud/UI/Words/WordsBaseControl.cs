using Dragablz;
using LollyShared;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyCloud
{
    public class WordsBaseControl : UserControl, ILollySettings
    {
        protected DictWebBrowserStatus dictStatus = DictWebBrowserStatus.Ready;
        protected string selectedWord = "";
        protected int selectedWordID = 0;
        protected string originalText = "";
        protected virtual string NewWord => null;
        public virtual DataGrid dgWordsBase => null;
        public virtual MWordInterface ItemForRow(int row) => null;
        public virtual SettingsViewModel vmSettings => null;
        public virtual ToolBar ToolBarDictBase => null;
        public virtual TabablzControl tcDictsBase => null;
        public ObservableCollection<ActionTabItem> Tabs { get; } = new ObservableCollection<ActionTabItem>();
        public ActionInterTabClient ActionInterTabClient { get; } = new ActionInterTabClient();

        public virtual void dgWords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchDict(null, null);
            SearchPhrases();
            App.Speak(vmSettings, selectedWord);
        }

        public void SearchDict(object sender, RoutedEventArgs e)
        {
            void f(string word) =>
                Tabs.ForEach(async o => await ((WordsDictControl)o.Content).SearchWord(word));

            var row = dgWordsBase.SelectedIndex;
            if (row == -1)
            {
                selectedWord = "";
                selectedWordID = 0;
                f(NewWord);
            }
            else
            {
                var item = ItemForRow(row);
                selectedWord = item.WORD;
                selectedWordID = item.WORDID;
                f(selectedWord);
            }
        }

        public void miCopy_Click(object sender, RoutedEventArgs e) => Clipboard.SetText(selectedWord);

        public void miGoogle_Click(object sender, RoutedEventArgs e) => CommonApi.GoogleString(selectedWord);

        public async Task ChangeLevel(int delta)
        {
            var row = dgWordsBase.SelectedIndex;
            if (row == -1) return;
            var item = ItemForRow(row);
            var newLevel = item.LEVEL + delta;
            if (newLevel != 0 && !vmSettings.USLEVELCOLORS.ContainsKey(newLevel)) return;
            item.LEVEL = newLevel;
            await LevelChanged(row);
        }

        public virtual async Task LevelChanged(int row) { }

        public virtual async void dgWords_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyboardDevice.Modifiers == ModifierKeys.Alt && (e.SystemKey == Key.Up || e.SystemKey == Key.Down))
            {
                await ChangeLevel(e.SystemKey == Key.Up ? 1 : -1);
                e.Handled = true;
            }
        }

        public virtual async Task OnSettingsChanged()
        {
            Tabs.Clear();
            ToolBarDictBase.Items.Clear();
            int j = -1;
            vmSettings.DictsReference.ForEach((item, i) =>
            {
                var name = item.DICTNAME;
                var b = new CheckBox
                {
                    Content = name,
                    Tag = item,
                };
                b.Click += async (s, e) =>
                {
                    var o = Tabs.FirstOrDefault(o2 => o2.Header == name);
                    if (o == null)
                    {
                        var item2 = (MDictionary)((CheckBox)s).Tag;
                        var item3 = vmSettings.DictsReference.First(o2 => o2.DICTNAME == item2.DICTNAME);
                        var c = new WordsDictControl
                        {
                            vmSettings = vmSettings,
                            Dict = item3
                        };
                        Tabs.Add(new ActionTabItem { Header = name, Content = c });
                        tcDictsBase.SelectedIndex = tcDictsBase.Items.Count - 1;
                        await c.SearchWord(selectedWord);
                    }
                    else
                        Tabs.Remove(o);
                };
                ToolBarDictBase.Items.Add(b);
                // I don't know why, but if we click the button here,
                // buttons to be added later will not be added to the toolbar.
                if (item == vmSettings.SelectedDictReference) j = i;
            });
            if (j != -1)
            {
                var b = (CheckBox)ToolBarDictBase.Items[j];
                b.IsChecked = true;
                b.PerformClick();
            }
        }
        public ItemActionCallback ClosingTabItemHandler { get; } = args =>
        {
            var name = ((ActionTabItem)args.DragablzItem.Content).Header;
            var self = UIHelper.FindVisualParent<WordsBaseControl>(args.DragablzItem);
            var o = self.ToolBarDictBase.Items.Cast<CheckBox>().First(o2 => (string)o2.Content == name);
            o.IsChecked = false;
        };

        public virtual async Task SearchPhrases() { }
    }
}
