using System;
using System.Collections.Generic;

namespace textdiffcore
{
    public interface ITextDiffEngine
    {
        List<Diffrence> GenerateDiff(string oldText, string newText);
    }
}
