using System;
using System.Collections.Generic;

namespace FileCombiner
{
    public static class AsciiCombiner
    {
        public static string Combine(List<string> lines)
        {
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