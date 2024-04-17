using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FileContentAnalyzer.Logic
{
    public class FileContentAnalyzerService : IFileContentAnalyzerService
    {
        public IDictionary<string, int> GetFrequency(string content)
        {
            var words = PrepareText(content);
            var frequency = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (frequency.ContainsKey(word))
                {
                    frequency[word]++;
                }
                else
                {
                    frequency.Add(word, 1);
                }
            }
            return frequency;
        }

        public IEnumerable<string> GetLongestWords(string content)
        {
            var words = PrepareText(content);
            var maxLength = words.Max(w => w.Length);
            return words.Where(w => w.Length == maxLength).ToList();
        }

        public int GetUniqueWordsCount(string content)
        {
            var words = PrepareText(content);
            return words.Distinct().Count();
        }

#pragma warning disable CA1822 // Member als statisch markieren
        private string[] PrepareText(string content)

        {
            return new string(content.Where(c => char.IsLetter(c) || char.IsWhiteSpace(c)).Select(char.ToLower).ToArray()).Split(" ");
        }

        public decimal GetSimilarity(string content1, string content2)
        {
            var wordCount1 = GetFrequency(content1);
            var wordCount2 = GetFrequency(content2);

            var intersection = wordCount1.Keys.Intersect(wordCount2.Keys);
            var dotProduct = 0;

            foreach (var word in intersection)
            {
                dotProduct += wordCount1[word] * wordCount2[word];

                var magnitude1 = GetMagnitude(wordCount1);
                var magnitude2 = GetMagnitude(wordCount2);

                if (magnitude1 == 0 || magnitude2 == 0)
                {
                    return 0;
                }

                var cosineSimilarity = dotProduct / (magnitude1 * magnitude2);
                return cosineSimilarity;
            }


            return 0;
        }

        private decimal GetMagnitude(IDictionary<string, int> wordCount)
        {
            var sum = 0;
            foreach (var item in wordCount)
            {
                sum += (int) Math.Pow(item.Value, 2);
            }
            return (decimal)Math.Sqrt(sum);
        }
    }
#pragma warning restore CA1822 // Member als statisch markieren
}
