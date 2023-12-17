using MyDiaryApp.DatabaseHandler;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MyDiaryApp.Commands
{
    public class SavingCommand<T> : CommandBase where T : class, new()
    {
        private T dataModel;

        private string fileName;

        public SavingCommand(T DataModel, string filename)
        {
            dataModel = DataModel;

            fileName = filename;
        }

        public override void Execute(object? parameter)
        {
            _ = SaveData();
        }

        private async Task SaveData()
        {
            try 
            {
                LocalStorageHandler localStorageHandler = new LocalStorageHandler();

                await localStorageHandler.SavePageData<T>(dataModel, fileName);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Error Occured during Saving Data:{ex.Message}");
            }
        }
    }
}
