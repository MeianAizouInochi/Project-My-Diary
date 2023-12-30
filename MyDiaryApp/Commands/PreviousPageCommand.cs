using MyDiaryApp.Models;
using MyDiaryApp.DatabaseHandler;
using System.IO;
using MyDiaryApp.ViewModels.Interfaces;

namespace MyDiaryApp.Commands
{
    public class PreviousPageCommand : CommandBase
    {
        private const int LEFT = -1;

        private const int RIGHT = 1;   

        private IPageHandler _pageHandler;

        private string _FolderPath;

        public PreviousPageCommand(IPageHandler pageHandler, string FolderPath)
        {
            _pageHandler = pageHandler;

            _FolderPath = FolderPath;
        }

        public override void Execute(object? parameter)
        {
            GetPreviousPage();
        }

        private async void GetPreviousPage()
        {
            LocalStorageHandler _LocalStorageHandler = new LocalStorageHandler();

            DiaryPageDataModel? TempLeftReference = _pageHandler.GetDataModel(LEFT);

            DiaryPageDataModel? TempRightReference = _pageHandler.GetDataModel(RIGHT);

            if (TempLeftReference != null)
            {
                DiaryPageDataModel? _NewRightPage = await _LocalStorageHandler.LoadPageData<DiaryPageDataModel>(Path.Combine(_FolderPath, TempLeftReference.PrevFileName));

                if (_NewRightPage != null)
                {
                    DiaryPageDataModel? _NewLeftPage = await _LocalStorageHandler.LoadPageData<DiaryPageDataModel>(Path.Combine(_FolderPath, _NewRightPage.PrevFileName));

                    if (_NewLeftPage != null)
                    {
                        if (TempRightReference != null)
                        {
                            PageMemoryModel.NextPageStack.Push(TempRightReference.FileName);
                        }
                        else
                        {
                            PageMemoryModel.NextPageStack.Push(null);
                        }

                        PageMemoryModel.NextPageStack.Push(TempLeftReference.FileName);

                        _pageHandler.ReAssignDataModel(_NewLeftPage, _NewRightPage);
                    }
                    else
                    {
                        //TODO: Handle the file corruption error.
                    }
                }
                else
                {
                    //TODO: Check if we have reached the start of the diary or this is just a corruption in the file system.
                }


            }


        }
    }
}
