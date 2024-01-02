using System.ComponentModel;

namespace MyDiaryApp.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string? PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public ViewModelBase() 
        {
            //Debug.WriteLine("Initialised the ViewModelBase Object");
        }
    }
}

