using System;
using LollyShared.Properties;
using mshtml;
using System.Windows.Forms;

using static LollyShared.Win32;

namespace LollyShared
{
    public class Frhelper
    {
        public IntPtr hwndMain = IntPtr.Zero;
        public IntPtr hwndEditWord = IntPtr.Zero;
        public IntPtr hwndHtml = IntPtr.Zero;
        public IntPtr hwndListWords = IntPtr.Zero;
        public IHTMLElement elemHtml;

        public void FindFrhelper()
        {
            if (elemHtml != null) return;

            hwndMain = FindWindow(Settings.Default.FrhelperClassName, null);
            if (hwndMain == IntPtr.Zero) return;

            IntPtr hwndToolbar = FindWindowEx(hwndMain, IntPtr.Zero, "TbsSkinToolBar", "");
            IntPtr hwnd = FindWindowEx(hwndToolbar, IntPtr.Zero, "TbsSkinComboBox", null);
            hwndEditWord = FindWindowEx(hwnd, IntPtr.Zero, "TbsCustomEdit", null);

            IntPtr hwndPnlMain = FindWindowEx(hwndMain, IntPtr.Zero, "TbsSkinPanel", "panelMain");

            hwnd = FindWindowEx(hwndPnlMain, IntPtr.Zero, "TPanel", "leftPanel");
            hwndListWords = FindWindowEx(hwnd, IntPtr.Zero, "TVirtualStringTree", "");
            
            hwnd = FindWindowEx(hwndPnlMain, IntPtr.Zero, "Shell Embedding", "");
            hwnd = GetDlgItem(hwnd, 0);
            hwndHtml = GetDlgItem(hwnd, 0);
        }

        public string GetContent()
        {
            do
            {
                try
                {
                    uint WM_HTML_GETOBJECT = RegisterWindowMessage("WM_HTML_GETOBJECT");
                    UIntPtr lngRes;
                    SendMessageTimeout(hwndHtml, WM_HTML_GETOBJECT, UIntPtr.Zero, IntPtr.Zero,
                        SendMessageTimeoutFlags.SMTO_NOTIMEOUTIFNOTHUNG, 1000, out lngRes);
                    var doc = (HTMLDocument)ObjectFromLresult(lngRes, typeof(HTMLDocument).GUID, IntPtr.Zero);
                    elemHtml = doc.body.parentElement;
                }
                catch (System.Exception)
                {

                }
            } while (elemHtml == null || elemHtml.innerHTML == null);
            return elemHtml.outerHTML;
        }

        public string Search(string word)
        {
            FindFrhelper();
            SendMessage(hwndEditWord, WM_SETTEXT, 0, word);
            SendKey(hwndEditWord, Keys.Enter, false);
            System.Threading.Thread.Sleep(400);

            string lastWord = "", dictWord, text;
            for (;;)
            {
                text = GetContent();
                dictWord = GetControlText(hwndEditWord);
                if (string.Equals(dictWord, word, StringComparison.InvariantCultureIgnoreCase) || dictWord == lastWord) break;
                lastWord = dictWord;
                SendKey(hwndListWords, Keys.Down, false);
                System.Threading.Thread.Sleep(400);
            }
            return dictWord == lastWord ? "" : text;
        }

        public string Search(string word, string transform)
        {
            var text = Search(word);
            text = ExtensionClass.ExtractFromHtml(text, transform);
            if (text == "")
                text = ExtensionClass.NOTRANSLATION;
            return text;
        }
    }
}
