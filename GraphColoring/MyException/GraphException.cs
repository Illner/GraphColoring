using System;

namespace GraphColoring.MyException.GraphException
{
    /// <summary>
    /// Graph exceptions
    /// </summary>
    public class GraphException : Exception
    {
        public GraphException() { }

        public GraphException(string message) : base(message) { }

        public GraphException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Snaha provést nějakou operaci, která požaduje inicializovaný graf
    /// </summary>
    public class GraphNotInitializationException : GraphException
    {
        public GraphNotInitializationException() { }

        public GraphNotInitializationException(string message) : base(message) { }

        public GraphNotInitializationException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Graf není souvislý. U ConnectedSequentialAlgorithm.
    /// </summary>
    public class GraphIsNotConnected : GraphException
    {
        public GraphIsNotConnected() { }

        public GraphIsNotConnected(string message) : base(message) { }

        public GraphIsNotConnected(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Snaha provést nějakou operaci, která požaduje NEinicializovaný graf
    /// </summary>
    public class GraphInitializationException : GraphException
    {
        public GraphInitializationException() { }

        public GraphInitializationException(string message) : base(message) { }

        public GraphInitializationException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Graf již byl inicializovaný a požaduje se nová inicializace
    /// </summary>
    public class GraphAlreadyInitializedException : GraphException
    {
        public GraphAlreadyInitializedException() { }

        public GraphAlreadyInitializedException(string message) : base(message) { }

        public GraphAlreadyInitializedException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Do grafu se snažíme vložit hranu, která již existuje
    /// </summary>
    public class GraphDupliciteEdgeException : GraphException
    {
        public GraphDupliciteEdgeException() { }

        public GraphDupliciteEdgeException(string message) : base(message) { }

        public GraphDupliciteEdgeException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Vrchol grafu neexistuje
    /// </summary>
    public class GraphVertexDoesntExistException : GraphException
    {
        public GraphVertexDoesntExistException() { }

        public GraphVertexDoesntExistException(string message) : base(message) { }

        public GraphVertexDoesntExistException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Vrchol grafu již existuje
    /// </summary>
    public class GraphVertexAlreadyExistsException : GraphException
    {
        public GraphVertexAlreadyExistsException() { }

        public GraphVertexAlreadyExistsException(string message) : base(message) { }

        public GraphVertexAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Vertex userName exists
    /// </summary>
    public class GraphVertexUserNameAlreadyExistsException : GraphException
    {
        public GraphVertexUserNameAlreadyExistsException() { }

        public GraphVertexUserNameAlreadyExistsException(string message) : base(message) { }

        public GraphVertexUserNameAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Invalid vertex
    /// </summary>
    public class GraphInvalidVertexException : GraphException
    {
        public GraphInvalidVertexException() { }

        public GraphInvalidVertexException(string message) : base(message) { }

        public GraphInvalidVertexException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Hrana v grafu neexistuje
    /// </summary>
    public class GraphEdgeDoesntExistException : GraphException
    {
        public GraphEdgeDoesntExistException() { }

        public GraphEdgeDoesntExistException(string message) : base(message) { }

        public GraphEdgeDoesntExistException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Hrana v grafu již existuje
    /// </summary>
    public class GraphEdgeAlreadyExistsException : GraphException
    {
        public GraphEdgeAlreadyExistsException() { }

        public GraphEdgeAlreadyExistsException(string message) : base(message) { }

        public GraphEdgeAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Pokud počet alokovaných vrcholů (realCountVertices) je větší než počet vrcholů zadaných v konstruktoru (countVertices) 
    /// </summary>
    public class GraphInvalidCountVerticesException : GraphException
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
    public class GraphInvalidDecrementCountVertices : GraphException
    {
        public GraphInvalidDecrementCountVertices() { }

        public GraphInvalidDecrementCountVertices(string message) : base(message) { }

        public GraphInvalidDecrementCountVertices(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Snaha o dekrementaci nulového počtu hran
    /// </summary>
    public class GraphInvalidDecrementCountEdges : GraphException
    {
        public GraphInvalidDecrementCountEdges() { }

        public GraphInvalidDecrementCountEdges(string message) : base(message) { }

        public GraphInvalidDecrementCountEdges(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Snaha o inkrementaci / dekrementaci počtu vrcholů / hran mimo Graph
    /// </summary>
    public class GraphPermissionDeIncrementVerticesEdges : GraphException
    {
        public GraphPermissionDeIncrementVerticesEdges() { }

        public GraphPermissionDeIncrementVerticesEdges(string message) : base(message) { }

        public GraphPermissionDeIncrementVerticesEdges(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Graf nemá žádné vrcholy
    /// </summary>
    public class GraphDoesntHaveAnyVertices : GraphException
    {
        public GraphDoesntHaveAnyVertices() { }

        public GraphDoesntHaveAnyVertices(string message) : base(message) { }

        public GraphDoesntHaveAnyVertices(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Špatný stupen vrchol
    /// </summary>
    public class GraphInvalidDegreeVertex : GraphException
    {
        public GraphInvalidDegreeVertex() { }

        public GraphInvalidDegreeVertex(string message) : base(message) { }

        public GraphInvalidDegreeVertex(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// (Colored) Graph exception
    /// Snaha provést nějakou operaci, která požaduje inicializovaný obarvený graf
    /// </summary>
    public class ColoredGraphNotInitializationException : GraphException
    {
        public ColoredGraphNotInitializationException() { }

        public ColoredGraphNotInitializationException(string message) : base(message) { }

        public ColoredGraphNotInitializationException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// (Colored) Graph Exception
    /// Graf již byl inicializován a je snaha o druhou inicializaci
    /// </summary>
    public class ColoredGraphAlreadyInitializedException : GraphException
    {
        public ColoredGraphAlreadyInitializedException() { }

        public ColoredGraphAlreadyInitializedException(string message) : base(message) { }

        public ColoredGraphAlreadyInitializedException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// (Colored) Graph Exception
    /// Saturation nebyl inicializován (saturation = false)
    /// </summary>
    public class ColoredGraphNotInitializationSaturation : GraphException
    {
        public ColoredGraphNotInitializationSaturation() { }

        public ColoredGraphNotInitializationSaturation(string message) : base(message) { }

        public ColoredGraphNotInitializationSaturation(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph Exception
    /// Graph has to have at least one vertex
    /// </summary>
    public class GraphHasToHaveAtLeastOneVertexException : GraphException
    {
        public GraphHasToHaveAtLeastOneVertexException() { }

        public GraphHasToHaveAtLeastOneVertexException(string message) : base(message) { }

        public GraphHasToHaveAtLeastOneVertexException(string message, Exception inner) : base(message, inner) { }
    }
}
