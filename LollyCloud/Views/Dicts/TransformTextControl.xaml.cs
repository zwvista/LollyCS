﻿using Hardcodet.Wpf.Util;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace LollyCloud
{
    /// <summary>
    /// TransformTextControl.xaml の相互作用ロジック
    /// </summary>
    public partial class TransformTextControl : UserControl
    {
        public string Text { get; set; }

        public TransformTextControl()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
