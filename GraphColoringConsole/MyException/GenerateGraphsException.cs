using System;

namespace GraphColoringConsole.MyException.GenerateGraphsException
{
    class GenerateGraphsException : Exception
    {
        public GenerateGraphsException() { }

        public GenerateGraphsException(string message) : base(message) { }

        public GenerateGraphsException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// GenerateGraphs exception
    /// MinCount or MaxCount (represents count of vertices) is invalid
    /// </summary>
    class GenerateGraphsInvalidArgumentsMinCountMaxCountException : GenerateGraphsException
    {
        public GenerateGraphsInvalidArgumentsMinCountMaxCountException() { }

        public GenerateGraphsInvalidArgumentsMinCountMaxCountException(string message) : base(message) { }

        public GenerateGraphsInvalidArgumentsMinCountMaxCountException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// GenerateGraphs exception
    /// Invalid count of vertices
    /// </summary>
    class GenerateGraphsInvalidCountOfVerticesException : GenerateGraphsException
    {
        public GenerateGraphsInvalidCountOfVerticesException() { }

        public GenerateGraphsInvalidCountOfVerticesException(string message) : base(message) { }

        public GenerateGraphsInvalidCountOfVerticesException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// GenerateGraphs exception
    /// Algorithm does not exist
    /// </summary>
    class GenerateGraphsAlgorithmDoesNotExistException : GenerateGraphsException
    {
        public GenerateGraphsAlgorithmDoesNotExistException() { }

        public GenerateGraphsAlgorithmDoesNotExistException(string message) : base(message) { }

        public GenerateGraphsAlgorithmDoesNotExistException(string message, Exception inner) : base(message, inner) { }
    }
}
