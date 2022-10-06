using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static void FindAndAddBigrams(
            Dictionary<string, Dictionary<string, int>> frequencyDictionary, 
            List<string> sentence)
        {
            for (int i = 0; i <= sentence.Count - 2; i++)
            {
                if (frequencyDictionary.ContainsKey(sentence[i]))
                {
                    if (frequencyDictionary[sentence[i]].ContainsKey(sentence[i + 1]))
                        frequencyDictionary[sentence[i]][sentence[i + 1]]++;
                    else frequencyDictionary[sentence[i]][sentence[i + 1]] = 1;
                }
                else
                {
                    frequencyDictionary[sentence[i]] = new Dictionary<string, int> { [sentence[i + 1]] = 1 };
                }
            }    
        }

        public static void FindAndAddTrigrams(
            Dictionary<string, Dictionary<string, int>> frequencyDictionary,
            List<string> sentence)
        {
            for (int i = 0; i <= sentence.Count - 3; i++)
            {
                if (frequencyDictionary.ContainsKey(sentence[i] + " " + sentence[i + 1]))
                {
                    if (frequencyDictionary[sentence[i] + " " + sentence[i + 1]].ContainsKey(sentence[i + 2]))
                        frequencyDictionary[sentence[i] + " " + sentence[i + 1]][sentence[i + 2]]++;
                    else frequencyDictionary[sentence[i] + " " + sentence[i + 1]][sentence[i + 2]] = 1;
                }
                else
                {
                    frequencyDictionary[sentence[i] + " " + sentence[i + 1]] = new Dictionary<string, int>();
                    frequencyDictionary[sentence[i] + " " + sentence[i + 1]][sentence[i + 2]] = 1;
                }
            }
        }

        public static void SortFrequencyDictionary(
            Dictionary<string, Dictionary<string, int>> frequencyDictionary,
            Dictionary<string, string> result)
        {
            int maxFrequency = 0;
            var nGramBegin = new StringBuilder();
            string nGramEnd = "";
            foreach (var firstWords in frequencyDictionary)
            {
                nGramBegin.Append(firstWords.Key);
                foreach (var endWord in frequencyDictionary[firstWords.Key])
                {
                    if (endWord.Value > maxFrequency)
                    {
                        maxFrequency = endWord.Value;
                        nGramEnd = endWord.Key;
                    }
                    else if (endWord.Value == maxFrequency)
                    {
                        if (string.CompareOrdinal(endWord.Key, nGramEnd) < 0) nGramEnd = endWord.Key;
                    }
                }
                result[nGramBegin.ToString()] = nGramEnd;
                maxFrequency = 0;
                nGramBegin.Clear();
                nGramEnd = "";
            }
        }

        public static Dictionary<string, Dictionary<string, int>> BuildFrequencyDictionary(List<List<string>> text)
        {
            var frequencyDictionary = new Dictionary<string, Dictionary<string, int>>();

            foreach (var sentence in text)
            {
                if (sentence.Count < 2) continue;
                FindAndAddBigrams(frequencyDictionary, sentence);
                if (sentence.Count >= 3) FindAndAddTrigrams(frequencyDictionary, sentence);
            }

            return frequencyDictionary;
        }

        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();

            var frequencyDictionary = BuildFrequencyDictionary(text);

            SortFrequencyDictionary(frequencyDictionary, result);

            return result;
        }
    }
}