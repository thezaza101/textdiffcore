using System;
using System.Collections.Generic;
using System.Linq;
using textdiffcore;
using textdiffcore.TextDiffEngine.GoogleMyers;

namespace textdiffcore.TextDiffEngine
{
    public class MyersDiff : ITextDiffEngine
    {   
        public diff_match_patch InnerDMP
        {
            get
            {
                return dmp;
            }
            set
            {
                dmp = value;
            }
        }
        
        private diff_match_patch dmp;

        public MyersDiff()
        {
            dmp = new diff_match_patch();
            dmp.Diff_Timeout = 0;
        }
        public List<Diffrence> GenerateDiff(string oldText, string newText)
        {
            List<Diffrence> InnerList = new List<Diffrence>();
            
            List<Diff> ld = dmp.diff_main(oldText, newText);
            
            foreach (Diff d in ld)
            {
                switch (d.operation)
                {
                    case Operation.INSERT:
                        InnerList.Add(new Diffrence(){action = TextDiffAction.Add, value = d.text});
                        break;
                    case Operation.DELETE:
                        InnerList.Add(new Diffrence(){action = TextDiffAction.Remove, value = d.text});
                        break;
                    case Operation.EQUAL:
                        InnerList.Add(new Diffrence(){action = TextDiffAction.Equal, value = d.text});
                        break;
                    default: throw new InvalidOperationException("Error in MyersDiff.ld.operation");                    
                }                
            }
            return InnerList;
        }
    }
}
