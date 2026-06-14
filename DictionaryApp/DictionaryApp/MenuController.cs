using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DictionaryApp
{
    internal class MenuController
    {
        private DictionaryService currentDictionary;
        public void MainMenu ()
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("=== ГЛАВНОЕ МЕНЮ ===");
                Console.WriteLine("1. Создать новый словарь");
                Console.WriteLine("2. Открыть существующий словарь");
                Console.WriteLine("3. Выйти из программы");
                Console.Write("\nВыберите пункт меню: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        {
                            Console.WriteLine("Введите тип словаря, например, англо-русский");
                            string type = Console.ReadLine();

                            Console.WriteLine("Введите имя файла");
                            string nameFile = Console.ReadLine();

                            currentDictionary = new DictionaryService(type);
                            currentDictionary.SaveToFile(nameFile);
                            DictionaryMenu(nameFile);
                            break;
                        }
                    case "2":
                        {
                            Console.WriteLine("Введите имя файла");
                            string nameFile = Console.ReadLine();
                            currentDictionary = new DictionaryService("");
                            currentDictionary.LoadFromFile(nameFile);
                            DictionaryMenu(nameFile);
                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("Выходим из программы...");
                            return;
                        }
                    default:
                        {
                            Console.WriteLine("Введено неверное значение");
                            break;
                        }
                }
                
            }
        }
        public void DictionaryMenu(string fileName)
        {
            bool inDictionary = true;
            while(inDictionary)
            {
                Console.Clear();
                Console.WriteLine($"Работа со словарем: {currentDictionary.DictionaryType}");
                Console.WriteLine($"1. Найти перевод слова");
                Console.WriteLine($"2. Добавить слово / перевод");
                Console.WriteLine($"3. Заменить слово");
                Console.WriteLine($"4. Заменить перевод");
                Console.WriteLine($"5. Удалить слово");
                Console.WriteLine($"6. Удалить перевод");
                Console.WriteLine($"7. Экспортировать слово в файл");
                Console.WriteLine($"8. Назад в главное меню");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        {
                            Console.WriteLine("Введите слово для поиска:");
                            string word = Console.ReadLine();
                            currentDictionary.FindAndShow(word);
                            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                            Console.ReadKey();
                            break;
                        }
                    case "2":
                        {
                            Console.WriteLine("Введите слово: ");
                            string word = Console.ReadLine();

                            Console.WriteLine("Введите перевод: ");
                            string translation = Console.ReadLine();

                            currentDictionary.Add(word, translation);
                            currentDictionary.SaveToFile(fileName);
                            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                            Console.ReadKey();
                            break;
                        }
                    case "3":
                        {
                            Console.WriteLine("Введите старое слово: ");
                            string oldWord = Console.ReadLine();

                            Console.WriteLine("Введите новое слово: ");
                            string newWord = Console.ReadLine();
                            currentDictionary.UpdateWord(oldWord, newWord);
                            currentDictionary.SaveToFile(fileName);
                            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                            Console.ReadKey();
                            break;
                        }
                    case "4":
                        {
                            Console.WriteLine("Введите слово для поиска: ");
                            string word = Console.ReadLine();

                            Console.WriteLine("Введите старый перевод: ");
                            string oldTranslation = Console.ReadLine();

                            Console.WriteLine("Введите новый перевод: ");
                            string newTranslation = Console.ReadLine();

                            currentDictionary.UpdateTranslation(word, oldTranslation, newTranslation);
                            currentDictionary.SaveToFile(fileName);
                            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                            Console.ReadKey();
                            break;
                        }
                    case "5":
                        {
                            Console.WriteLine("Введите слово для удаления: ");
                            string word = Console.ReadLine();

                            currentDictionary.RemoveWord(word);
                            currentDictionary.SaveToFile(fileName);
                            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                            Console.ReadKey();
                            break;
                        }
                    case "6":
                        {
                            Console.WriteLine("Введите слово для удаления перевода: ");
                            string word = Console.ReadLine();

                            Console.WriteLine("Введите перевод для удаления: ");
                            string translation = Console.ReadLine();

                            currentDictionary.RemoveConcreteTranslation(word, translation);
                            currentDictionary.SaveToFile(fileName);
                            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                            Console.ReadKey();
                            break;
                        }
                    case "7":
                        {
                            Console.WriteLine("Введите слово для экспорта: ");
                            string word = Console.ReadLine();

                            Console.WriteLine("Введите имя файла для экспорта: ");
                            string resultFileName = Console.ReadLine();

                            currentDictionary.ExportWordToFile(word, resultFileName);
                            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                            Console.ReadKey();

                            break;
                        }
                    case "8":
                        {
                            Console.WriteLine("Возвращение в главное меню");
                            inDictionary = false;
                            break;
                        }
                }
            }
        }
    }
}
