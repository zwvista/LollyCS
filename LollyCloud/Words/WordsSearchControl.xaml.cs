using Hardcodet.Wpf.Util;
using LollyShared;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// WordsSearchControl.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsSearchControl : WordsBaseControl
    {
        public WordsSearchViewModel vm { get; set; }
        public override DataGrid dgWordsBase => dgWords;
        public override MWordInterface ItemForRow(int row) => vm.WordItems[row];
        public override SettingsViewModel vmSettings => vm.vmSettings;
        public override WebBrowser wbDictBase => wbDict;
        public override ToolBar ToolBarDictBase => ToolBarDict;
        public override TextBox tbURLBase => tbURL;

        public WordsSearchControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }

        public override async Task OnSettingsChanged()
        {
            vm = new WordsSearchViewModel(MainWindow.vmSettings, needCopy: true);
            DataContext = this;
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
            if (e.Key != Key.Return || string.IsNullOrEmpty(vm.NewWord)) return;
            var item = new MUnitWord
            {
                WORD = vmSettings.AutoCorrectInput(vm.NewWord),
                SEQNUM = vm.WordItems.Count + 1,
                NOTE = ""
            };
            vm.WordItems.Add(item);
        }

        public void SearchWord(string word)
        {
            vm.NewWord = word;
            tbNewWord_KeyDown(null, null);
        }

    }
}
