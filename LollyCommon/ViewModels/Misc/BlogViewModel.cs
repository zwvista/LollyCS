using ReactiveUI;
using System.Reactive;

namespace LollyCommon
{
    public class BlogViewModel : MBlog
    {
        public SettingsViewModel vmSettings;
        BlogService service = new BlogService();

        public ReactiveCommand<Unit, Unit> HtmlToMarkedCommand { get; }
        public string PatternUrl => service.GetPatternUrl(PatternNo);
        public string PatternMarkDown => service.GetPatternMarkDown(PatternText);
        public ReactiveCommand<Unit, Unit> AddNotesCommand { get; }

        public BlogViewModel(SettingsViewModel vmSettings, bool needCopy)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();

            HtmlToMarkedCommand = ReactiveCommand.Create(() =>
            {
                MarkedText = service.HtmlToMarked(HtmlText);
            });
            AddNotesCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                MarkedText = await service.AddNotes(this.vmSettings, MarkedText);
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
            var str = HtmlTransformService.ToHtml(HtmlText);
            return str;
        }
    }
}
