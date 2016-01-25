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
        public void FindNextWords(RouteFinder rf, string word, List<string> result)
        { 
            var expected = new List<string> {"burl", "curl", "pure"};

            "Given a dictionary file"
                .x(() => rf = new RouteFinder(new List<string> { "purl", "curl", "burl", "burp", "curb", "pure", "jure", "fury", "jury", "bury" }));

            "And the word 'purl'"
                .x(() => word = "purl");

            "When I find the words in the dictionary with one character different"
                .x(() => result = rf.FindNextWords(word));

            "Then the result should be burl, curl, pure"
                .x(() => Assert.Equal(expected, result));
        }
    }
}
