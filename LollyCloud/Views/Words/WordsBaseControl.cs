using Dragablz;
using LollyCommon;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LollyCloud
{
    public class WordsPhrasesBaseControl : UserControl, ILollySettings
    {
        protected string originalText = "";
        public virtual WordsPhrasesBaseViewModel vmWP => null;
        public virtual SettingsViewModel vmSettings => null;
        public virtual ToolBar ToolBarDictBase => null;
        public virtual TabablzControl tcDictsBase => null;
        public ObservableCollection<ActionTabItem> Tabs { get; } = new ObservableCollection<ActionTabItem>();
        public ActionInterTabClient ActionInterTabClient { get; } = new ActionInterTabClient();

        public virtual void dgWords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var word = vmWP.SelectedWord == "" ? vmWP.NewWord : vmWP.SelectedWord;
            Tabs.ForEach(async o => await ((WordsDictControl)o.Content).SearchDict(word));
            App.Speak(vmSettings, vmWP.SelectedWord);
            SearchPhrases();
        }
        public virtual async Task SearchPhrases() { }

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
                    await c.SearchDict(vmWP.SelectedWord);
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
            var self = UIHelper.FindVisualParent<WordsPhrasesBaseControl>(args.DragablzItem);
            var o = self.ToolBarDictBase.Items.Cast<CheckBox>().First(o2 => (string)o2.Content == name);
            o.IsChecked = false;
        };
    }
    public class WordsBaseControl : WordsPhrasesBaseControl
    {
        public void miCopy_Click(object sender, RoutedEventArgs e) => Clipboard.SetText(vmWP.SelectedWord);
        public void miGoogle_Click(object sender, RoutedEventArgs e) => vmWP.SelectedWord.Google();
        public void miOnlineDict_Click(object sender, RoutedEventArgs e) =>
            Tabs.ForEach(o => Process.Start(((WordsDictControl)o.Content).Url));

    }
}
