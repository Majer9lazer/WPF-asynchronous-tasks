using System;
using System.IO;
using System.Numerics;

namespace UI.Logic
{
    public class FileWriter
    {
        private static readonly FileInfo FactorialLogPath = new FileInfo(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent?.Parent?.FullName + "/Files/Factorial_log.txt");
        private static readonly FileInfo ConvertToMilitaryPath = new FileInfo(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent?.Parent?.FullName + "/Files/Convert_to_military.txt");
        public static object O= new object();
        public static void WriteToFileFactorial(BigInteger val, byte numOfFactorial)
        {
            lock (O)
            {
                using (StreamWriter stream = new StreamWriter(FactorialLogPath.FullName, true))
                {
                    stream.WriteLine($"User = {Environment.UserName} , factorialNumber = {numOfFactorial} , value = {val}, time = {DateTime.Now}");
                }
            }
       
        }
        public static void WriteToFileConvertedDate(string originalDate, string convertedDate)
        {
            lock (O)
            {
                using (StreamWriter stream = new StreamWriter(ConvertToMilitaryPath.FullName, true))
                {
                    stream.WriteLine($"User = {Environment.UserName} , {nameof(originalDate)} = {originalDate} , {nameof(convertedDate)} = {convertedDate}, time = {DateTime.Now}");
                }
            }
          
        }
    }
}
