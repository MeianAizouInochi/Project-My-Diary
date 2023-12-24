using MyDiaryApp.DatabaseHandler;
using MyDiaryApp.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyDiaryApp.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        private const string DiaryHeuristicsModelFileName = "DiaryCache.xml";

        //TODO: Create Bi-Directional Data Relation between DiarypageDataModel object and,
        //the string properties of left mpage and right page.

        public DiaryPageDataModel? LeftPageDataModel { get; set; } 

        public DiaryPageDataModel? RightPageDataModel { get; set; }

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

            DoInitialProcess();
        }


        public async void DoInitialProcess()
        {
            if (CheckIfFileExist(Storage_Folder_Path, Date + ".xml"))
            {
                DiaryPageDataModel? TempObj_CurrentPage = await LoadData<DiaryPageDataModel>(Storage_Folder_Path, Date+".xml");

                if (TempObj_CurrentPage != null && TempObj_CurrentPage.Side == -1)
                {
                    LeftPageDataModel = TempObj_CurrentPage;

                    RightPageDataModel = null;

                    //TODO: Update UI
                }

                else if (TempObj_CurrentPage != null && TempObj_CurrentPage.Side == 1)
                {
                    if (CheckIfFileExist(Storage_Folder_Path, DiaryHeuristicsModelFileName))
                    {
                        DiaryHeuristicsModel? _Obj = await LoadData<DiaryHeuristicsModel>(Storage_Folder_Path, DiaryHeuristicsModelFileName);

                        if (_Obj != null) 
                        {
                            if (_Obj.PrevFileName == null)
                            {
                                //Handle Error
                            }

                            else if (_Obj.PrevFileName.Equals(_Obj.CurrentFileName))
                            {
                                //Handle Error
                            }

                            else
                            {
                                DiaryPageDataModel? _tempObj = await LoadData<DiaryPageDataModel>(Storage_Folder_Path, _Obj.PrevFileName);

                                LeftPageDataModel = _tempObj;

                                //TODO: Make the object non-Editable in UI

                                RightPageDataModel = TempObj_CurrentPage;

                                //TODO: Make the object editable in the UI
                            }
                        }

                        else
                        {
                            //TODO: Handle Error Situation and prompt for fixing.
                        }

                    }

                    else
                    {
                        //TODO: Handle Error Situation and prompt for fixing.
                    }
                }

            }

            else
            {
                if(CheckIfFileExist(Storage_Folder_Path,DiaryHeuristicsModelFileName))
                {
                    DiaryHeuristicsModel? _Obj = await LoadData<DiaryHeuristicsModel>(Storage_Folder_Path,DiaryHeuristicsModelFileName);

                    if (_Obj == null)
                    {
                        //TODO: Handle Error.
                    }

                    else if (_Obj.PrevFileName.Equals(_Obj.CurrentFileName))
                    {
                        //TODO: Handle Error.
                    }

                    else
                    {
                        DiaryPageDataModel? _TempObjPrev = await LoadData<DiaryPageDataModel>(Storage_Folder_Path, _Obj.CurrentFileName);

                        if (_TempObjPrev != null)
                        {
                            if (_TempObjPrev.Side == -1)
                            {
                                LeftPageDataModel = _TempObjPrev;

                                //TODO: Make object Above non-editable in UI

                                RightPageDataModel = new DiaryPageDataModel(null, Date + ".xml", 1);

                                SaveData<DiaryPageDataModel>(RightPageDataModel, Storage_Folder_Path, Date + ".xml");

                                //TODO: Make the object Editable in UI


                            }

                            else if (_TempObjPrev.Side == 1)
                            {
                                RightPageDataModel = null;

                                //TODO: Make the above object non-editable.

                                LeftPageDataModel = new DiaryPageDataModel(null, Date + ".xml", -1);

                                SaveData<DiaryPageDataModel>(LeftPageDataModel, Storage_Folder_Path, Date + ".xml");

                                //TODO: Make the above object Editable.
                            }

                            else
                            {
                                //TODO: Handle Error.
                            }
                        }

                        else
                        {
                            //TODO: Handle Error.
                        }
                    }
                }

                else
                {

                }
            }
            
        }

        public int CheckSide(DiaryPageDataModel obj)
        {
            return obj.Side;
        }


        public bool CheckIfFileExist(string Folder, string FileName)
        {
            return (new DiaryUILogicHandlerViewModel()).DoesFileExists(Folder, FileName);
        }

        public async Task<T?> LoadData<T>(string Folder, string FileName) where T: class, new()
        {
            T? obj = await (new LocalStorageHandler()).LoadPageData<T>(Path.Combine(Folder, FileName));

            return obj;

        }

        public async void SaveData<T>(T Object, string Folder, string FileName) where T: class, new()
        {
            await new LocalStorageHandler().SavePageData<T>(Object, Path.Combine(Folder, FileName));
        }

    }
}
