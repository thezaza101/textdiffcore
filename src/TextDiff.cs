using System;
using System.Linq;
using System.Collections.Generic;


namespace textdiffcore
{
    public class TextDiff
    {
        private ITextDiffEngine diffEngine;
        private IDiffOutputGenerator outputGenEngine;
        public List<Diffrence> InnerList {get; private set;}

        public TextDiff(ITextDiffEngine engine, IDiffOutputGenerator outputEngine)
        {
            diffEngine = engine;
            outputGenEngine = outputEngine;
        }

        public List<Diffrence> GenerateDiffList (string oldText, string newText)
        {
            return diffEngine.GenerateDiff(oldText,newText);
        }
        public string GenerateDiffOutput(string oldText, string newText)
        {
            string output = "";
            InnerList = GenerateDiffList(oldText,newText);
            foreach (Diffrence d in InnerList)
            {
                output += outputGenEngine.GenerateOutput(d);
            }
            return output;
        }
    }

}
