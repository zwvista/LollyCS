using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace LollyShared
{
    public class TextbookDataStore : LollyDataStore<MTextbook>
    {
        public async Task<List<MTextbook>> GetDataByLang(int langid)
        {
            var lst = (await GetDataByUrl<MTextbooks>($"TEXTBOOKS?filter=LANGID,eq,{langid}")).records;
            List<string> f(string units)
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
            lst.ForEach(o =>
            {
                o.Units = f(o.UNITS).Select((s, i) => new MSelectItem(i + 1, s)).ToList();
                o.Parts = o.PARTS.Split(',').Select((s, i) => new MSelectItem(i + 1, s)).ToList();
            });
            return lst;
        }
    }
}
