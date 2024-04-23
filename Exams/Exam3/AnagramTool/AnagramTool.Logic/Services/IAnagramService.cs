// HAU: ⚠️ wrong namespace - typo AnagramTool.Logic.Services
// HAU: ℹ️ use single line namespace declaration to reduce code nesting
// HAU: ℹ️ move the service interface to AnagramTool.Logic.Contracts namespace to separate it from the implementation namespace
namespace AnagramTool.Logic.Service
{
    public interface IAnagramService
    {
        /// <summary>
        /// Checks a word is an anagram of another word
        /// </summary>
        /// <param name="word1"></param>
        /// <param name="word2"></param>
        /// <returns></returns>
        bool CheckAnagram(string word1, string word2);

        /// <summary>
        /// Finds all anagrams that are possible with a word
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> FindAnagramsAsync(string word);
    }
}