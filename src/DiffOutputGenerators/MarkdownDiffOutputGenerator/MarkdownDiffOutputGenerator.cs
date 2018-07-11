using System;
using System.Collections.Generic;
using textdiffcore;

namespace textdiffcore.DiffOutputGenerators
{
    public class MarkdownDiffOutputGenerator : IDiffOutputGenerator
    {
        public string AddMDStart {get;set;}
        public string AddMDEnd {get;set;}

        public string RemoveMDStart {get;set;}
        public string RemoveMDEnd {get;set;}

        public string EqualMDStart {get;set;}
        public string EqualMDEnd {get;set;}

        public MarkdownDiffOutputGenerator(string AddMD = "**", string RemoveMD = "~~", string EqualMD = "")
         : this (AddMD,AddMD,RemoveMD,RemoveMD,EqualMD,EqualMD)
        {   }

        private MarkdownDiffOutputGenerator(string AddMDPre = "",string AddMDPost = "", string RemoveMDPre = "", string RemoveMDPost = "", string EqualMDPre = "", string EqualMDPost = "")
        {
            AddMDStart = AddMDPre;
            AddMDEnd = AddMDPost;
            RemoveMDStart = RemoveMDPre;
            RemoveMDEnd = RemoveMDPost;
            EqualMDStart = EqualMDPre;
            EqualMDEnd = EqualMDPost;
        }
        public string GenerateOutput(Diffrence diffrence)
        {            
            return GenerateHTMLElement(diffrence);
        }

        private string GenerateHTMLElement(Diffrence d)
        {
            string start;
            string end;

            switch (d.action)
            {
                case TextDiffAction.Add: 
                    start = AddMDStart; 
                    end = AddMDEnd; 
                break;
                case TextDiffAction.Remove:
                    start = RemoveMDStart;
                    end = RemoveMDEnd;
                break;
                case TextDiffAction.Equal: 
                    start = EqualMDStart;
                    end = EqualMDEnd;
                break; 
                default: throw new InvalidOperationException("Diffrence.action is not set to valid value");
            }

            string output = d.value;

            if (output[0]==' ')
            {
                output = output.Insert(1, start);
            }
            else
            {
                output = start + output;
            }

            if (output[output.Length-1]==' ')
            {
                output = output.Insert(output.Length - 1, end);
            }
            else
            {
                output = output + end;
            }

            return output;
        }
        

    }
}
