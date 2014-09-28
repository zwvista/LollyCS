using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LollyBase.Properties;
using mshtml;

namespace LollyBase
{
    public class Lingoes
    {
        public IntPtr hwndMain = IntPtr.Zero;
        public IntPtr hwndEditWord = IntPtr.Zero;
        public IntPtr hwndButtonSearch = IntPtr.Zero;
        public IntPtr hwndListWords = IntPtr.Zero;
        public IHTMLElement elemHtml;

        public void FindLingoes()
        {
            if (elemHtml != null) return;

            //hwndMain = Win32.FindWindow(Settings.Default.LingoesClassName, null);
            //hwndMain = Win32.FindWindow(Program.lingoesClassName, Program.LingoesWindowName);
            // couldn't find "Lingoes 灵格斯" window in the Japanese OS,
            // although Win32.GetText(hwndMain) == "Lingoes 灵格斯"
            hwndMain = Win32.FindWindow(null, Settings.Default.LingoesWindowName);
            if (hwndMain == IntPtr.Zero) return;

            IntPtr hwndDlg = Win32.GetDlgItem(hwndMain, 0);
            hwndEditWord = Win32.GetDlgItem(hwndDlg, 0x67);
            hwndButtonSearch = Win32.GetDlgItem(hwndDlg, 0x68);

            IntPtr hwndDlg2 = Win32.GetWindow(hwndDlg, Win32.GW_HWNDNEXT);
            hwndListWords = Win32.GetDlgItem(hwndDlg2, 0x3F7);

            IntPtr hwndDlg3 = Win32.GetWindow(hwndDlg2, Win32.GW_HWNDNEXT);
            IntPtr hwnd = Win32.GetDlgItem(hwndDlg3, 0x71);
            hwnd = Win32.GetDlgItem(hwnd, 0);
            hwnd = Win32.GetDlgItem(hwnd, 0);
            hwnd = Win32.GetDlgItem(hwnd, 0);

            try
            {
                uint WM_HTML_GETOBJECT = Win32.RegisterWindowMessage("WM_HTML_GETOBJECT");
                UIntPtr lngRes;
                Win32.SendMessageTimeout(hwnd, WM_HTML_GETOBJECT, UIntPtr.Zero, IntPtr.Zero, SendMessageTimeoutFlags.SMTO_NOTIMEOUTIFNOTHUNG, 1000, out lngRes);
                var doc = (HTMLDocument)Win32.ObjectFromLresult(lngRes, typeof(HTMLDocument).GUID, IntPtr.Zero);
                elemHtml = doc.body.parentElement;
            }
            catch (System.Exception)
            {
            	
            }
        }

        public string GetContent()
        {
            return elemHtml.outerHTML;
        }

        public string Search(string word)
        {
            FindLingoes();
            Win32.SendMessage(hwndEditWord, Win32.WM_SETTEXT, 0, word);
            Win32.SendMessage(hwndButtonSearch, Win32.BM_CLICK, 0, 0);
            return GetContent();
        }

        private const string mainWnd = "<DIV id=main_wnd>\r\n";
        private const string dictArea = "<DIV id=lingoes_dictarea></DIV>\r\n";
        private const string foot = "<DIV style=\"PADDING-BOTTOM: 10px; LINE-HEIGHT: normal;";
        private const string ad = "<DIV style=\"LINE-HEIGHT: normal; OVERFLOW-X: hidden;";
        public string Search(string word, string[] dicts)
        {
            FindLingoes();
            
            string text = Search(word);

            int p = text.IndexOf(mainWnd) + mainWnd.Length;
            string result = text.Substring(0, p);
            text = text.Substring(p);

	        string str;
	        bool bFoundOne = false;
	        do{
		        p = text.IndexOf(dictArea);
		        if(p == -1)
                {
			        str = text;
                    text = "";
                }
		        else
                {
			        str = text.Substring(0, p);
                    text = text.Substring(p + dictArea.Length);
                }
		        bool bFound = dicts.Any(dict => str.Contains(dict));
		        if(bFound){
				    bFoundOne = true;
                    if (text == "")
                    {
				        p = str.IndexOf(foot);
				        if(p != -1)
                            str = str.Substring(0, p);
			        }
                    p = str.IndexOf(ad);
			        if(p != -1)
                        str = str.Substring(0, p) + str.Substring(str.IndexOf("</DIV>", p) + 6);
			        result += dictArea + str;
		        }
            } while (text != "");
	        if(bFoundOne)
		        result += "</DIV></DIV></BODY></HTML>";
	        else
                result = string.Format("<HTML><BODY>{0}</BODY></HTML>", ExtensionClass.NOTRANS);
            return result;
        }
    }
}
