using System;
using System.IO;

namespace MyDiaryApp.ViewModels
{
    public class DiaryUILogicHandlerViewModel
    {

        public bool DoesFileExists(string Folder, string FileName) 
        {

            if (File.Exists(Path.Combine(Folder, FileName)))
            {
                return true;
            }

            return false;
        }

        public bool CanWrite(string filename) 
        {
            string Date = DateTime.Now.ToString("dd-MM-yyyy");

            string F_Name = Date + ".xml";

            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string AppFolder = Path.Combine(appDataFolder, "MyDiaryApp");

            string FullFileName = Path.Combine(AppFolder, F_Name);

            if (FullFileName.Equals(filename))
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}
