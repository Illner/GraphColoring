using System;

namespace GraphColoring.MyException.GraphColoringAlgorithmException
{
    /// <summary>
    /// Graph exceptions
    /// </summary>
    public class GraphColoringAlgorithmException : Exception
    {
         public GraphColoringAlgorithmException() { }

         public GraphColoringAlgorithmException(string message) : base(message) { }

         public GraphColoringAlgorithmException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Invalid sequence of vertices (less vertices, vertices are repeating etc.)
    /// </summary>
    public class AlgorithmInvalidVertexSequence : GraphColoringAlgorithmException
    {
        public AlgorithmInvalidVertexSequence() { }

        public AlgorithmInvalidVertexSequence(string message) : base(message) { }

        public AlgorithmInvalidVertexSequence(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph is not colored
    /// </summary>
    public class AlgorithmGraphIsNotColored : GraphColoringAlgorithmException
    {
        public AlgorithmGraphIsNotColored() { }

        public AlgorithmGraphIsNotColored(string message) : base(message) { }

        public AlgorithmGraphIsNotColored(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Invalid number of population
    /// </summary>
    public class AlgorithmGraphGeneticAlgorithmInvalidPopulationCount : GraphColoringAlgorithmException
    {
        public AlgorithmGraphGeneticAlgorithmInvalidPopulationCount() { }

        public AlgorithmGraphGeneticAlgorithmInvalidPopulationCount(string message) : base(message) { }

        public AlgorithmGraphGeneticAlgorithmInvalidPopulationCount(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Random is out of range
    /// </summary>
    public class AlgorithmGraphGeneticAlgorithmRandomNumberOutRange : GraphColoringAlgorithmException
    {
        public AlgorithmGraphGeneticAlgorithmRandomNumberOutRange() { }

        public AlgorithmGraphGeneticAlgorithmRandomNumberOutRange(string message) : base(message) { }

        public AlgorithmGraphGeneticAlgorithmRandomNumberOutRange(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Algorithm doesn't exist
    /// </summary>
    public class AlgorithmDoesntExist : GraphColoringAlgorithmException
    {
        public AlgorithmDoesntExist() { }

        public AlgorithmDoesntExist(string message) : base(message) { }

        public AlgorithmDoesntExist(string message, Exception inner) : base(message, inner) { }
    }
}
