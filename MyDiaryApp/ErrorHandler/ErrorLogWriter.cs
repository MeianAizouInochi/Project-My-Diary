using MyDiaryApp.Models;
using System;
using System.IO;

namespace MyDiaryApp.ErrorHandler
{
    public class ErrorLogWriter
    {
        public ErrorLogWriter() { }

        public bool LogWriter(string Message)
        {
            try
            {
                using (StreamWriter sr = new StreamWriter(Path.Combine(PageMemoryModel.BasePath, PageMemoryModel.LogFile)))
                {
                    sr.WriteLine(Message);
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
