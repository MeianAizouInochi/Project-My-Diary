
using System.Windows;

namespace MyDiaryApp.Commands
{
    public class MinimizeCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
    }
}
