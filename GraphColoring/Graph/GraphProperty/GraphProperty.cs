using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphColoring.Graph.GraphProperty
{
    public partial class GraphProperty
    {
        #region Variable
        private Graph graph;
        #endregion
        
        #region Constructor
        public GraphProperty(Graph graph, int countVertices)
        {
            this.graph = graph;
            SetCountVertices(countVertices);
        }
        #endregion
        
        #region Method
        /// <summary>
        /// Reset all graph's properties
        /// Time complexity: O(1)
        /// </summary>
        public void Reset()
        {
            // SequencesPolynomialsOthers
            degreeSequence = null;
            isDegreeSequenceSorted = false;
            spanningTreeBFS = null;
            matching = null;
            cutVertices = null;
            bridges = null;

            // Properties
            isConnected = null;
            isRegular = null;
            isCyclic = null;
            isEulerian = EulerianGraphEnum.undefined;

            // Component
            componentsList = null;

            // IntegralInvariants
            countComponents = null;
            circuitRank = null;
            girth = null;
            minimumVertexDegree = null;
            maximumVertexDegree = null;
            averageVertexDegree = null;
            medianVertexDegree = null;
            cayleysFormula = null;

            // Chordal graph
            isChordal = null;
            perfectEliminationOrderingList = null;
            righNeighborhoodDictionary = null;
        }

        public void VertexAdd(IVertexInterface vertex)
        {
            // Component
            if (componentsList != null)
            {
                if (countComponents > 2)
                {
                    // Create a new component with the vertex
                    IGraphEdgeListInterface newGraph = new GraphEdgeList(1);
                    newGraph.AddVertex(vertex.GetUserName());
                    newGraph.InitializeGraph();

                    componentsList.Add(newGraph);
                    countComponents++;
                    isConnected = false;
                }
                else
                {
                    componentsList = null;
                    countComponents = null;
                    isConnected = null;
                }
            }

            // Properties
            //isCyclic = isCyclic;
            isEulerian = EulerianGraphEnum.notEulerian;
            if (countComponents != order)
                isRegular = false;
            else
                isRegular = true;

            // IntegralInvariants
            //circuitRank = circuitRank;
            //girth = girth;
            cayleysFormula = null;

            // SequencesAndPolynomialsOthers
            spanningTreeBFS = spanningTreeBFS = new List<IEdgeInterface>();
            //matching = matching;
            //cutVertices = cutVertices;
            //bridges = bridges;

            // DegreeSequence
            if (degreeSequence != null)
            {
                if (!isDegreeSequenceSorted)
                {
                    degreeSequence.Add(new KeyValuePair<IVertexInterface, int>(vertex, 0));
                    degreeSequenceVertex.Add(vertex);
                    degreeSequenceInt.Add(0);
                }
                else
                {
                    degreeSequence = null;
                    degreeSequenceVertex = null;
                    degreeSequenceInt = null;
                    isDegreeSequenceSorted = false;
                }

                if (0 < minimumVertexDegree)
                    minimumVertexDegree = 0;

                //maximumVertexDegree = maximumVertexDegree;
                averageVertexDegree = null;
                medianVertexDegree = null;
            }

            // Chordal
            //isChordal = isChordal;
            if (isChordal.HasValue && (bool)isChordal)
            {
                perfectEliminationOrderingList.Add(vertex);
            }

            // GraphClass
            //graphClass = graphClass;
        }

        public void VertexDelete(IVertexInterface vertex)
        {
            // Component
            if (componentsList != null)
            {
                // More components => copies of components
                if (countComponents > 1)
                {
                    // Get component which contains the vertex
                    IVertexInterface myVertexComponent = null;
                    IGraphInterface myComponentGraph = null;
                    foreach (IGraphInterface componentGraph in componentsList)
                    {
                        if (componentGraph.ExistsUserName(vertex.GetUserName()))
                        {
                            myComponentGraph = componentGraph;
                            myVertexComponent = myComponentGraph.GetVertexByUserName(vertex.GetUserName());
                            break;
                        }
                    }

                    // The vertex is component
                    if (myComponentGraph.CountNeighbours(myVertexComponent) == 0)
                    {
                        componentsList.Remove(myComponentGraph);
                        countComponents--;
                        isConnected = countComponents == 1 ? true : false;
                    }
                    // The vertex is in a component with another vertices
                    else
                    {
                        componentsList = null;
                        countComponents = null;
                        isConnected = null;
                    }
                }
                else
                {
                    if (cutVertices != null && !cutVertices.Contains(vertex))
                    {
                        //componentsList = componentsList;
                        //countComponents = countComponents;
                        //isConnected = isConnected;
                    }
                    else
                    {
                        componentsList = null;
                        countComponents = null;
                        isConnected = null;
                    }
                }
            }

            // Properties
            if (isCyclic!= null && (bool)isCyclic)
                isCyclic = null;
            if (isConnected != null && !(bool)isConnected)
                isEulerian = EulerianGraphEnum.notEulerian;
            else
                isEulerian = EulerianGraphEnum.undefined;
            
            // IntegralInvariants
            circuitRank = null;
            girth = null;
            cayleysFormula = null;

            // SequencesAndPolynomialsOthers
            if (isConnected != null && !(bool)isConnected)
                spanningTreeBFS = spanningTreeBFS = new List<IEdgeInterface>();
            else
                spanningTreeBFS = null;
            matching = null;
            cutVertices = null;
            bridges = null;

            // DegreeSequence
            degreeSequence = null;
            maximumVertexDegree = null;
            minimumVertexDegree = null;
            averageVertexDegree = null;
            medianVertexDegree = null;
            isDegreeSequenceSorted = false;
            isRegular = null;

            // Chordal
            isChordal = null;
            perfectEliminationOrderingList = null;
            righNeighborhoodDictionary = null;

            // GraphClass
            graphClass = GraphClass.GraphClass.GraphClassEnum.undefined;
        }

        public void VertexContraction(IVertexInterface vertex)
        {
            // Component
            if (componentsList != null)
            {
                // Need change => copies of component
                if (countComponents > 1)
                {
                    // Get component which contains the vertex
                    IVertexInterface myVertexComponent = null;
                    IGraphInterface myComponentGraph = null;
                    foreach (IGraphInterface componentGraph in componentsList)
                    {
                        if (componentGraph.ExistsUserName(vertex.GetUserName()))
                        {
                            myComponentGraph = componentGraph;
                            myVertexComponent = myComponentGraph.GetVertexByUserName(vertex.GetUserName());
                            break;
                        }
                    }

                    myComponentGraph.VertexContraction(myVertexComponent);
                }

                //componentsList = componentsList;
                //countComponents = countComponents;
                //isConnected = isConnected;
            }

            // Properties
            if (isCyclic != null && (bool)isCyclic)
                isCyclic = null;
            isEulerian = EulerianGraphEnum.undefined;

            // IntegralInvariants
            circuitRank = null;
            if (girth != null && girth > 2)
                girth = null;
            cayleysFormula = null;

            // SequencesAndPolynomialsOthers
            if (isConnected != null && !(bool)isConnected)
                spanningTreeBFS = spanningTreeBFS = new List<IEdgeInterface>();
            else
                spanningTreeBFS = null;
            matching = null;
            cutVertices = null;
            bridges = null;
            
            // DegreeSequence
            degreeSequence = null;
            maximumVertexDegree = null;
            minimumVertexDegree = null;
            averageVertexDegree = null;
            medianVertexDegree = null;
            isDegreeSequenceSorted = false;
            isRegular = null;

            // Chordal
            isChordal = null;
            perfectEliminationOrderingList = null;
            righNeighborhoodDictionary = null;

            // GraphClass
            graphClass = GraphClass.GraphClass.GraphClassEnum.undefined;
        }

        public void VertexSuppression(IVertexInterface vertex)
        {
            // Component
            if (componentsList != null)
            {
                // Need change => copies of component
                if (countComponents > 1)
                {
                    // Get component which contains the vertex
                    IVertexInterface myVertexComponent = null;
                    IGraphInterface myComponentGraph = null;
                    foreach (IGraphInterface componentGraph in componentsList)
                    {
                        if (componentGraph.ExistsUserName(vertex.GetUserName()))
                        {
                            myComponentGraph = componentGraph;
                            myVertexComponent = myComponentGraph.GetVertexByUserName(vertex.GetUserName());
                            break;
                        }
                    }

                    myComponentGraph.VertexSuppression(myVertexComponent);
                }

                //componentsList = componentsList;
                //countComponents = countComponents;
                //isConnected = isConnected;
            }

            // Properties
            if (isCyclic != null && !(bool)isCyclic)
                isCyclic = false;
            else
                isCyclic = null;
            isEulerian = EulerianGraphEnum.notEulerian;
            
            // IntegralInvariants
            circuitRank = null;
            if (girth != null && girth > 2)
                girth = null;
            cayleysFormula = null;

            // SequencesAndPolynomialsOthers
            if (isConnected != null && !(bool)isConnected)
                spanningTreeBFS = spanningTreeBFS = new List<IEdgeInterface>();
            else
                spanningTreeBFS = null;
            matching = null;
            cutVertices = null;
            bridges = null;
            
            // DegreeSequence
            degreeSequence = null;
            maximumVertexDegree = null;
            minimumVertexDegree = null;
            averageVertexDegree = null;
            medianVertexDegree = null;
            isDegreeSequenceSorted = false;
            isRegular = null;

            // Chordal
            if (isChordal.HasValue && (bool)isChordal)
            {
                isChordal = null;
                perfectEliminationOrderingList = null;
                righNeighborhoodDictionary = null;
            }

            // GraphClass
            graphClass = GraphClass.GraphClass.GraphClassEnum.undefined;
        }

        public void VertexExpansion(IVertexInterface vertex)
        {
            // Component
            if (componentsList != null)
            {
                // Need change => copies of component
                if (countComponents > 1)
                {
                    // Get component which contains the vertex
                    IVertexInterface myVertexComponent = null;
                    IGraphInterface myComponentGraph = null;
                    foreach (IGraphInterface componentGraph in componentsList)
                    {
                        if (componentGraph.ExistsUserName(vertex.GetUserName()))
                        {
                            myComponentGraph = componentGraph;
                            myVertexComponent = myComponentGraph.GetVertexByUserName(vertex.GetUserName());
                            break;
                        }
                    }

                    myComponentGraph.VertexExpansion(myVertexComponent);
                }

                //componentsList = componentsList;
                //countComponents = countComponents;
                //isConnected = isConnected;
            }

            // Properties
            isCyclic = null;
            isEulerian = EulerianGraphEnum.undefined;

            // IntegralInvariants
            circuitRank = null;
            girth = null;
            cayleysFormula = null;

            // SequencesAndPolynomialsOthers
            if (isConnected != null && !(bool)isConnected)
                spanningTreeBFS = spanningTreeBFS = new List<IEdgeInterface>();
            else
                spanningTreeBFS = null;
            matching = null;
            cutVertices = null;
            bridges = null;

            // DegreeSequence
            degreeSequence = null;
            maximumVertexDegree = null;
            minimumVertexDegree = null;
            averageVertexDegree = null;
            medianVertexDegree = null;
            isDegreeSequenceSorted = false;
            isRegular = null;

            // Chordal
            if (isChordal.HasValue && (bool)isChordal)
            {
                isChordal = null;
                perfectEliminationOrderingList = null;
                righNeighborhoodDictionary = null;
            }

            // GraphClass
            graphClass = GraphClass.GraphClass.GraphClassEnum.undefined;
        }

        public void EdgeAdd(IEdgeInterface edge)
        {
            // Variable
            IVertexInterface vertex1 = graph.GetVertexByUserName(edge.GetVertex1().GetUserName());
            IVertexInterface vertex2 = graph.GetVertexByUserName(edge.GetVertex2().GetUserName());

            // Component
            if (componentsList != null)
            {
                // Need change => copies of component
                if (countComponents > 1)
                {
                    // Get component which contains the vertex
                    bool inSameComponent = false;
                    IGraphInterface myComponentGraph = null;
                    foreach (IGraphInterface componentGraph in componentsList)
                    {
                        if (componentGraph.ExistsUserName(vertex1.GetUserName()))
                        {
                            if (componentGraph.ExistsUserName(vertex2.GetUserName()))
                            {
                                inSameComponent = true;
                                myComponentGraph = componentGraph;
                            }

                            break;
                        }
                    }

                    if (!inSameComponent)
                    {
                        componentsList = null;
                        countComponents = null;
                        isConnected = null;
                    }
                    else
                    {
                        myComponentGraph.EdgeAdd(new Edge(myComponentGraph.GetVertexByUserName(vertex1.GetUserName()), myComponentGraph.GetVertexByUserName(vertex2.GetUserName())));
                    }
                }
                else
                {
                    //componentsList = componentsList;
                    //countComponents = countComponents;
                    //isConnected = isConnected;
                }
            }

            // Properties
            if (isCyclic != null && !(bool)isCyclic)
                    isCyclic = null;
            if (isEulerian == EulerianGraphEnum.eulerian)
                isEulerian = EulerianGraphEnum.semiEulerian;
            else
                isEulerian = EulerianGraphEnum.undefined;
                
            // IntegralInvariants
            circuitRank = null;
            girth = null;
            //cayleysFormula = cayleysFormula;
                    
            // SequencesAndPolynomialsOthers
            if (isConnected != null && !(bool)isConnected)
                spanningTreeBFS = spanningTreeBFS = new List<IEdgeInterface>();
            else
                spanningTreeBFS = null;
            matching = null;
            cutVertices = null;
            bridges = null;

            // DegreeSequence
            if (degreeSequence != null)
            {
                int countNeighbourVertex1 = graph.CountNeighbours(vertex1);
                int countNeighbourVertex2 = graph.CountNeighbours(vertex2);
                if (!isDegreeSequenceSorted)
                {
                    // Variable
                    int index1, index2;

                    index1 = degreeSequenceInt.FindIndex(i => i == (countNeighbourVertex1 - 1));
                    degreeSequenceInt[index1] = countNeighbourVertex1;
                    index2 = degreeSequenceInt.FindIndex(i => i == (countNeighbourVertex2 - 1));
                    degreeSequenceInt[index2] = countNeighbourVertex2;

                    index1 = degreeSequence.FindIndex(p => p.Key == vertex1);
                    index2 = degreeSequence.FindIndex(p => p.Key == vertex2);
                    degreeSequence[index1] = new KeyValuePair<IVertexInterface, int>(vertex1, countNeighbourVertex1);
                    degreeSequence[index2] = new KeyValuePair<IVertexInterface, int>(vertex2, countNeighbourVertex2);
                }
                else
                {
                    degreeSequence = null;
                    degreeSequenceVertex = null;
                    degreeSequenceInt = null;
                    isDegreeSequenceSorted = false;
                }

                if (maximumVertexDegree.HasValue)
                {
                    if (countNeighbourVertex1 > maximumVertexDegree)
                        maximumVertexDegree = countNeighbourVertex1;
                    if (countNeighbourVertex2 > maximumVertexDegree)
                        maximumVertexDegree = countNeighbourVertex2;
                }

                if (minimumVertexDegree.HasValue)
                {
                    if (((countNeighbourVertex1 - 1) == minimumVertexDegree) ||
                        ((countNeighbourVertex2 - 1) == minimumVertexDegree))
                        minimumVertexDegree = null;
                }

                averageVertexDegree = null;
                medianVertexDegree = null;
                isRegular = null;
            }

            // Chordal
            isChordal = null;
            perfectEliminationOrderingList = null;
            righNeighborhoodDictionary = null;

            // GraphClass
            graphClass = GraphClass.GraphClass.GraphClassEnum.undefined;
        }

        public void EdgeDelete(IEdgeInterface edge)
        {
            // Variable
            IVertexInterface vertex1 = graph.GetVertexByUserName(edge.GetVertex1().GetUserName());
            IVertexInterface vertex2 = graph.GetVertexByUserName(edge.GetVertex2().GetUserName());

            // Component
            if (componentsList != null)
            {
                if (countComponents > 1)
                {
                    // Get component which contains the vertex
                    IGraphInterface myComponentGraph = null;
                    foreach (IGraphInterface componentGraph in componentsList)
                    {
                        if (componentGraph.ExistsUserName(vertex1.GetUserName()))
                        {
                            myComponentGraph = componentGraph;
                            break;
                        }
                    }

                    myComponentGraph.EdgeDelete(new Edge(myComponentGraph.GetVertexByUserName(vertex1.GetUserName()), myComponentGraph.GetVertexByUserName(vertex2.GetUserName())));
                    
                    componentsList = null;
                    countComponents = null;
                    isConnected = null;
                }
                else
                {
                    if (bridges != null)
                    {
                        // The edge is a bridge
                        if (bridges.Any(e => ((e.GetVertex1().Equals(vertex1) && e.GetVertex2().Equals(vertex2)) ||
                                                  (e.GetVertex1().Equals(vertex2) && e.GetVertex2().Equals(vertex1)))))
                        {
                            componentsList = null;
                            countComponents = null;
                            isConnected = null;
                        }
                        // The edge isn't a bridge
                        else
                        {
                            //componentsList = componentsList;
                            //countComponents = countComponents;
                            //isConnected = isConnected;
                        }
                    }
                }
            }

            // Properties
            if (isCyclic != null && !(bool)isCyclic)
                isCyclic = false;
            else
                isCyclic = null;
            if (isEulerian == EulerianGraphEnum.eulerian)
                isEulerian = EulerianGraphEnum.semiEulerian;
            else
                isEulerian = EulerianGraphEnum.undefined;

            // IntegralInvariants
            circuitRank = null;
            if (girth != null && girth > 2)
                girth = null;
            //cayleysFormula = cayleysFormula;

            // SequencesAndPolynomialsOthers
            if (isConnected != null && !(bool)isConnected)
                spanningTreeBFS = spanningTreeBFS = new List<IEdgeInterface>();
            else
                spanningTreeBFS = null;
            matching = null;
            cutVertices = null;
            bridges = null;

            // DegreeSequence
            if (degreeSequence != null)
            {
                int countNeighbourVertex1 = graph.CountNeighbours(vertex1);
                int countNeighbourVertex2 = graph.CountNeighbours(vertex2);
                if (!isDegreeSequenceSorted)
                {
                    // Variable
                    int index1, index2;
                    
                    index1 = degreeSequenceInt.FindIndex(i => i == (countNeighbourVertex1 + 1));
                    degreeSequenceInt[index1] = countNeighbourVertex1;
                    index2 = degreeSequenceInt.FindIndex(i => i == (countNeighbourVertex2 + 1));
                    degreeSequenceInt[index2] = countNeighbourVertex2;

                    index1 = degreeSequence.FindIndex(p => p.Key == vertex1);
                    index2 = degreeSequence.FindIndex(p => p.Key == vertex2);
                    degreeSequence[index1] = new KeyValuePair<IVertexInterface, int>(vertex1, countNeighbourVertex1);
                    degreeSequence[index2] = new KeyValuePair<IVertexInterface, int>(vertex2, countNeighbourVertex2);
                }
                else
                {
                    degreeSequence = null;
                    degreeSequenceVertex = null;
                    degreeSequenceInt = null;
                    isDegreeSequenceSorted = false;
                }

                if (minimumVertexDegree.HasValue)
                {
                    if (countNeighbourVertex1 < minimumVertexDegree)
                        minimumVertexDegree = countNeighbourVertex1;
                    if (countNeighbourVertex2 < minimumVertexDegree)
                        minimumVertexDegree = countNeighbourVertex2;
                }

                if (maximumVertexDegree.HasValue)
                {
                    if (((countNeighbourVertex1 + 1) == maximumVertexDegree) ||
                        ((countNeighbourVertex2 + 1) == maximumVertexDegree))
                        maximumVertexDegree = null;
                }

                averageVertexDegree = null;
                medianVertexDegree = null;
                isRegular = null;
            }

            // Chordal
            isChordal = null;
            perfectEliminationOrderingList = null;
            righNeighborhoodDictionary = null;

            // GraphClass
            graphClass = GraphClass.GraphClass.GraphClassEnum.undefined;
        }

        public void EdgeContraction(IEdgeInterface edge)
        {
            // Component
            if (componentsList != null)
            {
                componentsList = null;
                countComponents = null;
                isConnected = null;
            }

            // Properties
            if (isCyclic != null && !(bool)isCyclic)
                isCyclic = false;
            else
                isCyclic = null;
            isEulerian = EulerianGraphEnum.undefined;

            // IntegralInvariants
            circuitRank = null;
            if (girth != null && girth > 2)
                girth = null;
            cayleysFormula = null;

            // SequencesAndPolynomialsOthers
            if (isConnected != null && !(bool)isConnected)
                spanningTreeBFS = spanningTreeBFS = new List<IEdgeInterface>();
            else
                spanningTreeBFS = null;
            matching = null;
            cutVertices = null;
            bridges = null;

            // DegreeSequence
            degreeSequence = null;
            maximumVertexDegree = null;
            minimumVertexDegree = null;
            averageVertexDegree = null;
            medianVertexDegree = null;
            isDegreeSequenceSorted = false;
            isRegular = null;

            // Chordal
            isChordal = null;
            perfectEliminationOrderingList = null;
            righNeighborhoodDictionary = null;

            // GraphClass
            graphClass = GraphClass.GraphClass.GraphClassEnum.undefined;
        }

        public void EdgeSubdivision(IEdgeInterface edge)
        {
            // Variable
            IVertexInterface vertex1 = graph.GetVertexByUserName(edge.GetVertex1().GetUserName());
            IVertexInterface vertex2 = graph.GetVertexByUserName(edge.GetVertex2().GetUserName());

            // Component
            if (componentsList != null)
            {
                if (countComponents > 1)
                {
                    // Get component which contains the vertex
                    IGraphInterface myComponentGraph = null;
                    foreach (IGraphInterface componentGraph in componentsList)
                    {
                        if (componentGraph.ExistsUserName(vertex1.GetUserName()))
                        {
                            myComponentGraph = componentGraph;
                            break;
                        }
                    }

                    myComponentGraph.EdgeSubdivision(new Edge(myComponentGraph.GetVertexByUserName(vertex1.GetUserName()), myComponentGraph.GetVertexByUserName(vertex2.GetUserName())));
                }
                else
                {
                    //componentsList = componentsList;
                    //countComponents = countComponents;
                    //isConnected = isConnected;
                }
            }

            // Properties
            //isCyclic = isCyclic;
            //isEulerian = isEulerian;

            // IntegralInvariants
            //circuitRank = circuitRank;
            if (girth.HasValue && girth > 2)
                girth = null;
            cayleysFormula = null;

            // SequencesAndPolynomialsOthers
            if (isConnected != null && !(bool)isConnected)
                spanningTreeBFS = spanningTreeBFS = new List<IEdgeInterface>();
            else
                spanningTreeBFS = null;
            matching = null;
            cutVertices = null;
            bridges = null;

            // DegreeSequence
            degreeSequence = null;
            maximumVertexDegree = null;
            minimumVertexDegree = null;
            averageVertexDegree = null;
            medianVertexDegree = null;
            isDegreeSequenceSorted = false;
            isRegular = null;

            // Chordal
            if (isChordal.HasValue && (bool)isChordal)
            {
                isChordal = null;
                perfectEliminationOrderingList = null;
                righNeighborhoodDictionary = null;
            }

            // GraphClass
            graphClass = GraphClass.GraphClass.GraphClassEnum.undefined;
        }

        override
        public string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("Count of vertices: " + GetCountVertices());
            stringBuilder.AppendLine("Count of edges: " + GetCountEdges());

            // Circuit rank
            if (circuitRank.HasValue)
                stringBuilder.AppendLine("Circuit rank: " + GetCircuitRank());
            
            // Component
            if (GetIsInitializedComponent())
            {
                stringBuilder.AppendLine("Count of components: " + GetCountComponents());
                stringBuilder.AppendLine("Is connected: " + GetIsConnected());
            }

            // Graph class
            if (graphClass != GraphClass.GraphClass.GraphClassEnum.undefined)
                stringBuilder.AppendLine("Class: " + GetGraphClass());

            // IsChordal
            if (isChordal.HasValue)
                stringBuilder.AppendLine("Is chordal: " + GetIsChordal());

            // IsRegular
            if (isRegular.HasValue)
                stringBuilder.AppendLine("Is regular: " + GetIsRegular());

            // IsCyclic
            if (isCyclic.HasValue)
                stringBuilder.AppendLine("Is cyclic: " + GetIsCyclic());

            // IsEulerian
            if (isEulerian != EulerianGraphEnum.undefined)
                stringBuilder.AppendLine("Is eulerian: " + GetIsEulerian());

            // Vertex degree
            if (maximumVertexDegree.HasValue)
            {
                stringBuilder.AppendLine("Maximum vertex degree: " + GetMaximumVertexDegree());
                stringBuilder.AppendLine("Minimum vertex degree: " + GetMinimumVertexDegree());
                stringBuilder.AppendLine("Average vertex degree: " + GetAverageVertexDegree());
                stringBuilder.AppendLine("Median vertex degree: " + GetMedianVertexDegree());
            }

            // Cut vertices and bridges
            if (cutVertices != null)
            {
                stringBuilder.AppendLine("Count of cut vertices: " + GetCutVertices().Count);
                stringBuilder.AppendLine("Coutn of bridges: " + GetBridges().Count);
            }

            // Girth
            if (girth.HasValue)
                stringBuilder.AppendLine("Girth: " + girth);

            // Cayleys formula
            if (cayleysFormula.HasValue)
                stringBuilder.AppendLine("Cayleys formula: " + GetCayleysFormula());

            // Simplicial vertex
            if (isChordal.HasValue)
            {
                if (GetIsChordal())
                    stringBuilder.AppendLine("Simplicial vertex: " + GetPerfectEliminationOrdering().Last().GetUserName());
                else
                    stringBuilder.AppendLine("Simplicial vertex: None");
            }
                
            return stringBuilder.ToString();
        }
        #endregion
    }
}