using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovChain
{
    /// <summary>
    /// Generates frequency tables from text.
    /// </summary>
    public class DictionaryGenerator
    {
        private static readonly char[] TrimChars = { '.', ',', '(', ')', ':' };
        private static readonly char[] Separators = { ' ', '\n' };

        public Dictionary<string, List<string>> GenerateFrequencyTable(string txt)
        {
            string[] split = txt.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < split.Length; i++)
            {
                split[i] = split[i].Trim();
                //split[i] = split[i].Trim(TrimChars);
                //split[i] = split[i].ToLower();
            }

            var words = split.ToList();
            var toDelete = new List<string>();
            foreach (var w in words)
            {
                if (w == "")
                {
                    toDelete.Add(w);
                }
            }
            toDelete.ForEach(i => words.Remove(i));

            var frequencyTable = new Dictionary<string, List<string>>();

            while (words.Count != 0)
            {
                var word = words.First();

                if (frequencyTable.Keys.Contains(word) == false)
                {
                    frequencyTable.Add(word, GetDescendants(words, word));
                }

                words.Remove(word);
            }

            return frequencyTable;
        }

        public Dictionary<string, List<string>> GenerateFrequencyTableFromFile(string path)
        {
            string txt = File.ReadAllText(path, Encoding.Default);

            return GenerateFrequencyTable(txt);
        }

        private List<string> GetDescendants(List<string> words, string target)
        {
            var descendants = new List<string>();
            for (int i = 0; i < words.Count - 1; i++)
            {
                if (words[i] == target)
                {
                    descendants.Add(words[i + 1]);
                }
            }
            return descendants;
        }
    }
}
