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
    /// PhrasesLangDetailDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class PhrasesLangDetailDlg : Window
    {
        public MLangPhrase itemOriginal;
        public SettingsViewModel vmSettings => MainWindow.vmSettings;
        public PhrasesLangViewModel vm;
        MLangPhrase2 item = new MLangPhrase2();
        public PhrasesLangDetailDlg()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            tbPhrase.Focus();
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var o = item.VM;
            itemOriginal.CopyProperties(o);
            DataContext = item;
            dgPhrases.DataContext = new SinglePhraseViewModel(o.PHRASE, vmSettings);
        }

        async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            var o = item.VM;
            o.PHRASE = vmSettings.AutoCorrectInput(o.PHRASE);
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
