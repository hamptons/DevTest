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
    public class RouteFinderFeature
    {
        [Scenario]
        public void FindNextWords(string dictionaryFile, string word)
        { 
            var expected = new string[] {"purl", "burl", "bury", "jury"};
            var rf = new RouteFinder();
            List<string> result;

            "Given a dictionary file"
                .x(() => dictionaryFile = @"words-english.txt");

            "And a word of 'purl'"
                .x(() => word = "purl");

            "When I find the words with one character different"
                .x(() => result = rf.FindNextWords(word));

            //todo
        }
    }
}
