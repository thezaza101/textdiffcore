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
        public void EqualTextPatternTest()
        {
            string stringToTest = "The quick brown fox";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "EEEE"), "#1 Pattern matching failed");
        }

        //#2 Add at start
        [Fact]
        public void AddAtStartPatternTest()
        {
            string stringToTest = "Once The quick brown fox";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "AEEEE"), "#2 Pattern matching failed");
        }

        //#3 Remove at start
        [Fact]
        public void RemoveAtStartPatternTest()
        {
            string stringToTest = "quick brown fox";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "REEE"), "#3 Pattern matching failed");
        }

        //#4 Add at middle
        [Fact]
        public void AddAtMiddlePatternTest()
        {
            string stringToTest = "The quick agile brown fox";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "EEAEE"), "#4 Pattern matching failed");
        }

        //#5 Remove at middle
        [Fact]
        public void RemoveAtMiddlePatternTest()
        {
            string stringToTest = "The quick fox";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "EERE"), "#5 Pattern matching failed");
        }

        //#6 Add at end
        [Fact]
        public void AddAtEndPatternTest()
        {
            string stringToTest = "The quick brown fox jumped";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "EEEEA"), "#6 Pattern matching failed");
        }

        //#7 Remove at end
        [Fact]
        public void RemoveAtEndPatternTest()
        {
            string stringToTest = "The quick brown";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "EEER"), "#7 Pattern matching failed");
        }

        //#8 Update at start
        [Fact]
        public void UpdateAtStartPatternTest()
        {
            string stringToTest = "A quick brown fox";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "RAEEE"), "#8 Pattern matching failed");
        }

        //#9 Update at middle
        [Fact]
        public void UpdateAtMiddlePatternTest()
        {
            string stringToTest = "The quick blue fox";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "EERAE"), "#9 Pattern matching failed");
        }

        //#10 Upadte at end
        [Fact]
        public void UpdateAtEndPatternTest()
        {
            string stringToTest = "The quick brown cat";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "EEERA"), "#10 Pattern matching failed");
        }

        //#11 Multiple add
        [Fact]
        public void MultipleAddPatternTest()
        {
            string stringToTest = "The quick agile brown fox jumped";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "EEAEEA"), "#11 Pattern matching failed");
        }

        //#12 Multiple remove
        [Fact]
        public void MultipleRemovePatternTest()
        {
            string stringToTest = "quick fox";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputPatternMatch(listOutput, "RRRE"), "#12 Pattern matching failed");
        }

        //#13 Multiple updates
        [Fact]
        public void MultipleUpdatePatternTest()
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
                case 'R':
                    return TextDiffAction.Remove;
                case 'E':
                    return TextDiffAction.Equal;
                default:
                    throw new Exception("invalid action");
            }
        }

        //#1 Equal Test
        [Fact]
        public void EqualTextTextTest()
        {
            string stringToTest = "The quick brown fox";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputTextMatch(listOutput, "The ", "quick ", "brown ", "fox"), "#1 Text matching failed");
        }

        //#2 Add at start
        [Fact]
        public void AddAtStartTextTest()
        {
            string stringToTest = "Once The quick brown fox";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputTextMatch(listOutput,"Once ","The ", "quick ", "brown ", "fox"), "#2 Text matching failed");            
        }

        //#3 Remove at start
        [Fact]
        public void RemoveAtStartTextTest()
        {
            string stringToTest = "quick brown fox";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputTextMatch(listOutput, "The ", "quick ", "brown ", "fox"), "#3 Text matching failed");            
        }

        //#4 Add at middle
        [Fact]
        public void AddAtMiddleTextTest()
        {
            string stringToTest = "The quick agile brown fox";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputTextMatch(listOutput, "The ", "quick ", "agile ", "brown ", "fox"), "#4 Text matching failed");
        }

        //#5 Remove at middle
        [Fact]
        public void RemoveAtMiddleTextTest()
        {
            string stringToTest = "The quick fox";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputTextMatch(listOutput, "The ", "quick ", "brown ", "fox"), "#5 Text matching failed");            
        }

        //#6 Add at end
        [Fact]
        public void AddAtEndTextTest()
        {
            string stringToTest = "The quick brown fox jumped";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputTextMatch(listOutput, "The ", "quick ", "brown ", "fox ", "jumped"), "#6 Text matching failed");            
        }

        //#7 Remove at end
        [Fact]
        public void RemoveAtEndTextTest()
        {
            string stringToTest = "The quick brown";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputTextMatch(listOutput, "The ", "quick ", "brown ", "fox"), "#7 Text matching failed");            
        }

        //#8 Update at start
        [Fact]
        public void UpdateAtStartTextTest()
        {
            string stringToTest = "A quick brown fox";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputTextMatch(listOutput, "The ","A ", "quick ", "brown ", "fox"), "#8 Text matching failed");            
        }

        //#9 Update at middle
        [Fact]
        public void UpdateAtMiddleTextTest()
        {
            string stringToTest = "The quick blue fox";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputTextMatch(listOutput, "The ", "quick ", "brown ","blue ", "fox"), "#9 Text matching failed");            
        }

        //#10 Upadte at end
        [Fact]
        public void UpdateAtEndTextTest()
        {
            string stringToTest = "The quick brown cat";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputTextMatch(listOutput, "The ", "quick ", "brown ", "fox ", "cat"), "#10 Text matching failed");            
        }

        //#11 Multiple add
        [Fact]
        public void MultipleAddTextTest()
        {
            string stringToTest = "The quick agile brown fox jumped";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputTextMatch(listOutput, "The ", "quick ", "agile ", "brown ", "fox ", "jumped"), "#11 Text matching failed");            
        }

        //#12 Multiple remove
        [Fact]
        public void MultipleRemoveTextTest()
        {
            string stringToTest = "quick fox";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputTextMatch(listOutput, "The ", "quick ", "brown ", "fox"), "#12 Text matching failed");            
        }

        //#13 Multiple updates
        [Fact]
        public void MultipleUpdateTextTest()
        {
            string stringToTest = "The slow brown cat";
            List<Diffrence> listOutput = diffengine.GenerateDiffList(originalText,stringToTest);
            Assert.True(OutputTextMatch(listOutput, "The ", "quick ", "slow ", "brown ", "fox ", "cat"), "#13 Text matching failed");            
        }

        private bool OutputTextMatch (List<Diffrence> list, params string[] expectedList)
        {
            bool result = true;
            if (list.Count != expectedList.Length)
            {
                return false;
            }
            for (int i = 0; i<list.Count; i++)
            {
                if(list[i].value != expectedList[i])
                {
                    result = false;
                }
            }
            return result;
        }
    }
}
