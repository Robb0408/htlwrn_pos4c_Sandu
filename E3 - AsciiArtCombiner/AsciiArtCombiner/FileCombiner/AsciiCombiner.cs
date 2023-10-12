using System.Collections.Generic;

namespace FileCombiner
{
    public static class AsciiCombiner
    {
        /// <summary>
        /// Combines art by overlapping them (replacing whitespaces).
        /// </summary>
        /// <param name="filesContent">List containing parts of art.</param>
        /// <returns>Combined string.</returns>
        public static string Combine(List<string> filesContent)
        {
            
            char[] charsGround = filesContent[0].ToCharArray(); // Base for overlapping
            List<char> remaining = new List<char>(); // List for remaining chars if length differs from base

            for (int i = 1; i < filesContent.Count; i++)
            {
                char[] chars = filesContent[i].ToCharArray();
                for (int j = 0; j < chars.Length; j++)
                {
                    if (j < charsGround.Length && charsGround[j] == ' ')
                    {
                        charsGround[j] = chars[j];
                    }
                    else if (j >= charsGround.Length)
                    {
                        remaining.Add(chars[j]);
                    }
                }
            }
            return new string(charsGround) + new string(remaining.ToArray());
        }
    }
}