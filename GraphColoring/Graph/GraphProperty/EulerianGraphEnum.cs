using System.ComponentModel;

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
    }
}