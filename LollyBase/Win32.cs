using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace LollyBase
{
    [Flags]
    public enum SendMessageTimeoutFlags : uint
    {
        SMTO_NORMAL = 0x0,
        SMTO_BLOCK = 0x1,
        SMTO_ABORTIFHUNG = 0x2,
        SMTO_NOTIMEOUTIFNOTHUNG = 0x8
    }

    public class Win32
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint Command);

        public const uint GW_HWNDNEXT = 2;
        public const uint BM_CLICK = 0x00F5;

        [DllImport("user32.dll")]
        public static extern uint RegisterWindowMessage(string lpString);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = System.Runtime.InteropServices.CharSet.Auto)] //
        public static extern bool SendMessage(IntPtr hWnd, uint Msg, int wParam, StringBuilder lParam);
        [DllImport("user32.dll", EntryPoint = "SendMessageW")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, [MarshalAs(UnmanagedType.LPTStr)] string lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, UIntPtr wParam, IntPtr lParam, SendMessageTimeoutFlags fuFlags, uint uTimeout, out UIntPtr lpdwResult);

        [DllImport("user32.dll", EntryPoint = "PostMessageA", SetLastError = true)]
        public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("oleacc.dll", PreserveSig = false)]
        [return: MarshalAs(UnmanagedType.Interface)]
        public static extern object ObjectFromLresult(UIntPtr lResult, [MarshalAs(UnmanagedType.LPStruct)] Guid refiid, IntPtr wParam);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        [DllImport("user32")]
        public static extern int mouse_event(UInt32 dataFlags, UInt32 dataX, UInt32 dataY, UInt32 data, IntPtr dataExtraInfo);

        [DllImport("user32.dll")]
        public static extern byte MapVirtualKey(byte wCode, int wMap);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, ref  uint lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, IntPtr lpBuffer, int nSize, ref uint vNumberOfBytesRead);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll")]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        [DllImport("kernel32.dll")]
        public static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint dwFreeType);

        public static string GetText(IntPtr hWnd)
        {
            // Allocate correct string length first
            int length = GetWindowTextLength(hWnd);
            StringBuilder sb = new StringBuilder(length + 1);
            GetWindowText(hWnd, sb, sb.Capacity);
            return sb.ToString();
        }

        /// <summary>Shows a Window</summary>
        /// <remarks>
        /// <para>To perform certain special effects when showing or hiding a 
        /// window, use AnimateWindow.</para>
        ///<para>The first time an application calls ShowWindow, it should use 
        ///the WinMain function's nCmdShow parameter as its nCmdShow parameter. 
        ///Subsequent calls to ShowWindow must use one of the values in the 
        ///given list, instead of the one specified by the WinMain function's 
        ///nCmdShow parameter.</para>
        ///<para>As noted in the discussion of the nCmdShow parameter, the 
        ///nCmdShow value is ignored in the first call to ShowWindow if the 
        ///program that launched the application specifies startup information 
        ///in the structure. In this case, ShowWindow uses the information 
        ///specified in the STARTUPINFO structure to show the window. On 
        ///subsequent calls, the application must call ShowWindow with nCmdShow 
        ///set to SW_SHOWDEFAULT to use the startup information provided by the 
        ///program that launched the application. This behavior is designed for 
        ///the following situations: </para>
        ///<list type="">
        ///    <item>Applications create their main window by calling CreateWindow 
        ///    with the WS_VISIBLE flag set. </item>
        ///    <item>Applications create their main window by calling CreateWindow 
        ///    with the WS_VISIBLE flag cleared, and later call ShowWindow with the 
        ///    SW_SHOW flag set to make it visible.</item>
        ///</list></remarks>
        /// <param name="hWnd">Handle to the window.</param>
        /// <param name="nCmdShow">Specifies how the window is to be shown. 
        /// This parameter is ignored the first time an application calls 
        /// ShowWindow, if the program that launched the application provides a 
        /// STARTUPINFO structure. Otherwise, the first time ShowWindow is called, 
        /// the value should be the value obtained by the WinMain function in its 
        /// nCmdShow parameter. In subsequent calls, this parameter can be one of 
        /// the WindowShowStyle members.</param>
        /// <returns>
        /// If the window was previously visible, the return value is nonzero. 
        /// If the window was previously hidden, the return value is zero.
        /// </returns>
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, WindowShowStyle nCmdShow);

        /// <summary>Enumeration of the different ways of showing a window using 
        /// ShowWindow</summary>
        public enum WindowShowStyle : uint
        {
            /// <summary>Hides the window and activates another window.</summary>
            /// <remarks>See SW_HIDE</remarks>
            Hide = 0,
            /// <summary>Activates and displays a window. If the window is minimized 
            /// or maximized, the system restores it to its original size and 
            /// position. An application should specify this flag when displaying 
            /// the window for the first time.</summary>
            /// <remarks>See SW_SHOWNORMAL</remarks>
            ShowNormal = 1,
            /// <summary>Activates the window and displays it as a minimized window.</summary>
            /// <remarks>See SW_SHOWMINIMIZED</remarks>
            ShowMinimized = 2,
            /// <summary>Activates the window and displays it as a maximized window.</summary>
            /// <remarks>See SW_SHOWMAXIMIZED</remarks>
            ShowMaximized = 3,
            /// <summary>Maximizes the specified window.</summary>
            /// <remarks>See SW_MAXIMIZE</remarks>
            Maximize = 3,
            /// <summary>Displays a window in its most recent size and position. 
            /// This value is similar to "ShowNormal", except the window is not 
            /// actived.</summary>
            /// <remarks>See SW_SHOWNOACTIVATE</remarks>
            ShowNormalNoActivate = 4,
            /// <summary>Activates the window and displays it in its current size 
            /// and position.</summary>
            /// <remarks>See SW_SHOW</remarks>
            Show = 5,
            /// <summary>Minimizes the specified window and activates the next 
            /// top-level window in the Z order.</summary>
            /// <remarks>See SW_MINIMIZE</remarks>
            Minimize = 6,
            /// <summary>Displays the window as a minimized window. This value is 
            /// similar to "ShowMinimized", except the window is not activated.</summary>
            /// <remarks>See SW_SHOWMINNOACTIVE</remarks>
            ShowMinNoActivate = 7,
            /// <summary>Displays the window in its current size and position. This 
            /// value is similar to "Show", except the window is not activated.</summary>
            /// <remarks>See SW_SHOWNA</remarks>
            ShowNoActivate = 8,
            /// <summary>Activates and displays the window. If the window is 
            /// minimized or maximized, the system restores it to its original size 
            /// and position. An application should specify this flag when restoring 
            /// a minimized window.</summary>
            /// <remarks>See SW_RESTORE</remarks>
            Restore = 9,
            /// <summary>Sets the show state based on the SW_ value specified in the 
            /// STARTUPINFO structure passed to the CreateProcess function by the 
            /// program that started the application.</summary>
            /// <remarks>See SW_SHOWDEFAULT</remarks>
            ShowDefault = 10,
            /// <summary>Windows 2000/XP: Minimizes a window, even if the thread 
            /// that owns the window is hung. This flag should only be used when 
            /// minimizing windows from a different thread.</summary>
            /// <remarks>See SW_FORCEMINIMIZE</remarks>
            ForceMinimized = 11
        }
        public enum ComboBoxMessages : uint
        {

            CB_GETEDITSEL = 0x0140,
            CB_LIMITTEXT = 0x0141,
            CB_SETEDITSEL = 0x0142,
            CB_ADDSTRING = 0x0143,
            CB_DELETESTRING = 0x0144,
            CB_DIR = 0x0145,
            CB_GETCOUNT = 0x0146,
            CB_GETCURSEL = 0x0147,
            CB_GETLBTEXT = 0x0148,
            CB_GETLBTEXTLEN = 0x0149,
            CB_INSERTSTRING = 0x014A,
            CB_RESETCONTENT = 0x014B,
            CB_FINDSTRING = 0x014C,
            CB_SELECTSTRING = 0x014D,
            CB_SETCURSEL = 0x014E,
            CB_SHOWDROPDOWN = 0x014F,
            CB_GETITEMDATA = 0x0150,
            CB_SETITEMDATA = 0x0151,
            CB_GETDROPPEDCONTROLRECT = 0x0152,
            CB_SETITEMHEIGHT = 0x0153,
            CB_GETITEMHEIGHT = 0x0154,
            CB_SETEXTENDEDUI = 0x0155,
            CB_GETEXTENDEDUI = 0x0156,
            CB_GETDROPPEDSTATE = 0x0157,
            CB_FINDSTRINGEXACT = 0x0158,
            CB_SETLOCALE = 0x0159,
            CB_GETLOCALE = 0x015A,
            CB_GETTOPINDEX = 0x015B,
            CB_SETTOPINDEX = 0x015C,
            CB_GETHORIZONTALEXTENT = 0x015D,
            CB_SETHORIZONTALEXTENT = 0x015E,
            CB_GETDROPPEDWIDTH = 0x015F,
            CB_SETDROPPEDWIDTH = 0x0160,
            CB_INITSTORAGE = 0x0161
            /*  CB_MSGMAX    = 354*/
        }

        public const UInt32 WM_ACTIVATE = 0x0006;
        public const UInt32 WM_ACTIVATEAPP = 0x001C;
        public const UInt32 WM_AFXFIRST = 0x0360;
        public const UInt32 WM_AFXLAST = 0x037F;
        public const UInt32 WM_APP = 0x8000;
        public const UInt32 WM_ASKCBFORMATNAME = 0x030C;
        public const UInt32 WM_CANCELJOURNAL = 0x004B;
        public const UInt32 WM_CANCELMODE = 0x001F;
        public const UInt32 WM_CAPTURECHANGED = 0x0215;
        public const UInt32 WM_CHANGECBCHAIN = 0x030D;
        public const UInt32 WM_CHANGEUISTATE = 0x0127;
        public const UInt32 WM_CHAR = 0x0102;
        public const UInt32 WM_CHARTOITEM = 0x002F;
        public const UInt32 WM_CHILDACTIVATE = 0x0022;
        public const UInt32 WM_CLEAR = 0x0303;
        public const UInt32 WM_CLOSE = 0x0010;
        public const UInt32 WM_COMMAND = 0x0111;
        public const UInt32 WM_COMPACTING = 0x0041;
        public const UInt32 WM_COMPAREITEM = 0x0039;
        public const UInt32 WM_CONTEXTMENU = 0x007B;
        public const UInt32 WM_COPY = 0x0301;
        public const UInt32 WM_COPYDATA = 0x004A;
        public const UInt32 WM_CREATE = 0x0001;
        public const UInt32 WM_CTLCOLORBTN = 0x0135;
        public const UInt32 WM_CTLCOLORDLG = 0x0136;
        public const UInt32 WM_CTLCOLOREDIT = 0x0133;
        public const UInt32 WM_CTLCOLORLISTBOX = 0x0134;
        public const UInt32 WM_CTLCOLORMSGBOX = 0x0132;
        public const UInt32 WM_CTLCOLORSCROLLBAR = 0x0137;
        public const UInt32 WM_CTLCOLORSTATIC = 0x0138;
        public const UInt32 WM_CUT = 0x0300;
        public const UInt32 WM_DEADCHAR = 0x0103;
        public const UInt32 WM_DELETEITEM = 0x002D;
        public const UInt32 WM_DESTROY = 0x0002;
        public const UInt32 WM_DESTROYCLIPBOARD = 0x0307;
        public const UInt32 WM_DEVICECHANGE = 0x0219;
        public const UInt32 WM_DEVMODECHANGE = 0x001B;
        public const UInt32 WM_DISPLAYCHANGE = 0x007E;
        public const UInt32 WM_DRAWCLIPBOARD = 0x0308;
        public const UInt32 WM_DRAWITEM = 0x002B;
        public const UInt32 WM_DROPFILES = 0x0233;
        public const UInt32 WM_ENABLE = 0x000A;
        public const UInt32 WM_ENDSESSION = 0x0016;
        public const UInt32 WM_ENTERIDLE = 0x0121;
        public const UInt32 WM_ENTERMENULOOP = 0x0211;
        public const UInt32 WM_ENTERSIZEMOVE = 0x0231;
        public const UInt32 WM_ERASEBKGND = 0x0014;
        public const UInt32 WM_EXITMENULOOP = 0x0212;
        public const UInt32 WM_EXITSIZEMOVE = 0x0232;
        public const UInt32 WM_FONTCHANGE = 0x001D;
        public const UInt32 WM_GETDLGCODE = 0x0087;
        public const UInt32 WM_GETFONT = 0x0031;
        public const UInt32 WM_GETHOTKEY = 0x0033;
        public const UInt32 WM_GETICON = 0x007F;
        public const UInt32 WM_GETMINMAXINFO = 0x0024;
        public const UInt32 WM_GETOBJECT = 0x003D;
        public const UInt32 WM_GETTEXT = 0x000D;
        public const UInt32 WM_GETTEXTLENGTH = 0x000E;
        public const UInt32 WM_HANDHELDFIRST = 0x0358;
        public const UInt32 WM_HANDHELDLAST = 0x035F;
        public const UInt32 WM_HELP = 0x0053;
        public const UInt32 WM_HOTKEY = 0x0312;
        public const UInt32 WM_HSCROLL = 0x0114;
        public const UInt32 WM_HSCROLLCLIPBOARD = 0x030E;
        public const UInt32 WM_ICONERASEBKGND = 0x0027;
        public const UInt32 WM_IME_CHAR = 0x0286;
        public const UInt32 WM_IME_COMPOSITION = 0x010F;
        public const UInt32 WM_IME_COMPOSITIONFULL = 0x0284;
        public const UInt32 WM_IME_CONTROL = 0x0283;
        public const UInt32 WM_IME_ENDCOMPOSITION = 0x010E;
        public const UInt32 WM_IME_KEYDOWN = 0x0290;
        public const UInt32 WM_IME_KEYLAST = 0x010F;
        public const UInt32 WM_IME_KEYUP = 0x0291;
        public const UInt32 WM_IME_NOTIFY = 0x0282;
        public const UInt32 WM_IME_REQUEST = 0x0288;
        public const UInt32 WM_IME_SELECT = 0x0285;
        public const UInt32 WM_IME_SETCONTEXT = 0x0281;
        public const UInt32 WM_IME_STARTCOMPOSITION = 0x010D;
        public const UInt32 WM_INITDIALOG = 0x0110;
        public const UInt32 WM_INITMENU = 0x0116;
        public const UInt32 WM_INITMENUPOPUP = 0x0117;
        public const UInt32 WM_INPUTLANGCHANGE = 0x0051;
        public const UInt32 WM_INPUTLANGCHANGEREQUEST = 0x0050;
        public const UInt32 WM_KEYDOWN = 0x0100;
        public const UInt32 WM_KEYFIRST = 0x0100;
        public const UInt32 WM_KEYLAST = 0x0108;
        public const UInt32 WM_KEYUP = 0x0101;
        public const UInt32 WM_KILLFOCUS = 0x0008;
        public const UInt32 WM_LBUTTONDBLCLK = 0x0203;
        public const UInt32 WM_LBUTTONDOWN = 0x0201;
        public const UInt32 WM_LBUTTONUP = 0x0202;
        public const UInt32 WM_MBUTTONDBLCLK = 0x0209;
        public const UInt32 WM_MBUTTONDOWN = 0x0207;
        public const UInt32 WM_MBUTTONUP = 0x0208;
        public const UInt32 WM_MDIACTIVATE = 0x0222;
        public const UInt32 WM_MDICASCADE = 0x0227;
        public const UInt32 WM_MDICREATE = 0x0220;
        public const UInt32 WM_MDIDESTROY = 0x0221;
        public const UInt32 WM_MDIGETACTIVE = 0x0229;
        public const UInt32 WM_MDIICONARRANGE = 0x0228;
        public const UInt32 WM_MDIMAXIMIZE = 0x0225;
        public const UInt32 WM_MDINEXT = 0x0224;
        public const UInt32 WM_MDIREFRESHMENU = 0x0234;
        public const UInt32 WM_MDIRESTORE = 0x0223;
        public const UInt32 WM_MDISETMENU = 0x0230;
        public const UInt32 WM_MDITILE = 0x0226;
        public const UInt32 WM_MEASUREITEM = 0x002C;
        public const UInt32 WM_MENUCHAR = 0x0120;
        public const UInt32 WM_MENUCOMMAND = 0x0126;
        public const UInt32 WM_MENUDRAG = 0x0123;
        public const UInt32 WM_MENUGETOBJECT = 0x0124;
        public const UInt32 WM_MENURBUTTONUP = 0x0122;
        public const UInt32 WM_MENUSELECT = 0x011F;
        public const UInt32 WM_MOUSEACTIVATE = 0x0021;
        public const UInt32 WM_MOUSEFIRST = 0x0200;
        public const UInt32 WM_MOUSEHOVER = 0x02A1;
        public const UInt32 WM_MOUSELAST = 0x020D;
        public const UInt32 WM_MOUSELEAVE = 0x02A3;
        public const UInt32 WM_MOUSEMOVE = 0x0200;
        public const UInt32 WM_MOUSEWHEEL = 0x020A;
        public const UInt32 WM_MOUSEHWHEEL = 0x020E;
        public const UInt32 WM_MOVE = 0x0003;
        public const UInt32 WM_MOVING = 0x0216;
        public const UInt32 WM_NCACTIVATE = 0x0086;
        public const UInt32 WM_NCCALCSIZE = 0x0083;
        public const UInt32 WM_NCCREATE = 0x0081;
        public const UInt32 WM_NCDESTROY = 0x0082;
        public const UInt32 WM_NCHITTEST = 0x0084;
        public const UInt32 WM_NCLBUTTONDBLCLK = 0x00A3;
        public const UInt32 WM_NCLBUTTONDOWN = 0x00A1;
        public const UInt32 WM_NCLBUTTONUP = 0x00A2;
        public const UInt32 WM_NCMBUTTONDBLCLK = 0x00A9;
        public const UInt32 WM_NCMBUTTONDOWN = 0x00A7;
        public const UInt32 WM_NCMBUTTONUP = 0x00A8;
        public const UInt32 WM_NCMOUSEMOVE = 0x00A0;
        public const UInt32 WM_NCPAINT = 0x0085;
        public const UInt32 WM_NCRBUTTONDBLCLK = 0x00A6;
        public const UInt32 WM_NCRBUTTONDOWN = 0x00A4;
        public const UInt32 WM_NCRBUTTONUP = 0x00A5;
        public const UInt32 WM_NEXTDLGCTL = 0x0028;
        public const UInt32 WM_NEXTMENU = 0x0213;
        public const UInt32 WM_NOTIFY = 0x004E;
        public const UInt32 WM_NOTIFYFORMAT = 0x0055;
        public const UInt32 WM_NULL = 0x0000;
        public const UInt32 WM_PAINT = 0x000F;
        public const UInt32 WM_PAINTCLIPBOARD = 0x0309;
        public const UInt32 WM_PAINTICON = 0x0026;
        public const UInt32 WM_PALETTECHANGED = 0x0311;
        public const UInt32 WM_PALETTEISCHANGING = 0x0310;
        public const UInt32 WM_PARENTNOTIFY = 0x0210;
        public const UInt32 WM_PASTE = 0x0302;
        public const UInt32 WM_PENWINFIRST = 0x0380;
        public const UInt32 WM_PENWINLAST = 0x038F;
        public const UInt32 WM_POWER = 0x0048;
        public const UInt32 WM_POWERBROADCAST = 0x0218;
        public const UInt32 WM_PRINT = 0x0317;
        public const UInt32 WM_PRINTCLIENT = 0x0318;
        public const UInt32 WM_QUERYDRAGICON = 0x0037;
        public const UInt32 WM_QUERYENDSESSION = 0x0011;
        public const UInt32 WM_QUERYNEWPALETTE = 0x030F;
        public const UInt32 WM_QUERYOPEN = 0x0013;
        public const UInt32 WM_QUEUESYNC = 0x0023;
        public const UInt32 WM_QUIT = 0x0012;
        public const UInt32 WM_RBUTTONDBLCLK = 0x0206;
        public const UInt32 WM_RBUTTONDOWN = 0x0204;
        public const UInt32 WM_RBUTTONUP = 0x0205;
        public const UInt32 WM_RENDERALLFORMATS = 0x0306;
        public const UInt32 WM_RENDERFORMAT = 0x0305;
        public const UInt32 WM_SETCURSOR = 0x0020;
        public const UInt32 WM_SETFOCUS = 0x0007;
        public const UInt32 WM_SETFONT = 0x0030;
        public const UInt32 WM_SETHOTKEY = 0x0032;
        public const UInt32 WM_SETICON = 0x0080;
        public const UInt32 WM_SETREDRAW = 0x000B;
        public const UInt32 WM_SETTEXT = 0x000C;
        public const UInt32 WM_SETTINGCHANGE = 0x001A;
        public const UInt32 WM_SHOWWINDOW = 0x0018;
        public const UInt32 WM_SIZE = 0x0005;
        public const UInt32 WM_SIZECLIPBOARD = 0x030B;
        public const UInt32 WM_SIZING = 0x0214;
        public const UInt32 WM_SPOOLERSTATUS = 0x002A;
        public const UInt32 WM_STYLECHANGED = 0x007D;
        public const UInt32 WM_STYLECHANGING = 0x007C;
        public const UInt32 WM_SYNCPAINT = 0x0088;
        public const UInt32 WM_SYSCHAR = 0x0106;
        public const UInt32 WM_SYSCOLORCHANGE = 0x0015;
        public const UInt32 WM_SYSCOMMAND = 0x0112;
        public const UInt32 WM_SYSDEADCHAR = 0x0107;
        public const UInt32 WM_SYSKEYDOWN = 0x0104;
        public const UInt32 WM_SYSKEYUP = 0x0105;
        public const UInt32 WM_TCARD = 0x0052;
        public const UInt32 WM_TIMECHANGE = 0x001E;
        public const UInt32 WM_TIMER = 0x0113;
        public const UInt32 WM_UNDO = 0x0304;
        public const UInt32 WM_UNINITMENUPOPUP = 0x0125;
        public const UInt32 WM_USER = 0x0400;
        public const UInt32 WM_USERCHANGED = 0x0054;
        public const UInt32 WM_VKEYTOITEM = 0x002E;
        public const UInt32 WM_VSCROLL = 0x0115;
        public const UInt32 WM_VSCROLLCLIPBOARD = 0x030A;
        public const UInt32 WM_WINDOWPOSCHANGED = 0x0047;
        public const UInt32 WM_WINDOWPOSCHANGING = 0x0046;
        public const UInt32 WM_WININICHANGE = 0x001A;
        public const UInt32 WM_XBUTTONDBLCLK = 0x020D;
        public const UInt32 WM_XBUTTONDOWN = 0x020B;
        public const UInt32 WM_XBUTTONUP = 0x020C;

        const int MOUSEEVENTF_MOVE = 0x0001;
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        const int MOUSEEVENTF_LEFTUP = 0x0004;
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        const int MOUSEEVENTF_RIGHTUP = 0x0010;
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        public const uint VK_RETURN = 0x0D;

        public static void ClickLeftMouse(int x, int y)
        {
            var op = Cursor.Position;
            Cursor.Position = new System.Drawing.Point(x, y);
            mouse_event(MOUSEEVENTF_MOVE, 0, 0, 0, IntPtr.Zero);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, IntPtr.Zero);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, IntPtr.Zero);
            Cursor.Position = op;
        }
        public static void SendKey(IntPtr hwnd, Keys keyCode, bool extended)
        {
            uint scanCode = MapVirtualKey((byte)keyCode, 0);
            uint lParam;

            //KEY DOWN
            lParam = (0x00000001 | (scanCode << 16));
            if (extended)
            {
                lParam = lParam | 0x01000000;
            }

            PostMessage(hwnd, WM_KEYDOWN, (IntPtr)keyCode, (IntPtr)lParam);

            //KEY UP

            PostMessage(hwnd, WM_KEYUP, (IntPtr)keyCode, (IntPtr)lParam);
        }

        public static string GetControlText(IntPtr hWnd)
        {

            StringBuilder title = new StringBuilder();

            // Get the size of the string required to hold the window title. 
            Int32 size = SendMessage(hWnd, WM_GETTEXTLENGTH, 0, 0);

            // If the return is 0, there is no title. 
            if (size > 0)
            {
                title = new StringBuilder(size + 1);

                SendMessage(hWnd, WM_GETTEXT, title.Capacity, title);


            }
            return title.ToString();
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LVItem
        {
            public int mask;
            public int iItem;
            public int iSubItem;
            public int state;
            public int stateMask;
            public IntPtr pszText;
            public int cchTextMax;
            public int iImage;
            public IntPtr lParam;
            public int iIndent;
            public int iGroupId;
            public int cColumns;
            public IntPtr puColumns;
        }

        public const int LB_GETTEXT = 0x0189;
        public const int LB_GETCOUNT = 0x018B;
        public const int LVM_FIRST = 0x1000;
        public const int LVM_GETITEMCOUNT = LVM_FIRST + 4;
        public const int LVM_GETITEMTEXT = LVM_FIRST + 75;  //LVM_FIRST + 45 doesn't work
        public const int LVM_SETITEMSTATE = LVM_FIRST + 43;

        public const uint PROCESS_VM_OPERATION = 0x0008;
        public const uint PROCESS_VM_READ = 0x0010;
        public const uint PROCESS_VM_WRITE = 0x0020;

        public const uint MEM_COMMIT = 0x1000;
        public const uint MEM_RELEASE = 0x8000;
        public const uint MEM_RESERVE = 0x2000;
        public const uint PAGE_READWRITE = 4;

        public const int LVIF_TEXT = 0x0001;
        public const int LVIS_FOCUSED = 0x0001;
        public const int LVIS_SELECTED = 0x0002;
    }
}
