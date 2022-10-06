using System.Collections.Generic;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            string[] beginningWords = phraseBeginning.Split(' ');
            var phrase = new List<string>();
            foreach (var beginningWord in beginningWords)
                phrase.Add(beginningWord);

            for (int i = 0; i < wordsCount; i++)
            {
                if (phrase.Count >= 2 
                    && nextWords.ContainsKey(phrase[phrase.Count - 2] + " " + phrase[phrase.Count - 1]))
                {
                    phrase.Add(nextWords[phrase[phrase.Count - 2] + " " + phrase[phrase.Count - 1]]);
                }
                else if (nextWords.ContainsKey(phrase[phrase.Count - 1]))
                {
                    phrase.Add(nextWords[phrase[phrase.Count - 1]]);
                }
                else break;
            }

            phraseBeginning = string.Join(" ", phrase.ToArray());

            return phraseBeginning;
        }
    }
}