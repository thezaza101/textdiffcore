using System;
using System.Collections.Generic;
using textdiffcore;

namespace textdiffcore.DiffOutputGenerators
{
    public class HTMLDiffOutputGenerator : IDiffOutputGenerator
    {
        public string AttributeName {get;set;}
        public string AddAttributeValue {get;set;}
        public string RemoveAttributeValue {get;set;}
        public string EqualAttributeValue {get;set;}
        public string TagType {get; set;}

        public HTMLDiffOutputGenerator(string tagType = "span", string attributeNae = "", string addAttributeValue = "", string removeAttributeValue = "", string equalAttributeValue = "")
        {
            TagType = tagType;
            AttributeName = attributeNae;
            AddAttributeValue = addAttributeValue;
            RemoveAttributeValue = removeAttributeValue;
            EqualAttributeValue = equalAttributeValue;
        }

        public string GenerateOutput(List<Diffrence> diffrences)
        {
            string output = "";
            foreach (Diffrence d in diffrences)
            {
                output += GenerateOutput(d);
            }
            return output;
        }
         
        public string GenerateOutput(Diffrence diffrence)
        {            
            return GenerateHTMLElement(diffrence).Replace(Environment.NewLine,Environment.NewLine+"<br/>");
        }
        private string GetAttributeValue(Diffrence d)
        {
            switch (d.action)
            {
                case TextDiffAction.Add:return AddAttributeValue;
                case TextDiffAction.Remove:return RemoveAttributeValue;
                case TextDiffAction.Equal:return EqualAttributeValue;
                default: return "";
            }
        }
        private string GenerateHTMLElement(Diffrence d)
        {
            if (string.IsNullOrEmpty(GetAttributeValue(d)))
            {
                return d.value;
            }
            else
            {
                return "<"+TagType+" "+AttributeName+"=\""+GetAttributeValue(d)+"\">"+d.value+"</"+TagType+">";
            }
        }

    }
}
