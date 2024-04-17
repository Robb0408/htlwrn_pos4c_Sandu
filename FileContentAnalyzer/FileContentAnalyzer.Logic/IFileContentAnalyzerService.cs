using System.Collections.Generic;

namespace FileContentAnalyzer.Logic
{
    public interface IFileContentAnalyzerService
    {
        IDictionary<string, int> GetFrequency(string content);
        int GetUniqueWordsCount(string content);
        IEnumerable<string> GetLongestWords(string content);
        decimal GetSimilarity(string content1, string content2);
    }
}
