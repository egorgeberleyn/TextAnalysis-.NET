using System;
using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();

            if (text == null) return null; //проверка на наличие текста
            text = text.ToLower(); //каст в ловеркейс

            var sentences = text.Split(".:;?!()".ToCharArray(),//парсеный на предложения текст
                StringSplitOptions.RemoveEmptyEntries);
            foreach (var sentence in sentences)
            {
                var words = new List<string>();
                var word = new StringBuilder();
                foreach(var symbol in sentence)
                {
                    if (char.IsLetter(symbol) || symbol == '\'') //проверка на соответствие символа букве или апострофу
                        word.Append(symbol); //добавление символа в слово
                    else
                        AddWord(word, words);
                }
                AddWord(word, words);
                if (words.Count > 0)
                    sentencesList.Add(words);
            }
            
            return sentencesList;
        }

        public static void AddWord(StringBuilder word, List<string> words) //доп метод, который добавляет слово в список
        {
            if(word.Length > 0)
            {
                words.Add(word.ToString());
                word.Clear();
            }
        }
    }
}