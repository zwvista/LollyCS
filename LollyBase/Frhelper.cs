using System;
using LollyBase.Properties;
using mshtml;
using System.Windows.Forms;

namespace LollyBase
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

            hwndMain = Win32.FindWindow(Settings.Default.FrhelperClassName, null);
            if (hwndMain == IntPtr.Zero) return;

            IntPtr hwndToolbar = Win32.FindWindowEx(hwndMain, IntPtr.Zero, "TbsSkinToolBar", "");
            IntPtr hwnd = Win32.FindWindowEx(hwndToolbar, IntPtr.Zero, "TbsSkinComboBox", null);
            hwndEditWord = Win32.FindWindowEx(hwnd, IntPtr.Zero, "TbsCustomEdit", null);

            IntPtr hwndPnlMain = Win32.FindWindowEx(hwndMain, IntPtr.Zero, "TbsSkinPanel", "panelMain");

            hwnd = Win32.FindWindowEx(hwndPnlMain, IntPtr.Zero, "TPanel", "leftPanel");
            hwndListWords = Win32.FindWindowEx(hwnd, IntPtr.Zero, "TVirtualStringTree", "");
            
            hwnd = Win32.FindWindowEx(hwndPnlMain, IntPtr.Zero, "Shell Embedding", "");
            hwnd = Win32.GetDlgItem(hwnd, 0);
            hwndHtml = Win32.GetDlgItem(hwnd, 0);
        }

        public string GetContent()
        {
            do
            {
                try
                {
                    uint WM_HTML_GETOBJECT = Win32.RegisterWindowMessage("WM_HTML_GETOBJECT");
                    UIntPtr lngRes;
                    Win32.SendMessageTimeout(hwndHtml, WM_HTML_GETOBJECT, UIntPtr.Zero, IntPtr.Zero,
                        SendMessageTimeoutFlags.SMTO_NOTIMEOUTIFNOTHUNG, 1000, out lngRes);
                    var doc = (HTMLDocument)Win32.ObjectFromLresult(lngRes, typeof(HTMLDocument).GUID, IntPtr.Zero);
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
            Win32.SendMessage(hwndEditWord, Win32.WM_SETTEXT, 0, word);
            Win32.SendKey(hwndEditWord, Keys.Enter, false);
            System.Threading.Thread.Sleep(400);

            string lastWord = "", dictWord, text;
            for (;;)
            {
                text = GetContent();
                dictWord = Win32.GetControlText(hwndEditWord);
                if (string.Equals(dictWord, word, StringComparison.InvariantCultureIgnoreCase) || dictWord == lastWord) break;
                lastWord = dictWord;
                Win32.SendKey(hwndListWords, Keys.Down, false);
                System.Threading.Thread.Sleep(400);
            }
            return dictWord == lastWord ? "" : text;
        }

        public string Search(string word, string transform)
        {
            var text = Search(word);
            text = ExtensionClass.ExtractFromHtml(text, transform);
            if (text == "")
                text = ExtensionClass.NOTRANS;
            return text;
        }
    }
}
