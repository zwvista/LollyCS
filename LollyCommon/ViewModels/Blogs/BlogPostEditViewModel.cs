using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class BlogPostEditViewModel : MBlogPostEdit
    {
        public SettingsViewModel vmSettings;
        BlogPostEditService service = new BlogPostEditService();
        LangBlogPostContentDataStore contentDS = new LangBlogPostContentDataStore();
        MLangBlogPostContent? itemPost = null;
        bool isUnitBlogPost => itemPost == null;
        public string Title { get; set; }

        public ReactiveCommand<Unit, Unit> HtmlToMarkedCommand { get; }
        public string PatternUrl => service.GetPatternUrl(PatternNo);
        public ReactiveCommand<Unit, Unit> AddNotesCommand { get; }

        public BlogPostEditViewModel(SettingsViewModel vmSettings, bool needCopy, MLangBlogPostContent itemPost)
        {
            this.vmSettings = !needCopy ? vmSettings : vmSettings.ShallowCopy();
            this.itemPost = itemPost;
            Title = isUnitBlogPost ? vmSettings.UNITINFO : itemPost.TITLE;

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
        public string MarkedToHtml() =>
            service.MarkedToHtml(MarkedText, "\n");
        public async Task<string> LoadBlog() =>
            isUnitBlogPost ? await vmSettings.GetBlogContent() :
            (await contentDS.GetDataById(itemPost!.ID))?.CONTENT ?? "";
        public async Task SaveBlog(string content)
        {
            if (isUnitBlogPost)
            {
                await vmSettings.SaveBlogContent(content);
            }
            else
            {
                itemPost.CONTENT = content;
                await contentDS.Update(itemPost);
            }
        }
    }
}
