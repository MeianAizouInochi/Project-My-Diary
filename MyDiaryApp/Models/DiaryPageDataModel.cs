using System;

namespace MyDiaryApp.Models
{
    [Serializable]
    public class DiaryPageDataModel
    {
        //public string? FILE_PATH;

        public int Side { get; set; }

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
        public DiaryPageDataModel(string? pageData, string fileName, int side)
        {
            //string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            
            //string AppFolder = Path.Combine(appDataFolder, "MyDiaryApp");

            PageData = pageData;

            FileName = fileName;

            Side = side;

            //FILE_PATH = Path.Combine(AppFolder, FileName); 
        }

    }
}
