using MyDiaryApp.Commands;
using MyDiaryApp.DatabaseHandler;
using MyDiaryApp.ErrorHandler;
using MyDiaryApp.Models;
using MyDiaryApp.ViewModels.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyDiaryApp.ViewModels
{
    public class MainViewModel:ViewModelBase, IPageHandler
    {
        private readonly string DiaryHeuristicsModelFileName = PageMemoryModel.DiaryHeuristicsFileName;

        private const int LEFT = -1;

        private const int RIGHT = 1;

        private bool canWriteToLeft;

        public bool CanWriteToLeft
        {
            get { return canWriteToLeft; }
            set {
                canWriteToLeft = value;

                SaveButtonVis = (CanWriteToLeft != CanWriteToRight);

                OnPropertyChanged(nameof(CanWriteToLeft));
            }
        }

        private bool canWriteToRight;

        public bool CanWriteToRight
        {
            get { return canWriteToRight; }
            set { 
                canWriteToRight = value;

                SaveButtonVis = (CanWriteToRight != CanWriteToLeft);

                OnPropertyChanged(nameof(CanWriteToRight));
            }
        }

        private bool saveButtonVis;

        public bool SaveButtonVis
        {
            get { return saveButtonVis; }
            set 
            { 
                saveButtonVis = value;

                OnPropertyChanged(nameof(SaveButtonVis));
            }
        }

        public string Storage_Folder_Path { get; set; }

        public string Date { get; set; }

        public string CurrentFileName { get; set; }

        private DiaryPageDataModel? leftPageDataModel;

        public DiaryPageDataModel? LeftPageDataModel
        {
            get { 
                return leftPageDataModel;
            }
            set {

                leftPageDataModel = value;

                if (leftPageDataModel != null)
                {
                    LeftPageDocument = leftPageDataModel.PageData;

                    CanWriteToLeft = CanWriteTo(leftPageDataModel.FileName);
                }
                else
                {
                    LeftPageDocument = null;
                    CanWriteToLeft = false;
                }

                OnPropertyChanged(nameof(LeftPageDataModel));
            }
        }

        private DiaryPageDataModel? rightPageDataModel;

        public DiaryPageDataModel? RightPageDataModel
        {
            get { 
                return rightPageDataModel;
            }
            set { 
                rightPageDataModel = value;

                if (rightPageDataModel != null)
                {
                    RightPageDocument = rightPageDataModel.PageData;

                    CanWriteToRight = CanWriteTo(rightPageDataModel.FileName);
                }
                else
                {
                    RightPageDocument = null;
                    CanWriteToRight = false;
                }

                OnPropertyChanged(nameof(RightPageDataModel));
            }
        }

        private string? leftPageDocument;

        public string? LeftPageDocument
        {
            get
            {
                return leftPageDocument;
            }

            set
            {

                if (LeftPageDataModel != null)
                {
                    leftPageDocument = value;

                    LeftPageDataModel.PageData = leftPageDocument;
                }

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
                if (RightPageDataModel != null)
                {
                    rightPageDocument = value;

                    RightPageDataModel.PageData = rightPageDocument;
                }
                else
                {
                    if (PageMemoryModel.NextPageStack.Count == 0)
                    {
                        rightPageDocument = null;
                    }
                    else 
                    {
                        Debug.WriteLine("Something has gone wrong with RightPageDataModel object!");
                    }
                }
                OnPropertyChanged(nameof(RightPageDocument));


            }
        }

        public ICommand? ShowPrevCommand { get; set; }

        public ICommand? ShowNextCommand { get; set; }

        public ICommand? SavePageCommand { get; set; }

        public ICommand? CloseAppCommand { get; set; }

        public ICommand? MinimizeAppCommand { get; set; }


        /// <summary>
        /// Constructor of the MainView model Class.
        /// This runs the inital Initialisations, and a Initial Page Loading Algorithm.
        /// </summary>
        public MainViewModel()
        {
            Storage_Folder_Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyDiaryApp");

            Date = DateTime.Now.ToString("dd-MM-yyyy");

            CurrentFileName = Date + ".xml";

            DoInitialProcess();

            ShowPrevCommand = new PreviousPageCommand(this, PageMemoryModel.BasePath);

            ShowNextCommand = new NextPageCommand(this, PageMemoryModel.BasePath);

            SavePageCommand = new SavingCommand(this, PageMemoryModel.BasePath, CurrentFileName);

            MinimizeAppCommand = new MinimizeCommand();

            CloseAppCommand = new CloseCommand();
        }

        /// <summary>
        /// This method Runs the Initial Page Loading algorithm on the basis of the Stored Files integrity and availability.
        /// </summary>
        private async void DoInitialProcess()
        {
            try
            {
                if (CheckIfFileExist(PageMemoryModel.BasePath, CurrentFileName))
                {
                    DiaryPageDataModel? TempObj_CurrentPage = await LoadData<DiaryPageDataModel>(PageMemoryModel.BasePath, CurrentFileName);

                    if (TempObj_CurrentPage != null)
                    {
                        if (TempObj_CurrentPage.Side == LEFT)
                        {
                            ReAssignDataModel(TempObj_CurrentPage, null);
                        }
                        else if (TempObj_CurrentPage.Side == RIGHT)
                        {
                            if (TempObj_CurrentPage.PrevFileName != null)
                            {
                                DiaryPageDataModel? _TempLeftPageModel = await LoadData<DiaryPageDataModel>(Storage_Folder_Path, TempObj_CurrentPage.PrevFileName);

                                if (_TempLeftPageModel != null)
                                {
                                    if (_TempLeftPageModel.Side == LEFT)
                                    {
                                        ReAssignDataModel(_TempLeftPageModel, TempObj_CurrentPage);
                                    }
                                    else
                                    {
                                        throw new Exception($"The {_TempLeftPageModel.FileName} has a Wrong value of property Side.");
                                    }
                                }
                                else
                                {
                                    throw new Exception($"The loading of previous page: {TempObj_CurrentPage.PrevFileName} of Current page: {CurrentFileName} have resulted in null Object denoting a corrupted File error.");
                                }
                            }
                            else
                            {
                                throw new Exception($"The Current Page: {CurrentFileName} Object had null Value in Previous File Name. This Created no Link to its Previous File for the Linked List Implementation.");
                            }
                        }
                        else
                        {
                            throw new Exception($"The object of Current Page: {CurrentFileName} had Side Value 0.");
                        }
                    }
                    else
                    {
                        throw new Exception($"Existing Current Page named: {CurrentFileName} returns Null Object!");
                    }
                }
                else
                {
                    if (CheckIfFileExist(Storage_Folder_Path, DiaryHeuristicsModelFileName))
                    {
                        DiaryHeuristicsModel? _heuristicsTempObj = await LoadData<DiaryHeuristicsModel>(Storage_Folder_Path, DiaryHeuristicsModelFileName);

                        if (_heuristicsTempObj == null)
                        {
                            throw new Exception("Error Occured in Loading the Data heuristics.");
                        }
                        else
                        {
                            DiaryPageDataModel? _TempObjPrev = await LoadData<DiaryPageDataModel>(Storage_Folder_Path, _heuristicsTempObj.CacheFileName);

                            if (_TempObjPrev == null)
                            {
                                throw new Exception("The Diary Page Data Model Loaded from Data heuristics returned NULL object.");
                            }
                            else
                            {
                                if (_TempObjPrev.Side == LEFT)
                                {
                                    ReAssignDataModel(_TempObjPrev, new DiaryPageDataModel(_TempObjPrev.FileName, null, CurrentFileName, RIGHT));
                                }
                                else if (_TempObjPrev.Side == RIGHT)
                                {
                                    ReAssignDataModel(new DiaryPageDataModel(_TempObjPrev.FileName, null, CurrentFileName, LEFT), null);
                                }
                                else
                                {
                                    throw new Exception("Side Property have unintended value : 0");
                                }
                            }
                        }
                    }
                    else
                    {
                        string[] FileList = Directory.GetFiles(Storage_Folder_Path); //Might become more expensive operation, the more entries increase in the application.

                        if (FileList.Length == 0)
                        {

                            FirstRunProcess(); //Runs First Time File Creations and Document handling.

                        }
                        else
                        {
                            throw new Exception("File system is Corrupted. Diary Cache File is Missing");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogWriter ErrorWriter = new ErrorLogWriter();

                ErrorWriter.LogWriter("Exception Occured in DoInitialProcess Method: " + ex.Message);
            }
        }

        private void FirstRunProcess()
        {
            try
            {
                DiaryHeuristicsModel _diaryHeuristics = new DiaryHeuristicsModel(CurrentFileName);

                SaveData<DiaryHeuristicsModel>(_diaryHeuristics, Storage_Folder_Path, DiaryHeuristicsModelFileName);

                DiaryPageDataModel _diaryPageDataModel = new DiaryPageDataModel(null, null, CurrentFileName, LEFT);

                SaveData<DiaryPageDataModel>(_diaryPageDataModel, Storage_Folder_Path, CurrentFileName);

                ReAssignDataModel(_diaryPageDataModel, null);
            }
            catch (Exception Ex)
            {
                ErrorLogWriter ErrorWriter = new ErrorLogWriter();

                ErrorWriter.LogWriter("Error occured in FirstRunProcess Method: " + Ex.Message);
            }
            
        }

        private bool CheckIfFileExist(string Folder, string FileName)
        {
            return (new DiaryUILogicHandlerViewModel()).DoesFileExists(Folder, FileName);
        }

        private async Task<T?> LoadData<T>(string Folder, string FileName) where T: class, new()
        {
            T? obj = await (new LocalStorageHandler()).LoadPageData<T>(Path.Combine(Folder, FileName));

            return obj;

        }

        private async void SaveData<T>(T Object, string Folder, string FileName) where T: class, new()
        {
            await new LocalStorageHandler().SavePageData<T>(Object, Path.Combine(Folder, FileName));
        }

        /// <summary>
        /// This method helps to reassign the DataModel Objects.
        /// Usage in the App: Helps to reassign the DataModel Objects to Call the setter block and hence update the Text Shown in UI.
        /// </summary>
        /// <param name="_LeftPageDataModel"></param>
        /// <param name="_RightPageDataModel"></param>
        public void ReAssignDataModel(DiaryPageDataModel? _LeftPageDataModel, DiaryPageDataModel? _RightPageDataModel)
        {
            LeftPageDataModel = _LeftPageDataModel;

            if (_RightPageDataModel == null)
            {
                
            }

            RightPageDataModel = _RightPageDataModel;
        }

        /// <summary>
        /// This returns a reference to the DiaryPageDataModel Object of the respective PageSide as provided in Param.
        /// Param Value Range: -1 and 1
        /// </summary>
        /// <param name="SIDE"></param>
        /// <returns></returns>
        public DiaryPageDataModel? GetDataModel(int SIDE)
        {
            if (SIDE == LEFT)
            {
                return LeftPageDataModel;
            }
            else if(SIDE == RIGHT)
            {
                return RightPageDataModel;
            }
            else
            {
                return null; //throw error.
            }
        }

        private bool CanWriteTo(string FileName)
        {
            return (new DiaryUILogicHandlerViewModel()).CanWrite(FileName);
        }

        public DiaryPageDataModel? GetDataModel(string fileName)
        {
            if(LeftPageDataModel!=null)
            {
                if (LeftPageDataModel.FileName.Equals(fileName))
                {
                    return LeftPageDataModel;
                }
            }
            
            if(RightPageDataModel!=null)
            {
                if(RightPageDataModel.FileName.Equals(fileName))
                {
                    return RightPageDataModel;
                }
            }

            return null;
        }

        public string GetTodayFileName()
        {
            return CurrentFileName;
        }
    }
}
