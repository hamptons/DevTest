using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DevTest
{
    public class RouteFinder
    {
        List<string> dictionaryWords = new List<string>();
        Dictionary<string, string> visitedWordsDictionary = new Dictionary<string, string>();
        Queue<string> q = new Queue<string>();
        string currentWord;
        string root;
        bool firstWord = true;
        string p = "";

        public string[] FindRoute(List<string> wordList, string startWord, string endWord)
        { 
            dictionaryWords = wordList;
            var finalPath = RecursiveWordSearch("", startWord, endWord);
            try
            {
                var finalArray = new string[finalPath.Count];
                //add words to array and return
                finalPath.CopyTo(finalArray, 0);
                return finalArray;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("");
                var result = string.Format("There is no valid route from {0} to {1}.", startWord, endWord);
                Console.WriteLine(result);
                return null;
            }
            
        }

        public List<string> RecursiveWordSearch(string parentWord, string startWord, string endWord)
        {
           
            var parent = parentWord;
            currentWord = startWord;
            
            if (firstWord)
            {
                root = currentWord;
                visitedWordsDictionary.Add(currentWord, parent);
                q.Enqueue(currentWord);
                firstWord = false;
            }

            if (currentWord == endWord)
            {
                return calculatePathFromDictionary();
            }

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
        

        public List<string> FindNextWords(string word)
        {
            var nextWordsList = new List<string>();

            for (int x = 0; x < word.Length; x++)
            {
                for (int y = 'a'; y <= 'z'; y++)
                {
                    string z = word.Substring(0, x) + (char)y + word.Substring(x + 1, (word.Length) - (x + 1));
                    if (WordExists(z) && (!z.Equals(word)) && (!visitedWordsDictionary.ContainsKey(z)) && (!q.Contains(z)) )
                    {
                        nextWordsList.Add(z);
                    }
                }
            }

            return nextWordsList;
        }

        public bool WordExists(string word)
        {
            return dictionaryWords.Contains(word);
        }


        public string GetClosestWord(List<string> nextWords, string endWord)
        {
            var dictionary = new Dictionary<string, int>();

            foreach (string s in nextWords)
            {
                var distance = CalculateHammingDistance(s, endWord);
                dictionary.Add(s, distance);
            }

            List<KeyValuePair<string, int>> sortedDictionary = (from kv in dictionary orderby kv.Value select kv).ToList();

            var nextWord = sortedDictionary.First().Key;

            return nextWord;
        }

        public int CalculateHammingDistance(string w1, string w2)
        {
            var distance = 0;

            for (int i = 0; i < 4; i++)
            {
                var difference = ((int)w1[i] - (int)w2[i]);                

                if (difference < 0)
                {
                    difference = difference * -1;
                }
                
                distance += difference;
            }

                return distance;
        }

        public List<string> calculatePathFromDictionary()
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
