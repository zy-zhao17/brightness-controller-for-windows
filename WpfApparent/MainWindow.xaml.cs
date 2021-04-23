using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;



namespace WpfApparent
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr window, int index);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr window, int index, int value);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);


        [DllImport("User32.Dll")]
        public static extern void SetWindowText(int h, String s);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
            IntPtr hWnd,
            int id,
            KeyModifiers fsModifiers,
            int vk
            );

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
            IntPtr hWnd,
            int id
            );

        [DllImport("kernel32", SetLastError = true)]
        public static extern short GlobalAddAtom(string lpString);

        [DllImport("kernel32", SetLastError = true)]
        public static extern short GlobalDeleteAtom(short nAtom);

        [Flags()]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8
        }
        public const int WmHotkey = 0x312;
        readonly Dictionary<string, short> hotKeyDic = new Dictionary<string, short>();


        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);    //窗体置顶
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);    //取消窗体置顶
        public const uint SWP_NOMOVE = 0x0002;    //不调整窗体位置
        public const uint SWP_NOSIZE = 0x0001;    //不调整窗体大小
        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_TRANSPARENT = 0x00000020;
        public IntPtr hwnd;
        byte liangdu = 65;
        byte r = 0;
        byte g = 0;
        byte b = 0;
        bool s = false;


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            hwnd = new WindowInteropHelper(this).Handle;
            var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
            SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT | 3);


            var wpfHwnd = new WindowInteropHelper(this).Handle;
            var hWndSource = HwndSource.FromHwnd(wpfHwnd);
            if (hWndSource != null) hWndSource.AddHook(MainWindowProc);
            hotKeyDic.Add("F3", GlobalAddAtom("F3"));
            hotKeyDic.Add("F4", GlobalAddAtom("F4"));
            hotKeyDic.Add("R+", GlobalAddAtom("R+"));
            hotKeyDic.Add("R-", GlobalAddAtom("R-"));
            hotKeyDic.Add("G+", GlobalAddAtom("G+"));
            hotKeyDic.Add("G-", GlobalAddAtom("G-"));
            hotKeyDic.Add("B+", GlobalAddAtom("B+"));
            hotKeyDic.Add("B-", GlobalAddAtom("B-"));
            hotKeyDic.Add("S", GlobalAddAtom("S"));
            RegisterHotKey(wpfHwnd, hotKeyDic["F3"], KeyModifiers.None, (int)Keys.F3);
            RegisterHotKey(wpfHwnd, hotKeyDic["F4"], KeyModifiers.None, (int)Keys.F4);
            RegisterHotKey(wpfHwnd, hotKeyDic["R+"], KeyModifiers.Alt, (int)Keys.R);
            RegisterHotKey(wpfHwnd, hotKeyDic["R-"], KeyModifiers.Alt, (int)Keys.T);
            RegisterHotKey(wpfHwnd, hotKeyDic["G+"], KeyModifiers.Alt, (int)Keys.G);
            RegisterHotKey(wpfHwnd, hotKeyDic["G-"], KeyModifiers.Alt, (int)Keys.H);
            RegisterHotKey(wpfHwnd, hotKeyDic["B+"], KeyModifiers.Alt, (int)Keys.B);
            RegisterHotKey(wpfHwnd, hotKeyDic["B-"], KeyModifiers.Alt, (int)Keys.N);
            RegisterHotKey(wpfHwnd, hotKeyDic["S"], KeyModifiers.Alt, (int)Keys.S);
        }


        private IntPtr MainWindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WmHotkey:
                    {
                        int sid = wParam.ToInt32();

                        if (sid == hotKeyDic["F3"])
                        {

                            if (liangdu <= 250) liangdu += 5;
                            Background = new SolidColorBrush(Color.FromArgb(liangdu, r, g, b));
                        }
                        else if (sid == hotKeyDic["F4"])
                        {
                            if (liangdu >= 5) liangdu -= 5;
                            Background = new SolidColorBrush(Color.FromArgb(liangdu, r, g, b));
                        }
                        else if (sid == hotKeyDic["R-"])
                        {
                            if (r >= 5) r -= 5;
                            Background = new SolidColorBrush(Color.FromArgb(liangdu, r, g, b));
                        }
                        else if (sid == hotKeyDic["R+"])
                        {
                            if (r <= 250) r += 5;
                            Background = new SolidColorBrush(Color.FromArgb(liangdu, r, g, b));
                        }
                        else if (sid == hotKeyDic["G-"])
                        {
                            if (g >= 5) g -= 5;
                            Background = new SolidColorBrush(Color.FromArgb(liangdu, r, g, b));
                        }
                        else if (sid == hotKeyDic["G+"])
                        {
                            if (g <= 250) g += 5;
                            Background = new SolidColorBrush(Color.FromArgb(liangdu, r, g, b));
                        }
                        else if (sid == hotKeyDic["B-"])
                        {
                            if (b >= 5) b -= 5;
                            Background = new SolidColorBrush(Color.FromArgb(liangdu, r, g, b));
                        }
                        else if (sid == hotKeyDic["B+"])
                        {
                            if (b <= 250) b += 5;
                            Background = new SolidColorBrush(Color.FromArgb(liangdu, r, g, b));
                        }
                        else if (sid == hotKeyDic["S"])
                        {
                            s = !s;
                            if (s) { Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0)); }
                            else { Background = new SolidColorBrush(Color.FromArgb(liangdu, r, g, b)); }
                        }
                        handled = true;
                        break;
                    }
            }

            return IntPtr.Zero;
        }

    }
}
