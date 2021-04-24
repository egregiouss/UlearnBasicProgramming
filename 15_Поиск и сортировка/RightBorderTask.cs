using System;
using System.Collections.Generic;
using System.Linq;
namespace Autocomplete
{
    public class RightBorderTask
    {
        public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            while (left < right)
            {
                var middle = left + (right - left) / 2;
                if (middle < 0) return right;
                if (string.Compare(prefix, phrases[middle]) <= 0 && 
					!phrases[middle].StartsWith(prefix))
                    right = middle;
                else left = middle + 1;
            }
            return right;
        }
    }
}
