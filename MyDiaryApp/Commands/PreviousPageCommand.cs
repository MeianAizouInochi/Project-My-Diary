using MyDiaryApp.Models;
using MyDiaryApp.DatabaseHandler;
using System.IO;
using MyDiaryApp.ViewModels.Interfaces;
using System.Windows;
using System;
using MyDiaryApp.ErrorHandler;

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
            try
            {
                LocalStorageHandler _LocalStorageHandler = new LocalStorageHandler();

                DiaryPageDataModel? TempLeftReference = _pageHandler.GetDataModel(LEFT);

                DiaryPageDataModel? TempRightReference = _pageHandler.GetDataModel(RIGHT);

                string TodayFileName = _pageHandler.GetTodayFileName();

                if (TempLeftReference != null && TempLeftReference.FileName.Equals(TodayFileName))
                {
                    await _LocalStorageHandler.SavePageData<DiaryPageDataModel>(TempLeftReference, Path.Combine(_FolderPath, TempLeftReference.FileName));
                }
                else if (TempRightReference != null && TempRightReference.FileName.Equals(TodayFileName))
                {
                    await _LocalStorageHandler.SavePageData<DiaryPageDataModel>(TempRightReference, Path.Combine(_FolderPath, TempRightReference.FileName));
                }
                else
                {
                    //do nothing
                }

                if (TempLeftReference != null)
                {
                    if (TempLeftReference.PrevFileName == null)
                    {
                        MessageBox.Show("We have reached the Start of the Diary");

                        return;
                    }

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
                            throw new Exception($"The File {_NewRightPage.PrevFileName} is returning Null value on trying to Load it to memory.");
                        }
                    }
                    else
                    {
                        throw new Exception($"We have not reached the Start of Diary, but the right Page file {TempLeftReference.PrevFileName} returns a null value on trying to Load.");
                    }
                }
                else
                {
                    throw new Exception("MainViewModel leftPage Data model is Having Null Value.");
                }
            }
            catch (Exception Ex)
            {
                ErrorLogWriter ErrorWriter = new ErrorLogWriter();

                ErrorWriter.LogWriter("Error Occured in Previous Page Command:" + Ex.Message);
                
            }
        }
    }
}
