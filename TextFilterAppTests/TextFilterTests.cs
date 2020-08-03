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
            var x = "test";
            output.ShouldBe(expectedOutput);
        }


        [Theory()]
        [InlineData("too", new char[] { 'a', 'e', 'i', 'o', 'u' }, new char[] { 't' }, 3, "")]
        [InlineData("what", new char[] { 'a', 'e', 'i', 'o', 'u' }, new char[] { 't' }, 3, "")]
        [InlineData("hello", new char[] { 'a', 'e', 'i', 'o', 'u' }, new char[] { 't' }, 3, "hello")]
        [InlineData("rather", new char[] { 'a', 'e', 'i', 'o', 'u' }, new char[] { 't' }, 3, "")]
        [InlineData("currently", new char[] { 'a', 'e', 'i', 'o', 'u' }, new char[] { 't' }, 3, "")]
        [InlineData("to", new char[] { 'a', 'e', 'i', 'o', 'u' }, new char[] { 't' }, 3, "")]
        [InlineData("pp", new char[] { 'a', 'e', 'i', 'o', 'u' }, new char[] { 't' }, 3, "")]
        [InlineData("sss", new char[] { 'a', 'e', 'i', 'o', 'u' }, new char[] { 't' }, 3, "sss")]
        
        public void FilterCriteria(string word, char[] vowels, char[] filterCharacters, int wordLength, string expectedOutput)
        {
            TextFilter textFilter = new TextFilter();
            var output = textFilter.FilterCrieria(word, vowels, filterCharacters, wordLength);
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
            var output = textFilter.TrimPunctuationStart(word, out string punctuationStart);
            output.ShouldBe(expectedOutput);
            punctuationStart.ShouldBe(expectedPunctuation);
        }

        [Theory()]
        [InlineData("book,\'", "book", ",\'")]
        [InlineData("--book", "--book", "")]
        [InlineData("book--", "book", "--")]
        [InlineData("--book--", "--book", "--")]
        public void PunctuationEndTest(string word, string expectedOutput, string expectedPunctuation)
        {
            TextFilter textFilter = new TextFilter();
            var output = textFilter.TrimPunctuationEnd(word, out string punctuationEnd);
            output.ShouldBe(expectedOutput);
            punctuationEnd.ShouldBe(expectedPunctuation);
        }
    }
}
