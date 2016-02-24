namespace DevTest
{
    using System;
    using System.IO;
    using System.Diagnostics;

    class Program
    {

        static void Main(string[] args)
        {
            var cont = true;
            Stopwatch sw = new Stopwatch();

            while (cont)
            {
                Console.Clear();
                Console.WriteLine("*** Word Test ***");
                Console.WriteLine(string.Empty);

                // Set the startWord
                Console.WriteLine("Enter word 1:");
                string startWord = Console.ReadLine();

                // we don't want to continue unless entered word is 4 characters
                while (startWord.Length != 4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter a 4 letter word.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Enter word 1:");
                    startWord = Console.ReadLine();
                }

                // Set the endWord
                Console.WriteLine("Enter word 2:");
                var endWord = Console.ReadLine();

                while (endWord.Length != 4)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter a 4 letter word.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Enter word 2:");
                    endWord = Console.ReadLine();
                }

                sw.Start();

                Console.WriteLine(string.Empty);
                Console.WriteLine(startWord + " ---> " + endWord);
                Console.WriteLine(string.Empty);
                Console.WriteLine("Analysing dictionary file...");
                Console.WriteLine(string.Empty);

                // Set the dictionary file
                const string DictionaryFile = @"words-english.txt";

                // Set the result file
                const string ResultFile = @"ResultFile.txt";

                var da = new DictionaryAnalyser();

                // get the path
                var result = da.Analyse(DictionaryFile, startWord, endWord, ResultFile);

                sw.Stop();

                if (result != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(string.Empty);
                    Console.WriteLine("The shortest route is " + (result.Length - 1) + " steps.");

                    using (var writer = new StreamWriter(ResultFile))
                    {
                        try
                        {
                            for (var i = 0; i < result.Length; i++)
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
                    Console.WriteLine(string.Empty);

                    TimeSpan ts = sw.Elapsed;
                    Console.WriteLine($"Elapsed time = {ts.Seconds}.{ts.Milliseconds} seconds");
                    Console.WriteLine(string.Empty);
                    Console.WriteLine("See " + ResultFile + " for results.");
                }

                Console.WriteLine(string.Empty);
                Console.WriteLine("Press 'q' to quit or enter to restart");
                var input = Console.ReadKey();

                // break while loop if user enters 'q' or clear window and continue
                if (input.KeyChar == 'q')
                {
                    cont = false;
                }
            }
        }
    }
}
