using CefSharp;
using CefSharp.Wpf;
using LollyCommon;
using LollyWPF.Properties;
using ReactiveUI.Builder;
using Splat.Builder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.Versioning;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Windows;

namespace LollyWPF
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        [SupportedOSPlatform("windows")]
        static SpeechSynthesizer synth = new SpeechSynthesizer();
        [SupportedOSPlatform("windows")]
        static List<InstalledVoice> voices = synth.GetInstalledVoices().ToList();

        [SupportedOSPlatform("windows")]
        public static void Speak(SettingsViewModel vmSettings, string text)
        {
            if (!App.voices.Any(o => o.VoiceInfo.Name == vmSettings.SelectedVoice.VOICENAME)) return;
            synth.SpeakAsyncCancelAll();
            synth.SelectVoice(vmSettings.SelectedVoice.VOICENAME);
            synth.SpeakAsync(text);
        }

        [SupportedOSPlatform("windows")]
        public static void Speak(PromptBuilder pb)
        {
            synth.SpeakAsyncCancelAll();
            synth.SpeakAsync(pb);
        }

        [SupportedOSPlatform("windows")]
        public static void AddPrompt(PromptBuilder pb, SettingsViewModel vmSettings, string text)
        {
            pb.StartVoice(vmSettings.SelectedVoice.VOICENAME);
            pb.AppendText(text);
            pb.EndVoice();
        }
        // https://stackoverflow.com/questions/3145511/how-to-set-the-default-font-for-a-wpf-application
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var app = RxAppBuilder.CreateReactiveUIBuilder()
                .WithWpf() // Or WithMaui(), WithBlazor(), WithWinUI(), etc.
                .WithViewsFromAssembly(typeof(App).Assembly)
                .BuildApp();

            FrameworkElement.StyleProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata
            {
                DefaultValue = FindResource(typeof(Window))
            });
            var settings = new CefSettings();
            Cef.Initialize(settings);
        }
        public static void SaveUserId()
        {
            // https://stackoverflow.com/questions/4216809/configurationmanager-doesnt-save-settings
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["userid"].Value = CommonApi.UserId;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
