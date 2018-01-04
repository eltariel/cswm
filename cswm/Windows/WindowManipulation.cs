using System;
using System.Runtime.InteropServices;

namespace cswm.Windows
{
    public class WindowManipulation
    {
        public static void RemoveBorder(IntPtr hwnd)
        {
            var style = Win32.GetWindowLong(hwnd, Win32.GWL_STYLE);
            Win32.SetWindowLong(hwnd, Win32.GWL_STYLE, (style & ~Win32.WS_SYSMENU & ~Win32.WS_CAPTION));
            Win32.SetWindowPos(hwnd, 0, 0, 0, 0, 0, Win32.SWP_NOMOVE | Win32.SWP_NOZORDER | Win32.SWP_NOSIZE | Win32.SWP_SHOWWINDOW);
        }

        private class Win32
        {
            [DllImport("USER32.DLL")]
            public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

            [DllImport("USER32.DLL")]
            public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

            [DllImport("USER32.DLL")]
            public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

            [DllImport("USER32.DLL")]
            public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

            [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
            public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int cx, int cy,
                int wFlags);

            public const int GWL_STYLE = -16;
            public const int WS_CHILD = 0x40000000; //child window
            public const int WS_BORDER = 0x00800000; //window with border
            public const int WS_DLGFRAME = 0x00400000; //window with double border but no title
            public const int WS_CAPTION = WS_BORDER | WS_DLGFRAME; //window with a title bar 
            public const int WS_SYSMENU = 0x00080000; //window menu  
            public const short SWP_NOMOVE = 0X2;
            public const short SWP_NOSIZE = 1;
            public const short SWP_NOZORDER = 0X4;
            public const int SWP_SHOWWINDOW = 0x0040;
        }
    }
}