using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace InputEmulator.App
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            foreach (var button in this.StackPanel.Children.Cast<UIElement>().OfType<Button>())
            {
                button.MouseEnter += (sender, e) => this.TextBlock.Text += $"{(string)((Button)sender).Content} mouse enter" + Environment.NewLine;
                button.MouseLeave += (sender, e) => this.TextBlock.Text += $"{(string)((Button)sender).Content} mouse leave" + Environment.NewLine;
                button.MouseLeftButtonDown += (sender, e) => this.TextBlock.Text += $"{(string)((Button)sender).Content} mouse left button down" + Environment.NewLine;
                button.MouseLeftButtonUp += (sender, e) => this.TextBlock.Text += $"{(string)((Button)sender).Content} mouse left button up" + Environment.NewLine;
                button.Click += (sender, e) => this.TextBlock.Text += $"{(string)((Button)sender).Content} clicked" + Environment.NewLine;
                button.IsMouseDirectlyOverChanged += (sender, e) => this.TextBlock.Text += $"{(string)((Button)sender).Content} is mouse directly over changed (was {e.OldValue}, now is {e.NewValue})" + Environment.NewLine;
            }

            //this.TestClick();
        }

        public static async Task ApplicationIdle()
        {
            Thread.Sleep(500);

            await Application.Current.Dispatcher.InvokeAsync(() => { }, DispatcherPriority.ApplicationIdle);
        }

        private async Task TestClick()
        {
            await ApplicationIdle();

            Rect rect = new(this.Left, this.Top, this.RenderSize.Width, this.RenderSize.Height);

            Input.MoveCursor(new Point(rect.Left + 40, rect.Top + 40));

            await ApplicationIdle();

            Input.MouseLeftDown();

            await ApplicationIdle();

            Input.MouseLeftUp();

            await ApplicationIdle();

            Input.MoveCursor(new Point(rect.Left + 40, rect.Top + 60));

            await ApplicationIdle();

            Input.MouseLeftDown();

            await ApplicationIdle();

            Input.MouseLeftUp();

            await ApplicationIdle();

            Input.MoveCursor(new Point(rect.Left + 40, rect.Top + 80));

            await ApplicationIdle();

            Input.MouseLeftDown();

            await ApplicationIdle();

            Input.MouseLeftUp();

            await ApplicationIdle();

            Input.MoveCursor(new Point(rect.Left + 40, rect.Top + 100));
        }
    }
}
