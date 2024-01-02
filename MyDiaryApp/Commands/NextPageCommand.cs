using MyDiaryApp.DatabaseHandler;
using MyDiaryApp.ErrorHandler;
using MyDiaryApp.Models;
using MyDiaryApp.ViewModels.Interfaces;
using System;
using System.IO;
using System.Windows;

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
            try
            {
                if (PageMemoryModel.NextPageStack.Count == 0)
                {
                    MessageBox.Show("Reached the End of the Diary!");

                    return;
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

                            DiaryPageDataModel? _NewRight = await _localStorageHandler.LoadPageData<DiaryPageDataModel>(Path.Combine(_FolderPath, TempFileNameRight));

                            _pageHandler.ReAssignDataModel(_NewLeft, _NewRight);
                        }
                        else
                        {
                            if (PageMemoryModel.NextPageStack.Count == 0)
                            {
                                DiaryPageDataModel? _NewLeft = await _localStorageHandler.LoadPageData<DiaryPageDataModel>(Path.Combine(_FolderPath, TempFileNameLeft));

                                _pageHandler.ReAssignDataModel(_NewLeft, null);
                            }
                            else
                            {
                                throw new Exception("right Side File  Name is Null, even when there are remaining files in the memory stack.");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("the Left Side File Poped From Stack Gives Null name");
                    }


                }
            }
            catch (Exception Ex)
            {
                ErrorLogWriter ErrorWriter = new ErrorLogWriter();

                ErrorWriter.LogWriter("Error Occured in Next Page Command:" + Ex.Message);
            }
        }
    }
}
