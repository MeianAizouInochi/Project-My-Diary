using System;
using System.IO;

namespace MyDiaryApp.Models
{
    [Serializable]
    public class DiaryPageDataModel
    {
        //public string? FILE_PATH;

        public string? FileName {get; set;}

        public string? PageData {get; set;}

        public DiaryPageDataModel()
        {
            //default constructor
        }

        /// <summary>
        /// Constructor to initialize the properties and a File Path implicitely.
        /// </summary>
        /// <param name="pageData"></param>
        /// <param name="fileName"></param>
        public DiaryPageDataModel(string? pageData, string fileName)
        {
            //string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            
            //string AppFolder = Path.Combine(appDataFolder, "MyDiaryApp");

            PageData = pageData;

            FileName = fileName;

            //FILE_PATH = Path.Combine(AppFolder, FileName); 
        }

    }
}
