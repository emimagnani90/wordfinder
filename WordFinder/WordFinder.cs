using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WordFinder
{
    public class WordFinder
    {
        /// <summary>
        /// Matrix with horizontal words
        /// </summary>
        private IEnumerable<string> _horizontalMatrix;

        /// <summary>
        /// Matrix with generated vertical words from matrix
        /// </summary>
        private IEnumerable<string> _verticalMatrix;

        /// <summary>
        /// Size of the matrix
        /// </summary>
        private int _matrixSize;

        /// <summary>
        /// Max length for words
        /// </summary>
        const int MaxSize = 64;

        public WordFinder(IEnumerable<string> matrix)
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

            this._horizontalMatrix = matrix;

            var vertical = new List<string>();
            // -- Create vertical words
            for (var i = 0; i < matrix.Count(); i++)
            {
                var sb = new StringBuilder();
                foreach (var word in matrix)
                {
                    sb.Append(word[i]);
                }
                vertical.Add(sb.ToString());
            }

            this._verticalMatrix = vertical;
            this._matrixSize = matrix.Count();
        }

        /// <summary>
        /// Find words in matrix
        /// </summary>
        /// <param name="wordstream"></param>
        /// <returns></returns>
        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            // -- If no words, return
            if (wordstream.Count() == 0)
            {
                return Enumerable.Empty<string>();
            }

            wordstream = wordstream.Distinct();

            // -- Results of words found
            Dictionary<string, int> matches = new Dictionary<string, int>();

            // -- Loop words
            foreach (var word in wordstream)
            {
                // -- Don't check the word if the size exceeds the limit
                if (word.Length > this._matrixSize)
                {
                    continue;
                }
                var horizontalCount = this.CountMatches(this._horizontalMatrix, word);
                var verticalCount = this.CountMatches(this._verticalMatrix, word);

                if (horizontalCount != 0 || verticalCount != 0)
                {
                    // -- Current value or 0
                    var currentValue = matches.ContainsKey(word) ? matches[word] : 0;
                    // -- Increase by 1
                    matches[word] = currentValue + horizontalCount + verticalCount;
                }
            }

            return matches.OrderByDescending(x => x.Value).Select(x => x.Key);
        }

        /// <summary>
        /// Count matches of a word in the matrix
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        private int CountMatches(IEnumerable<string> matrix, string word)
        {
            var count = 0;
            foreach (var currentWord in matrix)
            {
                var index = 0;

                while (index >= 0)
                {
                    index = currentWord.IndexOf(word, index);

                    if (index >= 0)
                    {
                        count++;

                        index++;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return count;
        }

    }
}
