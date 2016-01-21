using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTest
{
    class RouteFinder
    {
        List<string> dictionaryWords = new List<string>();
        List<Stack<string>> visitedPaths = new List<Stack<string>>();
        Stack<string> currentPath = new Stack<string>();
        Stack<string> path = new Stack<string>();

        string currentWord;
        string nextWord;
        int steps = 0;
        int shortestPath = 10000;

        public string[] FindRoute(List<string> wordList, string startWord, string endWord)
        { 
            dictionaryWords = wordList;
            var finalPath = RecursiveWordSearch(startWord, endWord);
            var finalArray = new string[finalPath.Count];
            //add words to array and reverse
            finalPath.CopyTo(finalArray, 0);
            Array.Reverse(finalArray);
            return finalArray;
        }

        public Stack<string> RecursiveWordSearch(string startWord, string endWord)
        {
                currentWord = startWord;

                if (!currentPath.Contains(currentWord))
                {
                    currentPath.Push(currentWord);
                    steps++;
                }

                var nextWordsQueue = FindNextWords(currentWord);

                if (nextWordsQueue.Count > 0 && (currentPath.Count < shortestPath))
                {
                    if ((nextWordsQueue.Contains(endWord)))
                    {
                        currentPath.Push(endWord);
                        path = new Stack<string>(new Stack<string>(currentPath));
                        shortestPath = path.Count;
                    }
                    else if (currentPath.Count > 0)
                    {
                        nextWord = nextWordsQueue.Dequeue();
                        return RecursiveWordSearch(nextWord, endWord);
                    }
                }

                var visitedPath = new Stack<string>(new Stack<string>(currentPath));
                visitedPaths.Add(visitedPath);
                currentPath.Pop();
                steps--;

                if (currentPath.Count > 0)
                {
                    return RecursiveWordSearch(currentPath.Peek(), endWord);
                }
                else
                {
                    return path;
                }
           }

        public Queue<string> FindNextWords(string word)
        {
            var nextWordsQueue = new Queue<string>();

            for (int x = 0; x < word.Length; x++)
            {
                for (int y = 'a'; y <= 'z'; y++)
                {
                    string z = word.Substring(0, x) + (char)y + word.Substring(x + 1, (word.Length) - (x + 1));
                    if (WordExists(z) && !z.Equals(word) && !currentPath.Contains(z) && !NextPathAlreadyVisited(currentPath, z))
                    {
                        nextWordsQueue.Enqueue(z);
                    }
                }
            }
            
            return nextWordsQueue;
        }

        public bool WordExists(string word)
        {
            return dictionaryWords.Contains(word);
        }

        public bool NextPathAlreadyVisited(Stack<string> path, string potentialNextWord)
        {
            var newStack = new Stack<string>(new Stack<string>(currentPath));
            newStack.Push(potentialNextWord);

            var array1 = newStack.ToArray();

            foreach (Stack<string> element in visitedPaths)
            {
                var array2 = element.ToArray();
                if (array1.SequenceEqual(array2))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
