using System;
using Xunit;
using textdiffcore;
using textdiffcore.DiffOutputGenerators;
using textdiffcore.TextDiffEngine;

namespace tests
{
    public class csDiffOutputList
    {
        private readonly TextDiff diffengine;

        public csDiffOutputList()
        {
            diffengine = new TextDiff(new csDiff(), new HTMLDiffOutputGenerator());
        }
        [Fact]
        public void TestListGen()
        {
            var list = diffengine.GenerateDiffList("The quick brown fox jumped","The slow brown fox jumped over");
            Assert.True(list.Count == 7, "There should be 7 elements in the diff list");
        }
    }
}
