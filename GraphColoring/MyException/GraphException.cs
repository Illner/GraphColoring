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
    /// Graph is not initialized
    /// </summary>
    public class GraphNotInitializationException : GraphException
    {
        public GraphNotInitializationException() { }

        public GraphNotInitializationException(string message) : base(message) { }

        public GraphNotInitializationException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Graph is not connected - ConnectedSequentialAlgorithm
    /// </summary>
    public class GraphIsNotConnected : GraphException
    {
        public GraphIsNotConnected() { }

        public GraphIsNotConnected(string message) : base(message) { }

        public GraphIsNotConnected(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Graph is initialized
    /// </summary>
    public class GraphInitializationException : GraphException
    {
        public GraphInitializationException() { }

        public GraphInitializationException(string message) : base(message) { }

        public GraphInitializationException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Reinitialization of a graph
    /// </summary>
    public class GraphAlreadyInitializedException : GraphException
    {
        public GraphAlreadyInitializedException() { }

        public GraphAlreadyInitializedException(string message) : base(message) { }

        public GraphAlreadyInitializedException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Vertex does not exist
    /// </summary>
    public class GraphVertexDoesntExistException : GraphException
    {
        public GraphVertexDoesntExistException() { }

        public GraphVertexDoesntExistException(string message) : base(message) { }

        public GraphVertexDoesntExistException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Attempt ot insert an existing vertex
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
    /// Edge does not exist
    /// </summary>
    public class GraphEdgeDoesntExistException : GraphException
    {
        public GraphEdgeDoesntExistException() { }

        public GraphEdgeDoesntExistException(string message) : base(message) { }

        public GraphEdgeDoesntExistException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Attempt to insert an existing edge
    /// </summary>
    public class GraphEdgeAlreadyExistsException : GraphException
    {
        public GraphEdgeAlreadyExistsException() { }

        public GraphEdgeAlreadyExistsException(string message) : base(message) { }

        public GraphEdgeAlreadyExistsException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// RealCountVertices is not equal to countVertices (constructor)
    /// </summary>
    public class GraphInvalidCountVerticesException : GraphException
    {
        public GraphInvalidCountVerticesException() { }

        public GraphInvalidCountVerticesException(string message) : base(message) { }

        public GraphInvalidCountVerticesException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Attempt to decrement count vertices which is equal to zero
    /// </summary>
    public class GraphInvalidDecrementCountVertices : GraphException
    {
        public GraphInvalidDecrementCountVertices() { }

        public GraphInvalidDecrementCountVertices(string message) : base(message) { }

        public GraphInvalidDecrementCountVertices(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Attempt to decrement count edges which is equal to zero
    /// </summary>
    public class GraphInvalidDecrementCountEdges : GraphException
    {
        public GraphInvalidDecrementCountEdges() { }

        public GraphInvalidDecrementCountEdges(string message) : base(message) { }

        public GraphInvalidDecrementCountEdges(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// No permission for decrementation / incrementation count vertices / edges
    /// </summary>
    public class GraphPermissionDeIncrementVerticesEdges : GraphException
    {
        public GraphPermissionDeIncrementVerticesEdges() { }

        public GraphPermissionDeIncrementVerticesEdges(string message) : base(message) { }

        public GraphPermissionDeIncrementVerticesEdges(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Graph does not have any vertices
    /// </summary>
    public class GraphDoesntHaveAnyVertices : GraphException
    {
        public GraphDoesntHaveAnyVertices() { }

        public GraphDoesntHaveAnyVertices(string message) : base(message) { }

        public GraphDoesntHaveAnyVertices(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Invalid vertex degree
    /// </summary>
    public class GraphInvalidDegreeVertex : GraphException
    {
        public GraphInvalidDegreeVertex() { }

        public GraphInvalidDegreeVertex(string message) : base(message) { }

        public GraphInvalidDegreeVertex(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// ClassGraph not set to bipartite or undefined
    /// </summary>
    public class GraphIsNotBipartiteOrUndefined : GraphException
    {
        public GraphIsNotBipartiteOrUndefined() { }

        public GraphIsNotBipartiteOrUndefined(string message) : base(message) { }

        public GraphIsNotBipartiteOrUndefined(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Graph exception
    /// Can't create line because 0 edges
    /// </summary>
    public class GraphLineGraphInvalidCountOfEdges : GraphException
    {
        public GraphLineGraphInvalidCountOfEdges() { }

        public GraphLineGraphInvalidCountOfEdges(string message) : base(message) { }

        public GraphLineGraphInvalidCountOfEdges(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// (Colored) Graph exception
    /// Colored graph is not initialized
    /// </summary>
    public class ColoredGraphNotInitializationException : GraphException
    {
        public ColoredGraphNotInitializationException() { }

        public ColoredGraphNotInitializationException(string message) : base(message) { }

        public ColoredGraphNotInitializationException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// (Colored) Graph Exception
    /// Reinitialization of a colored graph
    /// </summary>
    public class ColoredGraphAlreadyInitializedException : GraphException
    {
        public ColoredGraphAlreadyInitializedException() { }

        public ColoredGraphAlreadyInitializedException(string message) : base(message) { }

        public ColoredGraphAlreadyInitializedException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// (Colored) Graph Exception
    /// Saturation has not been initialized (saturation  = false)
    /// </summary>
    public class ColoredGraphNotInitializationSaturation : GraphException
    {
        public ColoredGraphNotInitializationSaturation() { }

        public ColoredGraphNotInitializationSaturation(string message) : base(message) { }

        public ColoredGraphNotInitializationSaturation(string message, Exception inner) : base(message, inner) { }
    }
}
