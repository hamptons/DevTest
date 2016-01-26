namespace DevTest
{
    using System;
    using System.Collections.Generic;

    public class RouteFinder
    {
        private readonly List<string> dictionaryWords;
        private Dictionary<string, string> visitedWordsDictionary = new Dictionary<string, string>();
        private Queue<string> q = new Queue<string>();
        private string currentWord;
        private string root;
        private bool firstWord = true;
        

        // create a new instance of routefinder and load the list of words
        public RouteFinder(List<string> dictionaryWords)
        {
            this.dictionaryWords = dictionaryWords;
        }

        public string[] FindRoute(string startWord, string endWord)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Calculating shortest route...");
            var finalPath = this.RecursiveWordSearch(string.Empty, startWord, endWord);

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

        public List<string> RecursiveWordSearch(string parentWord, string startWord, string endWord)
        {
            // set currentWord and its parent
            var parent = parentWord;
            this.currentWord = startWord;
            
            
            // if this is the first time the method has been invoked we need to set the currentWord to be the 
            // root and add this to the queue
            if (this.firstWord)
            {
                this.root = this.currentWord;
                this.visitedWordsDictionary.Add(this.currentWord, parent);
                this.q.Enqueue(this.currentWord);
                this.firstWord = false;
            }

            // return the endword once found
            if (this.currentWord == endWord)
            {
                return this.CalculatePathFromDictionary();
            }

            // keep searching while there are still words in the queue and we haven't already found the endWord
            if (this.q.Count > 0)
            {
                var nextWordsList = this.FindNextWords(this.currentWord);
                for (var i = 0; i < nextWordsList.Count; i++)
                {
                    this.visitedWordsDictionary.Add(nextWordsList[i], this.currentWord);
                    this.q.Enqueue(nextWordsList[i]);
                }
            }

            this.q.Dequeue();

            if (this.q.Count < 1)
            {
                return null;
            }

            string value;
            if (this.visitedWordsDictionary.TryGetValue(this.q.Peek(), out value))
            {
                parent = value;
            }

            return this.RecursiveWordSearch(parent, this.q.Peek(), endWord);
        }  
        
        public List<string> FindNextWords(string currentWord)
        {
            var nextWordsList = new List<string>();

            for (var wordIndex = 0; wordIndex < currentWord.Length; wordIndex++)
            {
                for (var letter = 'a'; letter <= 'z'; letter++)
                {
                    var nextWord = currentWord.Substring(0, wordIndex) + (char)letter + currentWord.Substring(wordIndex + 1, (currentWord.Length) - (wordIndex + 1));
                    if (this.dictionaryWords.Contains(nextWord) && (!nextWord.Equals(currentWord)) && (!this.visitedWordsDictionary.ContainsKey(nextWord)) && (!this.q.Contains(nextWord)))
                    {
                        nextWordsList.Add(nextWord);
                    }
                }
            }

            return nextWordsList;
        }

        public List<string> CalculatePathFromDictionary()
        {
            var path = new List<string>();
            var word = this.currentWord;

            while (!string.IsNullOrEmpty(word))
            {
                path.Add(word);
                string value;
                if (this.visitedWordsDictionary.TryGetValue(word, out value))
                {
                    word = value;
                }
            }

            path.Reverse();
            return path;
        }
    }
}
