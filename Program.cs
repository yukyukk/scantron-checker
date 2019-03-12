using System;
using System.IO;

namespace c_project
{
   class Program
   {
      static void writeToFile(string target, string content)
      {
         try
         {
            StreamWriter sw = new StreamWriter(target);
            sw.Write(content);
            sw.Close();
         }
         catch (Exception error)
         {
            Console.WriteLine("Error: " + error.Message);
         }
      }
      static string readFromFile(string target)
      {
         String line, fullFile = "";
         try
         {
            StreamReader sr = new StreamReader(target);
            line = sr.ReadLine();
            while (line != null)
            {
               fullFile += line + "\n";
               Console.WriteLine("newl: " + line);
               line = sr.ReadLine();
            }
            sr.Close();
         }
         catch (Exception error)
         {
            Console.WriteLine("Error: " + error.Message);
         }
         return fullFile;
      }
      static void Main(string[] args)
      {
         string file, target = "files/scantron1.txt";
         file = readFromFile(target);
         Console.WriteLine("file: " + file);
         writeToFile(target, file + DateTime.Now.ToString("HH:mm:ss tt"));
         Console.WriteLine("Finished execution");
      }
   }
}
