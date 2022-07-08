using System;
using System.Collections.Generic;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            var frequencyDict = new Dictionary<string, Dictionary<string, int>>();           

            foreach (var sentence in text)
            {
                if(sentence.Count < 2)
                {
                    continue;
                }
                if(sentence.Count < 3)
                {
                    BuildBigram(frequencyDict, sentence);                    
                }
                else
                {
                    BuildBigram(frequencyDict, sentence);
                    BuildTrigram(frequencyDict, sentence);                    
                }              
            }
            CheckFrequency(frequencyDict, result);
            return result;
        }

        public static void BuildBigram(Dictionary<string, Dictionary<string, int>> frequency , List<string> words)
        {
            for (int i = 0; i < words.Count-1; i++)
            {
                if(frequency.ContainsKey(words[i]))
                {
                    var inDict = frequency[words[i]];

                    if (inDict.ContainsKey(words[i + 1]))
                        inDict[words[i + 1]] += 1;
                    else
                        frequency[words[i]].Add(words[i + 1], 1);
                    continue;
                }                                           
                frequency.Add(words[i], new Dictionary<string, int>() { { words[i + 1] , 1 } });
            }
        }
       
        public static void BuildTrigram(Dictionary<string, Dictionary<string, int>> frequency, List<string> words)
        {
            for (int i = 0; i < words.Count-2; i++)
            {
                string[] values = new string[] { words[i], words[i + 1] };
                var str = string.Join(" ", values);

                if (frequency.ContainsKey(str))
                {
                    var inDict = frequency[str];

                    if (inDict.ContainsKey(words[i + 2]))
                        inDict[words[i + 2]] += 1;
                    else
                        frequency[str].Add(words[i + 2], 1);
                    continue;
                }                                                       
                frequency.Add(str, new Dictionary<string, int>() { { words[i + 2], 1 } });

            }
        }

        public static void CheckFrequency(Dictionary<string, Dictionary<string, int>> frequency, Dictionary<string, string> result)
        {                                          
            foreach(var item in frequency)
            {
                if (result.ContainsKey(item.Key))
                    continue;
                
                var inDict = item.Value;
                string frequencyKey = null;
                
                var max = 0;
                foreach (var keyValuePair in inDict)
                {                    
                    if (keyValuePair.Value > max)
                    {
                        max = keyValuePair.Value;
                        frequencyKey = keyValuePair.Key;
                    }
                    else if(keyValuePair.Value == max)
                    {
                        var res = String.CompareOrdinal(frequencyKey, keyValuePair.Key);
                        if(res > 0)
                            frequencyKey = keyValuePair.Key;
                    }
                }
                result.Add(item.Key, frequencyKey);                                               
            }
        }
    }
}