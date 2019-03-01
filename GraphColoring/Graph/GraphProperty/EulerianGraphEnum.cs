using System.ComponentModel;
using System.Collections.Generic;

namespace GraphColoring.Graph.GraphProperty
{
    partial class GraphProperty
    {
        public enum EulerianGraphEnum
        {
            undefined,      // Default value
            [Description("Eulerian graph")]
            eulerian,       // Eulerian cycle
            [Description("Semi-Eulerian graph")]
            semiEulerian,   // Eulerian path
            [Description("Not Eulerian graph")]
            notEulerian     // Not eulerian
        }

        public static Dictionary<EulerianGraphEnum, string> WCMEulerianGraphDictionary = new Dictionary<EulerianGraphEnum, string>()
        {
            { EulerianGraphEnum.eulerian, "Eulerian graph" },
            { EulerianGraphEnum.semiEulerian, "Semi-Eulerian graph" },
            { EulerianGraphEnum.notEulerian, "Not Eulerian graph" }
        };
    }
}