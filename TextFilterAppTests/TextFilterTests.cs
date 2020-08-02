using System;
using Shouldly;
using TextFilterApp;
using Xunit;

namespace TextFilterAppTests
{
    public class TextFilterTests
    {
        [Theory()]
        [InlineData("clean", new char[] { 'a', 'e', 'i', 'o', 'u' }, true)]
        [InlineData("what", new char[] { 'a', 'e', 'i', 'o', 'u' }, true)]
        [InlineData("currently", new char[] { 'a', 'e', 'i', 'o', 'u' }, true)]
        [InlineData("the", new char[] { 'a', 'e', 'i', 'o', 'u' }, false)]
        [InlineData("rather", new char[] { 'a', 'e', 'i', 'o', 'u' }, false)]
        [InlineData("book,\'", new char[] { 'a', 'e', 'i', 'o', 'u' }, true)]
        public void FilterMiddleContainsLetters(string word, char[] filterCharacters, bool expectedOutput)
        {
            TextFilter textFilter = new TextFilter();
            var output = textFilter.WordMiddleLetterContainsFilterCharacter(word,filterCharacters);
            output.ShouldBe(expectedOutput);
        }

        [Theory()]
        [InlineData("too", new char[] { 't' }, true)]
        [InlineData("what", new char[] { 'a', 't'}, true)]
        [InlineData("hello", new char[] { 'a', 't'}, false)]
        public void FilterContainsLetters(string word, char[] filterCharacters, bool expectedOutput)
        {
            TextFilter textFilter = new TextFilter();
            var output = textFilter.WordFilterByCharacter(word, filterCharacters);
            output.ShouldBe(expectedOutput);
        }


        [Theory()]
        [InlineData("clean", 3, false)]
        [InlineData("to", 3, true)]
        [InlineData("too", 3, false)]
        public void FilterLength(string word, int length, bool expectedOutput)
        {
            TextFilter textFilter = new TextFilter();
            var output = textFilter.WordLengthFilter(word, length);
            output.ShouldBe(expectedOutput);
        }


        [Theory()]
        [InlineData("book,\'", "book,\'", "")]
        [InlineData("--book", "book", "--")]
        [InlineData("book--", "book--", "")]
        [InlineData("--book--", "book--", "--")]
        [InlineData("??book--", "book--", "??")]
        public void PunctuationStartTest(string word, string expectedOutput, string expectedPunctuation)
        {
            TextFilter textFilter = new TextFilter();
            var output = textFilter.TrimPunctuationStart(word, out string puncuationStart);
            output.ShouldBe(expectedOutput);
            puncuationStart.ShouldBe(expectedPunctuation);
        }

        [Theory()]
        [InlineData("book,\'", "book", ",\'")]
        [InlineData("--book", "--book", "")]
        [InlineData("book--", "book", "--")]
        [InlineData("--book--", "--book", "--")]
        public void PunctuationEndTest(string word, string expectedOutput, string expectedPunctuation)
        {
            TextFilter textFilter = new TextFilter();
            var output = textFilter.TrimPunctuationEnd(word, out string puncuationEnd);
            output.ShouldBe(expectedOutput);
            puncuationEnd.ShouldBe(expectedPunctuation);
        }
    }
}
