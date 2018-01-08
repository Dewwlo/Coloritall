using System;
using System.Collections.Generic;
using System.Text;

namespace ColorItAll
{
    public static class DictionaryTranslater
    {
        public static Dictionary<string, int> GameMode = new Dictionary<string, int>
        {
            {"Easy", 3},
            {"Normal", 5},
            {"Hard", 7}
        };
    }
}
