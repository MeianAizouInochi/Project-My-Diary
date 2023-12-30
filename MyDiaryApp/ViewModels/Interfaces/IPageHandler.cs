using MyDiaryApp.Models;

namespace MyDiaryApp.ViewModels.Interfaces
{
    public interface IPageHandler
    {
        void ReAssignDataModel(DiaryPageDataModel? _LeftPageDataModel, DiaryPageDataModel? _RightPageDataModel);

        DiaryPageDataModel? GetDataModel(int SIDE);

        DiaryPageDataModel? GetDataModel(string fileName);
    }
}
