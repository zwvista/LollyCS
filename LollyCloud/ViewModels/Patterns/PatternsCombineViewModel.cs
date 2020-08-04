using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LollyCloud
{
    public class PatternsCombineViewModel : ReactiveObject
    {
        public ObservableCollection<MPattern> PatternItems { get; set; }
        public ObservableCollection<StringWrapper> PatternVariations { get; set; }
        public string PATTERN { get; set; }
        public string NOTE { get; set; }
        public string TAGS { get; set; }

        public PatternsCombineViewModel(List<MPattern> items)
        {
            PatternItems = new ObservableCollection<MPattern>(items);
            var strs = items.SelectMany(o => o.PATTERN.Split('／')).OrderBy(s => s).Distinct().ToList();
            PatternVariations = new ObservableCollection<StringWrapper>(strs.Select(s => new StringWrapper { Value = s }));
            Action f = () => PATTERN = string.Join("／", PatternVariations.Select(o => o.Value));
            PatternVariations.CollectionChanged += (s, e) => f();
            f();
            strs = items.Select(o => o.NOTE).Where(s => !string.IsNullOrEmpty(s)).SelectMany(s => s.Split(',')).Distinct().ToList();
            NOTE = string.Join(",", strs);
            strs = items.Select(o => o.TAGS).Where(s => !string.IsNullOrEmpty(s)).SelectMany(s => s.Split(',')).OrderBy(s => s).Distinct().ToList();
            TAGS = string.Join(",", strs);
        }
    }
}
