using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.Graph.GraphProperty
{
    partial class GraphProperty
    {
        // Variable
        #region
        /// <summary>
        /// components - List of connected components
        /// </summary>
        private List<Graph> componentsList;
        #endregion

        // Method
        #region
        /// <summary>
        /// Rozloží graf na komponenty souvislosti
        /// countComponents, componentsList
        /// </summary>
        private void Components()
        {
            // Variable
            int componentNumber = 0;
            Dictionary<Vertex, int> allVerticesDictionary = graph.AllVertices().ToDictionary(x => x, x => 0);

            // Core algorithm
            for (int i = 0; i < allVerticesDictionary.Count; i++)
            {
                KeyValuePair<Vertex, int> record = allVerticesDictionary.ElementAt(i);

                if (record.Value != 0)
                    continue;
                
                ComponentsDFS(record.Key, allVerticesDictionary, ++componentNumber);
            }

            countComponents = componentNumber;

            // Create graphs
            componentsList = new List<Graph>(componentNumber);

            for (int i = 1; i <= componentNumber; i++)
            {
                var vertexComponent = from entry in allVerticesDictionary
                                      where entry.Value == i
                                      select entry.Key;

                GraphEdgeList graphComponent = new GraphEdgeList(vertexComponent.Count());
                graphComponent.SetName(graph.GetName());

                // Add edges
                foreach (Vertex vertex1 in vertexComponent)
                {
                    List<Vertex> neighboursVertexList = graph.Neighbours(vertex1);

                    if (neighboursVertexList.Count() == 0)
                    {
                        graphComponent.AddVertex(vertex1.GetUserName());
                        continue;
                    }

                    foreach (Vertex vertex2 in neighboursVertexList)
                    {
                        graphComponent.AddEdge(vertex1.GetUserName(), vertex2.GetUserName());
                    }
                }

                graphComponent.InitializeGraph();
                componentsList.Add(graphComponent);
            }
        }

        private void ComponentsDFS(Vertex root, Dictionary<Vertex, int> vertexDictionary, int componentNumber)
        {
            // Variable
            Queue<Vertex> vertexQueue = new Queue<Vertex>();
            List<Vertex> neighboursVertexList;
            Vertex dequeuedVertex;

            vertexQueue.Enqueue(root);
            vertexDictionary[root] = componentNumber;

            while (vertexQueue.Count != 0)
            {
                dequeuedVertex = vertexQueue.Dequeue();

                neighboursVertexList = graph.Neighbours(dequeuedVertex);

                foreach (Vertex vertex in neighboursVertexList)
                {
                    if (vertexDictionary[vertex] == 0)
                    {
                        vertexDictionary[vertex] = componentNumber;
                        vertexQueue.Enqueue(vertex);
                    }
                }
            }
        }
        #endregion

        // Property
        #region
        /// <summary>
        /// Vrátí list komponent souvislosti
        /// Lze přepisovat!!!
        /// </summary>
        /// <returns>list komponent souvislosti</returns>
        public List<Graph> GetComponents()
        {
            if (componentsList == null)
                Components();

            return componentsList;
        }
        #endregion
    }
}
