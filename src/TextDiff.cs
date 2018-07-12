using System;
using System.Linq;
using System.Collections.Generic;


namespace textdiffcore
{
    public class TextDiff
    {
        public enum RenderMode
        {
            ElementOnly,
            ContextAware
        }
        private ITextDiffEngine diffEngine;
        private IDiffOutputGenerator outputGenEngine;
        
        private RenderMode rMode;
        public List<Diffrence> InnerList {get; private set;}

        public TextDiff(ITextDiffEngine engine, IDiffOutputGenerator outputEngine, RenderMode renderMode = RenderMode.ContextAware)
        {
            diffEngine = engine;
            outputGenEngine = outputEngine;
            rMode = renderMode;
        }

        public List<Diffrence> GenerateDiffList (string oldText, string newText)
        {
            return diffEngine.GenerateDiff(oldText,newText);
        }
        public string GenerateDiffOutput(string oldText, string newText)
        {
            string output = "";
            InnerList = GenerateDiffList(oldText,newText);

            if (rMode == RenderMode.ElementOnly)
            {
                foreach (Diffrence d in InnerList)
                {
                    output += outputGenEngine.GenerateOutput(d);
                }
            }
            else
            {
                output = outputGenEngine.GenerateOutput(InnerList);
            }
            
            return output;
        }
    }

}
