namespace DevTest
{
    using System;
    using System.Collections.Generic;

    public class RouteFinder
    {    

        public string[] FindRoute(string startWord, string endWord, HashSet<string> wordList)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Calculating shortest route...");

            var finalPath = this.WordSearch(startWord, endWord, wordList);

            // add words to array and return
            try
            {
                var finalArray = new string[finalPath.Count];
                finalPath.CopyTo(finalArray, 0);
                return finalArray;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(string.Empty);
                Console.ForegroundColor = ConsoleColor.Red;
                var result = string.Format("There is no valid route from {0} to {1}.", startWord, endWord);
                Console.WriteLine(result);
                Console.ForegroundColor = ConsoleColor.White;
                return null;
            }
            
        }

        public List<string> WordSearch(string startWord, string endWord, HashSet<string> wordList)
        {
            // set currentWord and its parent
            var parent = "";
            var currentWord = startWord;
            var q = new Queue<string>();
            var firstWord = true;
            var visitedWordsDictionary = new Dictionary<string, string>();
            var dictionaryWords = wordList;
            
            // if this is the first time the method has been invoked we need to add the current word to the queue
            if (firstWord)
            {
                visitedWordsDictionary.Add(currentWord, parent);
                q.Enqueue(currentWord);
                dictionaryWords.Remove(currentWord);
                firstWord = false;
            }

            // loop until the currentWord matches the endWord
            while (currentWord != endWord)
            {
                // keep searching while there are still words in the queue and we haven't already found the endWord
                if (q.Count > 0)
                {
                    var nextWordsList = FindNextWords(currentWord, dictionaryWords, visitedWordsDictionary, q);
                    for (var i = 0; i < nextWordsList.Count; i++)
                    {
                        var nextWord = nextWordsList[i];
                        visitedWordsDictionary.Add(nextWord, currentWord);
                        q.Enqueue(nextWord);
                        dictionaryWords.Remove(nextWord);
                    }
                }

                q.Dequeue();

                if (q.Count < 1)
                {
                    return null;
                }
                
                currentWord = q.Peek();
            }
                       
            // calculate the route from the endWord to the startWord
            return this.CalculatePathFromDictionary(currentWord, visitedWordsDictionary);
        }

        public List<string> FindNextWords(string currentWord, HashSet<string> dictionaryWords, Dictionary<string, string> visitedWordsDictionary, Queue<string> q)
        {
            var nextWordsList = new List<string>();

            // check each word in the dictionaryList to see if it is one char different to the current word
            foreach (string word in dictionaryWords)
            {
                if (!word.Equals(currentWord))
                {
                    var neighbouringWord = this.HasOneCharacterDifferent(currentWord, word);

                    if (neighbouringWord)
                    {
                        nextWordsList.Add(word);
                    }
                }
            }

            return nextWordsList;
        }

        public bool HasOneCharacterDifferent(string word1, string word2)
        {
            var count = 0;

            for (int i = 0; i < 4; i++)
            {
                if (count > 1)
                {
                    return false;
                }

                if (!(word1.ToLower().Substring(i, 1)).Equals(word2.ToLower().Substring(i, 1)))
                {
                    count++;
                }
            }

            if (count == 1)
            {
                return true;
            }

            return false;
        }

        public List<string> CalculatePathFromDictionary(string currentWord, Dictionary<string, string> visitedWordsDictionary)
        {
            var path = new List<string>();
            var word = currentWord;

            while (!string.IsNullOrEmpty(word))
            {
                path.Add(word);
                string value;
                if (visitedWordsDictionary.TryGetValue(word, out value))
                {
                    word = value;
                }
            }

            path.Reverse();
            return path;
        }
    }
}
