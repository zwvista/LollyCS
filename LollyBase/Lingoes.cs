﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LollyBase.Properties;
using mshtml;

using static LollyBase.Win32;

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

            //hwndMain = FindWindow(Settings.Default.LingoesClassName, null);
            //hwndMain = FindWindow(Program.lingoesClassName, Program.LingoesWindowName);
            // couldn't find "Lingoes 灵格斯" window in the Japanese OS,
            // although GetText(hwndMain) == "Lingoes 灵格斯"
            hwndMain = FindWindow(null, Settings.Default.LingoesWindowName);
            if (hwndMain == IntPtr.Zero) return;

            IntPtr hwndDlg = GetDlgItem(hwndMain, 0);
            hwndEditWord = GetDlgItem(hwndDlg, 0x67);
            hwndButtonSearch = GetDlgItem(hwndDlg, 0x68);

            IntPtr hwndDlg2 = GetWindow(hwndDlg, GW_HWNDNEXT);
            hwndListWords = GetDlgItem(hwndDlg2, 0x3F7);

            IntPtr hwndDlg3 = GetWindow(hwndDlg2, GW_HWNDNEXT);
            IntPtr hwnd = GetDlgItem(hwndDlg3, 0x71);
            hwnd = GetDlgItem(hwnd, 0);
            hwnd = GetDlgItem(hwnd, 0);
            hwnd = GetDlgItem(hwnd, 0);

            try
            {
                uint WM_HTML_GETOBJECT = RegisterWindowMessage("WM_HTML_GETOBJECT");
                UIntPtr lngRes;
                SendMessageTimeout(hwnd, WM_HTML_GETOBJECT, UIntPtr.Zero, IntPtr.Zero, SendMessageTimeoutFlags.SMTO_NOTIMEOUTIFNOTHUNG, 1000, out lngRes);
                var doc = (HTMLDocument)ObjectFromLresult(lngRes, typeof(HTMLDocument).GUID, IntPtr.Zero);
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
            SendMessage(hwndEditWord, WM_SETTEXT, 0, word);
            SendMessage(hwndButtonSearch, BM_CLICK, 0, 0);
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
            do
            {
                p = text.IndexOf(dictArea);
                if (p == -1)
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
                if (bFound)
                {
                    bFoundOne = true;
                    if (text == "")
                    {
                        p = str.IndexOf(foot);
                        if (p != -1)
                            str = str.Substring(0, p);
                    }
                    p = str.IndexOf(ad);
                    if (p != -1)
                        str = str.Substring(0, p) + str.Substring(str.IndexOf("</DIV>", p) + 6);
                    result += dictArea + str;
                }
            } while (text != "");
            if (bFoundOne)
                result += "</DIV></DIV></BODY></HTML>";
            else
                result = $"<HTML><BODY>{ExtensionClass.NOTRANSLATION}</BODY></HTML>";
            return result;
        }
    }
}
