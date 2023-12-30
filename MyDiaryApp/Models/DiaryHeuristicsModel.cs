
namespace MyDiaryApp.Models
{
    public class DiaryHeuristicsModel
    {
        public string CacheFileName { get; set; }

        public DiaryHeuristicsModel()
        {
            
        }

        public DiaryHeuristicsModel(string CurrentFileName)
        {
            this.CacheFileName = CurrentFileName;
        }
    }
}
