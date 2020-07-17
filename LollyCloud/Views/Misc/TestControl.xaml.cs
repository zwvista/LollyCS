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
            var r = new Regex(@"(\d+)(.+)");
            var lines = File.ReadAllLines("a.txt")
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();
            var store = new WebPageDataStore();
            foreach (var s in lines)
            {
                var m = r.Match(s);
                var item = new MWebPage { TITLE = m.Groups[2].Value, URL = $"http://viethuong.web.fc2.com/MONDAI/{m.Groups[1].Value}.html" };
                await store.Create(item);
            }
        }

    }
}
