using System;
using Xunit;
using textdiffcore.TextDiffEngine.GoogleMyers;

namespace tests
{
    public class DiffMatchPatchTests
    {
        [Theory]
        [InlineData("", "", 0)]
        [InlineData("abc", "", 0)]
        [InlineData("", "abc", 0)]
        [InlineData("abc", "abc", 3)]
        [InlineData("abc", "abd", 2)]
        [InlineData("abcdef", "abc", 3)]
        [InlineData("abc", "abcdef", 3)]
        [InlineData("ABC", "abc", 0)]
        [InlineData("a\u0300", "a\u0300", 2)]
        [InlineData("a\u0300", "a", 1)]
        [InlineData("hello world", "hello friend", 6)]
        public void TestDiffCommonPrefix(string text1, string text2, int expected)
        {
            var dmp = new diff_match_patch();
            Assert.Equal(expected, dmp.diff_commonPrefix(text1, text2));
        }
    }
}
