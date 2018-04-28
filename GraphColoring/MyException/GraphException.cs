using System;

namespace GraphColoring.MyException
{
    /// <summary>
    /// Graph exceptions
    /// </summary>
    class GraphException : Exception
    {
        public GraphException() { }

        public GraphException(string message) : base(message) { }

        public GraphException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Graf není inicializovaný, tj. NEbyly do něj vloženy hrany 
    /// </summary>
    class GraphInitializationException : GraphException
    {
        public GraphInitializationException() { }

        public GraphInitializationException(string message) : base(message) { }

        public GraphInitializationException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Graf již byl inicializovaný a požaduje se nová inicializace
    /// </summary>
    class GraphAlreadyInitializedException : GraphException
    {
        public GraphAlreadyInitializedException() { }

        public GraphAlreadyInitializedException(string message) : base(message) { }

        public GraphAlreadyInitializedException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Graf nebyl doposud inicializován
    /// </summary>
    class GraphWasNotInitializedException : GraphException
    {
        public GraphWasNotInitializedException() { }

        public GraphWasNotInitializedException(string message) : base(message) { }

        public GraphWasNotInitializedException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Do grafu se snažíme vložit hranu, která již existuje
    /// </summary>
    class GraphDupliciteEdgeException : GraphException
    {
        public GraphDupliciteEdgeException() { }

        public GraphDupliciteEdgeException(string message) : base(message) { }

        public GraphDupliciteEdgeException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Vrchol grafu neexistuje
    /// </summary>
    class GraphVertexDoesntExistException : GraphException
    {
        public GraphVertexDoesntExistException() { }

        public GraphVertexDoesntExistException(string message) : base(message) { }

        public GraphVertexDoesntExistException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Pokud počet alokovaných vrcholů (realCountVertices) je větší než počet vrcholů zadaných v konstruktoru (countVertices) 
    /// </summary>
    class GraphInvalidCountVerticesException : GraphException
    {
        public GraphInvalidCountVerticesException() { }

        public GraphInvalidCountVerticesException(string message) : base(message) { }

        public GraphInvalidCountVerticesException(string message, Exception inner) : base(message, inner) { }
    }
}
