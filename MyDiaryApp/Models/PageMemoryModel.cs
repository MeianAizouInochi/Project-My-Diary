using System;
using System.Collections.Generic;
using System.IO;

namespace MyDiaryApp.Models
{
    public class PageMemoryModel
    {
        static public Stack<string?> NextPageStack = new Stack<string?>();

        static public string DiaryHeuristicsFileName = "DiaryCache.xml";

        static public string LogFile = "MyDiaryApp_Log.txt";

        static public string BasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyDiaryApp");

    }
}
