using InputEmulator.App;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Runtime.InteropServices;
using System.Drawing;
using InputDll;

namespace InputEmulator.Test
{
    public class UnitTest1
    {
        [STAThread]
        [Fact]
        public void Test1()
        {

            StartApp();
            int i = 1;
            Rect rect;
            Thread.Sleep(2000);
            //-----------BEGIN: DISPLAY IMAGE ---------------------------
            Rect rect1 = Window.GetWindowRect("InputEmulator.App", "MainWindow");
            //-----------END: DISPLAY IMAGE ----------------------------
            //-------------------BEGIN: MOVE AND CLICK ------------------------------            
            while (true)
            {
                i++;
                rect = Window.GetWindowRect("InputEmulator.App", "MainWindow");
                System.Windows.Point m_pos = GetCursorPosition();
                System.Windows.Point point = new(rect.Left + 600, rect.Top + i * 20);

                Thread.Sleep(2000);

                if (point.X < -30000) break;
                InputDll.Mouse.MoveCursor(point.X, point.Y);
                InputDll.Mouse.MouseClick();
                InputDll.Mouse.MoveCursor(m_pos.X, m_pos.Y);
                //Thread.Sleep(2000);
                if (i == 4) break;
            }
            //-------------------END: MOVE AND CLICK -------------------------------

            //--------------------BEGIN: DRAG AND DROP -------------------------------
            rect = Window.GetWindowRect("InputEmulator.App", "MainWindow");
            System.Windows.Point point4 = new System.Windows.Point(rect.Left + 200, rect.Top + 10);
            System.Windows.Point cursorPosition3 = GetCursorPosition();
            InputDll.Mouse.MoveCursor(point4.X, point4.Y);
            InputDll.Mouse.MouseDrag(rect.Left + 150, rect.Top + 150);
            InputDll.Mouse.MoveCursor(cursorPosition3.X, cursorPosition3.Y);
            //--------------------END: DRAG AND DROP -------------------------------
            Thread.Sleep(1000);


            //--------------------BEGIN: CONTEXT MENU CLICK -------------------------------
            rect = Window.GetWindowRect("InputEmulator.App", "MainWindow");
            System.Windows.Point contextMenuPt = new System.Windows.Point(rect.Left + 200, rect.Top + 200);
            System.Windows.Point cursorPosition4 = GetCursorPosition();

            InputDll.Mouse.MoveCursor(contextMenuPt.X, contextMenuPt.Y);
            InputDll.Mouse.MouseRightClick();
            InputDll.Mouse.MoveCursor(cursorPosition4.X, cursorPosition4.Y);
            Thread.Sleep(500);

            System.Windows.Point cursorPosition5 = GetCursorPosition();
            InputDll.Mouse.MoveCursor(contextMenuPt.X + 20, contextMenuPt.Y + 10);
            InputDll.Mouse.MouseClick();
            InputDll.Mouse.MoveCursor(cursorPosition5.X, cursorPosition5.Y);
            //--------------------END: CONTEXT MENU CLICK ---------------------------------
        }
        private static void StartApp()
        {
            string solutionDirectory = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\"));
            string projectExecutable = Path.Combine(solutionDirectory, "InputEmulator.App\\bin\\Debug\\net7.0-windows\\InputEmulator.App.exe");

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    FileName = projectExecutable,
                },
            };

            process.Start();
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
            public static implicit operator System.Windows.Point(POINT point)
            {
                return new System.Windows.Point(point.X, point.Y);
            }
        }
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);
        public static System.Windows.Point GetCursorPosition()
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


    }
}