using MyDiaryApp.ViewModels;
using System;
using System.IO;
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
            base.OnStartup(e);

            string BaseAppFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyDiaryApp");

            if (!Directory.Exists(BaseAppFolder))
            {
                Directory.CreateDirectory(BaseAppFolder);    
            }
            if (!File.Exists(Path.Combine(BaseAppFolder, "MyDiaryApp_Log.txt")))
            {
                File.Create(Path.Combine(BaseAppFolder, "MyDiaryApp_Log.txt"));
            }

            MainWindow = new MainWindow() {

                 DataContext = new MainViewModel()
            };

            MainWindow.Show();
        }
    }
}
