using System;
using System.Collections.Generic;
using textdiffcore;

namespace textdiffcore.DiffOutputGenerators
{
    public class MarkdownDiffOutputGenerator : IDiffOutputGenerator
    {
        List<char> esc = new List<char>(){' ', '\n', '\r', '\t'};
        public string AddMDStart {get;set;}
        public string AddMDEnd {get;set;}

        public string RemoveMDStart {get;set;}
        public string RemoveMDEnd {get;set;}

        public string EqualMDStart {get;set;}
        public string EqualMDEnd {get;set;}

        public MarkdownDiffOutputGenerator(string AddMD = "**", string RemoveMD = "**~~", string EqualMD = "")
         : this (AddMD,ReverseString(AddMD),RemoveMD,ReverseString(RemoveMD),EqualMD,ReverseString(EqualMD))
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
        private static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        public string GenerateOutput(List<Diffrence> diffrences)
        {
            string output = "";

            List<string> mdElements = new List<string>();
            string mdElement;
            for (int i = 0; i<diffrences.Count; i++)
            {
                mdElement = GenerateOutput(diffrences[i]);
                mdElements.Add(mdElement);
            }

            for (int i = 0; i<mdElements.Count; i++)
            {
                if ((i+1) < mdElements.Count)
                {
                    if(!esc.Contains(mdElements[i+1][0]) & !esc.Contains(mdElements[i][mdElements[i].Length-1]))
                    {
                        mdElements[i] = mdElements[i] + " ";
                    }
                }
            }
            foreach (string s in mdElements)
            {
                output += s;
            }


            return output;
        }
        public string GenerateOutput(Diffrence diffrence)
        {            
            return GenerateMDElement(diffrence);
        }

        private string GenerateMDElement(Diffrence d)
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
