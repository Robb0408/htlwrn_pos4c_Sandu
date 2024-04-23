using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnagramTool.Logic.Service;

namespace AnagramTool.Logic.Services.Implementations
{
    public class AnagramDictionaryService : IAnagramDictionaryService
    {
        public List<string> Anagrams { get; set; } = new List<string>();

        /// <inheritdoc/>
        public List<string> GetAnagramDictionary()
        {
            return Anagrams;
        }

        /// <inheritdoc/>
        public async Task ReadAnagramDictionaryAsync()
        {
            var fileContent = await File.ReadAllLinesAsync("../AnagramTool.Logic/dictionary.txt");

            foreach (var line in fileContent)
            {
                Anagrams.Add(line);
            }
        }
    }
}
