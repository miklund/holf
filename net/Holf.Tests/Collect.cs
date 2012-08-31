using System;
using System.Collections.Generic;
using System.Linq;

namespace Holf.Tests
{
    using System.IO;
    using System.Net;
    using System.Text.RegularExpressions;

    using Xunit;

    public class Collect
    {
        [Fact]
        public void ShouldMergeArrays()
        {
            // assert
            var data = new[] { new[] { 1, 2 }, new[] { 3 }, new[] { 4, 5 } };

            // act
            var result = data.Collect(a => a);

            // assert
            Assert.Equal(new[] { 1, 2, 3, 4, 5 }, result);
        }

        private IEnumerable<string> BookTitlesFromUrl(string url)
        {
            var expression = new Regex(@"<li><i><a href=""(.*?)"" title=""(.*?)"">(?<title>.*?)</a></i>");

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.0.6) Gecko/20060728 Firefox/1.5";
            request.Accept = "*/*";
            using (var response = request.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var html = reader.ReadToEnd();
                return expression.Matches(html)
                    .Cast<Match>()
                    .Map(match => match.Groups["title"].Value);
            }
        }

        // fetch all fantasy novel titles from wikipedia
        [Fact]
        public void ShouldFetchFantasyNovelTitlesFromWikipedia()
        {
            // arrange
            var urls = new[]
            {
                "http://en.wikipedia.org/wiki/List_of_fantasy_novels_(A-H)",
                "http://en.wikipedia.org/wiki/List_of_fantasy_novels_(I-R)",
                "http://en.wikipedia.org/wiki/List_of_fantasy_novels_(S-Z)"
            };

            // act
            var titles = urls.Collect(BookTitlesFromUrl);
            
            // assert
            Assert.True(titles.Contains("Good Omens")); // A-H
            Assert.True(titles.Contains("Quidditch Through The Ages")); // I-R
            Assert.True(titles.Contains("Song of Ice and Fire")); // S-Z
        }
    }
}
