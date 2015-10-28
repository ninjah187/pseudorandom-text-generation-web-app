using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovChain
{
    /// <summary>
    /// Handles saving and loading FrequencyTables as *.dict files.
    /// </summary>
    public class DictionaryFileManager
    {
        public void Save(string path, Dictionary<string, List<string>> dict)
        {
            using (var stream = new FileStream(path, FileMode.Create))
            {
                using (var streamWriter = new StreamWriter(stream))
                {
                    foreach (var pair in dict)
                    {
                        streamWriter.WriteLine("word=" + pair.Key);
                        foreach (var d in pair.Value)
                        {
                            streamWriter.WriteLine("desc=" + d);
                        }
                        streamWriter.WriteLine();
                    }
                }
            }
        }

        public Dictionary<string, List<string>> Load(string path)
        {
            var dict = new Dictionary<string, List<string>>();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    string currentWord = "";
                    while (streamReader.EndOfStream == false)
                    {
                        string line = streamReader.ReadLine();

                        if (line == "")
                        {
                            continue;
                        }

                        string[] split = line.Split('=');

                        switch (split[0])
                        {
                            case "word":
                                dict.Add(split[1], new List<string>());
                                currentWord = split[1];
                                break;
                            case "desc":
                                dict[currentWord].Add(split[1]);
                                break;
                        }
                    }
                }
            }
            return dict;
        }

    }
}
