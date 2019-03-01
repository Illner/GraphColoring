using System;
using System.Linq;
using System.Collections.Generic;

namespace GraphColoring.Graph.GraphProperty
{
    partial class GraphProperty
    {
        // Variable
        #region
        /// <summary>
        /// componentsList - List of connected components
        /// </summary>
        private List<IGraphInterface> componentsList;
        private bool isInitializedComponent;
        #endregion

        // Method
        #region
        /// <summary>
        /// Partition a graph to connected components
        /// countComponents, componentsList
        /// BFS
        /// Time complexity: O(V + E)
        /// Space complexity: O(V + E) + new graphs
        /// </summary>
        private void Components()
        {
            // Variable
            int componentNumber = 0;
            Dictionary<IVertexInterface, int> allVerticesDictionary = graph.AllVertices().ToDictionary(x => x, x => 0);

            /* Invalid graph
            if (graph.GetRealCountVertices() == 0)
            {
                countComponents = 1;
                componentsList = new List<IGraphInterface>(componentNumber);
                componentsList.Add(graph);
                return;
            }
            */
            
            isInitializedComponent = true;

            // Core algorithm
            for (int i = 0; i < allVerticesDictionary.Count; i++)
            {
                KeyValuePair<IVertexInterface, int> record = allVerticesDictionary.ElementAt(i);

                if (record.Value != 0)
                    continue;

                ComponentsBFS(record.Key, allVerticesDictionary, ++componentNumber);
            }

            countComponents = componentNumber;

            // Create graphs
            componentsList = new List<IGraphInterface>(componentNumber);

            if (countComponents == 1)
            {
                componentsList.Add(graph);
                return;
            }

            for (int i = 1; i <= componentNumber; i++)
            {
                var vertexComponent = from record in allVerticesDictionary
                                      where record.Value == i
                                      select record.Key;

                GraphEdgeList graphComponent = new GraphEdgeList(vertexComponent.Count());
                graphComponent.SetName(graph.GetName());

                // Add edges
                foreach (IVertexInterface vertex1 in vertexComponent)
                {
                    List<IVertexInterface> neighboursVertexList = graph.Neighbours(vertex1);

                    if (neighboursVertexList.Count() == 0)
                    {
                        graphComponent.AddVertex(vertex1.GetUserName());
                        continue;
                    }

                    foreach (IVertexInterface vertex2 in neighboursVertexList)
                    {
                        graphComponent.AddEdge(vertex1.GetUserName(), vertex2.GetUserName());
                    }
                }

                graphComponent.InitializeGraph();
                componentsList.Add(graphComponent);
            }
        }

        private void ComponentsBFS(IVertexInterface root, Dictionary<IVertexInterface, int> vertexDictionary, int componentNumber)
        {
            // Variable
            Queue<IVertexInterface> vertexQueue = new Queue<IVertexInterface>();
            List<IVertexInterface> neighboursVertexList;
            IVertexInterface dequeuedVertex;

            vertexQueue.Enqueue(root);
            vertexDictionary[root] = componentNumber;

            while (vertexQueue.Count != 0)
            {
                dequeuedVertex = vertexQueue.Dequeue();

                neighboursVertexList = graph.Neighbours(dequeuedVertex);

                foreach (IVertexInterface vertex in neighboursVertexList)
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
        /// Return a list of connected components
        /// </summary>
        /// <returns>list of connected components</returns>
        public List<IGraphInterface> GetComponents()
        {
            if (componentsList == null)
                Components();

            return componentsList;
        }

        /// <summary>
        /// Return true if a graph has initialized components
        /// </summary>
        /// <returns>true if the graph has initialized components, otherwise false</returns>
        public bool GetIsInitializedComponent()
        #endregion
        {
            return isInitializedComponent;
        }
    }
}
