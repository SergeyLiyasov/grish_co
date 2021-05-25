using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class Searcher
    {
        public IEnumerable<T> PrefixSearch<T>(SortedList<string, T> list, string prefix)
        {
            var left = BinSearchLeftBorder(list.Keys, prefix);
            var right = BinSearchRightBorder(list.Keys, prefix);
            return list.Values.Skip(left + 1).Take(right - left - 1);
        }

        private static int BinSearchLeftBorder(IList<string> list, string prefix)
        {
            var left = -1;
            var right = list.Count;
            while (left + 1 != right)
            {
                var middle = (left + right) / 2;
                if (string.Compare(list[middle], prefix, StringComparison.OrdinalIgnoreCase) < 0)
                    left = middle;
                else
                    right = middle;
            }
            return left;
        }

        private static int BinSearchRightBorder(IList<string> list, string prefix)
        {
            var left = -1;
            var right = list.Count;
            while (left + 1 != right)
            {
                var middle = left + (right - left) / 2;
                if (string.Compare(list[middle], prefix, StringComparison.OrdinalIgnoreCase) <= 0 ||
                    list[middle].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    left = middle;
                else
                    right = middle;
            }
            return right;
        }
    }
}
