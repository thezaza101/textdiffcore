using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using textdiffcore;
using textdiffcore.DiffOutputGenerators;
using textdiffcore.TextDiffEngine;

namespace tests
{
    public class csDiffOutputList
    {
        private readonly TextDiff diffengine;
        private string originalText = "The quick brown fox";

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

        //#1 Equal Test
        [Fact]
        public void EqualTextTest()
        {
            string stringToTest = "The quick brown fox";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "EEEE"), "#1 Pattern matching failed");
        }

        //#2 Add at start
        [Fact]
        public void AddAtStartTest()
        {
            string stringToTest = "Once The quick brown fox";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "AEEEE"), "#2 Pattern matching failed");
        }

        //#3 Remove at start
        [Fact]
        public void RemoveAtStartTest()
        {
            string stringToTest = "quick brown fox";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "REEE"), "#3 Pattern matching failed");
        }

        //#4 Add at middle
        [Fact]
        public void AddAtMiddleTest()
        {
            string stringToTest = "The quick agile brown fox";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "EEAEE"), "#4 Pattern matching failed");
        }

        //#5 Remove at middle
        [Fact]
        public void RemoveAtMiddleTest()
        {
            string stringToTest = "The quick fox";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "EERE"), "#5 Pattern matching failed");
        }

        //#6 Add at end
        [Fact]
        public void AddAtEndTest()
        {
            string stringToTest = "The quick brown fox jumped";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "EEEEA"), "#6 Pattern matching failed");
        }

        //#7 Remove at end
        [Fact]
        public void RemoveAtEndTest()
        {
            string stringToTest = "The quick brown";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "EEER"), "#7 Pattern matching failed");
        }

        //#8 Update at start
        [Fact]
        public void UpdateAtStartTest()
        {
            string stringToTest = "A quick brown fox";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "RAEEE"), "#8 Pattern matching failed");
        }

        //#9 Update at middle
        [Fact]
        public void UpdateAtMiddleTest()
        {
            string stringToTest = "The quick blue fox";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "EERAE"), "#9 Pattern matching failed");
        }

        //#10 Upadte at end
        [Fact]
        public void UpdateAtEndTest()
        {
            string stringToTest = "The quick brown cat";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "EEERA"), "#10 Pattern matching failed");
        }

        //#11 Multiple add
        [Fact]
        public void MultipleAddTest()
        {
            string stringToTest = "The quick agile brown fox jumped";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "EEAEEA"), "#11 Pattern matching failed");
        }

        //#12 Multiple remove
        [Fact]
        public void MultipleRemoveTest()
        {
            string stringToTest = "quick fox";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "RRRE"), "#12 Pattern matching failed");
        }

        //#13 Multiple updates
        [Fact]
        public void MultipleUpdateTest()
        {
            string stringToTest = "The slow brown cat";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "ERAERA"), "#13 Pattern matching failed");
        }




        private bool OutputPatternMatch(List<Diffrence> list, string pattern)
        {
            bool result = true;
            if (list.Count != pattern.Length)
            {
                return false;
            }
            for (int i = 0; i<list.Count; i++)
            {
                if(list[i].action != GetDiffAction(pattern[i]))
                {
                    result = false;
                }
            }
            return result;
        }

        private TextDiffAction GetDiffAction(char action)
        {
            switch (action)
            {
                case 'A':
                    return TextDiffAction.Add;
                    break;

                case 'R':
                    return TextDiffAction.Remove;
                    break;

                case 'E':
                    return TextDiffAction.Equal;
                    break;
                
                default:
                    throw new Exception("invalid action");
                    break;
            }
        }
    }
}
