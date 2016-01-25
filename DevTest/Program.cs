using System;
using System.Collections.Generic;
using System.IO;

namespace DevTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var cont = true;

            while (cont)
            {
                string startWord;
                string endWord;
                string dictionaryFile;
                string resultFile;

                Console.Clear();
                Console.WriteLine("*** Word Test ***");
                Console.WriteLine("");

                //Set the startWord
                Console.WriteLine("Enter word 1:");
                startWord = Console.ReadLine();

                while (startWord.Length != 4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter a 4 letter word.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Enter word 1:");
                    startWord = Console.ReadLine();
                }

                //Set the endWord
                Console.WriteLine("Enter word 2:");
                endWord = Console.ReadLine();

                while (endWord.Length != 4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter a 4 letter word.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Enter word 2:");
                    endWord = Console.ReadLine();
                }

                Console.WriteLine("");
                Console.WriteLine(startWord + " ---> " + endWord);
                Console.WriteLine("");
                Console.WriteLine("Analysing dictionary file...");
                Console.WriteLine("");

                //Set the dictionary file
                dictionaryFile = @"words-english.txt";

                //Set the result file
                resultFile = @"ResultFile.txt";

                var da = new DictionaryAnalyser();

                //get the path
                var result = da.Analyse(dictionaryFile, startWord.ToLower(), endWord.ToLower(), resultFile);

                if (result != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("");
                    Console.WriteLine("The shortest route is " + (result.Length - 1) + " steps.");

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
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Unable to write to result file.");
                        }
                        finally
                        {
                            writer.Close();
                        }
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("");
                    Console.WriteLine("See " + resultFile + " for results.");
                }

                Console.WriteLine("");
                Console.WriteLine("Press 'q' to quit or enter to restart");
                var input = Console.ReadKey();

                //break while loop if user enters 'q' or clear window and continue
                if (input.KeyChar == 'q')
                {
                    cont = false;
                }
                
            }
        }
    }
}
