using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(Dictionary<string, string> nextWords, string phraseBeginning, int wordsCount)
        { 
            var listResult = new List<string>();
            var words = phraseBeginning.Split(new char[] { ' ' });

            listResult.AddRange(words);
            if (listResult.Count < 1)
            {
                return listResult.ToString();
            }
            
            for (int count = 0; count < wordsCount; count++)                        
            {                                
                if (listResult.Count >= 2 && nextWords.ContainsKey(listResult[listResult.Count - 2] + " " + listResult[listResult.Count - 1]))
                {                                      
                    listResult.Add(nextWords[listResult[listResult.Count - 2] + " " + listResult[listResult.Count - 1]]);                    
                }
                
                else if (listResult.Count >= 1 && nextWords.ContainsKey(listResult[listResult.Count - 1]))
                {
                    listResult.Add(nextWords[listResult[listResult.Count - 1]]);
                }
                
            }
            string phraseResult = string.Join(" ", listResult);
            return phraseResult;
        }
    } 
}