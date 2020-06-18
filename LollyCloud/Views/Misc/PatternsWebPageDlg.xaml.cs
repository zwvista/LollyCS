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
    /// PatternsWebPageDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class PatternsWebPageDlg : Window
    {
        public MPatternWebPage Item;
        public SettingsViewModel vmSettings => MainWindow.vmSettings;
        public PatternsViewModel vm;
        MPatternWebPageEdit itemEdit = new MPatternWebPageEdit();
        public PatternsWebPageDlg()
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            tbTitle.Focus();
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Item.CopyProperties(itemEdit);
            DataContext = itemEdit;
        }

        async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            itemEdit.CopyProperties(Item);
            Item.PATTERN = vmSettings.AutoCorrectInput(Item.PATTERN);
            if (Item.ID == 0)
                Item.ID = await vm.CreateWebPage(Item);
            else
                await vm.UpdateWebPage(Item);
            DialogResult = true;
            Close();
        }

        void btnCancel_Click(object sender, RoutedEventArgs e) => Close();
    }
}
