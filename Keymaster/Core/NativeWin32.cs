/*
 * Copyright 2022 Johnny Mast
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the 
 * "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the
 * following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
 * WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS 
 * IN THE SOFTWARE.
*/

using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SendKeys.Core
{
    public class NativeWin32
    {
        [DllImport("user32.dll")]
        public static extern int GetForegroundWindow();

        [DllImport("User32.dll")]
        public static extern int SetForegroundWindow(int hWnd);


        // [DllImport("user32.dll")]
        // public static extern int FindWindow(string lpClassName, string lpWindowName);
        //
        // [DllImport("user32")]
        // public static extern bool PostMessage(int hWnd, uint Msg, int wParam, IntPtr lParam);
        //
        // [DllImport("user32.dll")]
        // public static extern int SendMessage(int hWnd, uint uMsg, int wParam, IntPtr lParam);
        //
        //
        // [DllImport("user32.dll")]
        // public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass,
        //     string lpszWindow);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(int hwnd, StringBuilder ss, int count);
        

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll")]  
        public static extern int SendMessage(int hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPStr)] string lParam);  

        

        public const int WM_SETTEXT = 0X000C;
        public const int WM_PASTE = 0x0302;
    }
}