// HAU: ℹ️ remove not used using statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// HAU: ⚠️ wrong namespace - typo AnagramTool.Logic.Services
// HAU: ℹ️ move the service interface to AnagramTool.Logic.Contracts namespace to separate it from the implementation namespace
// HAU: ℹ️ use single line namespace declaration to reduce code nesting
namespace AnagramTool.Logic.Service
{
    public interface IAnagramDictionaryService
    {
        /// <summary>
        /// Reads a file containing words and their anagrams
        /// </summary>
        /// <returns></returns>
        Task ReadAnagramDictionaryAsync();

        /// <summary>
        /// Returns the dictionary
        /// </summary>
        /// <returns></returns>
        List<string> GetAnagramDictionary();
    }
}
