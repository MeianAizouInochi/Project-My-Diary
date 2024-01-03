using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyDiaryApp.DatabaseHandler
{
    /// <summary>
    /// This class helps to store the object after serialization in a .xml format.
    /// </summary>
    public class LocalStorageHandler
    {
        /// <summary>
        /// This method saves the data in a serialized format to a xml file. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="DataModel"></param>
        /// <param name="FILE_PATH"></param>
        /// <returns>Task</returns>
        public async Task SavePageData<T>(T DataModel, string FILE_PATH) where T : class, new()
        {
            try
            {
                if (DataModel != null)
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));

                    using (StreamWriter writer = new StreamWriter(FILE_PATH))
                    {
                        await Task.Run(() => serializer.Serialize(writer, DataModel));
                    }
                }
            }
            catch (Exception ex)
            {
                string ExMessage = "Error in Saving Method [LocalStorageHandler]: " + ex.Message;

                throw new Exception(ExMessage);
            }
            
        }

        /// <summary>
        /// This method Loads the data stored in the xml file after deserializing it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="FILE_PATH"></param>
        /// <returns>Task<T?></returns>
        public async Task<T?> LoadPageData<T>(string FILE_PATH) where T : class, new()
        {
            T? DataModel = null;

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));

                using (StreamReader reader = new StreamReader(FILE_PATH))
                {
                    DataModel = await Task.Run(() => (T?)serializer.Deserialize(reader));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in Loading Method [LocalStorageHandler]: " + ex.Message);
            }

            return DataModel;
        }
    }
}
