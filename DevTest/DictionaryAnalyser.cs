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
        public string[] Analyse(string dictionaryFile, string startWord, string endWord, string resultFile)
        {
            var resultPath = new string[]{};
            var rf = new RouteFinder();

            //get all the four letter words from the dictionary file
            var initialFourLetterWordList = this.loadFourLetterWords(dictionaryFile);

            //check words are in the dictionary
            if (!initialFourLetterWordList.Contains(startWord))
            {
                Console.WriteLine(startWord + " is not in the dictionary.");
            }

            if (!initialFourLetterWordList.Contains(endWord))
            {
                Console.WriteLine(endWord + " is not in the dictionary.");
            }

            resultPath = rf.FindRoute(initialFourLetterWordList, startWord, endWord);

            return resultPath;
        }

        public List<string> loadFourLetterWords(string dictionaryFile)
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
                    Console.WriteLine("Unable to load dictionary file.");
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
