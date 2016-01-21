using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string startWord;
            string endWord;
            string dictionaryFile;
            string resultFile;

            Console.WriteLine("*** Word Test ***");

            //Get the startWord
            Console.WriteLine("Enter word 1:");
            startWord = Console.ReadLine();

            Console.WriteLine("");

            //Get the endword
            Console.WriteLine("Enter word 2:");
            endWord = Console.ReadLine();

            Console.WriteLine("");
            Console.WriteLine(startWord + " ---> " + endWord);
            Console.WriteLine("");
            Console.WriteLine("Calculating shortest route...");
            Console.WriteLine("");

            //Get the dictionary file
            dictionaryFile = @"words-english.txt";

            //Get the result file
            resultFile = @"ResultFile.txt";

            var da = new DictionaryAnalyser();
            var result = da.Analyse(dictionaryFile, startWord.ToLower(), endWord.ToLower(), resultFile);

            Console.WriteLine("The shortest route is " + (result.Length - 1) + " steps." );
            Console.WriteLine("");

            using (StreamWriter writer = new StreamWriter(resultFile))
            {
                try
                {
                    for (int i = 0; i < result.Length; i++)
                    {
                        writer.WriteLine(result[i]);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unable to write to result file.");
                }
                finally
                {
                    writer.Close();
                }
            }
            
            Console.WriteLine("");
            Console.WriteLine("See " + resultFile + " for results.");
            Console.ReadKey();
           
        }
    }
}
