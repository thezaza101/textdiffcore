﻿using System;
using System.Collections.Generic;
using System.Linq;
using textdiffcore;
using textdiffcore.DiffOutputGenerators;
using textdiffcore.TextDiffEngine;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            //TextDiff diffobj = new TextDiff(new MyersDiff(), new HTMLDiffOutputGenerator("span", "style", "color:#003300;background-color:#ccff66;","color:#990000;background-color:#ffcc99;text-decoration:line-through;",""));
            //TextDiff diffobj = new TextDiff(new MyersDiff(), new HTMLDiffOutputGenerator("span", "class", "newText","deletedText",""));
            
            TextDiff diffobj = new TextDiff(new MyersDiff(), new MarkdownDiffOutputGenerator());
            
            
            string oldText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.\r\nMaecenas luctus ipsum sit amet turpis pulvinar, et consequat magna tincidunt.\r\nUt tristique sem vitae justo elementum, et fermentum arcu elementum.\r\nDonec blandit facilisis vulputate. Maecenas eu elit ut tortor volutpat condimentum.\r\nPhasellus faucibus vehicula turpis eu pharetra. In imperdiet iaculis cursus.";
            string newText = "Lorem ipsum dolor sit amet, consectetur adiafeeing elit.\r\nMaecenas  sit amet turpis pulvinar, et consequat magna tincidunt.\r\nUt tristique sem vitae justo elementum, et free arcu elementum.\r\nDwfvwc blandit facilisis vulputate. Maecenas eu elit ut tortor volutpat condimentum.\r\nPhasellus dotnet vehicula turpis eu pharetra. In imperdiet iaculis cursus chieown.";
            
            string output = diffobj.GenerateDiffOutput("oldText", "newText");

            if(true)
            {
                oldText = "The quick brown fox";
                output ="";
                

                //Equal
                System.Console.WriteLine("#1 Equal test");
                newText = "The quick brown fox";
                output += diffobj.GenerateDiffOutput(oldText,newText);
                PrintList(diffobj.InnerList,oldText,newText);
                OutputTextMatch(diffobj.InnerList, "The quick brown fox");
                OutputPatternMatch(diffobj.InnerList, "E");
                output+=Environment.NewLine;
                
                //Add at start
                System.Console.WriteLine("#2 Add at start");
                newText = "Once The quick brown fox";
                output += diffobj.GenerateDiffOutput(oldText,newText);
                PrintList(diffobj.InnerList,oldText,newText);
                OutputTextMatch(diffobj.InnerList, "Once ","The quick brown fox");
                OutputPatternMatch(diffobj.InnerList, "AE");
                output+=Environment.NewLine;

        
                //Remove at start
                System.Console.WriteLine("#3 Remove at start");
                newText = "quick brown fox";
                output += diffobj.GenerateDiffOutput(oldText,newText);
                PrintList(diffobj.InnerList,oldText,newText);
                OutputTextMatch(diffobj.InnerList, "The ", "quick brown fox");
                OutputPatternMatch(diffobj.InnerList, "RE");
                output+=Environment.NewLine;

    
                //Add at middle
                System.Console.WriteLine("#4 Add at middle");
                newText = "The quick agile brown fox";
                output += diffobj.GenerateDiffOutput(oldText,newText);
                PrintList(diffobj.InnerList,oldText,newText);
                OutputTextMatch(diffobj.InnerList, "The quick ", "agile ", "brown fox");
                OutputPatternMatch(diffobj.InnerList, "EAE");
                output+=Environment.NewLine;

        
                //Remove at middle
                System.Console.WriteLine("#5 Remove at middle");
                newText = "The quick fox";
                output += diffobj.GenerateDiffOutput(oldText,newText);
                PrintList(diffobj.InnerList,oldText,newText);
                OutputTextMatch(diffobj.InnerList, "The quick ", "brown ", "fox");
                OutputPatternMatch(diffobj.InnerList, "ERE");
                output+=Environment.NewLine;

                
                //Add at end
                System.Console.WriteLine("#6 Add at end");
                newText = "The quick brown fox jumped";
                output += diffobj.GenerateDiffOutput(oldText,newText);
                PrintList(diffobj.InnerList,oldText,newText);
                OutputTextMatch(diffobj.InnerList, "The quick brown fox", " jumped");
                OutputPatternMatch(diffobj.InnerList, "EA");
                output+=Environment.NewLine;


                //Remove at end
                System.Console.WriteLine("#7 Remove at end");
                newText = "The quick brown";
                output += diffobj.GenerateDiffOutput(oldText,newText);
                PrintList(diffobj.InnerList,oldText,newText);
                OutputTextMatch(diffobj.InnerList, "The quick brown", " fox");
                OutputPatternMatch(diffobj.InnerList, "ER");
                output+=Environment.NewLine;

                

                //Update at start
                System.Console.WriteLine("#8 Update at start");
                newText = "A quick brown fox";
                output += diffobj.GenerateDiffOutput(oldText,newText);
                PrintList(diffobj.InnerList,oldText,newText);
                OutputTextMatch(diffobj.InnerList, "The","A", " quick brown fox");
                OutputPatternMatch(diffobj.InnerList, "RAE");
                output+=Environment.NewLine;


                //Update at middle
                System.Console.WriteLine("#9 Update at middle");
                newText = "The quick blue fox";
                output += diffobj.GenerateDiffOutput(oldText,newText);
                PrintList(diffobj.InnerList,oldText,newText);
                OutputTextMatch(diffobj.InnerList, "The quick b", "rown","lue", " fox");
                OutputPatternMatch(diffobj.InnerList, "ERAE");
                output+=Environment.NewLine;


                //Update at end
                System.Console.WriteLine("#10 Update at end");
                newText = "The quick brown cat";
                output += diffobj.GenerateDiffOutput(oldText,newText);
                PrintList(diffobj.InnerList,oldText,newText);
                OutputTextMatch(diffobj.InnerList, "The quick brown ", "fox", "cat");
                OutputPatternMatch(diffobj.InnerList, "ERA");
                output+=Environment.NewLine;


                //Multiple add
                System.Console.WriteLine("#11 Multiple add");
                newText = "The quick agile brown fox jumped";
                output += diffobj.GenerateDiffOutput(oldText,newText);
                PrintList(diffobj.InnerList,oldText,newText);
                OutputTextMatch(diffobj.InnerList, "The quick ", "agile ", "brown fox", " jumped");
                OutputPatternMatch(diffobj.InnerList, "EAEA");
                output+=Environment.NewLine;


                //Multiple remove
                System.Console.WriteLine("#12 Multiple remove");
                newText = "quick fox";
                output += diffobj.GenerateDiffOutput(oldText,newText);
                PrintList(diffobj.InnerList,oldText,newText);
                OutputTextMatch(diffobj.InnerList, "The ", "quick", " brown", " fox");
                OutputPatternMatch(diffobj.InnerList, "RERE");
                output+=Environment.NewLine;


                //Multiple updates
                System.Console.WriteLine("#13 Multiple updates");
                newText = "The slow brown cat";
                output += diffobj.GenerateDiffOutput(oldText,newText);
                PrintList(diffobj.InnerList,oldText,newText);
                OutputTextMatch(diffobj.InnerList, "The ", "quick", "slow", " brown ", "fox", "cat");
                OutputPatternMatch(diffobj.InnerList, "ERAERA");
                output+=Environment.NewLine;
            }


           System.Console.WriteLine(output);
           Console.ReadLine();
        }

        static bool OutputTextMatch (List<Diffrence> list, params string[] expectedList)
        {
            bool result = true;
            if (list.Count != expectedList.Length)
            {
                System.Console.WriteLine("Text matching failed");
                return false;
            }
            for (int i = 0; i<list.Count; i++)
            {
                if(list[i].value != expectedList[i])
                {
                    result = false;
                }
            }

            if (result)
            {
                System.Console.WriteLine("Text matching passed");
            }
            else
            {
                System.Console.WriteLine("Text matching failed");
            }



            return result;
        }







        static void PrintList(List<Diffrence> d, string oldtext, string newtext)
        {
            System.Console.WriteLine("\"{0}\" vs \"{1}\"", oldtext, newtext);
            System.Console.WriteLine("-----------");
            foreach (Diffrence df in d)
            {                
                System.Console.WriteLine(df.action.ToString()  + " \t " +df.value);
            }
            System.Console.WriteLine();
        }

        static bool OutputPatternMatch(List<Diffrence> list, string pattern)
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
            
            if (result)
            {
                System.Console.WriteLine("Pattern matching passed");
            }
            else
            {
                System.Console.WriteLine("Pattern matching failed");
            }
            System.Console.WriteLine();
            System.Console.WriteLine();
            
            return result;
        }

        static TextDiffAction GetDiffAction(char action)
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

    }
}
