using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApp
{
    internal class DictionaryItem
    {
        public string Word { get; set; }
        public List<string> Translations { get; set; }
        public DictionaryItem(string word,string firstTranslation) 
        {
            Word = word;
            Translations = new List<string>();
            Translations.Add(firstTranslation);
        }
    }
}



