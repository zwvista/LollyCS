using LollyBlazor.Components;
using Toolbelt.Blazor.SpeechSynthesis;

namespace LollyBlazor.Services;

public class TextToSpeechService
{
    private readonly SpeechSynthesis _speechSynthesis;
    private IEnumerable<SpeechSynthesisVoice> _voices;

    public TextToSpeechService(SpeechSynthesis speechSynthesis)
    {
        _speechSynthesis = speechSynthesis;
    }

    public async Task InitializeAsync()
    {
        // 只初始化一次，避免重复获取
        if (_voices == null)
        {
            _voices = await _speechSynthesis.GetVoicesAsync();
        }
    }

    public async Task SpeakAsync(string text)
    {
        // 确保已初始化
        if (_voices == null)
            await InitializeAsync();

        var voice = _voices.FirstOrDefault(v => v.Name == App.vmSettings.SelectedVoice.VOICENAME);

        var utterance = new SpeechSynthesisUtterance
        {
            Text = text,
            Voice = voice
        };

        await _speechSynthesis.SpeakAsync(utterance);
    }
}