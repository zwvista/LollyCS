using Dragablz;
using LollyCommon;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyCloud
{
    public class WordsPhraseBaseControl : UserControl, ILollySettings
    {
        protected string selectedWord = "";
        protected int selectedWordID = 0;
        protected string originalText = "";
        public virtual DataGrid dgWordsBase => null;
        public virtual SettingsViewModel vmSettings => null;
        public virtual ToolBar ToolBarDictBase => null;
        public virtual TabablzControl tcDictsBase => null;
        public ObservableCollection<ActionTabItem> Tabs { get; } = new ObservableCollection<ActionTabItem>();
        public ActionInterTabClient ActionInterTabClient { get; } = new ActionInterTabClient();

        public virtual void dgWords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchDict(null, null);
            App.Speak(vmSettings, selectedWord);
        }

        public void SearchDict(object sender, RoutedEventArgs e)
        {
            void f(string word) =>
                Tabs.ForEach(async o => await ((WordsDictControl)o.Content).SearchWord(word));

            var item = (MWordInterface)dgWordsBase.SelectedItem;
            if (item == null)
            {
                selectedWord = "";
                selectedWordID = 0;
            }
            else
            {
                selectedWord = item.WORD;
                selectedWordID = item.WORDID;
            }
        }

        public virtual async Task OnSettingsChanged()
        {
            async Task F(string name)
            {
                var o = Tabs.FirstOrDefault(o2 => o2.Header == name);
                if (o == null)
                {
                    var c = new WordsDictControl
                    {
                        vmSettings = vmSettings,
                        Dict = vmSettings.DictsReference.First(o2 => o2.DICTNAME == name)
                    };
                    // Disable image loading
                    // c.wbDict.BrowserSettings.ImageLoading = CefState.Disabled;
                    Tabs.Add(new ActionTabItem { Header = name, Content = c });
                    tcDictsBase.SelectedIndex = tcDictsBase.Items.Count - 1;
                    await c.SearchWord(selectedWord);
                }
                else
                    Tabs.Remove(o);
            }

            Tabs.Clear();
            ToolBarDictBase.Items.Clear();
            foreach (var o in vmSettings.DictsReference)
            {
                var name = o.DICTNAME;
                var b = new CheckBox
                {
                    Content = name,
                    Tag = o,
                };
                b.Click += async (s, e) => await F(name);
                ToolBarDictBase.Items.Add(b);
            }
            foreach (var o in vmSettings.SelectedDictsReference)
            {
                var b = ToolBarDictBase.Items.Cast<CheckBox>().First(o2 => o2.Tag == o);
                b.IsChecked = true;
                await F(o.DICTNAME);
            }
        }
        public ItemActionCallback ClosingTabItemHandler { get; } = args =>
        {
            var name = ((ActionTabItem)args.DragablzItem.Content).Header;
            var self = UIHelper.FindVisualParent<WordsBaseControl>(args.DragablzItem);
            var o = self.ToolBarDictBase.Items.Cast<CheckBox>().First(o2 => (string)o2.Content == name);
            o.IsChecked = false;
        };

    }
    public class WordsBaseControl : WordsPhraseBaseControl
    {
        protected virtual string NewWord => null;

        public override void dgWords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            base.dgWords_SelectionChanged(sender, e);
            SearchPhrases();
        }

        public void SearchDict(object sender, RoutedEventArgs e)
        {
            base.SearchDict(sender, e);
        }

        public void miCopy_Click(object sender, RoutedEventArgs e) => Clipboard.SetText(selectedWord);

        public void miGoogle_Click(object sender, RoutedEventArgs e) => selectedWord.Google();

        public virtual async Task SearchPhrases() { }
    }
}
