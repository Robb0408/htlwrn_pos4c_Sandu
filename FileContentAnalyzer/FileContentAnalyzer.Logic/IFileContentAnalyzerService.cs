using System.Collections.Generic;

namespace FileContentAnalyzer.Logic
{
    public interface IFileContentAnalyzerService
    {
        /// <summary>
        /// Counts the frequency of every word in a text
        /// </summary>
        /// <param name="content">Text to analyze</param>
        /// <returns>Words an their count</returns>
        IDictionary<string, int> GetFrequency(string content);

        /// <summary>
        /// Counts the amount of unique words in a text
        /// </summary>
        /// <param name="content">Text to analyze</param>
        /// <returns>Unique words count</returns>
        int GetUniqueWordsCount(string content);

        /// <summary>
        /// Finds the longest words in a text
        /// </summary>
        /// <param name="content">Text to analyze</param>
        /// <returns>List of longest words</returns>
        IEnumerable<string> GetLongestWords(string content);

        /// <summary>
        /// Calculates the similarity between two texts
        /// </summary>
        /// <param name="content1">First text to analyze</param>
        /// <param name="content2">Second text to analyze</param>
        /// <returns>Similarity score</returns>
        double GetSimilarity(string content1, string content2);
    }
}
