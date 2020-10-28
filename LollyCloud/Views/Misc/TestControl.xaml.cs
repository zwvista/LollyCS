using Hardcodet.Wpf.Util;
using LollyCommon;
using LollyCommon.Crawlers;
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

namespace LollyCloud
{
    /// <summary>
    /// TestControl.xaml の相互作用ロジック
    /// </summary>
    public partial class TestControl : UserControl
    {
        public TestControl()
        {
            InitializeComponent();
        }

        async void btnTest_Click(object sender, RoutedEventArgs e)
        {
            var c = new MarishaCrawler();
            await c.Step2();
        }

    }
}
