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
    class GraphInitializationException : GraphException { }

    /// <summary>
    /// Graph exception
    /// Graf již byl inicializovaný a požaduje se nová inicializace
    /// </summary>
    class GraphAlreadyInitializedException : GraphException { }

    /// <summary>
    /// Graph exception
    /// Do grafu se snažíme vložit hranu, která již existuje
    /// </summary>
    class GraphDupliciteEdge : GraphException { }

    /// <summary>
    /// Graph exception
    /// Vrchol grafu neexistuje
    /// </summary>
    class GraphVertexDoesntExist : GraphException { }

    /// <summary>
    /// Graph exception
    /// Pokud počet alokovaných vrcholů (realCountVertices) je větší než počet vrcholů zadaných v konstruktoru (countVertices) 
    /// </summary>
    class GraphInvalidCountVertices : GraphException { }
}
