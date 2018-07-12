using System;
using System.Collections.Generic;

namespace textdiffcore
{
    public interface IDiffOutputGenerator
    {
        string GenerateOutput(Diffrence diffrence);
        string GenerateOutput(List<Diffrence> diffrences);

    }
}
