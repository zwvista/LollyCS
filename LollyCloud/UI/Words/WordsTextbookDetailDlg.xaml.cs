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
    /// WordsTextbookDetailDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsTextbookDetailDlg : Window
    {
        public MUnitWord itemOriginal;
        public SettingsViewModel vmSettings => vm.vmSettings;
        public WordsUnitViewModel vm;
        MUnitWord2 item = new MUnitWord2();
        public WordsTextbookDetailDlg()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            tbWord.Focus();
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var o = item.VM;
            itemOriginal.CopyProperties(o);
            DataContext = item;
            dgWords.DataContext = new SingleWordViewModel(o.WORD, vmSettings);
        }

        async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            var o = item.VM;
            o.WORD = vmSettings.AutoCorrectInput(o.WORD);
            if (o.ID == 0)
                o.ID = await vm.Create(o);
            else
                await vm.Update(o);
            o.CopyProperties(itemOriginal);
            DialogResult = true;
            Close();
        }

        void btnCancel_Click(object sender, RoutedEventArgs e) => Close();
    }
}
