using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MarkovChain;
using TextGenerationWeb.Models;

namespace TextGenerationWeb.Controllers
{
    public class MarkovChainTextController : Controller
    {
        private static readonly Random random = new Random();

        public string GenerateText(InputText inputTextType, string userInputText = "", int numberOfSentences = -1)
        {
            var dictFileManager = new DictionaryFileManager();
            string path = "";

            if (numberOfSentences == -1)
            {
                numberOfSentences = random.Next(1, 11);
            }

            switch (inputTextType)
            {
                case InputText.InDesertAndWilderness:
                    path = @"D:\Pisane\C#\PseudorandomTextGeneration\TextGenerationWeb\Dictionaries\puszcza.dict";
                    break;

                case InputText.LordOfTheRings:
                    path = @"D:\Pisane\C#\PseudorandomTextGeneration\TextGenerationWeb\Dictionaries\lotr_full.dict";
                    break;

                case InputText.Other:
                    var dictGenerator = new DictionaryGenerator();
                    var d = dictGenerator.GenerateFrequencyTable(userInputText);
                    var g = new MarkovChainTextGenerator(d);
                    return g.GetRandomChain(numberOfSentences);
            }

            var dict = dictFileManager.Load(path);

            var generator = new MarkovChainTextGenerator(dict);

            return generator.GetRandomChain(numberOfSentences);
        }

        //public string GenerateText(string userInputText)
        //{
        //    var dictGenerator = new DictionaryGenerator();
        //    var dict = 
        //}
    }
}