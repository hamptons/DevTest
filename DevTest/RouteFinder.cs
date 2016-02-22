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
           // var root = "";
            
            // if this is the first time the method has been invoked we need to set the currentWord to be the 
            // root and add this to the queue
            if (firstWord)
            {
                //root = currentWord;
                visitedWordsDictionary.Add(currentWord, parent);
                q.Enqueue(currentWord);
                dictionaryWords.Remove(currentWord);
                firstWord = false;
            }

            // return the endword once found
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

                //string value;
                //if (visitedWordsDictionary.TryGetValue(q.Peek(), out value))
                //{
                //    parent = value;
                //}

                //return WordSearch(parent, q.Peek(), endWord, dictionaryWords);
                currentWord = q.Peek();
            }
                       

            return this.CalculatePathFromDictionary(currentWord, visitedWordsDictionary);
        }

        public List<string> FindNextWords(string currentWord, HashSet<string> dictionaryWords, Dictionary<string, string> visitedWordsDictionary, Queue<string> q)
        {
            var nextWordsList = new List<string>();

            foreach (string word in dictionaryWords)
            {
                if (!word.Equals(currentWord) /*&& !visitedWordsDictionary.ContainsKey(word) && !q.Contains(word)*/)
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

                if (!(word1.Substring(i, 1)).Equals(word2.Substring(i, 1)))
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

        //public List<string> FindNextWords(string currentWord, List<string> dictionaryWords, Dictionary<string, string> visitedWordsDictionary, Queue<string> q)
        //{
        //    var nextWordsList = new List<string>();

        //    for (var wordIndex = 0; wordIndex < currentWord.Length; wordIndex++)
        //    {
        //        for (var letter = 'a'; letter <= 'z'; letter++)
        //        {
        //            var nextWord = currentWord.Substring(0, wordIndex) + (char)letter + currentWord.Substring(wordIndex + 1, (currentWord.Length) - (wordIndex + 1));
        //            if (dictionaryWords.Contains(nextWord) && (!nextWord.Equals(currentWord)) && (!visitedWordsDictionary.ContainsKey(nextWord)) && (!q.Contains(nextWord)))
        //            {
        //                nextWordsList.Add(nextWord);
        //            }
        //        }
        //    }

        //    return nextWordsList;
        //}

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
