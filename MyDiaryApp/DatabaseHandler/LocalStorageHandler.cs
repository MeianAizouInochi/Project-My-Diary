using MyDiaryApp.Models;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Xml;

namespace MyDiaryApp.DatabaseHandler
{
    public class LocalStorageHandler
    {
        public DiaryPageDataModel? DataModel { get; set; }

        /// <summary>
        /// Constructor for Getting the File to save the data.
        /// </summary>
        /// <param name="dataModel"></param>
        public LocalStorageHandler(DiaryPageDataModel dataModel)
        {
            DataModel = dataModel;
        }

        /// <summary>
        /// Constructor for getting the filename to load and send the data.
        /// </summary>
        /// <param name="fileName"></param>
        public LocalStorageHandler(string fileName) 
        {
            DataModel = new DiaryPageDataModel(null, fileName);
        }

        /// <summary>
        /// This method asynchronously saves the data of the flowdocument to a file as per the data inside the DiaryPageDataModel object.
        /// </summary>
        /// <returns>Nothing</returns>
        public async Task SavePageData()
        {
            if (DataModel != null)
            {
                using (XmlWriter writer = XmlWriter.Create(DataModel.FILE_PATH))
                {
                    await Task.Run(() => XamlWriter.Save(DataModel.PageData, writer));
                }
            }
        }

        /// <summary>
        /// This method loads the Data of a diary page i.e. flowdocument which is stored as a xaml file in the appdata, into the DiaryPageDataModel Object
        /// </summary>
        /// <returns>DiaryPageDataModel Object as a Task</returns>
        public async Task<DiaryPageDataModel?> LoadPageData()
        {
            if (DataModel != null)
            {
                try
                {
                    using (XmlReader reader = XmlReader.Create(DataModel.FILE_PATH))
                    {
                        DataModel.PageData = await Task.Run(() => (FlowDocument)XamlReader.Load(reader));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error in loading the Page Data:{ex.Message}");
                }
            }

            return DataModel;
        }
    }
}
