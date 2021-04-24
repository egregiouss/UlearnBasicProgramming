using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        Dictionary<int, string[]> IndexWithWords = new Dictionary<int, string[]>();
        
        Dictionary<string, HashSet<int>> 
			WordsWithIndex = new Dictionary<string, HashSet<int>>();
            
        Dictionary<int, Dictionary<string, List<int>>> 
			WordsPositions = new Dictionary<int, Dictionary<string, List<int>>>();

        public void Add(int id, string documentText)
        {
            var words = Regex.Split(documentText, @"\W+")
                             .Where(x => x.Length != 0)
                             .ToArray();
            IndexWithWords.Add(id, words);
            foreach (var word in words)
            {
                if (!WordsWithIndex.ContainsKey(word))
                    WordsWithIndex.Add(word, new HashSet<int> { id });
                if (!WordsWithIndex[word].Contains(id))
                    WordsWithIndex[word].Add(id);
                if (!WordsPositions.ContainsKey(id))
                {
                    WordsPositions.Add(id, new Dictionary<string, List<int>>());
                    WordsPositions[id].Add(word, WordPositions(word, documentText));
                }
                if (!WordsPositions[id].ContainsKey(word))
                    WordsPositions[id].Add(word, WordPositions(word, documentText));
            }
        }

        public List<int> WordPositions(string word, string text)
        {
            var wordPositions = new List<int>();
            var matches = Regex.Matches(text, $"\\b{word}\\b");
            foreach (Match match in matches)
                wordPositions.Add(match.Index);
            return wordPositions;
        }

        public List<int> GetIds(string word)
        {
            return WordsWithIndex.ContainsKey(word) ? WordsWithIndex[word].ToList() :
                new List<int>();
        }

        public List<int> GetPositions(int id, string word)
        {
            return WordsPositions.ContainsKey(id) && WordsPositions[id].ContainsKey(word) ? 
                WordsPositions[id][word] : new List<int>();
        }

        public void Remove(int id)
        {
            foreach (var e in IndexWithWords[id])
                WordsWithIndex[e].Remove(id);
            IndexWithWords.Remove(id);
            WordsPositions.Remove(id);
        }
    }
}
