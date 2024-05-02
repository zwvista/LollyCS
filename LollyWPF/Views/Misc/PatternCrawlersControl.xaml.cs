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

namespace LollyCloud
{
    /// <summary>
    /// PatternCrawlersControl.xaml の相互作用ロジック
    /// </summary>
    public partial class PatternCrawlersControl : UserControl
    {
        PatternCrawlersViewModel vm = new PatternCrawlersViewModel();
        public PatternCrawlersControl()
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
