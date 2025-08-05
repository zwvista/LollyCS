using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;

namespace LollyCommon
{
    public partial class UnitBlogPostViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings { get; set; }
        public List<MSelectItem> Units => vmSettings.Units;
        [Reactive]
        public partial int SelectedUnitIndex { get; set; }

        [Reactive]
        public partial string Html { get; set; }
        private BlogPostEditService _editService = new();

        public UnitBlogPostViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            SelectedUnitIndex = Units.FindIndex(o => o.Value == vmSettings.USUNITTO);
            this.WhenAnyValue(x => x.SelectedUnitIndex).Subscribe(async (int v) =>
            {
                var content = await vmSettings.GetBlogContent(Units[v].Value);
                Html = _editService.MarkedToHtml(content, "\n");
            });
        }

        public void Next(int delta) =>
            SelectedUnitIndex = (SelectedUnitIndex + delta + Units.Count) % Units.Count;
    }
}
