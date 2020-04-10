using ReactiveUI;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Reactive;

namespace LollyCloud
{
    public class BlogViewModel : MBlog
    {
        public SettingsViewModel vmSettings;
        NoteViewModel vmNote;
        BlogService service = new BlogService();

        public ReactiveCommand<Unit, Unit> HtmlToMarkedCommand { get; }
        public string PatternUrl => service.GetPatternUrl(PatternNo);
        public string PatternMarkDown => service.GetPatternMarkDown(PatternText);
        public ReactiveCommand<Unit, Unit> AddNotesCommand { get; }

        public BlogViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            vmNote = new NoteViewModel(this.vmSettings);

            HtmlToMarkedCommand = ReactiveCommand.Create(() =>
            {
                MarkedText = service.HtmlToMarked(HtmlText);
            });
            AddNotesCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                MarkedText = await service.AddNotes(vmNote, MarkedText);
            });
        }
        public string AddTagB(string str) => service.AddTagB(str);
        public string AddTagI(string str) => service.AddTagI(str);
        public string RemoveTagBI(string str) => service.RemoveTagBI(str);
        public string ExchangeTagBI(string str) => service.ExchangeTagBI(str);
        public string GetExplanation(string str) => service.GetExplanation(str);
        public string MarkedToHtml()
        {
            HtmlText = service.MarkedToHtml(MarkedText);
            var str = service.GetHtml(HtmlText);
            return str;
        }
    }
}
