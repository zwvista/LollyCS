using System;
using System.Threading.Tasks;
using System.Linq;
using System.Reactive.Linq;

namespace LollyShared
{
    public class NoteViewModel : LollyViewModel
    {
        public SettingsViewModel vmSettings;
        public MDictNote DictNote => vmSettings.SelectedDictNote;
        public const string ZeroNote = "O";

        public NoteViewModel(SettingsViewModel vmSettings)
        {
            this.vmSettings = vmSettings;
        }

        public async Task<string> GetNote(string word)
        {
            if (DictNote == null) return "";
            var url = DictNote.UrlString(word, vmSettings.AutoCorrects.ToList());
            var html = await vmSettings.client.GetStringAsync(url);
            return CommonApi.ExtractTextFromHtml(html, DictNote.TRANSFORM, "", (text, _) => text);
        }

        public async Task GetNotes(int wordCount, Func<int, bool> isNoteEmpty, Func<int, Task> getOne, Action allComplete)
        {
            if (DictNote == null) return;
            var i = 0;
            var interval = Observable.Interval(TimeSpan.FromMilliseconds((double)DictNote.WAIT));
            IDisposable disposable = null;
            disposable = interval.Subscribe(async _ =>
            {
                while (i < wordCount && !isNoteEmpty(i)) i++;
                if (i > wordCount)
                {
                    allComplete();
                    disposable.Dispose();
                }
                else
                {
                    if (i < wordCount)
                        await getOne(i);
                    i++;
                }
            });
        }
    }
}
