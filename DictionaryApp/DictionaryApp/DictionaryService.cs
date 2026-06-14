using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DictionaryApp
{
    internal class DictionaryService
    {
        public string DictionaryType { get; set; }
        private List<DictionaryItem> words;
        public DictionaryService(string type)
        {
            DictionaryType = type;
            words = new List<DictionaryItem>();

        }
        public void Add(string word, string firstTranslation)
        {
            DictionaryItem foundWord = words.Find(w => w.Word.Equals(word, StringComparison.OrdinalIgnoreCase));
            if (foundWord == null)
            {
                DictionaryItem item = new DictionaryItem(word, firstTranslation);
                words.Add(item);
            }
            else
            {
                if (foundWord.Translations.Contains(firstTranslation) == false)
                {
                    foundWord.Translations.Add(firstTranslation);
                }
                else
                {
                    Console.WriteLine("Такой перевод уже сущетсвует");
                }
            }
        }
        public void FindAndShow(string word)
        {
            DictionaryItem foundWord = words.Find(w => w.Word.Equals(word, StringComparison.OrdinalIgnoreCase));
            if (foundWord != null)
            {
                Console.WriteLine($"Слово {word} найдено!");
                Console.Write($"Его переводы:");
                for (int i = 0; i < foundWord.Translations.Count; i++)
                {
                    Console.Write(foundWord.Translations[i]);
                    if (i != foundWord.Translations.Count - 1)
                    {
                        Console.Write(", ");
                    }
                }
                Console.WriteLine();

            }
            else
            {
                Console.WriteLine($"Слово \"{word}\" не найдено в словаре.");
            }
        }
        public void UpdateWord(string oldWord, string newWord)
        {
            DictionaryItem foundWord = words.Find(w => w.Word.Equals(oldWord, StringComparison.OrdinalIgnoreCase));
            if (foundWord != null)
            {
                foundWord.Word = newWord;
            }
            else
            {
                Console.WriteLine($"Слово {oldWord} не найдено");
            }
        }
        public void UpdateTranslation(string word, string oldTranslation, string newTranslation)
        {
            DictionaryItem foundWord = words.Find(w => w.Word.Equals(word, StringComparison.OrdinalIgnoreCase));
            if (foundWord != null)
            {
                int index = foundWord.Translations.IndexOf(oldTranslation);
                if (index != -1)
                {
                    foundWord.Translations[index] = newTranslation;
                }
                else
                {
                    Console.WriteLine($"Перевод слова {word} не найден");
                }
            }
            else
            {
                Console.WriteLine($"Слово {word} не найдено для изменения перевода");
            }
        }
        public void RemoveWord(string word)
        {
            DictionaryItem foundWord = words.Find(w => w.Word.Equals(word, StringComparison.OrdinalIgnoreCase));
            if (foundWord != null)
            {
                words.Remove(foundWord);
            }
            else
            {
                Console.WriteLine($"Слово {word} не найдено для удаления");
            }
        }
        public void RemoveConcreteTranslation(string word, string translation)
        {
            DictionaryItem foundWord = words.Find(w => w.Word.Equals(word, StringComparison.OrdinalIgnoreCase));
            if (foundWord != null)
            {
                int size = foundWord.Translations.Count;
                if(size != 1)
                {
                    if (foundWord.Translations.Remove(translation)) Console.WriteLine($"Перевод {translation} удален");
                    else Console.WriteLine($"Перевод {translation} не был найден и не может быть удален");
                }
                else
                {
                    Console.WriteLine("Удаление переводов запрещено, так как существует только один вариант перевода.");
                }
            }
            else
            {
                Console.WriteLine($"Слово {word} не найдено!");
            }
        }
        public void LoadFromFile(string filename)
        {
            words.Clear();
            if (File.Exists(filename))
            {
                using (StreamReader reader = new StreamReader(filename))
                {
                    string wordLine;

                    while ((wordLine = reader.ReadLine()) != null)
                    {
                        string translationsLine = reader.ReadLine();

                        string[] translationArray = translationsLine.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                        DictionaryItem word = new DictionaryItem(wordLine, translationArray[0]);
                    for (int i = 1; i < translationArray.Length; i++)
                        {
                            word.Translations.Add(translationArray[i]);
                        }
                        words.Add(word);
                    }
                }
                Console.WriteLine("Словарь успешно загружен");
            }
            else
            {
                Console.WriteLine($"Файл под именем {filename} не найден");
                return;
            }
        }
        public void ExportWordToFile(string word, string resultFilename)
        {
            DictionaryItem foundWord = words.Find(w => w.Word.Equals(word, StringComparison.OrdinalIgnoreCase));
            if (foundWord != null)
            {
                using (StreamWriter writer = new StreamWriter(resultFilename))
                {
                    string allTranslations = string.Join(", ", foundWord.Translations);
                    writer.WriteLine($"{word} - {allTranslations}");
                }
                Console.WriteLine($"Слово {word} успешно экспортировано в файл {resultFilename}");
            }
            else
            {
                Console.WriteLine($"Слово {word} не найдено, экспорт невозможен!");
            }

        }
        public void SaveToFile(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (DictionaryItem word in words)
                {
                    writer.WriteLine(word.Word);
                    writer.WriteLine(string.Join("|", word.Translations));
                }
            }
        }
    }
}
