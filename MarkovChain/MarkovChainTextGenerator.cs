using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovChain
{
    public class MarkovChainTextGenerator
    {
        private static readonly Random random = new Random();

        public int NumberOfSentences { get; private set; }
        public int CurrentSentenceNumber { get; private set; }

        public Dictionary<string, List<string>> FrequencyTable { get; private set; }

        public MarkovChainTextGenerator(Dictionary<string, List<string>> frequencyTable)
        {
            FrequencyTable = frequencyTable;
            Reset();
        }

        public void Reset()
        {
            NumberOfSentences = random.Next(1, 11);
            CurrentSentenceNumber = 0;
        }

        public string GetRandomChain()
        {
            Reset();

            string chain = "";

            var index = random.Next(0, FrequencyTable.Keys.Count);
            var word = FrequencyTable.ElementAt(index).Key;

            // ConstructChain(word, ref chain); recursive call
            chain = ConstructChain(word);
            
            return chain;
        }

        // iteration
        private string ConstructChain(string word)
        {
            string chain = "";
            while (true)
            {
                var wordDescendants = FrequencyTable[word];

                if (wordDescendants.Count == 0)
                {
                    chain += ". ";
                    return chain;
                }
                else
                {
                    int index = random.Next(0, wordDescendants.Count);
                    var desc = wordDescendants[index];

                    if (chain.Length == 0)
                    {
                        chain += desc;
                    }
                    else
                    {
                        chain += " " + desc;
                    }

                    //word = desc;
                    if (chain.Last() != '.')
                    {
                        word = desc;
                    }
                    else
                    {
                        return chain;
                    }
                }
            }
        }

        // bug: sometimes occurs StackOverflowException
        // bug: check exit conditions of this function, add some upper limit of generated sentences
        //// recurrency
        private void ConstructChain(string word, ref string chain)
        {
            var wordDescendants = FrequencyTable[word];

            //Console.WriteLine(word);
            //Console.WriteLine("Descendants: ");
            //foreach (var w in wordDescendants)
            //{
            //    Console.WriteLine(" - " + w);
            //}

            if (wordDescendants.Count == 0)
            {
                chain += ". ";
                return;
            }
            else
            {
                int index = random.Next(0, wordDescendants.Count);
                var desc = wordDescendants[index];

                if (chain.Length == 0)
                {
                    chain += desc;
                }
                else
                {
                    chain += " " + desc;
                }

                ConstructChain(desc, ref chain);
                //if (chain.Last() != '.')
                //{
                //    ConstructChain(desc, ref chain);
                //}
            }
        }
    }
}
