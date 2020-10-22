using Dragablz;
using Hardcodet.Wpf.Util;
using LollyCommon;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace LollyCloud
{
    public class WordsPhrasesBaseControl : UserControl, ILollySettings
    {
        string originalText = "";
        protected virtual WordsPhrasesBaseViewModel vmWP => null;
        public virtual SettingsViewModel vmSettings => null;
        protected virtual ToolBar ToolBarDictBase => null;
        protected virtual TabablzControl tcDictsBase => null;
        public ObservableCollection<ActionTabItem> Tabs { get; } = new ObservableCollection<ActionTabItem>();
        public ActionInterTabClient ActionInterTabClient { get; } = new ActionInterTabClient();

        public virtual void dgWords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var word = vmWP.SelectedWord == "" ? vmWP.NewWord : vmWP.SelectedWord;
            Tabs.ForEach(async o => await ((WordsDictControl)o.Content).SearchDict(word));
            App.Speak(vmSettings, vmWP.SelectedWord);
            GetPhrases();
        }
        public virtual async Task GetPhrases() { }
        public void dgPhrases_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.Speak(vmSettings, vmWP.SelectedPhrase);
            GetWords();
        }
        public virtual async Task GetWords() { }

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
        public void miCopyWord_Click(object sender, RoutedEventArgs e) => Clipboard.SetText(vmWP.SelectedWord);
        public void miGoogleWord_Click(object sender, RoutedEventArgs e) => vmWP.SelectedWord.Google();
        public void miOnlineDict_Click(object sender, RoutedEventArgs e) =>
            Tabs.ForEach(o => Process.Start(((WordsDictControl)o.Content).Url));
        public void miCopyPhrase_Click(object sender, RoutedEventArgs e) => Clipboard.SetText(vmWP.SelectedPhrase);
        public void miGooglePhrase_Click(object sender, RoutedEventArgs e) => vmWP.SelectedPhrase.Google();
        public void OnBeginEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            var o = e.EditingEventArgs.Source;
            var o2 = (TextBlock)((o as DataGridCell)?.Content ?? o);
            originalText = o2.Text;
        }
        protected void OnEndEdit(object sender, DataGridCellEditEndingEventArgs e, string autoCorrectColumnName, Func<object, Task> updateFunc)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var item = e.Row.DataContext;
                var el = (TextBox)e.EditingElement;
                if (((Binding)((DataGridBoundColumn)e.Column).Binding).Path.Path == autoCorrectColumnName)
                    el.Text = vmSettings.AutoCorrectInput(el.Text);
                if (el.Text != originalText)
                    Observable.Timer(TimeSpan.FromMilliseconds(100), RxApp.MainThreadScheduler).Subscribe(async _ => {
                        await updateFunc(item);
                        ((DataGrid)sender).CancelEdit();
                    });
            }
        }
    }
    public class WordsBaseControl : WordsPhrasesBaseControl
    {
        protected PhrasesLangViewModel vmPhrasesLang;
        MLangPhrase SelectedPhraseItem => (MLangPhrase)vmWP.SelectedPhraseItem;
        public override async Task OnSettingsChanged()
        {
            await base.OnSettingsChanged();
            vmPhrasesLang = new PhrasesLangViewModel(vmSettings, false);
        }
        public void OnEndEditPhrase(object sender, DataGridCellEditEndingEventArgs e) =>
            OnEndEdit(sender, e, "PHRASE", async item => await vmPhrasesLang.Update((MLangPhrase)item));

        // https://stackoverflow.com/questions/22790181/wpf-datagrid-row-double-click-event-programmatically
        public void dgPhrases_RowDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UIHelpers.TryFindParent<DataGrid>((DataGridRow)sender).CancelEdit();
            miEditPhrase_Click(sender, null);
        }
        public void miEditPhrase_Click(object sender, RoutedEventArgs e)
        {
            // https://stackoverflow.com/questions/16236905/access-parent-window-from-user-control
            var dlg = new PhrasesLangDetailDlg(Window.GetWindow(this), vmPhrasesLang, SelectedPhraseItem);
            dlg.ShowDialog();
        }
    }
}
