using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTest
{
    public class DictionaryAnalyser
    {
        /// <summary>
        /// Sorts the supplied dictionary file and then requests the shortest path between 
        /// two given words from an object of the routefinder class. Passes the end result 
        /// back to the requesting object.
        /// </summary>
        /// <param name="dictionaryFile"></param>
        /// <param name="startWord"></param>
        /// <param name="endWord"></param>
        /// <param name="resultFile"></param>
        /// <returns>A string array containing the words which make up the shortest path.</returns>
        public string[] Analyse(string dictionaryFile, string startWord, string endWord, string resultFile)
        {
            var resultPath = new string[]{};
            
            //get all the four letter words from the dictionary file
            var initialFourLetterWordList = this.LoadFourLetterWords(dictionaryFile);

            //check the supplied words are in the dictionary
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

            //find the shortest route
            var rf = new RouteFinder(initialFourLetterWordList);
            resultPath = rf.FindRoute(startWord, endWord);

            return resultPath;
        }

        /// <summary>
        /// Loads all of the four letter words in the dictionary file into a list.
        /// </summary>
        /// <param name="dictionaryFile"></param>
        /// <returns>A list containing all of the </returns>
        public List<string> LoadFourLetterWords(string dictionaryFile)
        {
            var fourLetterWordList = new List<string>();

            using (StreamReader reader = new StreamReader(dictionaryFile))
            {
                try
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        //get four letter words
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
