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
                s.Split(',').ForEach(async (s2, i) =>
                {
                    var m = r.Match(s);
                    int j = int.Parse(m.Groups[1].Value) + 3;
                    var item = new MPatternWebPage { SEQNUM = i + 1, PATTERNID = o.ID, WEBPAGEID = j  };
                    await store2.Create(item);
                });
            }
        }

    }
}
