using System;
using System.Collections.Generic;

namespace textdiffcore
{
    public interface IDiffOutputGenerator
    {
        string GenerateOutput(Diffrence diffrence);
    }
}
