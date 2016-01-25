using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DevTest
{
    public class RouteFinder
    {
        private readonly List<string> dictionaryWords;
        private Dictionary<string, string> visitedWordsDictionary = new Dictionary<string, string>();
        private Queue<string> q = new Queue<string>();
        private string currentWord;
        private string root;
        private bool firstWord = true;
        private string p = "";

        //create a new instance of routefinder and load the list of words
        public RouteFinder(List<string> dictionaryWords)
        {
            this.dictionaryWords = dictionaryWords;
        }

        /// <summary>
        /// Finds the quickest route between the given words, changing one character at a time.
        /// </summary>
        /// <param name="startWord"></param>
        /// <param name="endWord"></param>
        /// <returns>A string array containing the words which make up the path between the supplied words</returns>
        public string[] FindRoute(string startWord, string endWord)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Calculating shortest route...");
            var finalPath = RecursiveWordSearch("", startWord, endWord);

            //add words to array and return
            try
            {
                var finalArray = new string[finalPath.Count];
                finalPath.CopyTo(finalArray, 0);
                return finalArray;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Red;
                var result = string.Format("There is no valid route from {0} to {1}.", startWord, endWord);
                Console.WriteLine(result);
                Console.ForegroundColor = ConsoleColor.White;
                return null;
            }
            
        }

        /// <summary>
        /// Performs a breadth-first search for the supplied endWord from the startWord.
        /// </summary>
        /// <param name="parentWord"></param>
        /// <param name="startWord"></param>
        /// <param name="endWord"></param>
        /// <returns>A list containing the words which make up the path between the supplied words</returns>
        public List<string> RecursiveWordSearch(string parentWord, string startWord, string endWord)
        {
            //set currentWord and its parent
            var parent = parentWord;
            currentWord = startWord;
            
            //if this is the first time the method has been invoked we need to set the currentWord to be the 
            //root and add this to the queue
            if (firstWord)
            {
                root = currentWord;
                visitedWordsDictionary.Add(currentWord, parent);
                q.Enqueue(currentWord);
                firstWord = false;
            }

            //return the endword once found
            if (currentWord == endWord)
            {
                return CalculatePathFromDictionary();
            }

            //keep searching while there are still words in the queue and we haven't already found the endWord
            if (q.Count > 0)
            {
                var nextWordsList = FindNextWords(currentWord);
                for (int i = 0; i < nextWordsList.Count; i++)
                {
                    visitedWordsDictionary.Add(nextWordsList[i], currentWord);
                    q.Enqueue(nextWordsList[i]);
                }
            }

            q.Dequeue();

            if (q.Count < 1)
            {
                return null;
            }
            else
            {
                string value;
                if (visitedWordsDictionary.TryGetValue(q.Peek(), out value))
                {
                    p = value;
                }

                return RecursiveWordSearch(p, q.Peek(), endWord);
            }
        }  
        
        /// <summary>
        /// Finds all the words in dictionaryWords which are one character different to the supplied word.
        /// </summary>
        /// <param name="word"></param>
        /// <returns>A list of valid words one character away from the supplied word</returns>
        public List<string> FindNextWords(string word)
        {
            var nextWordsList = new List<string>();

            for (int x = 0; x < word.Length; x++)
            {
                for (int y = 'a'; y <= 'z'; y++)
                {
                    string z = word.Substring(0, x) + (char)y + word.Substring(x + 1, (word.Length) - (x + 1));
                    if (dictionaryWords.Contains(z) && (!z.Equals(word)) && (!visitedWordsDictionary.ContainsKey(z)) && (!q.Contains(z)))
                    {
                        nextWordsList.Add(z);
                    }
                }
            }

            return nextWordsList;
        }

        /// <summary>
        /// Calculates the route from the endWord to the startWord from a dictionary of 
        /// all of the visited words and their parents.
        /// </summary>
        /// <returns>A list containing the words which make up the path between the supplied words</returns>
        public List<string> CalculatePathFromDictionary()
        {
            var path = new List<string>();
            var w = currentWord;
            string value;

            while (!string.IsNullOrEmpty(w))
            {
                path.Add(w);
                if (visitedWordsDictionary.TryGetValue(w, out value))
                {
                    w = value;
                }
            }
            path.Reverse();
            return path;
        }
    }
}
