using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MarkovChain
{
//    According to Wikipedia, a Markov Chain is a random process where the next state is dependent on the previous state. This is a little difficult to understand, so I'll try to explain it better:

//    What you're looking at, seems to be a program that generates a text-based Markov Chain. Essentially the algorithm for that is as follows:

//    Split a body of text into tokens (words, punctuation).
//    Build a frequency table. This is a data structure where for every word in your body of text, you have an entry (key). This key is mapped to another data structure that is basically a list of all the words that follow this word (the key) along with its frequency.
//    Generate the Markov Chain. To do this, you select a starting point (a key from your frequency table) and then you randomly select another state to go to (the next word). The next word you choose, is dependent on its frequency (so some words are more probable than others). After that, you use this new word as the key and start over.


    class Program
    {
        static void Main(string[] args)
        {
            var dictGenerator = new DictionaryGenerator();
            var dictManager = new DictionaryFileManager();

            //var dict = new DictionaryFileManager().Load("lotr.dict");
            //Console.WriteLine(new MarkovChainTextGenerator(dict).GetRandomChain());
            
            //var dict = dictGenerator.GenerateFrequencyTableFromFile("puszcza.txt");

            //dictManager.Save("puszcza.dict", dict);
            var dict = dictManager.Load("puszcza.dict");

            //dictManager.Save("lotr_full.dict", dict);
            //var dict = dictManager.Load("lotr_full.dict");

            //var markovGenerator = new MarkovChainTextGenerator(dict);
            //string txt = markovGenerator.GetRandomChain();
            //Console.WriteLine(txt);

            //using (var stream = File.Create("out.txt"))
            //{
            //    using (var streamWriter = new StreamWriter(stream))
            //    {
            //        streamWriter.Write(txt);
            //    }
            //}

            var markovGenerator = new MarkovChainTextGenerator(dict);

            while (true)
            {
                Console.Clear();
                Console.WriteLine(markovGenerator.GetRandomChain());
                Console.ReadKey();
            }

            Console.ReadKey();
        }
    }
}
