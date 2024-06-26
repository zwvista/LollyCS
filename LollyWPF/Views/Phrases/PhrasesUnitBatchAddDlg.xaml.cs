﻿using LollyCommon;
using ReactiveUI;
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


namespace LollyWPF
{
    /// <summary>
    /// PhrasesUnitBatchAddDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class PhrasesUnitBatchAddDlg : Window
    {
        PhrasesUnitBatchAddViewModel vmBatch;
        public PhrasesUnitBatchAddDlg(Window owner, PhrasesUnitViewModel vm)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            tbPhrases.Focus();
            Owner = owner;
            vmBatch = new PhrasesUnitBatchAddViewModel(vm);
            DataContext = vmBatch.ItemEdit;
        }
    }
}
