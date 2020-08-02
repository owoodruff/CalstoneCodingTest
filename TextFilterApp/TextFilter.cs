using System;

namespace TextFilterApp
{
    public class TextFilter
    {
        private char[] vowels =  new char[]{'a','e','i','o','u'};
        public string FilterText(string text)
        {
            var textNoSpaces = text.Split(' ');
            var output = "";
            foreach (var word in textNoSpaces)
            {
                var sanitizedWord = TrimPunctuationStart(word, out string puncuationStart);
                sanitizedWord = TrimPunctuationEnd(sanitizedWord, out string puncuationEnd);

                output += $"{puncuationStart}{FilterCrieria(sanitizedWord)}{puncuationEnd} ";
            }

            return output;
        }

        private string FilterCrieria(string sanitizedWord)
        {
            if (WordLengthFilter(sanitizedWord, 3))
            {
                return "";
            }

            if (WordFilterByCharacter(sanitizedWord, new char[] {'t'}))
            {
                return "";
            }

            if (WordMiddleLetterContainsFilterCharacter(sanitizedWord, vowels))
            {
                return "";
            }
            
            return $"{sanitizedWord}";
        }

        public string TrimPunctuationStart(string word, out string puncuationStart)
        {
            puncuationStart = "";
            foreach (char character in word)
            {
                if (char.IsPunctuation(character))
                {
                    puncuationStart += character;
                }
                else
                {
                    return word.Substring(puncuationStart.Length);
                }
            }

            return "";
        }

        public string TrimPunctuationEnd(string word, out string puncuationEnd)
        {
            puncuationEnd = "";
            for(int i = word.Length - 1; i>=0; i--)
            {
                var character = word[i];
                if (char.IsPunctuation(character))
                {
                    puncuationEnd += character;
                }
                else
                {
                    char[] puncuation = puncuationEnd.ToCharArray();
                    Array.Reverse(puncuation);
                    puncuationEnd = new string(puncuation);
                    return word.Substring(0, word.Length - puncuationEnd.Length);
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
