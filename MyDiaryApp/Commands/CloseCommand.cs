using System.Windows;

namespace MyDiaryApp.Commands
{
    public class CloseCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
