using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace TextAnalysis
{
    static class SentencesParserTask
    {
        static readonly char[] Separator = { '.', '!', '?', ';', ':', '(', ')' };

        public static List<List<string>> ParseSentences(string text)
        {
            return text.Split(Separator)
                       .Where(s => s.Length != 0)
                       .Select(FilterSentence)
                       .Select(s => s.Split()
                                     .Where(w => w.Length != 0)
                                     .ToList())
                       .Where(l => l.Count != 0)
                       .ToList();
        }

        public static string FilterSentence(string sentence)
        {
            return new StringBuilder()
                .Append(sentence.Select((ch) => IsTrueSymbol(ch) ? char.ToLower(ch) : ' ')
                                .ToArray())
                .ToString();
        }

        public static bool IsTrueSymbol(char symbol)
        {
            return char.IsLetter(symbol) || symbol == '\'';
        }
    }
}