namespace DevTest
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class DictionaryAnalyser
    {

        public string[] Analyse(string dictionaryFile, string startWord, string endWord, string resultFile)
        {
            // get all the four letter words from the dictionary file
            var initialFourLetterWordList = this.LoadFourLetterWords(dictionaryFile);

            // check the supplied words are in the dictionary
            if (!initialFourLetterWordList.Contains(startWord))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(startWord + " does not exist in the dictionary file.");
                Console.ForegroundColor = ConsoleColor.White;
                return null;
            }

            if (!initialFourLetterWordList.Contains(endWord))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(endWord + " does not exist in the dictionary file.");
                Console.ForegroundColor = ConsoleColor.White;
                return null;
            }

            // find the shortest route
            var rf = new RouteFinder(initialFourLetterWordList);
            var resultPath = rf.FindRoute(startWord, endWord);

            return resultPath;
        }

        public List<string> LoadFourLetterWords(string dictionaryFile)
        {
            var fourLetterWordList = new List<string>();

            using (var reader = new StreamReader(dictionaryFile))
            {
                try
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // get four letter words
                        if (line.Length == 4)
                        {
                            fourLetterWordList.Add(line.ToLower());
                            fourLetterWordList.Sort();
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unable to load dictionary file.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                finally
                {
                    reader.Close();
                }
            }

            return fourLetterWordList;
        }
    }
}
