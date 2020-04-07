using ReactiveUI;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace LollyCloud
{
    public class NoteViewModel : ReactiveObject
    {
        public SettingsViewModel vmSettings;
        public MDictionary DictNote => vmSettings.SelectedDictNote;
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
            for (int i = 0; ;)
            {
                await Task.Delay((int)DictNote.WAIT);
                while (i < wordCount && !isNoteEmpty(i)) i++;
                if (i > wordCount)
                {
                    allComplete();
                    break;
                }
                else
                {
                    if (i < wordCount)
                        await getOne(i);
                    i++;
                }
            }
        }
        public async Task ClearNotes(int wordCount, Func<int, bool> isNoteEmpty, Func<int, Task> getOne, Action allComplete)
        {
            if (DictNote == null) return;
            for (int i = 0; ;)
            {
                while (i < wordCount && !isNoteEmpty(i)) i++;
                if (i < wordCount)
                    await getOne(i);
                i++;
            }
        }
    }
}
