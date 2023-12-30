using MyDiaryApp.DatabaseHandler;
using MyDiaryApp.Models;
using MyDiaryApp.ViewModels.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace MyDiaryApp.Commands
{
    public class SavingCommand:CommandBase
    {
        private IPageHandler _pageHandler;

        private string _folder_Path;

        private string FileName;

        public SavingCommand(IPageHandler PageHandler, string Folder_Path, string fileName)
        {
            _pageHandler = PageHandler;

            _folder_Path = Folder_Path;

            FileName = fileName;
        }

        public override void Execute(object? parameter)
        {
            if (PageMemoryModel.NextPageStack.Count == 0)
            {
                _ = SaveData();
            }
            else
            {
                Debug.WriteLine("not in Today's Page of the Diary.");
            }
            
        }

        private async Task SaveData()
        {
            try 
            {
                DiaryPageDataModel? ToSavePageDataModel = _pageHandler.GetDataModel(FileName);

                if (ToSavePageDataModel != null)
                {
                    LocalStorageHandler localStorageHandler = new LocalStorageHandler();

                    await localStorageHandler.SavePageData<DiaryPageDataModel>(ToSavePageDataModel, Path.Combine(_folder_Path,FileName));

                    DiaryHeuristicsModel? obj = await localStorageHandler.LoadPageData<DiaryHeuristicsModel>(Path.Combine(_folder_Path, PageMemoryModel.DiaryHeuristicsFileName));

                    if (obj != null)
                    {
                        obj.CacheFileName = FileName;

                        await localStorageHandler.SavePageData<DiaryHeuristicsModel>(obj, Path.Combine(_folder_Path, PageMemoryModel.DiaryHeuristicsFileName));
                    }
                    else
                    {
                        throw new Exception("Corrupted Diary Cache File!");
                    }
                }
                
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Error Occured during Saving Data:{ex.Message}");
            }
        }
    }
}
