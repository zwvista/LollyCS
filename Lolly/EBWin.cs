using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Automation;
using System.Runtime.InteropServices;
using LollyBase;

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
            IntPtr hwndcbSearch = Win32.GetDlgItem(appHandle, 0xE81B);
            IntPtr hwnddlgSearch = Win32.GetDlgItem(hwndcbSearch, 0xE805);
            hwndComboWord = Win32.GetDlgItem(hwnddlgSearch, 0x3E8);
            hwndButtonFind = Win32.GetDlgItem(hwnddlgSearch, 0x3E9);
            hwndComboDicts = Win32.GetDlgItem(hwnddlgSearch, 0x427);
            IntPtr hwndFrame = Win32.GetDlgItem(appHandle, 0xE900);
            hwndlstWords = Win32.GetDlgItem(hwndFrame, 0xE900);
            hwndtbSearch = Win32.GetDlgItem(hwndcbSearch, 0xE882);
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
            uint threadid = Win32.GetWindowThreadProcessId(hwndlstWords, out processid);

            //open process
            IntPtr vProcess = Win32.OpenProcess(Win32.PROCESS_VM_OPERATION | Win32.PROCESS_VM_READ |
                Win32.PROCESS_VM_WRITE, false, processid);

            //get the number of list item
            int count = Win32.SendMessage(hwndlstWords, Win32.LVM_GETITEMCOUNT, 0, 0);

            //allocate memory in this process address space
            IntPtr vPointer = Win32.VirtualAllocEx(vProcess, IntPtr.Zero, 4096, Win32.MEM_RESERVE |
                Win32.MEM_COMMIT, Win32.PAGE_READWRITE);

            var words = new List<string>();
            try
            {
                for (int i = 0; i < count; i++)
                {
                    byte[] buffer = new byte[100];
                    Win32.LVItem[] vItem = new Win32.LVItem[1];
                    vItem[0].mask = Win32.LVIF_TEXT;
                    vItem[0].iItem = i;
                    vItem[0].iSubItem = 1;
                    vItem[0].cchTextMax = buffer.Length;
                    vItem[0].pszText = (IntPtr)((int)vPointer + Marshal.SizeOf(typeof(Win32.LVItem)));

                    uint vNumberOfBytesRead = 0;

                    //write the struct to the memory of target process
                    Win32.WriteProcessMemory(vProcess, vPointer, Marshal.UnsafeAddrOfPinnedArrayElement(vItem, 0),
                        Marshal.SizeOf(typeof(Win32.LVItem)), ref vNumberOfBytesRead);

                    //let the target process read item text to this struct
                    Win32.SendMessage(hwndlstWords, Win32.LVM_GETITEMTEXT, i, vPointer.ToInt32());

                    //read this struct
                    Win32.ReadProcessMemory(vProcess, (IntPtr)((int)vPointer + Marshal.SizeOf(typeof(Win32.LVItem))),
                        Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0), buffer.Length, ref vNumberOfBytesRead);

                    var w = Marshal.PtrToStringUni(Marshal.UnsafeAddrOfPinnedArrayElement(buffer, 0));

                    //add this item text to array
                    words.Add(w.Replace("-", "").Replace("・", "").Replace("･", ""));
                }
            }
            finally
            {
                Win32.VirtualFreeEx(vProcess, vPointer, 0, Win32.MEM_RELEASE);
                Win32.CloseHandle(vProcess);
            }
            return words;
        }

        public void LookUp(string word)
        {
            Win32.SendMessage(hwndButtonFind, Win32.BM_CLICK, 0, 0);
            Win32.SendMessage(hwndComboWord, (uint)Win32.ComboBoxMessages.CB_RESETCONTENT, 0, 0);
            Win32.SendMessage(hwndComboWord, (uint)Win32.ComboBoxMessages.CB_ADDSTRING, 0, word);
            Win32.SendMessage(hwndComboWord, (uint)Win32.ComboBoxMessages.CB_SETCURSEL, 0, 0);
            Win32.SendMessage(hwndButtonFind, Win32.BM_CLICK, 0, 0);
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
            //        //Win32.SendMessage(hwndlstWords, Win32.WM_KEYDOWN, (int)Win32.VK_RETURN, 1);
            //        //Win32.SendMessage(hwndlstWords, Win32.WM_KEYUP, (int)Win32.VK_RETURN, 1);
            //        return;
            //    }

            uint processid = 0;
            uint threadid = Win32.GetWindowThreadProcessId(hwndlstWords, out processid);

            //open process
            IntPtr vProcess = Win32.OpenProcess(Win32.PROCESS_VM_OPERATION | Win32.PROCESS_VM_READ |
                Win32.PROCESS_VM_WRITE, false, processid);

            //allocate memory in this process address space
            IntPtr vPointer = Win32.VirtualAllocEx(vProcess, IntPtr.Zero, 4096, Win32.MEM_RESERVE |
                Win32.MEM_COMMIT, Win32.PAGE_READWRITE);

            try
            {
                Win32.LVItem[] vItem = new Win32.LVItem[1];
                vItem[0].stateMask = Win32.LVIS_FOCUSED | Win32.LVIS_SELECTED;
                vItem[0].state = Win32.LVIS_FOCUSED | Win32.LVIS_SELECTED;

                uint vNumberOfBytesRead = 0;

                //write the struct to the memory of target process
                Win32.WriteProcessMemory(vProcess, vPointer, Marshal.UnsafeAddrOfPinnedArrayElement(vItem, 0),
                    Marshal.SizeOf(typeof(Win32.LVItem)), ref vNumberOfBytesRead);

                //let the target process read item text to this struct
                Win32.SendMessage(hwndlstWords, Win32.LVM_SETITEMSTATE, n, vPointer.ToInt32());
                Win32.SendMessage(hwndlstWords, Win32.WM_KEYDOWN, (int)Win32.VK_RETURN, 1);
                Win32.SendMessage(hwndlstWords, Win32.WM_KEYUP, (int)Win32.VK_RETURN, 1);
            }
            finally
            {
                Win32.VirtualFreeEx(vProcess, vPointer, 0, Win32.MEM_RELEASE);
                Win32.CloseHandle(vProcess);
            }
        }

        public void ChooseDict(string dict)
        {
            int n = Win32.SendMessage(hwndComboDicts, (uint)Win32.ComboBoxMessages.CB_FINDSTRINGEXACT, 0, dict);
            if (n != -1)
            {
                Win32.SendMessage(appHandle, (uint)Win32.WM_COMMAND, 33101 + n, 0);
                Win32.SendMessage(hwndButtonFind, Win32.BM_CLICK, 0, 0);
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
