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
using System.Windows.Shapes;


namespace LollyCloud
{
    /// <summary>
    /// WordsLangDetailDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsLangDetailDlg : Window
    {
        public MLangWord itemOriginal;
        public SettingsViewModel vmSettings => vm.vmSettings;
        public WordsLangViewModel vm;
        MLangWord item = new MLangWord();
        public WordsLangDetailDlg()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            tbWord.Focus();
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            itemOriginal.CopyProperties(item);
            DataContext = item;
            dgWords.DataContext = new SingleWordViewModel(item.WORD, vmSettings);
        }

        async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            item.WORD = vmSettings.AutoCorrectInput(item.WORD);
            if (item.ID == 0)
                item.ID = await vm.Create(item);
            else
                await vm.Update(item);
            item.CopyProperties(itemOriginal);
            DialogResult = true;
            Close();
        }

        void btnCancel_Click(object sender, RoutedEventArgs e) => Close();
    }
}
