using Hardcodet.Wpf.Util;
using LollyCommon;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;

namespace LollyWPF
{
    /// <summary>
    /// TestControl.xaml の相互作用ロジック
    /// </summary>
    public partial class TestControl : UserControl
    {
        TestViewModel vm = new TestViewModel();
        public TestControl()
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
