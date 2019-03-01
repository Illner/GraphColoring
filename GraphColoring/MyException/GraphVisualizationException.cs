using System;

namespace GraphColoring.MyException.GraphVisualizationException
{
    /// <summary>
    /// GraphVizualizationException
    /// </summary>
    class GraphVisualizationException : Exception
    {
        public GraphVisualizationException() { }

        public GraphVisualizationException(string message) : base(message) { }

        public GraphVisualizationException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph vizualization has not been created yet.
    /// </summary>
    class GraphVisualizationNotInitializationException : GraphVisualizationException
    {
        public GraphVisualizationNotInitializationException() { }

        public GraphVisualizationNotInitializationException(string message) : base(message) { }

        public GraphVisualizationNotInitializationException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph vizualization - generating graph
    /// </summary>
    class GraphVisualizationGeneratingGraphException : GraphVisualizationException
    {
        public GraphVisualizationGeneratingGraphException() { }

        public GraphVisualizationGeneratingGraphException(string message) : base(message) { }

        public GraphVisualizationGeneratingGraphException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph vizualization - DLLs don't exist
    /// </summary>
    class GraphVisualizationDLLDontExistException : GraphVisualizationException
    {
        public GraphVisualizationDLLDontExistException() { }

        public GraphVisualizationDLLDontExistException(string message) : base(message) { }

        public GraphVisualizationDLLDontExistException(string message, Exception inner) : base(message, inner) { }
    }
}
