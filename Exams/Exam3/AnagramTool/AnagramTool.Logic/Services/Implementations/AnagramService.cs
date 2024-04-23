using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnagramTool.Logic.Service;

namespace AnagramTool.Logic.Services.Implementations
{
    public class AnagramService : IAnagramService
    {
        // HAU: ℹ️ use readonly keyword to prevent reassignment
        private IAnagramDictionaryService dictionaryService;

        public AnagramService(IAnagramDictionaryService dictionaryService)
        {
            this.dictionaryService = dictionaryService;
        }

        /// <inheritdoc/>
        public bool CheckAnagram(string word1, string word2)
        {
            var lettersWord1 = GetLetters(word1);
            var lettersWord2 = GetLetters(word2);

            if (lettersWord1.Count != lettersWord2.Count)
            {
                return false;
            }

            foreach (var letter in lettersWord1)
            {
                if (lettersWord2.ContainsKey(letter.Key) && lettersWord2.Values.Contains(letter.Value))
                {
                    continue;
                }
                return false;
            }
            return true;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<string>> FindAnagramsAsync(string word)
        {
            await dictionaryService.ReadAnagramDictionaryAsync();
            var dictionary = dictionaryService.GetAnagramDictionary();
            var foundWords = new List<string>();

            foreach (var pair in dictionary)
            {
                var split = pair.Split(" = ");
                if (CheckAnagram(word, split[0]) && split[0] != word)
                {
                    foundWords.Add(split[0]);
                }
                else if (CheckAnagram(word, split[1]) && split[1] != word)
                {
                    foundWords.Add(split[1]);
                }
            }
            return foundWords.Distinct();
        }

        /// <summary>
        /// Stores every letter and its count in a word into a dictionary
        /// </summary>
        /// <param name="word">Word to process</param>
        /// <returns>Char as key, count as value</returns>
        private IDictionary<char, int> GetLetters(string word)
        {
            var letters = new Dictionary<char, int>();
            foreach (var c in word)
            {
                if (letters.ContainsKey(c))
                {
                    letters[c]++;
                }
                else
                {
                    letters.Add(c, 1);
                }
            }
            return letters;
        }
    }
}
