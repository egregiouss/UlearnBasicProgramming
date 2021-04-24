using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Autocomplete
{
    internal class AutocompleteTask
    {
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            return index + 1 < phrases.Count ? phrases[index + 1] : null;
        }

        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        { 
            var leftIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);

            return phrases.Skip(leftIndex + 1)
						  .Take(count)
						  .Where(x => x.StartsWith(prefix))
						  .ToArray();
        }

        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            if (prefix == "") return phrases.Count;
            var leftIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            var rightIndex = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
			
            return (rightIndex - 1  >= leftIndex + 1) ? rightIndex - leftIndex - 1 : 0;
        }
    }
	
	[TestFixture]
    public class AutocompleteTests
    {
        [Test]
        public void TopByPrefix_IsEmpty_WhenNoPhrases()
        {
            var phrases = new string[0];
            var actualTopWords = AutocompleteTask.GetTopByPrefix(phrases, "ab", 2);
            CollectionAssert.IsEmpty(actualTopWords);
        }

        [Test]
        public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
        {
            var phrases = new[] { "a", "ab", "bc" };
            var actualCount = AutocompleteTask.GetCountByPrefix(phrases, "");
            var expectedCount = phrases.Length;
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
