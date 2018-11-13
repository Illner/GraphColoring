using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace GraphColoring.GUI
{
    public partial class GraphColoringForm : Form
    {
        // Variable
        private string path;
        private Thread loadGraphThread;
        private Thread computationThread;
        private Thread visualizationThread;
        private Graph.IGraphInterface graph;
        private List<Graph.IGraphInterface> graphList;
        private Graph.IColoredGraphInterface coloredGraph;
        private List<Graph.IColoredGraphInterface> coloredGraphList;
        private GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum graphColoringAlgorithmEnum;
        private List<GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum> algorithmListBoxList;

        public GraphColoringForm()
        {
            InitializeComponent();

            // Fill algorithmListBox
            algorithmListBoxList = new List<GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum>();
            GenerateGraph.ErdosRenyiModel.ErdosRenyiModel erdosRenyiModel = new GenerateGraph.ErdosRenyiModel.ErdosRenyiModel(1);
            Graph.IGraphInterface graph = erdosRenyiModel.GenerateGraph();

            foreach (GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum enumAlgorithm in Enum.GetValues(typeof(GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum)))
            {
                GraphColoringAlgorithm.IGraphColoringAlgorithmInterface algorithm = InitializeGraphColoringAlgorithm(enumAlgorithm, graph);

                if (algorithm != null)
                {
                    algorithmListBox.Items.Add(algorithm);
                    algorithmListBoxList.Add(enumAlgorithm);
                }
            }
        }

        /// <summary>
        /// Create instance of algorithm
        /// If enum isn't implemented in switch throw AlgorithmDoesntExist
        /// </summary>
        /// <param name="graphColoringAlgorithmEnum">type of algorithm - enum</param>
        /// <param name="graph">the graph</param>
        /// <returns>instance of algorithm</returns>
        private GraphColoringAlgorithm.IGraphColoringAlgorithmInterface InitializeGraphColoringAlgorithm(GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum graphColoringAlgorithmEnum, Graph.IGraphInterface graph)
        {
            GraphColoringAlgorithm.GraphColoringAlgorithm graphColoringAlgorithm = null;

            switch (graphColoringAlgorithmEnum)
            {
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.combinationAlgorithm:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.CombinationAlgorithm.CombinationAlgorithm(graph);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedSequential:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.CombinationAlgorithm.CombinationAlgorithm(graph);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgortihm:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graph);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.greedyIndependentSet:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.GreedyIndependentSet.GreedyIndependentSet(graph);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequence:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchange:
                    graphColoringAlgorithm = null; // TODO
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.optimal:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.Optimal.Optimal(graph);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.probabilityAlgorithm:
                    graphColoringAlgorithm = null; // TODO
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchange:
                    graphColoringAlgorithm = null; // TODO
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.saturationLargestFirstSequence:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.SaturationLargestFirstSequence.SaturationLargestFirstSequence(graph);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequence:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchange:
                    graphColoringAlgorithm = null; // TODO
                    break;
                default:
                    throw new MyException.GraphColoringAlgorithmException.AlgorithmDoesntExist(graphColoringAlgorithmEnum.ToString());
            }

            return graphColoringAlgorithm;
        }

        /// <summary>
        /// Visualizate the graph
        /// New thread (visualizationThread)
        /// </summary>
        private void ShowGraph()
        {
            GraphVisualization.GraphVisualization graphVisualization = new GraphVisualization.GraphVisualization(graphList);

            // Thread
            visualizationThread = new Thread(new ThreadStart(graphVisualization.CreateGraphVisualization));
            visualizationThread.IsBackground = true;
            visualizationThread.Start();
            visualizationThread.Join();
            
            drawGraphPictureBox.Image = graphVisualization.GetImage();
        }

        /// <summary>
        /// Change text in StatusLabel
        /// </summary>
        /// <param name="text">The text</param>
        private void SetStatusLabel(string text)
        {
            statusStatusLabel.Text = text;
        }

        // Events
        private void loadGraphButton_Click(object sender, EventArgs e)
        {
            // Variable
            ReaderWriter.IReaderGraphInterface reader;

            // Dialog
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Graph (*.graph)|*.graph";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    path = openFileDialog.FileName;
                }
                // Path wasn't choosen
                else
                    return;
            }

            try
            {
                reader = new ReaderWriter.ReaderGraph(path);
                graph = reader.ReadFile();
                coloredGraph = graph.GetColoredGraph();

                // Component
                loadGraphThread = new Thread(() => { graphList = graph.GetGraphProperty().GetComponents(); });
                loadGraphThread.IsBackground = true;
                loadGraphThread.Start();
                loadGraphThread.Join();

                coloredGraphList = new List<Graph.IColoredGraphInterface>();
                foreach(Graph.IGraphInterface graph in graphList)
                {
                    coloredGraphList.Add(graph.GetColoredGraph());
                }
                
                ShowGraph();

                SetStatusLabel("The file has been loaded.");
            }
            catch (MyException.ReaderWriterException.ReaderWriterInavalidPathException ex)
            {
                ShowMessageBox("Error | Invalid path", "Path doesn't exist! The file has not been loaded!");
                SetStatusLabel("The file has not been loaded.");
                path = null;

                Console.WriteLine(ex);
            }
            catch (Exception ex)
            {
                if (ex is MyException.ReaderWriterException.ReaderWriterException || ex is MyException.GraphException.GraphException)
                {
                    ShowMessageBox("Error | Invalid file", "Invalid format of data. \n" + e.GetType().Name);
                    SetStatusLabel("The file has not been loaded.");
                    path = null;
                }
                else
                    ;

                Console.WriteLine(ex);
            }
        }

        private void colorGraphPlanScheduleButton_Click(object sender, EventArgs e)
        {
            if (graph == null)
            {
                ShowMessageBox("Error | No graph loaded", "Please load a file with graph which you want to color!");
                SetStatusLabel("The file has not been loaded.");
                return;
            }

            if (algorithmListBox.SelectedIndex == -1)
            {
                ShowMessageBox("Error | No selected algorithm", "An algorithm is not selected. Please select some!");
                SetStatusLabel("The file has not been loaded.");
                return;
            }

            try
            {
                // Reset colored graph
                foreach (Graph.IColoredGraphInterface coloredGraph in coloredGraphList)
                {
                    if (coloredGraph.GetIsInicializedColoredGraph())
                        coloredGraph.DeinicializationColoredGraph();
                }

                // Algorithm
                graphColoringAlgorithmEnum = algorithmListBoxList[algorithmListBox.SelectedIndex];
                computationThread = new Thread(() =>
                {
                    graphList.ForEach(graph =>
                    {
                        GraphColoringAlgorithm.IGraphColoringAlgorithmInterface algorithm = InitializeGraphColoringAlgorithm(graphColoringAlgorithmEnum, graph);
                        algorithm.Color();
                    });
                });
                computationThread.IsBackground = true;
                computationThread.Start();
                computationThread.Join();

                ShowGraph();

                SetStatusLabel("The graph has been colored!");
            }
            catch (Exception ex)
            {
                if (ex is MyException.GraphException.GraphException || ex is MyException.GraphColoringAlgorithmException.GraphColoringAlgorithmException)
                {
                    ShowMessageBox("Error | Something wrong", "Something wrong. The graph isn't colored! Try it again! \n" + ex.GetType().Name);
                    SetStatusLabel("The file has not been loaded.");
                }
                else
                    ;

                Console.WriteLine(ex);
            }
        }
        
        private void saveGraphButton_Click(object sender, EventArgs e)
        {
            if (graph == null)
            {
                ShowMessageBox("Error | No graph loaded", "Please load a file with graph which you want to color!");
                SetStatusLabel("The file has not been loaded.");
                return;
            }

            if (!coloredGraph.IsValidColored())
            {
                ShowMessageBox("Error | No colored graph", "Graph isn't colored! Please color graph!");
                SetStatusLabel("The file has not been loaded.");
                return;
            }

            // Variable
            ReaderWriter.IWriterGraphInterface writer = new ReaderWriter.WriterGraph(path);

            try
            {
                writer.WriteFile(graph, graphColoringAlgorithmEnum, false);

                ShowMessageBox("Saved graph", "Colored graph has been saved!");
                SetStatusLabel("Colored graph has been saved.");
            }
            catch (MyException.ReaderWriterException.ReaderWriterInavalidPathException ex)
            {
                ShowMessageBox("Error | Invalid path", "Path doesn't exist! The graph has not been saved! \n" + ex.GetType().Name);
                Console.WriteLine(ex);
            }
            catch (MyException.ReaderWriterException.ReaderWriterException ex)
            {
                ShowMessageBox("Error | Something wrong", "Something wrong. The colored graph has not been saved! Try it again! \n" + ex.GetType().Name);
                Console.WriteLine(ex);
            }
        }

        /// <summary>
        /// Show a messageBox
        /// </summary>
        /// <param name="caption">The caption</param>
        /// <param name="message">The text</param>
        /// <param name="messageBoxButtons">Type of button. Default is OK</param>
        private void ShowMessageBox(string caption, string message, MessageBoxButtons messageBoxButtons = MessageBoxButtons.OK)
        {
            MessageBox.Show(message, caption, messageBoxButtons);
        }
    }
}
