using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphColoring.Graph
{
    abstract partial class Graph
    {
        // Method
        #region
        /// <summary>
        /// Přidá vrchol do grafu
        /// ColoredGraph se deinicializuje
        /// Pokud vrchol již v grafu existuje, vyvolá výjimku GraphVertexAlreadyExistsException
        /// </summary>
        /// <param name="vertex">vrchol, který chceme přidat</param>
        public void VertexAdd(Vertex vertex)
        {
            // Vertex exists
            if (ExistsVertex(vertex))
                throw new MyException.GraphVertexAlreadyExistsException();

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
            if (coloredGraph.GetIsInicializedColoredGraph())
                coloredGraph.DeinicializationColoredGraph();

            // GraphProperty
            GetGraphProperty().Reset();
        }

        /// <summary>
        /// Odstraní vrchol z grafu
        /// ColoredGraph se deinicializuje
        /// Pokud vrchol neexistuje, vyvolá se výjimka GraphVertexDoesntExistException
        /// Time complexity: O(V + E)
        /// </summary>
        /// <param name="removeVertex">vrchol, který chceme odstranit</param>
        public void VertexDelete(Vertex removeVertex)
        {
            // Vertex doesn't exist
            if (!ExistsVertex(removeVertex))
                throw new MyException.GraphVertexDoesntExistException();

            // Variable
            int count = 0;
            HashSet<Vertex> removeVertexList = new HashSet<Vertex>();

            adjacencyList.Remove(ConvertVertexToVertexExtended(removeVertex));

            foreach (List<VertexExtended> vertexExtendedList in adjacencyList.Values)
            {
                foreach (VertexExtended vertexExtended in vertexExtendedList)
                {
                    if (vertexExtended.Equals(removeVertex))
                        removeVertexList.Add(vertexExtended);
                }
                count += removeVertexList.Count;

                vertexExtendedList.RemoveAll(x => removeVertexList.Contains(x));
                removeVertexList.Clear();
            }

            mapping.Remove(removeVertex.GetIdentifier());

            DecrementRealCountVertices();
            
            SetCanDeIncreaseCountVertices(true);
            GetGraphProperty().DecrementCountVertices();
            SetCanDeIncreaseCountVertices(false);

            SetCanDeIncreaseCountEdges(true);
            GetGraphProperty().DecrementCountEdges(count);
            SetCanDeIncreaseCountEdges(false);

            // ColoredGraph
            coloredGraph.RemoveVertexInHashSets(removeVertex);
            if (coloredGraph.GetIsInicializedColoredGraph())
                coloredGraph.DeinicializationColoredGraph();

            // GraphProperty
            GetGraphProperty().Reset();
        }

        /// <summary>
        /// Kontrahuje vrchol v grafu
        /// ColoredGraph se deinicializuje
        /// Pokud vrchol neexistuje, vyvolá výjimku GraphVertexDoesntExistException
        /// </summary>
        /// <param name="vertex">vrchol, který chceme kontrahovat</param>
        public void VertexContract(Vertex vertex)
        {
            if (!ExistsVertex(vertex))
                throw new MyException.GraphVertexDoesntExistException();

            if (CountNeighbours(vertex) == 0)
                return;

            // Variable
            string nameNewVertex;
            List<Vertex> neighboursVertexList, neighboursVertexUnionList, neighboursList;
            
            neighboursVertexUnionList = new List<Vertex>();

            neighboursList = Neighbours(vertex);
            VertexDelete(vertex);

            nameNewVertex = vertex.GetUserName();
            foreach(Vertex neighbour in neighboursList)
            {
                nameNewVertex += neighbour.GetUserName();
            }

            Vertex newVertex = new Vertex(nameNewVertex);

            foreach(Vertex neighbour in neighboursList)
            {
                neighboursVertexList = Neighbours(neighbour);

                neighboursVertexUnionList = neighboursVertexUnionList.Union(neighboursVertexList).ToList();
            }

            neighboursVertexUnionList = neighboursVertexUnionList.Except(neighboursList).ToList();

            // Delete vertices
            foreach (Vertex removeVertex in neighboursList)
            {
                VertexDelete(removeVertex);

                // ColoredGraph
                coloredGraph.RemoveVertexInHashSets(removeVertex);
            }

            // Add vertex
            VertexAdd(newVertex);
            // ColoredGraph
            coloredGraph.AddVertexInHashSets(newVertex);

            // Add edges
            foreach (Vertex neighbour in neighboursVertexUnionList)
                EdgeAdd(new Edge(newVertex, neighbour));

            // ColoredGraph
            if (coloredGraph.GetIsInicializedColoredGraph())
                coloredGraph.DeinicializationColoredGraph();

            // GraphProperty
            GetGraphProperty().Reset();
        }

        /// <summary>
        /// Odstraní vrchol z grafu, kde vrchol je stupne 2
        /// ColoredGraph se deinicializuje
        /// Pokud vrchol neexistuje, vyvolá výjimku GraphVertexDoesntExistException
        /// Pokud vrchol má jiny stupen nez 2, vyvolá výjimku GraphInvalidDegreeVertex
        /// </summary>
        /// <param name="vertex">Daný vrchol</param>
        public void VertexSuppression(Vertex vertex)
        {
            if (!ExistsVertex(vertex))
                throw new MyException.GraphVertexDoesntExistException();

            if (CountNeighbours(vertex) != 2)
                throw new MyException.GraphInvalidDegreeVertex();

            // Variable
            Vertex vertex1, vertex2;

            vertex1 = Neighbours(vertex).First();
            vertex2 = Neighbours(vertex).Last();

            VertexDelete(vertex);
            // ColoredGraph
            coloredGraph.RemoveVertexInHashSets(vertex);

            EdgeAdd(new Edge(vertex1, vertex2));

            // ColoredGraph
            if (coloredGraph.GetIsInicializedColoredGraph())
                coloredGraph.DeinicializationColoredGraph();

            // GraphProperty
            GetGraphProperty().Reset();
        }

        /// <summary>
        /// Expanduje daný vrchol v grafu.
        /// Daný vrchol se rozdělí na dva vrcholy. Dva nově vzniklé vrcholy budou sousedit se všemi sousedy původního vrcholu.
        /// ColoredGraph se deinicializuje
        /// Pokud vrchol neexistuje, vyvolá výjimku GraphVertexDoesntExistException
        /// </summary>
        /// <param name="vertex">vrchol, který chceme expandovat</param>
        public void VertexExpansion(Vertex vertex)
        {
            if (!ExistsVertex(vertex))
                throw new MyException.GraphVertexDoesntExistException();

            // Variable
            Vertex vertex1, vertex2;
            List<Vertex> neighboursList;

            neighboursList = Neighbours(vertex);

            vertex1 = new Vertex(vertex.GetUserName() + " (1)");
            vertex2 = new Vertex(vertex.GetUserName() + " (2)");
            VertexDelete(vertex);
            // ColoredGraph
            coloredGraph.RemoveVertexInHashSets(vertex);

            VertexAdd(vertex1);
            VertexAdd(vertex2);
            // ColoredGraph
            coloredGraph.AddVertexInHashSets(vertex1);
            coloredGraph.AddVertexInHashSets(vertex2);

            foreach(Vertex neighbour in neighboursList)
            {
                EdgeAdd(new Edge(vertex1, neighbour));
                EdgeAdd(new Edge(vertex2, neighbour));
            }

            EdgeAdd(new Edge(vertex1, vertex2));

            // ColoredGraph
            if (coloredGraph.GetIsInicializedColoredGraph())
                coloredGraph.DeinicializationColoredGraph();

            // GraphProperty
            GetGraphProperty().Reset();
        }

        /// <summary>
        /// Přidá hranu do grafu
        /// ColoredGraph se deinicializuje
        /// Pokud hrana v grafu již existuje, vyvolá se výjimka GraphEdgeAlreadyExistsException
        /// </summary>
        /// <param name="edge">hrana, kterou chceme přidat</param>
        public void EdgeAdd(Edge edge)
        {
            if (ExistsEdge(edge))
                throw new MyException.GraphEdgeAlreadyExistsException();

            AddEdgeToAdjacencyList(edge);

            // ColoredGraph
            if (coloredGraph.GetIsInicializedColoredGraph())
                coloredGraph.DeinicializationColoredGraph();

            // GraphProperty
            GetGraphProperty().Reset();
        }

        /// <summary>
        /// Odstraní hranu v grafu
        /// ColoredGraph se deinicializuje
        /// Pokud hrana v grafu neexistuje, vyvolá se výjimka GraphEdgeDoesntExistException
        /// </summary>
        /// <param name="edge">hrana, kterou chceme odstranit</param>
        public void EdgeDelete(Edge edge)
        {
            if (!ExistsEdge(edge))
                throw new MyException.GraphEdgeDoesntExistException();

            // Variable
            adjacencyList.TryGetValue(ConvertVertexToVertexExtended(edge.GetVertex1()), out List<VertexExtended> neighbooursList);
            neighbooursList.Remove(ConvertVertexToVertexExtended(edge.GetVertex2()));

            adjacencyList.TryGetValue(ConvertVertexToVertexExtended(edge.GetVertex2()), out neighbooursList);
            neighbooursList.Remove(ConvertVertexToVertexExtended(edge.GetVertex1()));

            SetCanDeIncreaseCountEdges(true);
            GetGraphProperty().DecrementCountEdges();
            SetCanDeIncreaseCountEdges(false);

            // ColoredGraph
            if (coloredGraph.GetIsInicializedColoredGraph())
                coloredGraph.DeinicializationColoredGraph();

            // GraphProperty
            GetGraphProperty().Reset();
        }

        /// <summary>
        /// Kontrahuje hranu v grafu
        /// ColoredGraph se deinicializuje
        /// Pokud hrana v grafu neexistuje, vyvolá se výjimka GraphEdgeDoesntExistException
        /// </summary>
        /// <param name="edge">hrana, kterou chceme kontrahovat</param>
        public void EdgeContract(Edge edge)
        {
            if (!ExistsEdge(edge))
                throw new MyException.GraphEdgeDoesntExistException();

            // Variable
            int neighbours1Count, neighbours2Count, neighboursCount;
            List<Vertex> neighboursVertex1List, neighboursVertex2List, neighboursList;

            EdgeDelete(edge);

            neighboursVertex1List = Neighbours(edge.GetVertex1());
            neighboursVertex2List = Neighbours(edge.GetVertex2());
            neighbours1Count = neighboursVertex1List.Count;
            neighbours2Count = neighboursVertex2List.Count;

            neighboursList = neighboursVertex1List.Union(neighboursVertex2List).ToList();
            neighboursCount = neighboursList.Count;

            Vertex newVertex = new Vertex(edge.GetVertex1().GetUserName() + edge.GetVertex2().GetUserName());

            // Delete vertices
            VertexDelete(edge.GetVertex1());
            VertexDelete(edge.GetVertex2());

            // Add vertex
            VertexAdd(newVertex);

            // Add edges
            foreach (Vertex neighbour in neighboursList)
                EdgeAdd(new Edge(newVertex, neighbour));

            // ColoredGraph
            if (coloredGraph.GetIsInicializedColoredGraph())
                coloredGraph.DeinicializationColoredGraph();

            // GraphProperty
            GetGraphProperty().Reset();
        }

        /// <summary>
        /// Dana hrana se nahradi cestou delky 2
        /// ColoredGraph se deinicializuje
        /// Pokud hrana v grafu neexistuje, vrátí výjimku GraphEdgeDoesntExistException
        /// </summary>
        /// <param name="edge">daná hrana</param>
        public void EdgeSubdivision(Edge edge)
        {
            if (!ExistsEdge(edge))
                throw new MyException.GraphEdgeDoesntExistException();

            EdgeDelete(edge);

            Vertex newVertex = new Vertex();

            VertexAdd(newVertex);
            EdgeAdd(new Edge(edge.GetVertex1(), newVertex));
            EdgeAdd(new Edge(newVertex, edge.GetVertex2()));

            // ColoredGraph
            if (coloredGraph.GetIsInicializedColoredGraph())
                coloredGraph.DeinicializationColoredGraph();

            // GraphProperty
            GetGraphProperty().Reset();
        }
        #endregion
    }
}
