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
            var r = new Regex(@"表現(\d+)");
            var store = new PatternDataStore();
            var patterns = await store.GetDataByLang(2);
            var store2 = new PatternWebPageDataStore();
            foreach (var o in patterns)
            {
                var s = o.TAGS;
                if (string.IsNullOrEmpty(s)) continue;
                var arr = s.Split(',').ToList();
                if (arr.Count < 2) continue;
                var webpages = await store2.GetDataByPattern(o.ID);
                webpages = webpages.Where(o2 => o2.URL.StartsWith("http://viethuong.web.fc2.com/MONDAI/")).ToList();
                arr.ForEach(async (s2, i) =>
                {
                    var item = webpages.SingleOrDefault(o2 => o2.SEQNUM == i + 1);
                    if (item == null) return;
                    var m2 = r.Match(s2);
                    int j2 = int.Parse(m2.Groups[1].Value) + 3;
                    item.WEBPAGEID = j2;
                    await store2.Update(item);
                });
            }
        }

    }
}
