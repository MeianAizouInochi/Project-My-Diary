﻿using MyDiaryApp.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace MyDiaryApp.DatabaseHandler
{
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
            if (DataModel != null)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(DiaryPageDataModel));

                using (StreamWriter writer = new StreamWriter(FILE_PATH))
                {
                    await Task.Run(() => serializer.Serialize(writer, DataModel));
                }
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
                MessageBox.Show($"Error in loading the Page Data:{ex.Message}");
            }

            return DataModel;
        }
    }
}
