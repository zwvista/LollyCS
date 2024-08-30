using LollyCommon;

namespace LollyMaui
{
    public partial class AppShell : Shell
    {
        public static SettingsViewModel vmSettings = new SettingsViewModel();
        public static List<Locale> SpeechLocales = null!;
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(WordsUnitDetailPage), typeof(WordsUnitDetailPage));
            Routing.RegisterRoute(nameof(WordsUnitBatchEditPage), typeof(WordsUnitBatchEditPage));
            Routing.RegisterRoute(nameof(WordsTextbookDetailPage), typeof(WordsTextbookDetailPage));
            Routing.RegisterRoute(nameof(WordsLangDetailPage), typeof(WordsLangDetailPage));
            Routing.RegisterRoute(nameof(WordsDictPage), typeof(WordsDictPage));
            Routing.RegisterRoute(nameof(PhrasesUnitDetailPage), typeof(PhrasesUnitDetailPage));
            Routing.RegisterRoute(nameof(PhrasesUnitBatchEditPage), typeof(PhrasesUnitBatchEditPage));
            Routing.RegisterRoute(nameof(PhrasesTextbookDetailPage), typeof(PhrasesTextbookDetailPage));
            Routing.RegisterRoute(nameof(PhrasesLangDetailPage), typeof(PhrasesLangDetailPage));
            Routing.RegisterRoute(nameof(PatternsDetailPage), typeof(PatternsDetailPage));
            Routing.RegisterRoute(nameof(PatternsWebPagePage), typeof(PatternsWebPagePage));
            Routing.RegisterRoute(nameof(ReviewOptionsPage), typeof(ReviewOptionsPage));
            Routing.RegisterRoute(nameof(WebTextbooksDetailPage), typeof(WebTextbooksDetailPage));
            Routing.RegisterRoute(nameof(WebTextbooksWebPagePage), typeof(WebTextbooksWebPagePage));

            Task.Run(async () =>
            {
                SpeechLocales = (await TextToSpeech.GetLocalesAsync()).ToList();
            });
        }

        public async void OnMenuItemClicked(object? sender, EventArgs? e)
        {
            CommonApi.UserId = "";
            MauiCommon.SaveUserId();
            await Current.GoToAsync("//LoginPage");
        }
    }
}
