using System.Collections.Generic;
namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var dictionary = new Dictionary<string, Dictionary<string, int>>();
            foreach (var sentence in text)
                for (var i = 0; i < sentence.Count - 1; i++)
                {
                    AddNgrams(dictionary, sentence[i], sentence[i + 1]);
                    if (i + 2 < sentence.Count)
                    {
                        var twoWords = "";
                        twoWords = sentence[i] + " " + sentence[i + 1];
                        AddNgrams(dictionary, twoWords, sentence[i + 2]);
                    }
                }
            return MostFrequentWords(dictionary);
        }

        public static void AddNgrams(
            Dictionary<string, Dictionary<string, int>> dictionary,
            string word,
            string nextWord)
        {
            if (!dictionary.ContainsKey(word))
                dictionary[word] = new Dictionary<string, int>();
            if (!dictionary[word].ContainsKey(nextWord))
                dictionary[word].Add(nextWord, 1);
            else dictionary[word][nextWord]++;
        }

        public static Dictionary<string, string> MostFrequentWords(
            Dictionary<string, Dictionary<string, int>> dictionary)
        {
            var result = new Dictionary<string, string>();
            foreach (var lineDistionary in dictionary)
            {
                var maxCount = 0;
                var minWord = "Harry Potter and the Sorcerer's Stone";
                foreach (var wordFrequency in lineDistionary.Value)
                {
                    if (wordFrequency.Value > maxCount ||
                        wordFrequency.Value == maxCount &&
                        string.CompareOrdinal(minWord, wordFrequency.Key) > 0)
                    {
                        maxCount = wordFrequency.Value;
                        minWord = wordFrequency.Key;
                    }
                }
                result.Add(lineDistionary.Key, minWord);
            }
            return result;
        }
    }
}