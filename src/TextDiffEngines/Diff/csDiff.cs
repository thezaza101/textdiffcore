using System;
using System.Collections.Generic;
using System.Linq;
using textdiffcore;

namespace textdiffcore.TextDiffEngine
{
    public class csDiff : ITextDiffEngine
    {   
        public char separator {get; set;} = ' ';
        public List<Diffrence> GenerateDiff(string oldText, string newText)
        {
            /* This method has 4 main steps
                1. Generate the diff based on the Diff class
                    a. Equal strings are not sent to the diff class
                2. Find the location of the first diff
                    a. add any preceding elements to the output as equal
                3. Loop through the changes and add them to the output
                4. Add any equal elements after the final change to the output as equal            
             */
            char sep = separator;
            string[] oldTextWords = oldText.Split(sep);
            string[] newTextWords = newText.Split(sep);
            List<Diffrence> InnerList = new List<Diffrence>();
            if(string.Equals(oldText,newText,StringComparison.Ordinal))
            {
                //#1a
                foreach (string s in oldTextWords)
                {
                    InnerList.Add(new Diffrence(){value = s+sep, action = TextDiffAction.Equal});
                }
                //Remove the padding from the final element in the list
                Diffrence lastEqualDiff = InnerList.LastOrDefault();            
                lastEqualDiff.value = lastEqualDiff.value.Trim();
                return InnerList;
            }
            //#1
            List<DiffEntry<string>> d = Diff.CreateDiff(oldTextWords,newTextWords);            

            //#2
            //find the first instance of an addition in the new text
            bool add = false;
            bool removed = false;
            int ix;
            try
            {
                ix = newTextWords.ToList().IndexOf(d.First(t => t.EntryType == DiffEntry<string>.DiffEntryType.Add).Object);
                add = true;
            }
            catch (System.InvalidOperationException)
            {
                ix = 0;
            }
            //find the first instance of a removal in the old text
            int iv;
            try
            {
                iv = oldTextWords.ToList().IndexOf(d.First(t => t.EntryType == DiffEntry<string>.DiffEntryType.Remove).Object);
                removed = true;
            
            }
            catch (System.InvalidOperationException)
            {
                iv = 0;
            }
            
            //if the first addition is the start of the new text then add it as a "Add" diffrence
            if (ix == 0 & (ix!=iv) & add)
            {
                InnerList.Add(new Diffrence(){value = d.First(t => t.EntryType == DiffEntry<string>.DiffEntryType.Add).Object+sep, action = TextDiffAction.Add});
            }
            //if the first removal is the start of the old text then add it as a "Remove" diffrence            
            if (iv == 0 & removed)
            {
                var v = d.First(t => t.EntryType == DiffEntry<string>.DiffEntryType.Remove);
                InnerList.Add(new Diffrence(){value = v.Object+sep, action = TextDiffAction.Remove});
            }

            //at which point do the changes start
            int changeIndexStart = (ix > iv) ? ix : iv;

            //#2a
            //any words before the first change is considerd equal
            for (int i = 0; i< changeIndexStart; i++)
            {
                InnerList.Add(new Diffrence(){value = oldTextWords[i]+sep, action = TextDiffAction.Equal});
            }

            int changeIndex = changeIndexStart;

            int startIndex = (ix > 0 & iv > 0) ? 0 : 1;
            startIndex = (InnerList.Count == changeIndexStart) ? 0 : startIndex;
            //#3
            //loop through the changes and add them to the diffrence list... 
            for (int i = startIndex; i<d.Count; i++)
            {
                switch (d[i].EntryType)
                {
                    case DiffEntry<string>.DiffEntryType.Add:
                        InnerList.Add(new Diffrence(){value = d[i].Object+sep, action = TextDiffAction.Add});
                        changeIndex++;
                        break;
                    case DiffEntry<string>.DiffEntryType.Remove:
                        InnerList.Add(new Diffrence(){value = d[i].Object+sep, action = TextDiffAction.Remove});
                        if (d.ElementAtOrDefault(i+1) != null)
                        {
                            if(!(d[i+1].EntryType == DiffEntry<string>.DiffEntryType.Add))
                            {
                                changeIndex++;
                            }
                        }
                        break;
                    case DiffEntry<string>.DiffEntryType.Equal:
                        for (int x = changeIndex; x < changeIndex + d[i].Count; x++)
                        {
                            InnerList.Add(new Diffrence(){value = newTextWords[x]+sep, action = TextDiffAction.Equal});
                        }
                        changeIndex += d[i].Count-1;
                        break;
                    default:
                        break;
                }
            }

            //#4
            //if the last diff isn’t the end of the text array then add the reminder of the array to the difference list
            if (changeIndex != newTextWords.Count())
            {
                for (int i = changeIndex; i< newTextWords.Count(); i++)
                {
                    if(InnerList.Last().value != newTextWords[i]+sep)
                    {
                        InnerList.Add(new Diffrence(){value = newTextWords[i]+sep, action = TextDiffAction.Equal});
                    }
                }
            }

            //Remove the padding from the final element in the list
            Diffrence lastDiff = InnerList.LastOrDefault();            
            lastDiff.value = lastDiff.value.Trim();
            
            return InnerList;
        }
    }
}
