using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class BlogEditViewModel : MBlogEdit
    {
        public SettingsViewModel vmSettings;
        BlogEditService service = new BlogEditService();
        LangBlogContentDataStore blogContentDS = new LangBlogContentDataStore();
        MLangBlogContent itemBlog = null;
        bool isUnitBlogPost => itemBlog == null;
        public string Title { get; set; }

        public ReactiveCommand<Unit, Unit> HtmlToMarkedCommand { get; }
        public string PatternUrl => service.GetPatternUrl(PatternNo);
        public ReactiveCommand<Unit, Unit> AddNotesCommand { get; }

        public BlogEditViewModel(SettingsViewModel vmSettings, bool needCopy, MLangBlogContent itemBlog)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            this.itemBlog = itemBlog;
            Title = isUnitBlogPost ? vmSettings.UNITINFO : itemBlog.TITLE;

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
            HtmlText = service.MarkedToHtml(MarkedText, "\n");
            var str = HtmlTransformService.ToHtml(HtmlText);
            return str;
        }
        public async Task<string> LoadBlog() =>
            isUnitBlogPost ? await vmSettings.GetBlogContent() :
            (await blogContentDS.GetDataById(itemBlog.ID))?.CONTENT ?? "";
        public async Task SaveBlog(string content)
        {
            if (isUnitBlogPost)
            {
                await vmSettings.SaveBlogContent(content);
            }
            else
            {
                itemBlog.CONTENT = content;
                await blogContentDS.Update(itemBlog);
            }
        }
    }
}
