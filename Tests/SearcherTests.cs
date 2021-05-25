using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class SearcherTests
    {
        private Searcher searcher;

        [SetUp]
        public void Setup()
        {
            searcher = new Searcher();
        }

        [Test]
        public void PrefixSearch_ShouldReturnSingle()
        {
            var list = new SortedList<string, int>() { ["rrr"] = 1, ["aaa"] = 2 };
            var result = searcher.PrefixSearch(list, "rr").ToList();
            Assert.That(result.Count == 1);
            Assert.That(result[0] == 1);
        }

        [Test]
        public void PrefixSearch_ShouldReturnSeveral()
        {
            var list = new SortedList<string, int>() { ["rrr1"] = 1, ["rrr2"] = 2 };
            var result = searcher.PrefixSearch(list, "rrr").ToList();
            Assert.That(result.Count == 2);
        }

        [TestCase("rr")]
        [TestCase("sf")]
        [TestCase("")]
        public void PrefixSearch_ShouldReturnEmptyOnEmptyList(string prefix)
        {
            var list = new SortedList<string, int>();
            var result = searcher.PrefixSearch(list, prefix).ToList();
            Assert.IsEmpty(result);
        }

        [Test]
        public void PrefixSearch_ShouldThrowOnNullPrefix()
        {
            var list = new SortedList<string, int>() { ["rrr1"] = 1, ["rrr2"] = 2, ["ad"] = 3, ["d"] = 4 };
            Assert.Throws<ArgumentNullException>(() => searcher.PrefixSearch(list, null));
        }

        [Test]
        public void PrefixSearch_ShouldReturnAllOnEmptyPrefix()
        {
            var list = new SortedList<string, int>() { ["rrr1"] = 1, ["rrr2"] = 2, ["ad"] = 3, ["d"] = 4 };
            var result = searcher.PrefixSearch(list, string.Empty).ToList();
            Assert.AreEqual(list.Values, result);
        }
    }
}