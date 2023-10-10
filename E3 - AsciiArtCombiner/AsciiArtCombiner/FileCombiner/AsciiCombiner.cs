using System.Collections.Generic;

namespace FileCombiner
{
    public static class AsciiCombiner
    {
        /// <summary>
        /// Combines art by overlapping them (replacing whitespaces).
        /// </summary>
        /// <param name="lines">List containing parts of art.</param>
        /// <returns>Combined string.</returns>
        public static string Combine(List<string> lines)
        {
            // Base for overlapping
            char[] charsGround = lines[0].ToCharArray();

            for (int i = 1; i < lines.Count; i++)
            {
                char[] chars = lines[i].ToCharArray();
                for (int j = 0; j < charsGround.Length; j++)
                {
                    if (charsGround[j] == ' ')
                    {
                        charsGround[j] = chars[j];
                    }
                }
            }
            return new string(charsGround);
        }
    }
}