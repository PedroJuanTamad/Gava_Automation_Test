using System;
using System.IO;

namespace Generic_Test_Framework
{
    public static class ErrorLogger
    {
        public static void LogExceptionError(string error)
        {
            using (StreamWriter file = new StreamWriter(@"" + AppDomain.CurrentDomain.BaseDirectory + "\\TestErrors.txt", true))
            {
              
                file.WriteLine(Environment.NewLine + ": " + DateTime.Now.ToString() + Environment.NewLine + error);
            }
        }
    }
}
