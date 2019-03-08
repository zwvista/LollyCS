using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace LollyShared
{
    public static class CommonApi
    {
        public static List<MSelectItem> UnitsFrom(string units)
        {
            List<string> f()
            {
                var m = new Regex(@"UNITS,(\d+)").Match(units);
                if (m.Success)
                {
                    var n = int.Parse(m.Groups[1].Value);
                    return Enumerable.Range(1, n).Select(i => i.ToString()).ToList();
                }
                m = new Regex(@"PAGES,(\d+),(\d+)").Match(units);
                if (m.Success)
                {
                    var n1 = int.Parse(m.Groups[1].Value);
                    var n2 = int.Parse(m.Groups[2].Value);
                    var n = (n1 + n2 - 1) / n2;
                    return Enumerable.Range(1, n).Select(i => $"{i * n2 - n2 + 1}~{i * n2}").ToList();
                }
                m = new Regex(@"CUSTOM,(.+)").Match(units);
                if (m.Success)
                    return m.Groups[1].Value.Split(',').ToList();
                return new List<string>();
            }
            return f().Select((s, i) => new MSelectItem(i + 1, s)).ToList();
        }

        public static List<MSelectItem> PartsFrom(string parts) =>
            parts.Split(',').Select((s, i) => new MSelectItem(i + 1, s)).ToList();
    }
}
