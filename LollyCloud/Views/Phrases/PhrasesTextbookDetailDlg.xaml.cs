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
    /// PhrasesUnitDetailDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class PhrasesTextbookDetailDlg : Window
    {
        public MUnitPhrase Item;
        public SettingsViewModel vmSettings => vm.vmSettings;
        public PhrasesUnitViewModel vm;
        MUnitPhraseEdit itemEdit = new MUnitPhraseEdit();
        public PhrasesTextbookDetailDlg()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            tbPhrase.Focus();
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Item.CopyProperties(itemEdit);
            DataContext = itemEdit;
            dgPhrases.DataContext = new SinglePhraseViewModel(Item.PHRASE, vmSettings);
        }

        async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            itemEdit.CopyProperties(Item);
            Item.PHRASE = vmSettings.AutoCorrectInput(Item.PHRASE);
            if (Item.ID == 0)
                Item.ID = await vm.Create(Item);
            else
                await vm.Update(Item);
            DialogResult = true;
            Close();
        }

        void btnCancel_Click(object sender, RoutedEventArgs e) => Close();
    }
}
