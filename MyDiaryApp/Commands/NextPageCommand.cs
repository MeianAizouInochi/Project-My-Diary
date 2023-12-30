using MyDiaryApp.DatabaseHandler;
using MyDiaryApp.Models;
using MyDiaryApp.ViewModels.Interfaces;
using System.IO;

namespace MyDiaryApp.Commands
{
    public class NextPageCommand : CommandBase
    {
        private IPageHandler _pageHandler;

        private string _FolderPath;

        public NextPageCommand(IPageHandler pageHandler, string FolderPath)
        {
            _pageHandler = pageHandler;

            _FolderPath = FolderPath;
        }

        public override void Execute(object? parameter)
        {
            GetNextPage();
            
        }


        private async void GetNextPage()
        {
            if (PageMemoryModel.NextPageStack.Count == 0)
            {
                //TODO: Write logic for reaching the end of the diary.
            }
            else
            {
                string? TempFileNameLeft = PageMemoryModel.NextPageStack.Pop();

                string? TempFileNameRight = PageMemoryModel.NextPageStack.Pop();

                LocalStorageHandler _localStorageHandler = new LocalStorageHandler();

                if (TempFileNameLeft != null)
                {
                    if (TempFileNameRight != null)
                    {
                        DiaryPageDataModel? _NewLeft = await _localStorageHandler.LoadPageData<DiaryPageDataModel>(Path.Combine(_FolderPath, TempFileNameLeft));

                        DiaryPageDataModel? _NewRight = await _localStorageHandler.LoadPageData<DiaryPageDataModel>(Path.Combine(_FolderPath,TempFileNameRight));

                        _pageHandler.ReAssignDataModel(_NewLeft, _NewRight);
                    }
                    else
                    {
                        if (PageMemoryModel.NextPageStack.Count == 0)
                        {
                            //might be just the last page pair of the diary, and we will be writting on the left page.
                            DiaryPageDataModel? _NewLeft = await _localStorageHandler.LoadPageData<DiaryPageDataModel>(Path.Combine(_FolderPath, TempFileNameLeft));

                            _pageHandler.ReAssignDataModel(_NewLeft, null);


                        }
                        else
                        {
                            //TODO: Handle Error.
                        }


                    }
                }
                else
                {
                    if (PageMemoryModel.NextPageStack.Count == 0)
                    {
                        //TODO: Show message "Reached End of Diary"
                    }
                    else
                    {
                        //TODO: Handle Error.
                    }

                }


            }
        }
    }
}
