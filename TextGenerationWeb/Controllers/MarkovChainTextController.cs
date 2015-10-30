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

        public ActionResult GenerateText(InputText inputTextType, string userInputText = "", int numberOfSentences = -1)
        {
            if (numberOfSentences == -1)
            {
                numberOfSentences = random.Next(1, 11);
            }

            Dictionary<string, List<string>> dict = null;
            switch (inputTextType)
            {
                case InputText.InDesertAndWilderness:
                    dict = PredefinedDictionaries.InDesertAndWilderness;
                    break;

                case InputText.LordOfTheRings:
                    dict = PredefinedDictionaries.Lotr;
                    break;

                case InputText.Other:
                    var dictGenerator = new DictionaryGenerator();
                    var d = dictGenerator.GenerateFrequencyTable(userInputText);
                    var g = new MarkovChainTextGenerator(d);
                    //return g.GetRandomChain(numberOfSentences);
                    return PartialView(new Chain(inputTextType, g.GetRandomChain(numberOfSentences)));
            }

            var generator = new MarkovChainTextGenerator(dict);

            //return generator.GetRandomChain(numberOfSentences);
            return PartialView(new Chain(inputTextType, generator.GetRandomChain(numberOfSentences)));
        }
    }
}