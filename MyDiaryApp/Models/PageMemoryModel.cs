using System.Collections.Generic;

namespace MyDiaryApp.Models
{
    public class PageMemoryModel
    {
        static public Stack<string?> NextPageStack = new Stack<string?>();

        static public string DiaryHeuristicsFileName = "DiaryCache.xml";

    }
}
