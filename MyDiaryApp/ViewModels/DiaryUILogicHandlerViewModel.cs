using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiaryApp.ViewModels
{
    public class DiaryUILogicHandlerViewModel
    {



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
