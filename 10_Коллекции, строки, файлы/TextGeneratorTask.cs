using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            var words = phraseBeginning.Split(' ').ToList();
            var builder = new StringBuilder();
            for (var i = 0; i < wordsCount; i++)
            {
                var twoLastWords = "";
                if (words.Count >= 2)
                    twoLastWords = $"{words[words.Count - 2]} {words[words.Count - 1]}";
                NextWords(words, nextWords, twoLastWords);
            }
            words.ForEach(x => builder.AppendFormat(" {0}", x));
            return builder.Remove(0, 1).ToString();
        }

        public static void NextWords(
            List<string> words,
            Dictionary<string, string> nextWords,
            string twoLastWords)
        {
            if (words.Count >= 2 && nextWords.ContainsKey(twoLastWords))
                words.Add(nextWords[twoLastWords]);
            else if (nextWords.ContainsKey(words.LastOrDefault()))
                words.Add(nextWords[words.LastOrDefault()]);
            else
                return;
        }
    }
}