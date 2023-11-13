using InputEmulator.App;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace InputEmulator.Test
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            StartApp();

            Thread.Sleep(2000);

            Rect rect = Window.GetWindowRect("InputEmulator.App", "MainWindow");

            Input.MoveCursor(new Point(rect.Left + 40, rect.Top + 40));

            Thread.Sleep(500);

            Input.MouseLeftDown();

            Thread.Sleep(500);

            Input.MouseLeftUp();

            Thread.Sleep(500);

            Input.MoveCursor(new Point(rect.Left + 40, rect.Top + 60));

            Thread.Sleep(500);

            Input.MouseLeftDown();

            Thread.Sleep(500);

            Input.MouseLeftUp();

            Thread.Sleep(500);

            Input.MoveCursor(new Point(rect.Left + 40, rect.Top + 80));

            Thread.Sleep(500);

            Input.MouseLeftDown();

            Thread.Sleep(500);

            Input.MouseLeftUp();

            Thread.Sleep(500);

            Input.MoveCursor(new Point(rect.Left + 40, rect.Top + 100));
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
    }
}