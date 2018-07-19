using System;

namespace GraphColoring.MyException
{
    /// <summary>
    /// Graph exceptions
    /// </summary>
    class GraphColoringAlgorithmExceptions : Exception
    {
         public GraphColoringAlgorithmExceptions() { }

         public GraphColoringAlgorithmExceptions(string message) : base(message) { }

         public GraphColoringAlgorithmExceptions(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Nevalidní sekvence vrcholů (méně vrcholů, opakující se vrcholy apod.)
    /// </summary>
    class AlgorithmInvalidVertexSequence : GraphColoringAlgorithmExceptions
    {
        public AlgorithmInvalidVertexSequence() { }

        public AlgorithmInvalidVertexSequence(string message) : base(message) { }

        public AlgorithmInvalidVertexSequence(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graf není obarven
    /// </summary>
    class AlgorithmGraphIsNotColored : GraphColoringAlgorithmExceptions
    {
        public AlgorithmGraphIsNotColored() { }

        public AlgorithmGraphIsNotColored(string message) : base(message) { }

        public AlgorithmGraphIsNotColored(string message, Exception inner) : base(message, inner) { }
    }
}
