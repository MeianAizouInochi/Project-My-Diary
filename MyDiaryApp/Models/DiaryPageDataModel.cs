using System;
using System.IO;
using System.Windows.Documents;

namespace MyDiaryApp.Models
{

    public class DiaryPageDataModel
    {
        public string FILE_PATH;

        public string FileName {get; set;}

        public string? PageData {get; set;}

        /// <summary>
        /// Constructor to initialize the properties and a File Path implicitely.
        /// </summary>
        /// <param name="pageData"></param>
        /// <param name="fileName"></param>
        public DiaryPageDataModel(string? pageData, string fileName)
        {
            string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            
            string AppFolder = Path.Combine(appDataFolder, "MyDiaryApp");

            PageData = pageData;

            FileName = fileName;

            FILE_PATH = Path.Combine(AppFolder, FileName); 
        }

    }
}
