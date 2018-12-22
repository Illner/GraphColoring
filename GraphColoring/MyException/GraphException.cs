using System;

namespace GraphColoring.MyException.GraphException
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
    /// Snaha provést nějakou operaci, která požaduje inicializovaný graf
    /// </summary>
    class GraphNotInitializationException : GraphException
    {
        public GraphNotInitializationException() { }

        public GraphNotInitializationException(string message) : base(message) { }

        public GraphNotInitializationException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Graf není souvislý. U ConnectedSequentialAlgorithm.
    /// </summary>
    class GraphIsNotConnected : GraphException
    {
        public GraphIsNotConnected() { }

        public GraphIsNotConnected(string message) : base(message) { }

        public GraphIsNotConnected(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Snaha provést nějakou operaci, která požaduje NEinicializovaný graf
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
    /// Vrchol grafu již existuje
    /// </summary>
    class GraphVertexAlreadyExistsException : GraphException
    {
        public GraphVertexAlreadyExistsException() { }

        public GraphVertexAlreadyExistsException(string message) : base(message) { }

        public GraphVertexAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Vertex userName exists
    /// </summary>
    class GraphVertexUserNameAlreadyExistsException : GraphException
    {
        public GraphVertexUserNameAlreadyExistsException() { }

        public GraphVertexUserNameAlreadyExistsException(string message) : base(message) { }

        public GraphVertexUserNameAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Invalid vertex
    /// </summary>
    class GraphInvalidVertexException : GraphException
    {
        public GraphInvalidVertexException() { }

        public GraphInvalidVertexException(string message) : base(message) { }

        public GraphInvalidVertexException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Hrana v grafu neexistuje
    /// </summary>
    class GraphEdgeDoesntExistException : GraphException
    {
        public GraphEdgeDoesntExistException() { }

        public GraphEdgeDoesntExistException(string message) : base(message) { }

        public GraphEdgeDoesntExistException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Hrana v grafu již existuje
    /// </summary>
    class GraphEdgeAlreadyExistsException : GraphException
    {
        public GraphEdgeAlreadyExistsException() { }

        public GraphEdgeAlreadyExistsException(string message) : base(message) { }

        public GraphEdgeAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
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

    /// <summary>
    /// Graph exception
    /// Snaha o dekrementaci nulového počtu vrcholů
    /// Nebo není oprávnění k dekrementaci
    /// </summary>
    class GraphInvalidDecrementCountVertices : GraphException
    {
        public GraphInvalidDecrementCountVertices() { }

        public GraphInvalidDecrementCountVertices(string message) : base(message) { }

        public GraphInvalidDecrementCountVertices(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Snaha o dekrementaci nulového počtu hran
    /// </summary>
    class GraphInvalidDecrementCountEdges : GraphException
    {
        public GraphInvalidDecrementCountEdges() { }

        public GraphInvalidDecrementCountEdges(string message) : base(message) { }

        public GraphInvalidDecrementCountEdges(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Snaha o inkrementaci / dekrementaci počtu vrcholů / hran mimo Graph
    /// </summary>
    class GraphPermissionDeIncrementVerticesEdges : GraphException
    {
        public GraphPermissionDeIncrementVerticesEdges() { }

        public GraphPermissionDeIncrementVerticesEdges(string message) : base(message) { }

        public GraphPermissionDeIncrementVerticesEdges(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Graf nemá žádné vrcholy
    /// </summary>
    class GraphDoesntHaveAnyVertices : GraphException
    {
        public GraphDoesntHaveAnyVertices() { }

        public GraphDoesntHaveAnyVertices(string message) : base(message) { }

        public GraphDoesntHaveAnyVertices(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Špatný stupen vrchol
    /// </summary>
    class GraphInvalidDegreeVertex : GraphException
    {
        public GraphInvalidDegreeVertex() { }

        public GraphInvalidDegreeVertex(string message) : base(message) { }

        public GraphInvalidDegreeVertex(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// (Colored) Graph exception
    /// Snaha provést nějakou operaci, která požaduje inicializovaný obarvený graf
    /// </summary>
    class ColoredGraphNotInitializationException : GraphException
    {
        public ColoredGraphNotInitializationException() { }

        public ColoredGraphNotInitializationException(string message) : base(message) { }

        public ColoredGraphNotInitializationException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// (Colored) Graph Exception
    /// Graf již byl inicializován a je snaha o druhou inicializaci
    /// </summary>
    class ColoredGraphAlreadyInitializedException : GraphException
    {
        public ColoredGraphAlreadyInitializedException() { }

        public ColoredGraphAlreadyInitializedException(string message) : base(message) { }

        public ColoredGraphAlreadyInitializedException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// (Colored) Graph Exception
    /// Saturation nebyl inicializován (saturation = false)
    /// </summary>
    class ColoredGraphNotInitializationSaturation : GraphException
    {
        public ColoredGraphNotInitializationSaturation() { }

        public ColoredGraphNotInitializationSaturation(string message) : base(message) { }

        public ColoredGraphNotInitializationSaturation(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph Exception
    /// Graph has to have at least one vertex
    /// </summary>
    class GraphHasToHaveAtLeastOneVertexException : GraphException
    {
        public GraphHasToHaveAtLeastOneVertexException() { }

        public GraphHasToHaveAtLeastOneVertexException(string message) : base(message) { }

        public GraphHasToHaveAtLeastOneVertexException(string message, Exception inner) : base(message, inner) { }
    }
}
