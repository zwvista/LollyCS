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
        bool isUnitBlog = false;
        MLangBlogContent itemBlog = null;

        public ReactiveCommand<Unit, Unit> HtmlToMarkedCommand { get; }
        public string PatternUrl => service.GetPatternUrl(PatternNo);
        public ReactiveCommand<Unit, Unit> AddNotesCommand { get; }

        public BlogEditViewModel(SettingsViewModel vmSettings, bool needCopy, MLangBlogContent itemBlog)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            this.itemBlog = itemBlog;
            isUnitBlog = itemBlog == null;

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
            HtmlText = service.MarkedToHtml(MarkedText, "\r\n");
            var str = HtmlTransformService.ToHtml(HtmlText);
            return str;
        }
        public async Task<string> LoadBlog()
        {
            if (isUnitBlog)
            {
                return await vmSettings.GetBlogContent();
            }
            else
            {
                var item = await blogContentDS.GetDataById(itemBlog.ID);
                return item?.CONTENT ?? "";
            }
        }
        public async Task SaveBlog(string content)
        {
            if (isUnitBlog)
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
