using MyDiaryApp.ViewModels;
using System;
using System.Diagnostics;
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
            Debug.WriteLine("Entered overrided onStartUp method of App class.");

            base.OnStartup(e);

            //TODO: Handle Folder creation and UAC request here.

            string BaseAppFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyDiaryApp");

            if (!Directory.Exists(BaseAppFolder))
            {
                Directory.CreateDirectory(BaseAppFolder);    
            }


            MainWindow = new MainWindow() {

                 DataContext = new MainViewModel()

            };

            MainWindow.Show();

            
        }
    }
}
