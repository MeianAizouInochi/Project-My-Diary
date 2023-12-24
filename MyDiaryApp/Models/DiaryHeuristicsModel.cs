
namespace MyDiaryApp.Models
{
    public class DiaryHeuristicsModel
    {
        public string? PrevFileName { get; set; }

        public string CurrentFileName { get; set; }


        public DiaryHeuristicsModel()
        {
            
        }

        public DiaryHeuristicsModel(string? PrevFileName = null, string CurrentFileName)
        {
            this.CurrentFileName = CurrentFileName;
            this.PrevFileName = PrevFileName;
        }
    }
}
