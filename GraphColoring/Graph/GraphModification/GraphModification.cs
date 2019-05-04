using System;
using System.Linq;
using System.Collections.Generic;

namespace GraphColoring.Graph
{
    abstract partial class Graph
    {
        // Method
        #region
        /// <summary>
        /// Add a new vertex
        /// Deinitialize colored graph
        /// If the vertex exists in the graph throws GraphVertexAlreadyExistsException
        /// </summary>
        /// <param name="vertex">vertex</param>
        public void VertexAdd(IVertexInterface vertex)
        {
            // Vertex exists
            if (ExistsVertex(vertex) || ExistsUserName(vertex.GetUserName()))
                throw new MyException.GraphException.GraphVertexAlreadyExistsException();

            VertexExtended vertexExtended = new VertexExtended(vertex.GetIdentifier());
            vertexExtended.SetColor(vertex.GetColor());
            vertexExtended.SetUserName(vertex.GetUserName());

            SetCanDeIncreaseCountVertices(true);
            GetGraphProperty().IncrementCountVertices();
            SetCanDeIncreaseCountVertices(false);

            AddVertexToAdjacencyList(vertexExtended);
            vertex = vertexExtended;

            // ColoredGraph
            coloredGraph.AddVertexInHashSets(vertex);
            if (coloredGraph.GetIsInitializedColoredGraph())
                coloredGraph.DeinitializationColoredGraph();

            // GraphProperty
            GetGraphProperty().VertexAdd(vertex);
        }

        /// <summary>
        /// Remove a vertex
        /// Deinitialize colored graph
        /// If the vertex does not exist in the graph throws GraphVertexDoesntExistException
        /// If the graph has only one vertex throws GraphHasToHaveAtLeastOneVertexException
        /// Time complexity: O(V + E)
        /// </summary>
        /// <param name="removeVertex">vertex</param>
        public void VertexDelete(IVertexInterface removeVertex)
        {
            // Vertex doesn't exist
            if (!ExistsVertex(removeVertex))
                throw new MyException.GraphException.GraphVertexDoesntExistException();
            
            /*
            if (GetGraphProperty().GetCountVertices() == 1)
                throw new MyException.GraphException.GraphHasToHaveAtLeastOneVertexException();
            */

            // Variable
            int count = 0;
            HashSet<IVertexInterface> removeVertexList = new HashSet<IVertexInterface>();

            adjacencyList.Remove(ConvertVertexToVertexExtended(removeVertex));

            foreach (HashSet<VertexExtended> vertexExtendedHashSet in adjacencyList.Values)
            {
                foreach (VertexExtended vertexExtended in vertexExtendedHashSet)
                {
                    if (vertexExtended.Equals(removeVertex))
                    {
                        removeVertexList.Add(vertexExtended);
                        break;
                    }
                }
                count += removeVertexList.Count;

                vertexExtendedHashSet.RemoveWhere(x => removeVertexList.Contains(x));
                removeVertexList.Clear();
            }

            mapping.Remove(removeVertex.GetIdentifier());
            mappingUserName.Remove(removeVertex.GetUserName());

            DecrementRealCountVertices();
            
            SetCanDeIncreaseCountVertices(true);
            GetGraphProperty().DecrementCountVertices();
            SetCanDeIncreaseCountVertices(false);

            SetCanDeIncreaseCountEdges(true);
            GetGraphProperty().DecrementCountEdges(count);
            SetCanDeIncreaseCountEdges(false);

            // ColoredGraph
            coloredGraph.RemoveVertexInHashSets(removeVertex);
            if (coloredGraph.GetIsInitializedColoredGraph())
                coloredGraph.DeinitializationColoredGraph();

            // GraphProperty
            GetGraphProperty().VertexDelete(removeVertex);
        }

        /// <summary>
        /// Contract a vertex
        /// Deinitialize colored graph
        /// If the vertex does not exist in the graph throws GraphVertexDoesntExistException
        /// </summary>
        /// <param name="vertex">vertex</param>
        public void VertexContraction(IVertexInterface vertex)
        {
            if (!ExistsVertex(vertex))
                throw new MyException.GraphException.GraphVertexDoesntExistException();

            if (CountNeighbours(vertex) == 0)
                return;

            // Variable
            string nameNewVertex;
            List<IVertexInterface> neighboursVertexList, neighboursVertexUnionList, neighboursList;
            
            neighboursVertexUnionList = new List<IVertexInterface>();

            neighboursList = Neighbours(vertex);
            VertexDelete(vertex);

            nameNewVertex = vertex.GetUserName();
            foreach(IVertexInterface neighbour in neighboursList)
            {
                nameNewVertex += neighbour.GetUserName();
            }

            IVertexInterface newVertex = new Vertex(nameNewVertex);

            foreach(IVertexInterface neighbour in neighboursList)
            {
                neighboursVertexList = Neighbours(neighbour);

                neighboursVertexUnionList = neighboursVertexUnionList.Union(neighboursVertexList).ToList();
            }

            neighboursVertexUnionList = neighboursVertexUnionList.Except(neighboursList).ToList();

            // Delete vertices
            foreach (IVertexInterface removeVertex in neighboursList)
            {
                VertexDelete(removeVertex);

                // ColoredGraph
                // coloredGraph.RemoveVertexInHashSets(removeVertex);
            }
            
            // Add vertex
            VertexAdd(newVertex);

            // ColoredGraph
            coloredGraph.AddVertexInHashSets(newVertex);

            // Add edges
            foreach (IVertexInterface neighbour in neighboursVertexUnionList)
                EdgeAdd(new Edge(newVertex, neighbour));

            // ColoredGraph
            if (coloredGraph.GetIsInitializedColoredGraph())
                coloredGraph.DeinitializationColoredGraph();

            // GraphProperty
            GetGraphProperty().VertexContraction(vertex);
        }

        /// <summary>
        /// Remove a vertex which has degree 2
        /// Deinitialize colored graph
        /// If the vertex does not exist in the graph throws GraphVertexDoesntExistException
        /// If the vertex does not have degree 2 throws GraphInvalidDegreeVertex
        /// </summary>
        /// <param name="vertex">vertex</param>
        public void VertexSuppression(IVertexInterface vertex)
        {
            if (!ExistsVertex(vertex))
                throw new MyException.GraphException.GraphVertexDoesntExistException();

            if (CountNeighbours(vertex) != 2)
                throw new MyException.GraphException.GraphInvalidDegreeVertex();

            // Variable
            IVertexInterface vertex1, vertex2;

            vertex1 = Neighbours(vertex).First();
            vertex2 = Neighbours(vertex).Last();

            VertexDelete(vertex);

            // ColoredGraph
            //coloredGraph.RemoveVertexInHashSets(vertex);

            if(!ExistsEdge(new Edge(vertex1, vertex2)))
                EdgeAdd(new Edge(vertex1, vertex2));

            // ColoredGraph
            if (coloredGraph.GetIsInitializedColoredGraph())
                coloredGraph.DeinitializationColoredGraph();

            // GraphProperty
            GetGraphProperty().VertexSuppression(vertex);
        }

        /// <summary>
        /// Expand a vertex
        /// Deinitialize colored graph
        /// If the vertex does not exist in the graph throws GraphVertexDoesntExistException
        /// </summary>
        /// <param name="vertex">vertex</param>
        public void VertexExpansion(IVertexInterface vertex)
        {
            if (!ExistsVertex(vertex))
                throw new MyException.GraphException.GraphVertexDoesntExistException();

            // Variable
            IVertexInterface vertex1, vertex2;
            List<IVertexInterface> neighboursList;

            neighboursList = Neighbours(vertex);
            
            vertex1 = new Vertex(vertex.GetUserName() + " (1)");
            vertex2 = new Vertex(vertex.GetUserName() + " (2)");

            VertexAdd(vertex1);
            VertexAdd(vertex2);

            VertexDelete(vertex);

            // ColoredGraph
            //coloredGraph.RemoveVertexInHashSets(vertex);

            // ColoredGraph
            coloredGraph.AddVertexInHashSets(vertex1);
            coloredGraph.AddVertexInHashSets(vertex2);

            foreach(IVertexInterface neighbour in neighboursList)
            {
                EdgeAdd(new Edge(vertex1, neighbour));
                EdgeAdd(new Edge(vertex2, neighbour));
            }

            EdgeAdd(new Edge(vertex1, vertex2));

            // ColoredGraph
            if (coloredGraph.GetIsInitializedColoredGraph())
                coloredGraph.DeinitializationColoredGraph();

            // GraphProperty
            GetGraphProperty().VertexExpansion(vertex);
        }

        /// <summary>
        /// Add an edge
        /// Deinitialize colored graph
        /// If the edge exists in the graph throws GraphEdgeAlreadyExistsException
        /// </summary>
        /// <param name="edge">edge</param>
        public void EdgeAdd(IEdgeInterface edge)
        {
            if (ExistsEdge(edge))
                throw new MyException.GraphException.GraphEdgeAlreadyExistsException();
            
            AddEdgeToAdjacencyList(edge);

            // ColoredGraph
            if (coloredGraph.GetIsInitializedColoredGraph())
                coloredGraph.DeinitializationColoredGraph();

            // GraphProperty
            GetGraphProperty().EdgeAdd(edge);
        }

        /// <summary>
        /// Remove an edge
        /// Deinitialize colored graph
        /// If the edge does not exist in the graph throws GraphEdgeDoesntExistException
        /// </summary>
        /// <param name="edge">edge</param>
        public void EdgeDelete(IEdgeInterface edge)
        {
            if (!ExistsEdge(edge))
                throw new MyException.GraphException.GraphEdgeDoesntExistException();

            // Variable
            adjacencyList.TryGetValue(ConvertVertexToVertexExtended(edge.GetVertex1()), out HashSet<VertexExtended> neighbooursHashSet);
            neighbooursHashSet.Remove(ConvertVertexToVertexExtended(edge.GetVertex2()));

            adjacencyList.TryGetValue(ConvertVertexToVertexExtended(edge.GetVertex2()), out neighbooursHashSet);
            neighbooursHashSet.Remove(ConvertVertexToVertexExtended(edge.GetVertex1()));

            SetCanDeIncreaseCountEdges(true);
            GetGraphProperty().DecrementCountEdges();
            SetCanDeIncreaseCountEdges(false);

            // ColoredGraph
            if (coloredGraph.GetIsInitializedColoredGraph())
                coloredGraph.DeinitializationColoredGraph();

            // GraphProperty
            GetGraphProperty().EdgeDelete(edge);
        }

        /// <summary>
        /// Contract an edge
        /// Deinitialize coloredGraph
        /// If the edge does not exist in the graph throws GraphEdgeDoesntExistException
        /// </summary>
        /// <param name="edge">edge</param>
        public void EdgeContraction(IEdgeInterface edge)
        {
            if (!ExistsEdge(edge))
                throw new MyException.GraphException.GraphEdgeDoesntExistException();

            // Variable
            int neighbours1Count, neighbours2Count, neighboursCount;
            List<IVertexInterface> neighboursVertex1List, neighboursVertex2List, neighboursList;

            EdgeDelete(edge);

            neighboursVertex1List = Neighbours(edge.GetVertex1());
            neighboursVertex2List = Neighbours(edge.GetVertex2());
            neighbours1Count = neighboursVertex1List.Count;
            neighbours2Count = neighboursVertex2List.Count;

            neighboursList = neighboursVertex1List.Union(neighboursVertex2List).ToList();
            neighboursCount = neighboursList.Count;

            IVertexInterface newVertex = new Vertex(edge.GetVertex1().GetUserName() + edge.GetVertex2().GetUserName());

            // Add vertex
            VertexAdd(newVertex);

            // Delete vertices
            VertexDelete(edge.GetVertex1());
            VertexDelete(edge.GetVertex2());

            // Add edges
            foreach (IVertexInterface neighbour in neighboursList)
                EdgeAdd(new Edge(newVertex, neighbour));

            // ColoredGraph
            if (coloredGraph.GetIsInitializedColoredGraph())
                coloredGraph.DeinitializationColoredGraph();

            // GraphProperty
            GetGraphProperty().EdgeContraction(edge);
        }

        /// <summary>
        /// Subdivide an edge
        /// Deinitialize colored graph
        /// If the edge does not exist in the graph throws GraphEdgeDoesntExistException
        /// </summary>
        /// <param name="edge">edge</param>
        public void EdgeSubdivision(IEdgeInterface edge)
        {
            if (!ExistsEdge(edge))
                throw new MyException.GraphException.GraphEdgeDoesntExistException();

            EdgeDelete(edge);

            IVertexInterface newVertex = new Vertex();

            VertexAdd(newVertex);
            EdgeAdd(new Edge(edge.GetVertex1(), newVertex));
            EdgeAdd(new Edge(newVertex, edge.GetVertex2()));

            // ColoredGraph
            if (coloredGraph.GetIsInitializedColoredGraph())
                coloredGraph.DeinitializationColoredGraph();

            // GraphProperty
            GetGraphProperty().EdgeSubdivision(edge);
        }
        #endregion
    }
}
