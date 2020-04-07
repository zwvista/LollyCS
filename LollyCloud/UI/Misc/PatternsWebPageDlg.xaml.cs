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

using LollyShared;

namespace LollyCloud
{
    /// <summary>
    /// PatternsWebPageDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class PatternsWebPageDlg : Window
    {
        public MPatternWebPage itemOriginal;
        public SettingsViewModel vmSettings => MainWindow.vmSettings;
        public PatternsViewModel vm;
        MPatternWebPage item = new MPatternWebPage();
        public PatternsWebPageDlg()
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
            item.PATTERN = vmSettings.AutoCorrectInput(item.PATTERN);
            if (item.ID == 0)
                item.ID = await vm.CreateWebPage(item);
            else
                await vm.UpdateWebPage(item);
            item.CopyProperties(itemOriginal);
            Close();
        }

        void btnCancel_Click(object sender, RoutedEventArgs e) => Close();
    }
}