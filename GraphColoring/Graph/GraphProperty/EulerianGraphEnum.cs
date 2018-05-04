using System;

namespace GraphColoring.Graph.GraphProperty
{
    partial class GraphProperty
    {
        public enum EulerianGraphEnum
        {
            undefined,      // Default value
            eulerian,       // Eulerian cycle
            semiEulerian,   // Eulerian path
            notEulerian     // Not eulerian
        }
    }
}