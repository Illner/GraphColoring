using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.GenerateGraph.ErdosRenyiModel
{
    partial class ErdosRenyiModel : IGenerateGraphInterface
    {
        public enum ErdosRenyiModelProbabilityEnum
        {
            cDividedByNLessThanOne,     // c / n ; 0 < c < 1
            cDividedByNMoreThanOne,     // c / n ; c >  1
            cLogNDividedByN,            // c log n / n; c >= 1
            notAssigned                 // default -- always the last one
        }
    }
}
