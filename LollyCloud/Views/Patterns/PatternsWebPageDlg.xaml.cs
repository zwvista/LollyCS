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
        public MPatternWebPageEdit itemEdit { get; } = new MPatternWebPageEdit();
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
            if (Item.WEBPAGEID == 0)
                Item.WEBPAGEID = await vm.CreateWebPage(Item);
            else
                await vm.UpdateWebPage(Item);
            if (Item.ID == 0)
                Item.ID = await vm.CreatePatternWebPage(Item);
            else
                await vm.UpdatePatternWebPage(Item);
            DialogResult = true;
        }

        void btnNew_Click(object sender, RoutedEventArgs e)
        {
            itemEdit.WEBPAGEID = 0;
        }

        void btnExisting_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new WebPageSelectDlg();
            dlg.Owner = Window.GetWindow(this);
            if (dlg.ShowDialog() == true)
            {
                var o = dlg.VM.SelectedWebPage;
                itemEdit.WEBPAGEID = o.ID;
                //itemEdit.TITLE = o.TITLE;
                //itemEdit.URL = o.URL;
                tbTitle.Text = o.TITLE;
                tbURL.Text = o.URL;
            }
        }
    }
}
