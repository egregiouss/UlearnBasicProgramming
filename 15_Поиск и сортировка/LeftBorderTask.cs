using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete
{
    public class LeftBorderTask
    {
        public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {   
			if (phrases.Count == 0 || phrases[0] == prefix) return left;
            if (phrases[phrases.Count - 1] == prefix) return right - 2;
            if (left == right - 1)
                return left;
			var middle = left + (right - left) / 2;
            return string.CompareOrdinal(prefix, phrases[middle]) > 0 ? 
				GetLeftBorderIndex(phrases, prefix, middle, right) :
             	GetLeftBorderIndex(phrases, prefix, left, middle);
        }
    }
}
