﻿using System;
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
        MUnitWord item = new MUnitWord();
        public WordsTextbookDetailDlg()
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
