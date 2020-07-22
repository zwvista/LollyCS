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
    /// PatternsDetailDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class PatternsDetailDlg : Window
    {
        PatternsDetailViewModel vmDetail;
        public MPattern Item { get; set; }
        public PatternsDetailDlg(Window owner, MPattern item, PatternsViewModel vm)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            tbPattern.Focus();
            Owner = owner;
            vmDetail = new PatternsDetailViewModel(Item = item, vm);
            DataContext = vmDetail.ItemEdit;
        }

        async void btnOK_Click(object sender, RoutedEventArgs e)
        {
            await vmDetail.OnOK();
            DialogResult = true;
        }
    }
}