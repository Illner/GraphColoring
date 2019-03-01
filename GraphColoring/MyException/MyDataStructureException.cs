using System;

namespace GraphColoring.MyException.MyDataStructureException
{
    /// <summary>
    /// The element has negative rank.
    /// </summary>
    public class MyNodeNegativeRankException : Exception
    {
        public MyNodeNegativeRankException() { }

        public MyNodeNegativeRankException(string message) : base(message) { }

        public MyNodeNegativeRankException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Attempt to decrease unknown element in the heap
    /// </summary>
    public class FibonacciHeapUnknownElementException : Exception
    {
        public FibonacciHeapUnknownElementException() { }

        public FibonacciHeapUnknownElementException(string message) : base(message) { }

        public FibonacciHeapUnknownElementException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Attempt to delete min in an empty heap
    /// </summary>
    public class FibonacciHeapAttempToExtractMinInEmptyHeapException : Exception
    {
        public FibonacciHeapAttempToExtractMinInEmptyHeapException() { }

        public FibonacciHeapAttempToExtractMinInEmptyHeapException(string message) : base(message) { }

        public FibonacciHeapAttempToExtractMinInEmptyHeapException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Attempt to insert an existing element
    /// </summary>
    public class FibonacciHeapInsertIdentifierAlreadyKnownException : Exception
    {
        public FibonacciHeapInsertIdentifierAlreadyKnownException() { }

        public FibonacciHeapInsertIdentifierAlreadyKnownException(string message) : base(message) { }

        public FibonacciHeapInsertIdentifierAlreadyKnownException(string message, Exception inner) : base(message, inner) { }
    }
}
