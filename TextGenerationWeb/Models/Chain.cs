using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TextGenerationWeb.Models
{
    public class Chain
    {
        public InputText InputTextType { get; set; }
        public string Text { get; set; }
        public List<string> Sentences { get; set; }

        public Chain(InputText inputTextType, string text)
        {
            InputTextType = inputTextType;
            Text = text;
            
            Sentences = new List<string>();
            string[] separators = { "." };
            string[] split = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < split.Length; i++)
            {
                split[i] += ".";
                Sentences.Add(split[i]);
            }
        }
    }
}