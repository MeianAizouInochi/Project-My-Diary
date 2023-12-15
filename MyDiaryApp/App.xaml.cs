using MyDiaryApp.ViewModels;
using System.Diagnostics;
using System.Windows;

namespace MyDiaryApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            Debug.WriteLine("Entered overrided onStartUp method of App class.");

            base.OnStartup(e);

            MainWindow = new MainWindow() {

                 DataContext = new MainViewModel()

            };

            MainWindow.Show();

            
        }
    }
}
