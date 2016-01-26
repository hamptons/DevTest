namespace Tests.Unit
{
    using System.Collections.Generic;

    using DevTest;

    using Xbehave;

    using Xunit;

    /// <summary>The route finder feature.</summary>
    public class RouteFinderFeature
    {
        /// <summary>The find next words.</summary>
        /// <param name="rf">The rf.</param>
        /// <param name="word">The word.</param>
        /// <param name="result">The result.</param>
        [Scenario]
        public void FindNextWords(RouteFinder rf, string word, List<string> result)
        {
            var expected = new List<string> { "burl", "curl", "pure" };

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
