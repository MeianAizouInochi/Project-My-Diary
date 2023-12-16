namespace MyDiaryApp.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        private string leftPageDocument;

        public string LeftPageDocument 
        {
            get 
            {
                return leftPageDocument;
            }

            set 
            {
                leftPageDocument = value;

                OnPropertyChanged(nameof(LeftPageDocument));
            }
        }


        private string rightPageDocument;

        public string RightPageDocument 
        {
            get 
            {
                return rightPageDocument;
            }

            set 
            {
                rightPageDocument = value;

                OnPropertyChanged(nameof(RightPageDocument));
            }
        }

        public MainViewModel()
        {
            
        }
    }
}
