using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static void ParseWords(
            string sentence, 
            List<List<string>> sentencesList, 
            int currentSentenceNumber)
        {
            var anotherWord = new StringBuilder();
            for (int i = 0; i < sentence.Length; i++)
            {
                if (char.IsLetter(sentence[i]) || sentence[i] == '\'')
                {
                    if (char.IsUpper(sentence[i])) anotherWord.Append(char.ToLower(sentence[i]));
                    else anotherWord.Append(sentence[i]);
                }
                if (((!char.IsLetter(sentence[i]) && sentence[i] != '\'') || i == sentence.Length - 1)
                    && anotherWord.Length > 0)
                {
                    sentencesList[currentSentenceNumber].Add(anotherWord.ToString());
                    anotherWord.Clear();
                }
            }
        }

        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();

            string[] sentences = text.Split('.', '!', '?', ';', ':', '(', ')');

            int currentSentenceNumber = -1;
            foreach (var sentence in sentences)
            {
                if (string.IsNullOrEmpty(sentence)) continue;
                if (string.IsNullOrWhiteSpace(sentence)) continue;
                sentencesList.Add(new List<string>());
                currentSentenceNumber++;
                ParseWords(sentence, sentencesList, currentSentenceNumber);
                if (sentencesList[currentSentenceNumber].Count == 0)
                {
                    sentencesList.RemoveAt(currentSentenceNumber);
                    currentSentenceNumber--;
                }
            }

            return sentencesList;
        }
    }
}