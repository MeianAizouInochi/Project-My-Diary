using MyDiaryApp.Commands;
using MyDiaryApp.Models;
using System;
using System.IO;
using System.Windows.Input;

namespace MyDiaryApp.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        public DiaryPageDataModel LeftPageDataModel { get; set; }

        public DiaryPageDataModel RightPageDataModel { get; set; }

        private string? leftPageDocument;

        public string? LeftPageDocument 
        {
            get 
            {
                return leftPageDocument;
            }

            set 
            {
                leftPageDocument = value;

                LeftPageDataModel.PageData = leftPageDocument;

                OnPropertyChanged(nameof(LeftPageDocument));
            }
        }


        private string? rightPageDocument;

        public string? RightPageDocument 
        {
            get 
            {
                return rightPageDocument;
            }

            set 
            {
                rightPageDocument = value;

                RightPageDataModel.PageData = rightPageDocument;

                OnPropertyChanged(nameof(RightPageDocument));


            }
        }

        public string Date { get; set; }

        public string Storage_Folder_Path { get; set; }

        public string FILE_PATH { get; set; }

        private ICommand? LeftPageSaveCommand;

        private ICommand? LoadCommand;

        public MainViewModel()
        {

            Storage_Folder_Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyDiaryApp");



            Date = DateTime.Now.ToString("dd-MM-yyyy");





            LeftPageSaveCommand = new SavingCommand<DiaryPageDataModel>(LeftPageDataModel, FILE_PATH);

            //TODO: Understand the WorkFLow of the app from the users perspective then continue development.
        }


    }
}
