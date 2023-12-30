using System;
using System.Diagnostics;
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


            if (F_Name.Equals(filename))
            {
                return true;
            }
            else
            {
                Debug.WriteLine("filename: " + filename);

                Debug.WriteLine("F_Name: " + F_Name);
                return false;
            }
        }

    }
}
