using System.ComponentModel;

namespace GraphColoring.GenerateGraph.ErdosRenyiModel
{
    partial class ErdosRenyiModel : IGenerateGraphInterface
    {
        public enum ErdosRenyiModelProbabilityEnum
        {
            [Description("Trees and cycles")]
            cDividedByNLessThanOne,     // c / n ; 0 < c < 1
            [Description("Disconnected graph")]
            cDividedByNMoreThanOne,     // c / n ; c >  1
            [Description("Connected graph")]
            cLogNDividedByN,            // c log n / n; c >= 1
            [Description("Random")]
            notAssigned                 // default -- always the last one
        }
    }
}
