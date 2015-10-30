using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MarkovChain;

namespace TextGenerationWeb
{
    public static class PredefinedDictionaries
    {
        public static Dictionary<string, List<string>> InDesertAndWilderness { get; private set; }
        public static Dictionary<string, List<string>> Lotr { get; private set; }

        static PredefinedDictionaries()
        {
            var dictFileManager = new DictionaryFileManager();

            InDesertAndWilderness =
                dictFileManager.Load(
                    @"D:\Pisane\C#\PseudorandomTextGeneration\TextGenerationWeb\Dictionaries\puszcza.dict");

            Lotr =
                dictFileManager.Load(
                    @"D:\Pisane\C#\PseudorandomTextGeneration\TextGenerationWeb\Dictionaries\lotr_full.dict");
        }
    }
}