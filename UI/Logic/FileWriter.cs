using System;
using System.IO;
using System.Numerics;

namespace UI.Logic
{
    public static class FileWriter
    {
        private static readonly FileInfo Path = new FileInfo(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent?.Parent?.FullName + "/Files/Factorial_log.txt");

        public static void WriteToFileFactorial(BigInteger val, byte numOfFactorial, string path = null)
        {
            using (StreamWriter stream = new StreamWriter(Path.FullName, true))
            {
                stream.WriteLine($"User = {Environment.UserName} , factorialNumber = {numOfFactorial} , value = {val}, time = {DateTime.Now}");
            }
        }

    }
}
