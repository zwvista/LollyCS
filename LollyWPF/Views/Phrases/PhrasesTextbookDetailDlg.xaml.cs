﻿using LollyCommon;
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
    /// PhrasesUnitDetailDlg.xaml の相互作用ロジック
    /// </summary>
    public partial class PhrasesTextbookDetailDlg : Window
    {
        PhrasesUnitDetailViewModel vmDetail;
        public MUnitPhrase Item { get; set; } = null!;
        public PhrasesTextbookDetailDlg(Window owner, PhrasesUnitViewModelWPF vm, MUnitPhrase item)
        {
            InitializeComponent();
            SourceInitialized += (x, y) => this.HideMinimizeAndMaximizeButtons();
            tbPhrase.Focus();
            Owner = owner;
            vmDetail = new PhrasesUnitDetailViewModel(vm, item, 0);
            DataContext = vmDetail.ItemEdit;
            dgPhrases.DataContext = vmDetail.vmSinglePhrase;
        }
    }
}
