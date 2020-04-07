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

using LollyShared;

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
        MLangPhrase item = new MLangPhrase();
        public PhrasesLangDetailDlg()
        {
            InitializeComponent();
            Style = (Style)FindResource(typeof(Window));
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
        }

        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            itemOriginal.CopyProperties(item);
            DataContext = item;
        }

        public async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            item.PHRASE = vmSettings.AutoCorrectInput(item.PHRASE);
            if (item.ID == 0)
                item.ID = await vm.Create(item);
            else
                await vm.Update(item);
            item.CopyProperties(itemOriginal);
            Close();
        }
    }
}
