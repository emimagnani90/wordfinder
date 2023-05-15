using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFinder
{
    public class WordFinderAlt
    {
        /// <summary>
        /// Matrix of words
        /// </summary>
        private IEnumerable<string> _matrix;

        /// <summary>
        /// Size of the matrix
        /// </summary>
        private int _matrixSize;

        /// <summary>
        /// Max length for words
        /// </summary>
        const int MaxSize = 64;

        public WordFinderAlt(IEnumerable<string> matrix)
        {
            // -- Validate matrix size
            if (matrix.Any(x => x.Length != matrix.Count()))
            {
                throw new Exception("Every word must have a legth equal to the number of words");
            }

            // -- Validate words size
            if (matrix.Any(x => x.Length > MaxSize) || matrix.Count() > MaxSize)
            {
                throw new Exception($"Max number or words and word's size is {MaxSize}");
            }

            this._matrix = matrix;
            this._matrixSize = matrix.Count();
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            // -- If no words, return
            if (wordstream.Count() == 0)
            {
                return new List<string>();
            }

            wordstream = wordstream.Distinct();

            // -- Results of words found
            Dictionary<string, int> matches = new Dictionary<string, int>();

            // -- Loop words
            foreach (var word in wordstream)
            {
                // -- Don't check the word if the size exceeds the limit
                if(word.Length > this._matrixSize)
                {
                    continue;
                }
                var wordIndex = 0;
                // -- Loop words in matrix
                foreach (var matrixWord in this._matrix)
                {
                    var charIndex = 0;

                    foreach (var character in matrixWord)
                    {
                        // -- Found first character
                        if (character == word[0])
                        {
                            // -- Don't check horizontal match
                            if (charIndex + word.Length <= matrixWord.Length)
                            {
                                // -- Set starting index at the next char because first char was already checked
                                var matrixWordCharIndex = charIndex + 1;
                                // -- Check if full word is contained
                                for (var c = 1; c < word.Length; c++)
                                {
                                    // -- If characters don't match, stop.
                                    if (word[c] != matrixWord[matrixWordCharIndex])
                                    {
                                        break;
                                    }

                                    // -- Word found!
                                    if (c == word.Length - 1)
                                    {
                                        // -- Current value or 0
                                        var currentValue = matches.ContainsKey(word) ? matches[word] : 0;
                                        // -- Increase by 1
                                        matches[word] = currentValue + 1;

                                        break;
                                    }
                                    // -- Increase the index
                                    matrixWordCharIndex++;
                                }
                            }

                            // -- Don't check vertical match
                            if (wordIndex + word.Length <= this._matrix.Count())
                            {
                                // -- Check if full word is contained
                                for (var c = 1; c < word.Length; c++)
                                {
                                    var nextWord = this._matrix.ElementAt(wordIndex + c);

                                    // -- If characters don't match, stop.
                                    if (word[c] != nextWord[charIndex])
                                    {
                                        break;
                                    }

                                    // -- Word found!
                                    if (c == word.Length - 1)
                                    {
                                        // -- Current value or 0
                                        var currentValue = matches.ContainsKey(word) ? matches[word] : 0;
                                        // -- Increase by 1
                                        matches[word] = currentValue + 1;

                                        break;
                                    }
                                }
                            }


                        }

                        charIndex++;
                    }

                    wordIndex++;
                }
            }

            return matches.OrderByDescending(x => x.Value).Select(x => x.Key);
        }
    }
}
