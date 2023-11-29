using System;
using System.Runtime.InteropServices;


namespace InputDll
{
    public class Mouse
    {
        private const int MOUSEEVENTF_MOVE = 0x0001;

        private const int MOUSEEVENTF_LEFTDOWN = 0x0002;

        private const int MOUSEEVENTF_LEFTUP = 0x0004;

        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;

        private const int MOUSEEVENTF_RIGHTUP = 0x0010;

        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        [DllImport("user32.dll")]
        private static extern bool BlockInput(bool fBlock);
        //-------------------------------------------------------------------
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }


        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);
        private static POINT GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);

            return lpPoint;
        }
        //-----customize function------

        [DllImport("user32.dll", SetLastError = false)]
        static extern IntPtr GetDesktopWindow();

        [DllImport("User32.dll", SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);
        //**************************************************************
        public static void LeftClick(double x, double y)
        {
            ShowCursor(false);
            POINT m_pos = GetCursorPosition();
            BlockInput(true);
            MoveCursor(x, y);
            MouseClick();
            MoveCursor(m_pos.X, m_pos.Y);
            BlockInput(false);
            ShowCursor(true);
        }
        public static void Drag(double x1, double y1, double x2, double y2)
        {
            POINT m_pos = GetCursorPosition();
            BlockInput(true);
            MoveCursor(x1, y1);
            MouseDrag(x2, y2);
            MoveCursor(m_pos.X, m_pos.Y);
            BlockInput(false);
        }

        public static void RightClick(double x, double y)
        {
            POINT m_pos = GetCursorPosition();
            BlockInput(true);
            MoveCursor(x, y);
            MouseRightClick();
            MoveCursor(m_pos.X, m_pos.Y);
            BlockInput(false);
        }
        //**************************************************************

        //------------------------------------------------------------------
        private static void MoveCursor(double X, double Y)
        {

            int x = (int)(X * 65535.0 / (GetPrimaryScreenWidth() - 1)) + 1;
            int y = (int)(Y * 65535.0 / (GetPrimaryScreenHeight() - 1)) + 1;
            mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, x, y, 0, 0);
        }

        private static void MouseClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        private static void MouseRightClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }

        private static void MouseLeftDown()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        }

        private static void MouseLeftUp()
        {
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        private static void MouseRightDown()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
        }

        private static void MouseRightUp()
        {
            mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
        }

        private static void MouseDrag(double X, double Y)
        {
            int x = (int)(X * 65535.0 / (GetPrimaryScreenWidth() - 1)) + 1;
            int y = (int)(Y * 65535.0 / (GetPrimaryScreenHeight() - 1)) + 1;
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, x, y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
        //--------------------------- Library function ------------------------------
        [DllImport("user32.dll")]
#pragma warning disable SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time
        private static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
#pragma warning restore SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time

        [DllImport("user32.dll")]
        private static extern int GetSystemMetrics(int nIndex);

        private const int SM_CXSCREEN = 0;

        private static int GetPrimaryScreenWidth()
        {
            int screenWidth = GetSystemMetrics(SM_CXSCREEN);
            return screenWidth;
        }

        private const int SM_CYSCREEN = 1;
        private static int GetPrimaryScreenHeight()
        {
            int screenHeight = GetSystemMetrics(SM_CYSCREEN);
            return screenHeight;
        }

        [DllImport("user32.dll")]
        static extern void ShowCursor(bool bShow);
    }
}
