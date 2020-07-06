using Hardcodet.Wpf.Util;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace LollyCloud
{
    /// <summary>
    /// TestControl.xaml の相互作用ロジック
    /// </summary>
    public partial class TestControl : UserControl, ILollySettings
    {
        public TestControl()
        {
            InitializeComponent();
            OnSettingsChanged();
        }


        public async Task OnSettingsChanged()
        {
        }

        async void btnTest_Click(object sender, RoutedEventArgs e)
        {
            var r = new Regex(@"(\d+).+");
            var lines = File.ReadAllLines("a.txt")
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();
            var store = new PatternDataStore();
            var lst = await store.GetDataByLang(2);
            foreach (var o in lst)
            {
                var lst2 = lines.Where(s => s.Contains(o.PATTERN)).Select(s => $"表現{r.Match(s).Groups[1].Value}").ToList();
                var s2 = string.Join(",", lst2);
                o.NOTE = s2;
            }
        }

    }
}
