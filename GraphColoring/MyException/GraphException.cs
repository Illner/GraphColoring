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
    /// Do grafu se snažíme vložit hranu, která již existuje
    /// </summary>
    class GraphDupliciteEdge : GraphException { }

    /// <summary>
    /// Graph exception
    /// Vrchol grafu neexistuje
    /// </summary>
    class GraphVertexDoesntExist : GraphException { }

}
