using ReactiveUI;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Reactive;

namespace LollyShared
{
    public class BlogViewModel : MBlog
    {
        public SettingsViewModel vmSettings;
        NoteViewModel vmNote;
        BlogService service = new BlogService();

        public ReactiveCommand<Unit, Unit> HtmlToMarkedCommand { get; }
        public ReactiveCommand<string, string> AddTagBCommand { get; }
        public ReactiveCommand<string, string> AddTagICommand { get; }
        public ReactiveCommand<string, string> RemoveTagBICommand { get; }
        public ReactiveCommand<string, string> ExchangeTagBICommand { get; }
        public ReactiveCommand<string, string> GetExplanationCommand { get; }
        public ReactiveCommand<Unit, string> MarkedToHtmlCommand { get; }
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
            AddTagBCommand = ReactiveCommand.Create((string str) => service.AddTagB(str));
            AddTagICommand = ReactiveCommand.Create((string str) => service.AddTagI(str));
            RemoveTagBICommand = ReactiveCommand.Create((string str) => service.RemoveTagBI(str));
            ExchangeTagBICommand = ReactiveCommand.Create((string str) => service.ExchangeTagBI(str));
            GetExplanationCommand = ReactiveCommand.Create((string str) => service.GetExplanation(str));
            MarkedToHtmlCommand = ReactiveCommand.Create(() =>
            {
                HtmlText = service.MarkedToHtml(MarkedText);
                var str = service.GetHtml(HtmlText);
                return str;
            });
            AddNotesCommand = ReactiveCommand.CreateFromTask(async () =>
                await service.AddNotes(vmNote, MarkedText, s => MarkedText = s)
            );
        }
    }
}
