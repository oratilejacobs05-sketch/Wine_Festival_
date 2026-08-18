using System;
using System.IO;

namespace Wine_Festival_project
{
    public static class Logger
    {
        private const string LogFile = "log.txt";

        public static void Log(string message)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(LogFile, true))
                {
                    writer.WriteLine(
                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +
                        " - " +
                        message);
                }
            }
            catch
            {
                // Prevent logging errors from stopping the application
            }
        }
    }
}