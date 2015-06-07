using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;
using System.Runtime.InteropServices;
using LollyShared;

using static LollyShared.Win32;

namespace Lolly
{
    public class EBWin
    {
        private IntPtr appHandle;
        private IntPtr hwndComboWord;
        private IntPtr hwndButtonFind;
        private IntPtr hwndComboDicts;
        private IntPtr hwndlstWords;
        private IntPtr hwndtbSearch;

        private const string waei = "(和英)";

        public EBWin(IntPtr inAppHandle)
        {
            appHandle = inAppHandle;
            IntPtr hwndcbSearch = GetDlgItem(appHandle, 0xE81B);
            IntPtr hwnddlgSearch = GetDlgItem(hwndcbSearch, 0xE805);
            hwndComboWord = GetDlgItem(hwnddlgSearch, 0x3E8);
            hwndButtonFind = GetDlgItem(hwnddlgSearch, 0x3E9);
            hwndComboDicts = GetDlgItem(hwnddlgSearch, 0x427);
            IntPtr hwndFrame = GetDlgItem(appHandle, 0xE900);
            hwndlstWords = GetDlgItem(hwndFrame, 0xE900);
            hwndtbSearch = GetDlgItem(hwndcbSearch, 0xE882);
        }

        private List<string> GetWordList()
        {
            //var el = AutomationElement.FromHandle(hwndlstWords);
            //var walker = TreeWalker.ContentViewWalker;

            //var words = new List<string>();
            //for (var child = walker.GetFirstChild(el); child != null; child = walker.GetNextSibling(child))
            //{
            //    var w = walker.GetNextSibling(walker.GetFirstChild(child)).Current.Name;
            //    words.Add(w.Replace("-", "").Replace("・", "").Replace("･", ""));
            //}
            //return words;

            uint processid = 0;
            uint threadid = GetWindowThreadProcessId(hwndlstWords, out processid);

            //open process
            IntPtr vProcess = OpenProcess(PROCESS_VM_OPERATION | PROCESS_VM_READ |
                PROCESS_VM_WRITE, false, processid);

            //get the number of list item
            int count = SendMessage(hwndlstWords, LVM_GETITEMCOUNT, 0, 0);

            //allocate memory in this process address space
            IntPtr vPointer = VirtualAllocEx(vProcess, IntPtr.Zero, 4096, MEM_RESERVE |
                MEM_COMMIT, PAGE_READWRITE);

            var words = new List<string>();
            try
            {
                for (int i = 0; i < count; i++)
                {
                    byte[] buffer = new byte[100];
                    LVItem[] vItem = new LVItem[1];
                    vItem[0].mask = LVIF_TEXT;
                    vItem[0].iItem = i;
                    vItem[0].iSubItem = 1;
                    vItem[0].cchTextMax = buffer.Length;
                    vItem[0].pszText = (IntPtr)((int)vPointer + Marshal.SizeOf(typeof(LVItem)));

                    uint vNumberOfBytesRead = 0;

                    //write the struct to the memory of target process
                    WriteProcessMemory(vProcess, vPointer, Marshal.UnsafeAddrOfPinnedArrayElement(vItem, 0),
                        Marshal.SizeOf(typeof(LVItem)), ref vNumberOfBytesRead);

                    //let the target process read item text to this struct
                    SendMessage(hwndlstWords, LVM_GETITEMTEXT, i, vPointer.ToInt32());

                    //read this struct
                    ReadProcessMemory(vProcess, (IntPtr)((int)vPointer + Marshal.SizeOf(typeof(LVItem))),
                        Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0), buffer.Length, ref vNumberOfBytesRead);

                    var w = Marshal.PtrToStringUni(Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0));

                    //add this item text to array
                    words.Add(w.Replace("-", "").Replace("・", "").Replace("･", ""));
                }
            }
            finally
            {
                VirtualFreeEx(vProcess, vPointer, 0, MEM_RELEASE);
                CloseHandle(vProcess);
            }
            return words;
        }

        public void LookUp(string word)
        {
            SendMessage(hwndButtonFind, BM_CLICK, 0, 0);
            SendMessage(hwndComboWord, (uint)ComboBoxMessages.CB_RESETCONTENT, 0, 0);
            SendMessage(hwndComboWord, (uint)ComboBoxMessages.CB_ADDSTRING, 0, word);
            SendMessage(hwndComboWord, (uint)ComboBoxMessages.CB_SETCURSEL, 0, 0);
            SendMessage(hwndButtonFind, BM_CLICK, 0, 0);
        }

        public void SelectEntry(string reference = "")
        {
            int n = GetWordList().FindIndex(w => w.Contains(reference));
            if (n < 1) return;

            //var el = AutomationElement.FromHandle(hwndlstWords);
            //var walker = TreeWalker.ContentViewWalker;

            //int i = 0;
            //for (var child = walker.GetFirstChild(el); child != null; child = walker.GetNextSibling(child))
            //    if (i++ == n)
            //    {
            //        (child.GetCurrentPattern(SelectionItemPattern.Pattern) as SelectionItemPattern).Select();
            //        //SendMessage(hwndlstWords, WM_KEYDOWN, (int)VK_RETURN, 1);
            //        //SendMessage(hwndlstWords, WM_KEYUP, (int)VK_RETURN, 1);
            //        return;
            //    }

            uint processid = 0;
            uint threadid = GetWindowThreadProcessId(hwndlstWords, out processid);

            //open process
            IntPtr vProcess = OpenProcess(PROCESS_VM_OPERATION | PROCESS_VM_READ |
                PROCESS_VM_WRITE, false, processid);

            //allocate memory in this process address space
            IntPtr vPointer = VirtualAllocEx(vProcess, IntPtr.Zero, 4096, MEM_RESERVE |
                MEM_COMMIT, PAGE_READWRITE);

            try
            {
                LVItem[] vItem = new LVItem[1];
                vItem[0].stateMask = LVIS_FOCUSED | LVIS_SELECTED;
                vItem[0].state = LVIS_FOCUSED | LVIS_SELECTED;

                uint vNumberOfBytesRead = 0;

                //write the struct to the memory of target process
                WriteProcessMemory(vProcess, vPointer, Marshal.UnsafeAddrOfPinnedArrayElement(vItem, 0),
                    Marshal.SizeOf(typeof(LVItem)), ref vNumberOfBytesRead);

                //let the target process read item text to this struct
                SendMessage(hwndlstWords, LVM_SETITEMSTATE, n, vPointer.ToInt32());
                SendMessage(hwndlstWords, WM_KEYDOWN, (int)VK_RETURN, 1);
                SendMessage(hwndlstWords, WM_KEYUP, (int)VK_RETURN, 1);
            }
            finally
            {
                VirtualFreeEx(vProcess, vPointer, 0, MEM_RELEASE);
                CloseHandle(vProcess);
            }
        }

        public void ChooseDict(string dict)
        {
            int n = SendMessage(hwndComboDicts, (uint)ComboBoxMessages.CB_FINDSTRINGEXACT, 0, dict);
            if (n != -1)
            {
                SendMessage(appHandle, (uint)WM_COMMAND, 33101 + n, 0);
                SendMessage(hwndButtonFind, BM_CLICK, 0, 0);
            }
        }

        public string FindKana(string word)
        {
            LookUp(word);
            var kanas = (from w in GetWordList()
                         let w2 = w.EndsWith(waei) ? w.Remove(w.Length - waei.Length) : w
                         let n = w2.IndexOf("【")
                         select n != -1 ? w2.Substring(0, n) : w2)
                         .Distinct().ToArray();
            switch (kanas.Length)
            {
                case 0:
                    return "";
                case 1:
                    return kanas[0];
                default:
                    var dlg = new SelectKanaDlg(word, kanas);
                    dlg.ShowDialog();
                    return dlg.SelectedKana;
            }
        }
    }
}
