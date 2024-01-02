using System;

namespace MyDiaryApp.Models
{
    [Serializable]
    public class DiaryPageDataModel
    {
        public string? PrevFileName { get; set; }

        public int Side { get; set; }

        public string FileName {get; set;}

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
        public DiaryPageDataModel(string? prevFileName, string? pageData, string fileName, int side)
        {

            PrevFileName = prevFileName;

            PageData = pageData;

            FileName = fileName;

            Side = side;

        }

    }
}
