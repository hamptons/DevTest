using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevTest;
using Xbehave;
using Xunit;

namespace Tests.Unit
{
    public class DictionaryAnalyserFeature
    {
        [Scenario]
        public void loadFourLetterWords(string dictionaryFile, DictionaryAnalyser da, List<string> result)
        {
            da = new DictionaryAnalyser();
            var expected = new List<string> { "/tst", "test", "test" };

            "Given a list of words of varying length and case."
                .x(() => dictionaryFile = @"testWords.txt");

            "When I extract the non four letter words from the list"
                .x(() => result = da.loadFourLetterWords(dictionaryFile));

            "Then the resulting list contains '/tst', 'test', 'test'."
                .x(() => Assert.Equal(expected, result));
        }

    }
}
