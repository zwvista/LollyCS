using Dragablz;
using LollyShared;
using MSHTML;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace LollyCloud
{
    public class WordsBaseControl : UserControl, ILollySettings
    {
        protected DictWebBrowserStatus dictStatus = DictWebBrowserStatus.Ready;
        protected int selectedDictItemIndex;
        protected string selectedWord = "";
        protected int selectedWordID = 0;
        protected string originalText = "";
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
            if (sender is RadioButton)
                selectedDictItemIndex = (int)(sender as RadioButton).Tag;
            var row = dgWordsBase.SelectedIndex;
            if (row == -1)
            {
                selectedWord = "";
                selectedWordID = 0;
            }
            else
            {
                var item = ItemForRow(row);
                selectedWord = item.WORD;
                selectedWordID = item.WORDID;
                Tabs.ForEach(o => ((WordsDictControl)o.Content).SearchWord(selectedWord));
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

        public virtual async void btnRefresh_Click(object sender, RoutedEventArgs e) => await OnSettingsChanged();


        public virtual async Task OnSettingsChanged()
        {
            selectedDictItemIndex = vmSettings.SelectedDictItemIndex;
            ToolBarDictBase.Items.Clear();
            for (int i = 0; i < vmSettings.DictItems.Count; i++)
            {
                var name = vmSettings.DictItems[i].DICTNAME;
                var b = new CheckBox
                {
                    Content = name,
                    Tag = i,
                };
                b.Click += (s, e) =>
                {
                    var o = Tabs.FirstOrDefault(o2 => o2.Header == name);
                    if (o == null)
                    {
                        var c = new WordsDictControl
                        {
                            vmSettings = vmSettings,
                            selectedDictItemIndex = (int)((CheckBox)s).Tag
                        };
                        Tabs.Add(new ActionTabItem { Header = name, Content = c });
                        tcDictsBase.SelectedIndex = tcDictsBase.Items.Count - 1;
                        c.SearchWord(selectedWord);
                    }
                    else
                        Tabs.Remove(o);
                };
                ToolBarDictBase.Items.Add(b);
                if (i == selectedDictItemIndex)
                {
                    b.IsChecked = true;
                    b.PerformClick();
                }
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
