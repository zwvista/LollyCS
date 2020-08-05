﻿using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace LollyCloud
{
    public class PatternsMergeViewModel : ReactiveObject
    {
        public ObservableCollection<MPattern> PatternItems { get; set; }
        // https://stackoverflow.com/questions/1427471/observablecollection-not-noticing-when-item-in-it-changes-even-with-inotifyprop
        public BindingList<MPattern> PatternVariations { get; set; }
        public MPattern MergedItem { get; } = new MPattern();

        public PatternsMergeViewModel(List<MPattern> items)
        {
            PatternItems = new ObservableCollection<MPattern>(items);
            var strs = items.SelectMany(o => o.PATTERN.Split('／')).OrderBy(s => s).Distinct().ToList();
            PatternVariations = new BindingList<MPattern>(strs.Select(s => new MPattern { PATTERN = s }).ToList());
            Action f = () => MergedItem.PATTERN = string.Join("／", PatternVariations.Select(o => o.PATTERN));
            PatternVariations.ListChanged += (s, e) => f();
            f();
            strs = items.Select(o => o.NOTE).Where(s => !string.IsNullOrEmpty(s)).SelectMany(s => s.Split(',')).Distinct().ToList();
            MergedItem.NOTE = string.Join(",", strs);
            strs = items.Select(o => o.TAGS).Where(s => !string.IsNullOrEmpty(s)).SelectMany(s => s.Split(',')).OrderBy(s => s).Distinct().ToList();
            MergedItem.TAGS = string.Join(",", strs);
        }
    }
}
