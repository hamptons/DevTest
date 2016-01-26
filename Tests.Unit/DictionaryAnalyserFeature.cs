namespace Tests.Unit
{
    using System.Collections.Generic;

    using DevTest;

    using Xbehave;

    using Xunit;

    /// <summary>The dictionary analyser feature.</summary>
    public class DictionaryAnalyserFeature
    {
        /// <summary>The load four letter words.</summary>
        /// <param name="dictionaryFile">The dictionary file.</param>
        /// <param name="da">The da.</param>
        /// <param name="result">The result.</param>
        [Scenario]
        public void LoadFourLetterWords(string dictionaryFile, DictionaryAnalyser da, List<string> result)
        {
            da = new DictionaryAnalyser();
            var expected = new List<string> { "/tst", "test", "test" };

            "Given a list of words of varying length and case."
                .x(() => dictionaryFile = @"testWords.txt");

            "When I extract the non four letter words from the list"
                .x(() => result = da.LoadFourLetterWords(dictionaryFile));

            "Then the resulting list contains '/tst', 'test', 'test'."
                .x(() => Assert.Equal(expected, result));
        }
    }
}
