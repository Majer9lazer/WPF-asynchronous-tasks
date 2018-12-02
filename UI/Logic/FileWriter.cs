using System;
using System.IO;
using System.Numerics;

namespace UI.Logic
{
    public static class FileWriter
    {
        private static readonly FileInfo FactorialLogPath = new FileInfo(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent?.Parent?.FullName + "/Files/Factorial_log.txt");
        private static readonly FileInfo ConvertToMilitaryPath = new FileInfo(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent?.Parent?.FullName + "/Files/Convert_to_military.txt.txt");
        public static void WriteToFileFactorial(BigInteger val, byte numOfFactorial)
        {
            using (StreamWriter stream = new StreamWriter(FactorialLogPath.FullName, true))
            {
                stream.WriteLine($"User = {Environment.UserName} , factorialNumber = {numOfFactorial} , value = {val}, time = {DateTime.Now}");
            }
        }
        public static void WriteToFileConvertedDate(string originalDate, string convertedDate)
        {
            using (StreamWriter stream = new StreamWriter(FactorialLogPath.FullName, true))
            {
                stream.WriteLine($"User = {Environment.UserName} , {nameof(originalDate)} = {originalDate} , {nameof(convertedDate)} = {convertedDate}, time = {DateTime.Now}");
            }
        }
    }
}
