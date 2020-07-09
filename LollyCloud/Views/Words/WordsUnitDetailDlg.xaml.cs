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
    /// WordsUnitDetailDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class WordsUnitDetailDlg : Window
    {
        public MUnitWord Item;
        public SettingsViewModel vmSettings => vm.vmSettings;
        public WordsUnitViewModel vm;
        MUnitWordEdit itemEdit = new MUnitWordEdit();
        public WordsUnitDetailDlg()
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            tbWord.Focus();
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Item.CopyProperties(itemEdit);
            DataContext = itemEdit;
            dgWords.DataContext = new SingleWordViewModel(Item.WORD, vmSettings);
        }

        async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            itemEdit.CopyProperties(Item);
            Item.WORD = vmSettings.AutoCorrectInput(Item.WORD);
            if (Item.ID == 0)
                Item.ID = await vm.Create(Item);
            else
                await vm.Update(Item);
            DialogResult = true;
            Close();
        }
    }
}
