using System;

namespace TextFilterApp
{
    public class TextFilter
    {   
        public string FilterText(string text, char[] vowels, char[] filterLetters, int wordLength)
        {
            var textNoSpaces = text.Split(' ');
            var output = "";
            foreach (var word in textNoSpaces)
            {
                var sanitizedWord = TrimPunctuationStart(word, out string punctuationStart);
                sanitizedWord = TrimPunctuationEnd(sanitizedWord, out string punctuationEnd);
                output += $"{punctuationStart}{FilterCrieria(sanitizedWord, vowels, filterLetters, wordLength)}{punctuationEnd} ";
            }

            return output;
        }

        public string FilterCrieria(string sanitizedWord, char[] vowels, char[] filterLetters, int wordLength)
        {
            if (WordLengthFilter(sanitizedWord, wordLength))
            {
                return "";
            }

            if (WordFilterByCharacter(sanitizedWord, filterLetters))
            {
                return "";
            }

            if (WordMiddleLetterContainsFilterCharacter(sanitizedWord, vowels))
            {
                return "";
            }
            
            return sanitizedWord;
        }

        public string TrimPunctuationStart(string word, out string punctuationStart)
        {
            punctuationStart = "";
            foreach (char character in word)
            {
                if (char.IsPunctuation(character))
                {
                    punctuationStart += character;
                }
                else
                {
                    return word.Substring(punctuationStart.Length);
                }
            }

            return "";
        }

        public string TrimPunctuationEnd(string word, out string punctuationEnd)
        {
            punctuationEnd = "";
            for(int i = word.Length - 1; i>=0; i--)
            {
                var character = word[i];
                if (char.IsPunctuation(character))
                {
                    punctuationEnd += character;
                }
                else
                {
                    char[] punctuation = punctuationEnd.ToCharArray();
                    Array.Reverse(punctuation);
                    punctuationEnd = new string(punctuation);
                    return word.Substring(0, word.Length - punctuationEnd.Length);
                }
            }

            return "";
        }

        public bool WordLengthFilter(string word, int wordLength)
        {
            if (word.Length < wordLength)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool WordFilterByCharacter(string word, char[] filterChars)
        {
            return word.IndexOfAny(filterChars) >= 0;
        }

        public bool WordMiddleLetterContainsFilterCharacter(string word, char[] filterChars)
        {
            var middleChar = "";
            if (word.Length % 2 == 0)
            {
                middleChar = word.Substring((word.Length / 2)-1, 2);
            }
            else
            {
                middleChar = word.Substring(word.Length / 2, 1);
            }

            return middleChar.IndexOfAny(filterChars) >= 0;
        }
    }
}
