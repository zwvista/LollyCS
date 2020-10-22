using Hardcodet.Wpf.Util;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using Dragablz;
using LollyCommon;

namespace LollyCloud
{
    /// <summary>
    /// WordsSearchControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsSearchControl : WordsBaseControl
    {
        WordsSearchViewModel vm;
        protected override WordsBaseViewModel vmWords => vm;
        public override SettingsViewModel vmSettings => vm.vmSettings;
        protected override ToolBar ToolBarDictBase => ToolBarDict;
        protected override TabablzControl tcDictsBase => tcDicts;

        public WordsSearchControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        public override async Task OnSettingsChanged()
        {
            DataContext = vm = new WordsSearchViewModel(MainWindow.vmSettings, needCopy: true);
            tcDicts.DataContext = this;
            await base.OnSettingsChanged();
        }

        void miDelete_Click(object sender, RoutedEventArgs e)
        {
            var row = dgWords.SelectedIndex;
            if (row == -1) return;
            var item = vm.WordItems[row];
        }

        void tbNewWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (!(e.Key == Key.Return || e.Key == Key.System) || string.IsNullOrEmpty(vm.NewWord)) return;
            SearchNewWord(vm.NewWord);
            vm.NewWord = "";
        }

        public void SearchNewWord(string word)
        {
            var item = new MUnitWord
            {
                WORD = vmSettings.AutoCorrectInput(word),
                SEQNUM = vm.WordItems.Count + 1,
                NOTE = ""
            };
            vm.WordItems.Add(item);
            dgWords.SelectedItem = vm.WordItems.Last();
        }
        public void btnRefresh_Click(object sender, RoutedEventArgs e) =>
            vm.WordItems.Clear();
    }
}
