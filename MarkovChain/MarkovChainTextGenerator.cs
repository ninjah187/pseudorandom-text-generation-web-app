using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovChain
{
    /// <summary>
    /// Generates pseudorandom Markov chain text based on delivered Frequency Table.
    /// Because of web apps security generated sentences amount limit is 50.
    /// Change it by editing value of SentencesLimit variable.
    /// </summary>
    public class MarkovChainTextGenerator
    {
        private static readonly Random random = new Random();

        public int NumberOfSentences { get; set; }
        public int CurrentSentenceNumber { get; private set; }
        public int SentencesLimit { get; set; } // for web apps security

        public Dictionary<string, List<string>> FrequencyTable { get; private set; }

        public MarkovChainTextGenerator(Dictionary<string, List<string>> frequencyTable)
        {
            SentencesLimit = 50;

            FrequencyTable = frequencyTable;
            Reset();
        }

        public void Reset()
        {
            NumberOfSentences = random.Next(1, 11);
            CurrentSentenceNumber = 0;
        }

        // bug: it crashes with dictionary containing words "halo halo halo."
        public string GetRandomChain()
        {
            return GetRandomChain(random.Next(1, 11));
        }

        public string GetRandomChain(int length)
        {
            if (FrequencyTable.Count == 0)
            {
                return "";
            }

            NumberOfSentences = length;
            CurrentSentenceNumber = 0;

            string chain = "";

            var index = random.Next(0, FrequencyTable.Keys.Count);
            var word = FrequencyTable.ElementAt(index).Key;

            // ConstructChain(word, ref chain); recursive call
            chain = ConstructChain(word);

            return chain;
        }

        public string GetRandomSentence()
        {
            return GetRandomChain(1);
        }

        // iteration
        private string ConstructChain(string word)
        {
            string chain = "";

            // indicates whether chain is at beginning of new sentence
            // so first word could be upper cased
            bool beginningOfSentence = true;

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
                        //if (beginningOfSentence)
                        //{
                        //    if (Char.IsLetter(desc[0]) || Char.IsNumber(desc[0]))
                        //    {
                        //        desc = desc.ToUpper();
                        //        beginningOfSentence = false;
                        //    }
                        //}
                        chain += " " + desc;
                    }

                    word = desc;
                    if (chain.Last() == '.')
                    {
                        CurrentSentenceNumber++;
                        //beginningOfSentence = true;
                    }
                    if (CurrentSentenceNumber >= NumberOfSentences || CurrentSentenceNumber > SentencesLimit)
                    {
                        return chain;
                    }
                    //if (chain.Last() != '.' && CurrentSentenceNumber < NumberOfSentences)
                    //{
                    //    word = desc;
                    //}
                    //else
                    //{
                    //    CurrentSentenceNumber++;
                    //}
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
