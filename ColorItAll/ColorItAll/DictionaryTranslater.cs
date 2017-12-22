using System;
using System.Collections.Generic;
using System.Text;

namespace ColorItAll
{
    public static class DictionaryTranslater
    {
        public static Dictionary<string, int> GameMode = new Dictionary<string, int>
        {
            {"PlayEasy", 3},
            {"PlayNormal", 4},
            {"PlayHard", 5}
        };
    }
}
