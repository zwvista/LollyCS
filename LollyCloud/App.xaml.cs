using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Speech.Synthesis;

namespace LollyCloud
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        static SpeechSynthesizer synth = new SpeechSynthesizer();
        static List<InstalledVoice> voices = synth.GetInstalledVoices().ToList();

        public static void Speak(SettingsViewModel vmSettings, string text)
        {
            if (!App.voices.Any(o => o.VoiceInfo.Name == vmSettings.SelectedVoice.VOICENAME)) return;
            synth.SpeakAsyncCancelAll();
            synth.SelectVoice(vmSettings.SelectedVoice.VOICENAME);
            synth.SpeakAsync(text);
        }

        public static void Speak(PromptBuilder pb)
        {
            synth.SpeakAsyncCancelAll();
            synth.SpeakAsync(pb);
        }

        public static void AddPrompt(PromptBuilder pb, SettingsViewModel vmSettings, string text)
        {
            pb.StartVoice(vmSettings.SelectedVoice.VOICENAME);
            pb.AppendText(text);
            pb.EndVoice();
        }
        // https://stackoverflow.com/questions/3145511/how-to-set-the-default-font-for-a-wpf-application
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            FrameworkElement.StyleProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata
            {
                DefaultValue = FindResource(typeof(Window))
            });
        }
    }
}
