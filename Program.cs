using System;
using System.IO;
using System.Collections.Generic;

namespace c_project
{
   class Program
   {
      static void writeToFile(string target, string content)
      {
         try
         {
            StreamWriter sw = new StreamWriter(target);
            // We can use Write or WriteLine here, depends on how we will handle other stuff
            sw.Write(content);
            sw.Close();
         }
         catch (Exception error)
         {
            Console.WriteLine("Error: " + error.Message);
         }
      }
      static void writeNewLineToFile(string target, string newLine)
      {
         string content;
         content = readFromFile(target);
         content += newLine;
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
            // Read one line in order to have the condition tested in the next loop
            line = sr.ReadLine();
            while (line != null)
            {
               // After checking the line is not null adds the string to fullFile (keeps looping)
               fullFile += line+"\n";
               // Console.WriteLine("newl: " + line);
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

      static List<string> readToList(string target)
      {
         string line;
         var lines = new List<string>();
         try
         {
            StreamReader sr = new StreamReader(target);
            line = sr.ReadLine();
            while (line != null)
            {
               lines.Add(line);
               // Console.WriteLine("newl: " + line);
               line = sr.ReadLine();
            }
            sr.Close();
         }
         catch (Exception error)
         {
            Console.WriteLine("Error: " + error.Message);
         }
         return lines;
      }
      static string[] parseLine(string line)
      {
         string[] param = { ",", ";", ":", " ", "\t" };
         string[] pair = line.Split(param, StringSplitOptions.RemoveEmptyEntries);
         return pair;
      }

      static void Main(string[] args)
      {
         string target = "files/scantron2.txt";
         string newFileName = target.Substring(0, target.Length-4)+"_results_"+DateTime.Now.ToString("HH-mm-ss")+".txt";

         List<string> lines = new List<string>();
         lines = readToList(target);

         int num = lines.Count;
         int[] ids = new int[num];
         string[] answers = new string[num];
         int[] score = new int[num];
         string answerKey = "ABCDEABCDEABCDEABCDEABCDE";

         Console.WriteLine("{0} lines were found in '{1}'", num, target);

         for (int i = 0; i < num; i++)
         {
            string[] pair = parseLine(lines[i]);
            Console.WriteLine("ID: {0} | Answers: {1}", pair[0], pair[1]);
            ids[i] = Convert.ToInt32(pair[0]);
            answers[i] = pair[1];

            // Calculate score
            for (int letter = 0; letter < answers[i].Length; letter++)
            {
               if (answers[i][letter] == 'X') { }
               else if (answers[i][letter] == answerKey[letter])
               {
                  score[i] += 4;
               }
               else
               {
                  score[i] -= 1;
               }
            }

            // Add result line in file
            string toWrite = $"ID: {ids[i]} | ANSWERS: {answers[i]} | SCORE: {score[i]}";
            writeNewLineToFile(newFileName, toWrite);
         }

         while (true)
         {
            int input = Convert.ToInt32(Console.ReadLine());
            if (input == -1) break;
            Console.WriteLine("Student ID is {0} the answers were {1} the result is {2}", ids[input], answers[input], score[input]);
         }
      }
   }
}
