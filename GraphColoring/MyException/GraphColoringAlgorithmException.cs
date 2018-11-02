using System;

namespace GraphColoring.MyException
{
    /// <summary>
    /// Graph exceptions
    /// </summary>
    class GraphColoringAlgorithmException : Exception
    {
         public GraphColoringAlgorithmException() { }

         public GraphColoringAlgorithmException(string message) : base(message) { }

         public GraphColoringAlgorithmException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Nevalidní sekvence vrcholů (méně vrcholů, opakující se vrcholy apod.)
    /// </summary>
    class AlgorithmInvalidVertexSequence : GraphColoringAlgorithmException
    {
        public AlgorithmInvalidVertexSequence() { }

        public AlgorithmInvalidVertexSequence(string message) : base(message) { }

        public AlgorithmInvalidVertexSequence(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graf není obarven
    /// </summary>
    class AlgorithmGraphIsNotColored : GraphColoringAlgorithmException
    {
        public AlgorithmGraphIsNotColored() { }

        public AlgorithmGraphIsNotColored(string message) : base(message) { }

        public AlgorithmGraphIsNotColored(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Invalid number of population
    /// </summary>
    class AlgorithmGraphGeneticAlgorithmInvalidPopulationCount : GraphColoringAlgorithmException
    {
        public AlgorithmGraphGeneticAlgorithmInvalidPopulationCount() { }

        public AlgorithmGraphGeneticAlgorithmInvalidPopulationCount(string message) : base(message) { }

        public AlgorithmGraphGeneticAlgorithmInvalidPopulationCount(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Random is out of range
    /// </summary>
    class AlgorithmGraphGeneticAlgorithmRandomNumberOutRange : GraphColoringAlgorithmException
    {
        public AlgorithmGraphGeneticAlgorithmRandomNumberOutRange() { }

        public AlgorithmGraphGeneticAlgorithmRandomNumberOutRange(string message) : base(message) { }

        public AlgorithmGraphGeneticAlgorithmRandomNumberOutRange(string message, Exception inner) : base(message, inner) { }
    }
}
