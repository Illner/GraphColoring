using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace GraphColoring.GUI
{
    public partial class GraphColoringForm : Form
    {
        // Variable
        #region
        private string path;
        private int maxUsedColors;
        private Thread coreThread;
        private bool canWrite = true;
        private bool showError = true;
        private Graph.IGraphInterface graph;
        private bool isModifiedGraph = false;
        private GraphVisualizationForm graphVisualizationForm;
        private static string MODIFIEDGRAPHNAME = "Modified graph";
        List<GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum> algorithmListBoxList;
        private GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum graphColoringAlgorithmEnum;
        #endregion

        // Constructor
        #region
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

            // Fill graph density - generated graph
            FillGraphDensityGenerateGraphComboBox();
            /*
            foreach (var density in Enum.GetValues(typeof(GenerateGraph.ErdosRenyiModel.ErdosRenyiModel.ErdosRenyiModelProbabilityEnum)))
                graphDensityGenerateGraphComboBox.Items.Add(density);
            graphDensityGenerateGraphComboBox.SelectedItem = GenerateGraph.ErdosRenyiModel.ErdosRenyiModel.ErdosRenyiModelProbabilityEnum.notAssigned;
            */

            // Fill namedGraphsComboBox
            /*
            foreach (Dictionary<GalleryOfNamedGraphs.NamedGraphs.NamedGraphsEnum, string> record in GalleryOfNamedGraphs.NamedGraphs.namedGraphsList)
            {
                foreach (GalleryOfNamedGraphs.NamedGraphs.NamedGraphsEnum namedGraph in record.Keys)
                {
                    string itemName;

                    //if (!GalleryOfNamedGraphs.NamedGraphs.WCMNamedGraphsDictionary.TryGetValue(namedGraph, out itemName))
                        itemName = namedGraph.ToString();

                    namedGraphsComboBox.Items.Add(namedGraph);
                }
                namedGraphsComboBox.Items.Add(delimiterNamedGraphComboBox);
            }
            namedGraphsComboBox.SelectedIndex = 0;
            */
            FillNamedGraphsComboBox();
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Create instance of algorithm
        /// If enum isn't implemented in switch throws AlgorithmDoesntExist
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
                    graphColoringAlgorithm = new GraphColoringAlgorithm.SequenceAlgorithm.ConnectedSequential.ConnectedSequential(graph);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graph, 1);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm2:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.GeneticAlgorithm.GeneticAlgorithm(graph, 2);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.greedyIndependentSet:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.GreedyIndependentSet.GreedyIndependentSet(graph);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequence:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchange:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtended:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.SequenceAlgorithm.LargestFirstSequence.LargestFirstSequence(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.optimal:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.Optimal.Optimal(graph);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchange:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtended:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.SequenceAlgorithm.RandomSequence.RandomSequence(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.saturationLargestFirstSequence:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.SaturationLargestFirstSequence.SaturationLargestFirstSequence(graph);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequence:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchange:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchange);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtended:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.SequenceAlgorithm.SmallestLastSequence.SmallestLastSequence(graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithInterchangeEnum.interchangeExtended);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.AI:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.AI.AI(graph);
                    break;
                case GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum.illnerAlgorithm:
                    graphColoringAlgorithm = new GraphColoringAlgorithm.IllnerAlgorithm.IllnerAlgorithm(graph);
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
            try
            {
                // Variable
                GraphVisualization.GraphVisualization graphVisualization = null;
                bool isSchedule;
                bool showSpanningTree;
                bool showSimplicialVertex;
                bool showCutVerticesAndBridges;
                bool showMaximumAndMinimumDegreeVertices;

                isSchedule = scheduleAppearanceCheckBox.Checked;
                showSpanningTree = showSpanningTreeCheckBox.Checked;
                showSimplicialVertex = showSimplicialVertexCheckBox.Checked;
                showCutVerticesAndBridges = showCutVerticesAndBridgesCheckBox.Checked;
                showMaximumAndMinimumDegreeVertices = showMaximumAndMinimumDegreeVerticesCheckBox.Checked;

                if (graph.GetColoredGraph().GetIsInitializedColoredGraph())
                    graphVisualization = new GraphVisualization.GraphVisualization(graph.GetGraphProperty().GetComponents(), isSchedule, showSpanningTree, showSimplicialVertex, showCutVerticesAndBridges, showMaximumAndMinimumDegreeVertices);
                else
                    graphVisualization = new GraphVisualization.GraphVisualization(graph.GetGraphProperty().GetComponents(), false, showSpanningTree, showSimplicialVertex, showCutVerticesAndBridges, showMaximumAndMinimumDegreeVertices);
                
                graphVisualization.CreateGraphVisualization();
                SetDrawGraphPictureBox(graphVisualization.GetImage());
            }
            catch (Exception ex)
            {
                // Print to console
                MyWrite(ex.ToString(), WriteEnum.error);
            }
        }

        private void scheduleAppearanceCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!ExistsGraph(false))
            {
                return;
            }

            // Disable all buttons
            EnableButtons(false);

            // Status
            SetStatusLabel(WCM.GraphDrawingProgressStatus);

            coreThread = new Thread(() =>
            {
                try
                {
                    ShowGraph();

                    // Status
                    SetStatusLabel(WCM.GraphDrawingStatus);
                }
                catch (ThreadAbortException)
                {
                    // Print to console
                    MyWrite(WCM.ApplicationReset, WriteEnum.reset);
                }
                finally
                {
                    // Enable all buttons
                    EnableButtons(true);
                }

            });
            coreThread.IsBackground = true;
            coreThread.Start();
        }

        
        private void showCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (!ExistsGraph(false))
            {
                return;
            }

            // Disable all buttons
            EnableButtons(false);

            // Status
            SetStatusLabel(WCM.GraphDrawingProgressStatus);

            coreThread = new Thread(() =>
            {
                try
                {
                    ShowGraph();

                    // Status
                    SetStatusLabel(WCM.GraphDrawingStatus);
                }
                catch (ThreadAbortException)
                {
                    // Print to console
                    MyWrite(WCM.ApplicationReset, WriteEnum.reset);
                }
                finally
                {
                    // Enable all buttons
                    EnableButtons(true);
                }

            });
            coreThread.IsBackground = true;
            coreThread.Start();
        }

        /// <summary>
        /// Change text in StatusLabel
        /// </summary>
        /// <param name="text">The text</param>
        private void SetStatusLabel(string text)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(SetStatusLabel), new object[] { text });
                return;
            }
            statusStatusLabel.Text = text;
        }


        private void SetDrawGraphPictureBox(Image image)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<Image>(SetDrawGraphPictureBox), new object[] { image });
                return;
            }
            drawGraphPictureBox.Image = image;
        }

        // Invoke
        #region
        private void SetClassValuePropertiesLabel(string text)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(SetClassValuePropertiesLabel), new object[] { text });
                return;
            }
            classValuePropertiesLabel.Text = text;
        }

        private void SetIsRegularValueGraphPropertiesLabel(string text)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(SetIsRegularValueGraphPropertiesLabel), new object[] { text });
                return;
            }
            isRegularValueGraphPropertiesLabel.Text = text;
        }

        private void SetIsCyclicValueGraphPropertiesLabel(string text)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(SetIsCyclicValueGraphPropertiesLabel), new object[] { text });
                return;
            }
            isCyclicValueGraphPropertiesLabel.Text = text;
        }

        private void SetIsChordalValueGraphPropertiesLabel(string text)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(SetIsChordalValueGraphPropertiesLabel), new object[] { text });
                return;
            }
            isChordalValueGraphPropertiesLabel.Text = text;
        }

        private void SetMaximumVertexDegreeValueGraphPropertiesLabel(string text)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(SetMaximumVertexDegreeValueGraphPropertiesLabel), new object[] { text });
                return;
            }
            maximumVertexDegreeValueGraphPropertiesLabel.Text = text;
        }

        private void SetMinimumVertexDegreeValueGraphPropertiesLabel(string text)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(SetMinimumVertexDegreeValueGraphPropertiesLabel), new object[] { text });
                return;
            }
            minimumVertexDegreeValueGraphPropertiesLabel.Text = text;
        }

        private void SetAverageVertexDegreeValueGraphPropertiesLabel(string text)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(SetAverageVertexDegreeValueGraphPropertiesLabel), new object[] { text });
                return;
            }
            averageVertexDegreeValueGraphPropertiesLabel.Text = text;
        }

        private void SetCountCutVerticesValueGraphPropertiesLabel(string text)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(SetCountCutVerticesValueGraphPropertiesLabel), new object[] { text });
                return;
            }
            countCutVerticesValueGraphPropertiesLabel.Text = text;
        }

        private void SetCountBridgesValueGraphPropertiesLabel(string text)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(SetCountBridgesValueGraphPropertiesLabel), new object[] { text });
                return;
            }
            countBridgesValueGraphPropertiesLabel.Text = text;
        }

        private void SetGirthValueGraphPropertiesLabel(string text)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(SetGirthValueGraphPropertiesLabel), new object[] { text });
                return;
            }
            girthValueGraphPropertiesLabel.Text = text;
        }

        private void SetCayleysFormulaValueGraphPropertiesLabel(string text)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(SetCayleysFormulaValueGraphPropertiesLabel), new object[] { text });
                return;
            }
            cayleysFormulaValueGraphPropertiesLabel.Text = text;
        }

        private void SetIsEulerianValueGraphPropertiesLabel(string text)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(SetIsEulerianValueGraphPropertiesLabel), new object[] { text });
                return;
            }
            isEulerianValueGraphPropertiesLabel.Text = text;
        }

        private void SetIsSimplicialVertexValueGraphPropertiesLabel(string text)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(SetIsSimplicialVertexValueGraphPropertiesLabel), new object[] { text });
                return;
            }
            SimplicialVertexValueGraphPropertiesLabel.Text = text;
        }

        private void SetCountOfUsedColorsValueGraphPropertiesLabel(string text)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(SetCountOfUsedColorsValueGraphPropertiesLabel), new object[] { text });
                return;
            }
            countOfUsedColorsValueGraphPropertiesLabel.Text = text;
        }

        private void SetEnableLoadGraphButton(bool enable)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<bool>(SetEnableLoadGraphButton), new object[] { enable });
                return;
            }
            loadGraphButton.Enabled = enable;
        }

        private void SetEnableSaveGraphButton(bool enable)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<bool>(SetEnableSaveGraphButton), new object[] { enable });
                return;
            }
            saveGraphButton.Enabled = enable;
        }

        private void SetEnableGenerateGraphButton(bool enable)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<bool>(SetEnableGenerateGraphButton), new object[] { enable });
                return;
            }
            generateGraphButton.Enabled = enable;
        }

        private void SetEnableColorGraphPlanScheduleButton(bool enable)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<bool>(SetEnableColorGraphPlanScheduleButton), new object[] { enable });
                return;
            }
            colorGraphPlanScheduleButton.Enabled = enable;
        }
        #endregion

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

        private enum WriteEnum
        {
            loadGraph,
            saveGraph,
            newGraph,
            reset,
            colorGraph,
            generateGraph,
            complementGraph,
            lineGraph,
            graphProperty,
            graphModification,
            error
        }

        /// <summary>
        /// If canWrite is true then print the text to console.
        /// loadGraph, newGraph, generateGraph, complementGraph, lineGraph, lineGraph - print the graph
        /// WriteEnum, WriteEnum - print nothing
        /// graphProperty - print the graph properties
        /// colorGraph - print the coloring graph
        /// error - if showError is true then print the text, otherwise nothing
        /// </summary>
        /// <param name="text">text</param>
        /// <param name="writeEnum">write enum</param>
        private void MyWrite(string text, WriteEnum writeEnum)
        {
            if (!canWrite)
                return;

            if (writeEnum != WriteEnum.error)
            {
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine(text);
                Console.WriteLine("--------------------------------------------------");
            }

            switch (writeEnum)
            {
                case WriteEnum.loadGraph:
                case WriteEnum.newGraph:
                case WriteEnum.generateGraph:
                case WriteEnum.complementGraph:
                case WriteEnum.lineGraph:
                case WriteEnum.graphModification:
                    Console.WriteLine(graph);
                    break;
                case WriteEnum.reset:
                case WriteEnum.saveGraph:
                    Console.WriteLine();
                    break;
                case WriteEnum.colorGraph:
                    Console.WriteLine(graph.GetColoredGraph());
                    break;
                case WriteEnum.graphProperty:
                    Console.WriteLine(graph.GetGraphProperty());
                    break;
                case WriteEnum.error:
                    if (showError)
                    {
                        Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                        Console.WriteLine(text);
                        Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    }
                    break;
            }
        }

        private void ResetProperty(bool useless = false)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<bool>(ResetProperty), new object[] { useless });
                return;
            }

            // Text
            countVerticesValueGraphPropertiesLabel.Text = "";
            countEdgesValueGraphPropertiesLabel.Text = "";
            circuitRankValueGraphPropertiesLabel.Text = "";
            countComponentValueGraphPropertiesLabel.Text = "";
            isConnectedValueGraphPropertiesLabel.Text = "";
            classValuePropertiesLabel.Text = "";
            isRegularValueGraphPropertiesLabel.Text = "";
            isCyclicValueGraphPropertiesLabel.Text = "";
            isChordalValueGraphPropertiesLabel.Text = "";
            maximumVertexDegreeValueGraphPropertiesLabel.Text = "";
            minimumVertexDegreeValueGraphPropertiesLabel.Text = "";
            averageVertexDegreeValueGraphPropertiesLabel.Text = "";
            countCutVerticesValueGraphPropertiesLabel.Text = "";
            countBridgesValueGraphPropertiesLabel.Text = "";
            girthValueGraphPropertiesLabel.Text = "";
            cayleysFormulaValueGraphPropertiesLabel.Text = "";
            isEulerianValueGraphPropertiesLabel.Text = "";
            SimplicialVertexValueGraphPropertiesLabel.Text = "";
            countOfUsedColorsValueGraphPropertiesLabel.Text = "";

            // Color
            classPropertiesLabel.ForeColor = Color.Black;
            isCyclicGraphPropertiesLabel.ForeColor = Color.Black;
            isRegularGraphPropertiesLabel.ForeColor = Color.Black;
            countCutVerticesGraphPropertiesLabel.ForeColor = Color.Black;
            countBridgesGraphPropertiesLabel.ForeColor = Color.Black;
            cayleysFormulaGraphPropertiesLabel.ForeColor = Color.Black;
            isEulerianGraphPropertiesLabel.ForeColor = Color.Black;

            if (graph != null)
            {
                // Grayed text
                if (graph.GetGraphProperty().GetCountComponents() != 1)
                {
                    classPropertiesLabel.ForeColor = Color.LightGray;
                    isCyclicGraphPropertiesLabel.ForeColor = Color.LightGray;
                    isRegularGraphPropertiesLabel.ForeColor = Color.LightGray;
                    countCutVerticesGraphPropertiesLabel.ForeColor = Color.LightGray;
                    countBridgesGraphPropertiesLabel.ForeColor = Color.LightGray;
                    cayleysFormulaGraphPropertiesLabel.ForeColor = Color.LightGray;
                    isEulerianGraphPropertiesLabel.ForeColor = Color.LightGray;
                }

                // Get graph properties - default
                countVerticesValueGraphPropertiesLabel.Text = graph.GetGraphProperty().GetCountVertices().ToString();
                countEdgesValueGraphPropertiesLabel.Text = graph.GetGraphProperty().GetCountEdges().ToString();
                circuitRankValueGraphPropertiesLabel.Text = graph.GetGraphProperty().GetCircuitRank().ToString();
                countComponentValueGraphPropertiesLabel.Text = graph.GetGraphProperty().GetCountComponents().ToString();
                isConnectedValueGraphPropertiesLabel.Text = graph.GetGraphProperty().GetIsConnected().ToString();
            }

            // Modification - inputs
            vertexNameGraphModificationVertexTextBox.Text = "";
            newVertexNameGraphModificationVertexTextBox.Text = "";
            firstVertexNameGraphModificationEdgeTextBox.Text = "";
            secondVertexNameGraphModificationEdgeTextBox.Text = "";
        }

        private void EnableButtons(bool enable)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<bool>(EnableButtons), new object[] { enable });
                return;
            }

            // First columnt
            loadGraphButton.Enabled = enable;
            saveGraphButton.Enabled = enable;
            colorGraphPlanScheduleButton.Enabled = enable;
            newGraphButton.Enabled = enable;
            generateGraphButton.Enabled = enable;
            scheduleAppearanceCheckBox.Enabled = enable;
            showSpanningTreeCheckBox.Enabled = enable;
            showSimplicialVertexCheckBox.Enabled = enable;
            showCutVerticesAndBridgesCheckBox.Enabled = enable;
            showMaximumAndMinimumDegreeVerticesCheckBox.Enabled = enable;

            // Second column
            graphPropertiesGroupBox.Enabled = enable;
            getGraphPropertiesButton.Enabled = enable;
            complementGraphGraphOperationButton.Enabled = enable;
            lineGraphGraphOperationButton.Enabled = enable;

            // Third column
            changeVertexNameGraphModificationVertexButton.Enabled = enable;
            vertexAddGraphModificationVertexButton.Enabled = enable;
            vertexDeleteGraphModificationVertexButton.Enabled = enable;
            vertexContractionGraphModificationVertexButton.Enabled = enable;
            vertexSuppressionGraphModificationVertexButton.Enabled = enable;
            vertexExpansionGraphModificationVertexButton.Enabled = enable;
            edgeAddGraphModificationEdgeButton.Enabled = enable;
            edgeDeleteGraphModificationEdgeButton.Enabled = enable;
            edgeContractionGraphModificationEdgeButton.Enabled = enable;
            edgeSubdivisionGraphModificationEdgeButton.Enabled = enable;
        }

        /// <summary>
        /// Return true if graph != null
        /// </summary>
        /// <returns></returns>
        private bool ExistsGraph(bool showMessageBox = true)
        {
            if (graph == null)
            {
                if (showMessageBox)
                    ShowMessageBox("Error | " + WCM.GraphNoExistsTitle, WCM.GraphNoExists);
                return false;
            }

            return true;
        }
        #endregion

        // Events
        #region
        private void loadGraphButton_Click(object sender, EventArgs e)
        {
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

            LoadGraph();
        }

        // Used by loadGraphButton_Click and drag and drop
        private void LoadGraph()
        {
            // Disable all buttons
            EnableButtons(false);

            // Status
            SetStatusLabel(WCM.LoadGraphProgressStatus);
            
            isModifiedGraph = false;

            coreThread = new Thread(() =>
            {
                try
                {
                    // Read a file
                    ReaderWriter.IReaderGraphInterface reader = new ReaderWriter.ReaderGraph(path);
                    graph = reader.ReadFile();

                    graph.GetGraphProperty().GetCountComponents();
                    ResetProperty();
                    ShowGraph();

                    // Status
                    SetStatusLabel(WCM.LoadGraphStatus);

                    // Print to console
                    MyWrite(WCM.LoadGraphStatus, WriteEnum.loadGraph);
                }
                catch (MyException.ReaderWriterException.ReaderWriterInvalidFileTypeException ex)
                {
                    ShowMessageBox("Error | " + WCM.LoadGraphInvalidTypeTitle, WCM.LoadGraphInvalidType);
                    SetStatusLabel(WCM.LoadGraphInvalidTypeStatus);
                    path = null;
                    graph = null;
                    ResetProperty();
                    drawGraphPictureBox.Image = null;

                    // Print to console
                    MyWrite(ex.ToString(), WriteEnum.error);
                }
                catch (MyException.ReaderWriterException.ReaderWriterInavalidPathException ex)
                {
                    ShowMessageBox("Error | " + WCM.LoadGraphInvalidPathTitle, WCM.LoadGraphInvalidPath);
                    SetStatusLabel(WCM.LoadGraphInvalidPathStatus);
                    path = null;
                    graph = null;
                    ResetProperty();
                    drawGraphPictureBox.Image = null;

                    // Print to console
                    MyWrite(ex.ToString(), WriteEnum.error);
                }
                catch (MyException.ReaderWriterException.ReaderWriterException ex)
                {
                    ShowMessageBox("Error | " + WCM.LoadGraphInvalidFileTitle, WCM.LoadGraphInvalidFile + "\n" + ex.GetType().Name);
                    SetStatusLabel(WCM.LoadGraphInvalidFileStatus);
                    path = null;
                    graph = null;
                    ResetProperty();
                    drawGraphPictureBox.Image = null;

                    // Print to console
                    MyWrite(ex.ToString(), WriteEnum.error);
                }
                catch (MyException.GraphException.GraphException ex)
                {
                    ShowMessageBox("Error | " + WCM.LoadGraphInvalidGraphTitle, WCM.LoadGraphInvalidGraph + "\n" + ex.GetType().Name);
                    SetStatusLabel(WCM.LoadGraphInvalidGraphStatus);
                    path = null;
                    graph = null;
                    ResetProperty();
                    drawGraphPictureBox.Image = null;

                    // Print to console
                    MyWrite(ex.ToString(), WriteEnum.error);
                }
                catch (ThreadAbortException)
                {
                    // Print to console
                    MyWrite(WCM.ApplicationReset, WriteEnum.reset);
                }
                finally
                {
                    // Enable all buttons
                    EnableButtons(true);
                }

            });
            coreThread.IsBackground = true;
            coreThread.Start();
        }

        private void colorGraphPlanScheduleButton_Click(object sender, EventArgs e)
        {
            if (!ExistsGraph())
            {
                return;
            }

            if (algorithmListBox.SelectedIndex == -1)
            {
                ShowMessageBox("Error | " + WCM.ColorGraphNoSelectedAlgorithmTitle, WCM.ColorGraphNoSelectedAlgorithm);
                SetStatusLabel(WCM.ColorGraphErrorStatus);
                return;
            }

            // Disable all buttons
            EnableButtons(false);

            // Status
            SetStatusLabel(WCM.ColorGraphProgressStatus);

            // Variable
            Object obj = new Object();
            maxUsedColors = 0;

            // Algorithm
            graphColoringAlgorithmEnum = algorithmListBoxList[algorithmListBox.SelectedIndex];
            coreThread = new Thread(() =>
            {
                try
                {
                    // Reset colored graph
                    foreach (Graph.IGraphInterface graph in graph.GetGraphProperty().GetComponents())
                    {
                        Graph.IColoredGraphInterface coloredGraph = graph.GetColoredGraph();
                        if (coloredGraph.GetIsInitializedColoredGraph())
                            coloredGraph.DeinitializationColoredGraph();
                    }

                    // Core color
                    graph.GetGraphProperty().GetComponents().ForEach(graph =>
                    {
                        GraphColoringAlgorithm.IGraphColoringAlgorithmInterface algorithm = InitializeGraphColoringAlgorithm(graphColoringAlgorithmEnum, graph);
                        algorithm.Color();

                        // Get count of used colors
                        lock (obj)
                        {
                            int usedColors = graph.GetColoredGraph().GetCountUsedColors();
                            if (usedColors > maxUsedColors)
                            {
                                maxUsedColors = usedColors;
                                SetCountOfUsedColorsValueGraphPropertiesLabel(maxUsedColors.ToString());
                            }
                        }
                    });

                    // Color disconnected graph
                    if (graph.GetGraphProperty().GetCountComponents() > 1)
                    {
                        Graph.IColoredGraphInterface coloredGraph = graph.GetColoredGraph();
                        if (coloredGraph.GetIsInitializedColoredGraph())
                            coloredGraph.DeinitializationColoredGraph();

                        foreach (Graph.IGraphInterface componentGraph in graph.GetGraphProperty().GetComponents())
                        {
                            Graph.IColoredGraphInterface componentColoredGraph = componentGraph.GetColoredGraph();

                            foreach (Graph.Vertex vertex in componentGraph.AllVertices())
                            {
                                coloredGraph.ColorVertex(graph.GetVertexByUserName(vertex.GetUserName()), vertex.GetColor());
                            }
                        }

                        coloredGraph.InitializeColoredGraph();
                    }

                    ShowGraph();

                    // Status
                    SetStatusLabel(WCM.ColorGraphStatus);

                    // Print to console
                    MyWrite(WCM.ColorGraphStatus, WriteEnum.colorGraph);
                }
                catch (ThreadAbortException)
                {
                    // Print to console
                    MyWrite(WCM.ApplicationReset, WriteEnum.reset);
                }
                catch (Exception ex)
                {
                    if (ex is MyException.GraphException.GraphException || ex is MyException.GraphColoringAlgorithmException.GraphColoringAlgorithmException)
                    {
                        ShowMessageBox("Error | " + WCM.ColorGraphSomethingWrongTitle, WCM.ColorGraphSomethingWrong + "\n" + ex.GetType().Name);
                        SetStatusLabel(WCM.ColorGraphSomethingWrongStatus);

                        // Print to console
                        MyWrite(ex.ToString(), WriteEnum.error);
                    }
                    else
                        throw ex;
                }
                finally
                {
                    // Enable all buttons
                    EnableButtons(true);
                }
            });
            coreThread.IsBackground = true;
            coreThread.Start();
        }

        private void saveGraphButton_Click(object sender, EventArgs e)
        {
            if (!ExistsGraph())
            {
                return;
            }

            // Variable
            bool newGraph = false;

            if (path == null)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.FileName = "graph.graph";
                    saveFileDialog.Filter = "Graph (*.graph)|*.graph";
                    saveFileDialog.FilterIndex = 2;
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        newGraph = true;
                        //Get the path of specified file
                        path = saveFileDialog.FileName;
                    }
                    // Path wasn't choosen
                    else
                        return;
                }
            }

            // Disable all buttons
            EnableButtons(false);

            // Status
            SetStatusLabel(WCM.WriteGraphProgressStatus);

            coreThread = new Thread(() =>
            {
                try
                {
                    // Variable
                    ReaderWriter.IWriterGraphInterface writer;

                    writer = new ReaderWriter.WriterGraph(path);

                    // generated / modified graph
                    if (newGraph)
                    {
                        writer.WriteFile(graph);
                    }

                    writer.WriteFileColor(graph, graphColoringAlgorithmEnum, false);

                    ShowMessageBox(WCM.WriteGraphTitle, WCM.WriteGraph);
                    SetStatusLabel(WCM.WriteGraphStatus);

                    // Print to console
                    MyWrite(WCM.WriteGraphStatus, WriteEnum.saveGraph);
                }
                catch (MyException.ReaderWriterException.ReaderWriterInavalidPathException ex)
                {
                    ShowMessageBox("Error | " + WCM.WriteGraphInvalidPathTitle, WCM.WriteGraphInvalidPath + "\n" + ex.GetType().Name);
                    SetStatusLabel(WCM.WriteGraphInvalidPathStatus);

                    // Print to console
                    MyWrite(ex.ToString(), WriteEnum.error);
                }
                catch (MyException.ReaderWriterException.ReaderWriterException ex)
                {
                    ShowMessageBox("Error | " + WCM.WriteGraphSomethingWrongTitle, WCM.WriteGraphSomethingWrong + "\n" + ex.GetType().Name);
                    SetStatusLabel(WCM.WriteGraphSomethingWrongStatus);

                    // Print to console
                    MyWrite(ex.ToString(), WriteEnum.error);
                }
                catch (MyException.GraphException.ColoredGraphNotInitializationException ex)
                {
                    // New graph will be only saved without coloring
                    if (!newGraph)
                    {
                        ShowMessageBox("Error | " + WCM.WriteGraphNotColoredTitle, WCM.WriteGraphNotColored);
                        SetStatusLabel(WCM.WriteGraphSomethingWrongStatus);

                        // Print to console
                        MyWrite(ex.ToString(), WriteEnum.error);
                    }
                    else
                    {
                        SetStatusLabel(WCM.WriteGraphStatus);
                    }
                }
                catch (ThreadAbortException)
                {
                    // Print to console
                    MyWrite(WCM.ApplicationReset, WriteEnum.reset);
                }
                finally
                {
                    // Enable all buttons
                    EnableButtons(true);
                }
            });
            coreThread.IsBackground = true;
            coreThread.Start();
        }

        private void getGraphPropertiesButton_Click(object sender, EventArgs e)
        {
            if (!ExistsGraph())
            {
                return;
            }

            // Disable all buttons
            EnableButtons(false);

            // Status
            SetStatusLabel(WCM.PropertyProgressStatus);

            coreThread = new Thread(() =>
            {
                try
                {
                    // Variable
                    string classType = "", isRegular = "", isCyclic = "", maximumVertexDegree = "", minimumVertexDegree = "", averageVertexDegree = "", countOfCutVertices = "", countOfBridges = "", girth = "", cayleysFormula = "", isEulerian = "", isChordal = "", simplicialVertexName = "None";

                    if (graph.GetGraphProperty().GetIsConnected())
                    {
                        // Class type
                        Graph.GraphClass.GraphClass.GraphClassEnum classTypeEnum = graph.GetGraphProperty().GetGraphClass();
                        if (!Graph.GraphClass.GraphClass.WCMClassGraphDictionary.TryGetValue(classTypeEnum, out classType))
                            classType = "";
                        
                        isCyclic = graph.GetGraphProperty().GetIsCyclic().ToString();
                        isRegular = graph.GetGraphProperty().GetIsRegular().ToString();
                        countOfCutVertices = graph.GetGraphProperty().GetCutVertices().Count.ToString();
                        countOfBridges = graph.GetGraphProperty().GetBridges().Count.ToString();
                        cayleysFormula = graph.GetGraphProperty().GetCayleysFormula().ToString();

                        // Eulerian
                        Graph.GraphProperty.GraphProperty.EulerianGraphEnum isEulerianEnum = graph.GetGraphProperty().GetIsEulerian();
                        if (!Graph.GraphProperty.GraphProperty.WCMEulerianGraphDictionary.TryGetValue(isEulerianEnum, out isEulerian))
                            isEulerian = "";
                    }

                    maximumVertexDegree = graph.GetGraphProperty().GetMaximumVertexDegree().ToString();
                    minimumVertexDegree = graph.GetGraphProperty().GetMinimumVertexDegree().ToString();
                    averageVertexDegree = graph.GetGraphProperty().GetAverageVertexDegree().ToString();
                    girth = graph.GetGraphProperty().GetGirth().ToString();
                    isChordal = graph.GetGraphProperty().GetIsChordal().ToString();
                    if (graph.GetGraphProperty().GetIsChordal())
                        simplicialVertexName = graph.GetGraphProperty().GetPerfectEliminationOrdering().Last().GetUserName();

                    // Display
                    SetClassValuePropertiesLabel(classType);
                    SetIsCyclicValueGraphPropertiesLabel(isCyclic);
                    SetIsRegularValueGraphPropertiesLabel(isRegular);
                    SetIsChordalValueGraphPropertiesLabel(isChordal);
                    SetCountCutVerticesValueGraphPropertiesLabel(countOfCutVertices);
                    SetCountBridgesValueGraphPropertiesLabel(countOfBridges);
                    SetCayleysFormulaValueGraphPropertiesLabel(cayleysFormula);
                    SetIsEulerianValueGraphPropertiesLabel(isEulerian);
                    SetMaximumVertexDegreeValueGraphPropertiesLabel(maximumVertexDegree);
                    SetMinimumVertexDegreeValueGraphPropertiesLabel(minimumVertexDegree);
                    SetAverageVertexDegreeValueGraphPropertiesLabel(averageVertexDegree);
                    SetGirthValueGraphPropertiesLabel(girth);
                    SetIsSimplicialVertexValueGraphPropertiesLabel(simplicialVertexName);

                    // Status
                    SetStatusLabel(WCM.PropertyStatus);

                    // Print to console
                    MyWrite(WCM.PropertyStatus, WriteEnum.graphProperty);

                    // Enable all buttons
                    EnableButtons(true);
                }
                catch (ThreadAbortException)
                {
                    // Print to console
                    MyWrite(WCM.ApplicationReset, WriteEnum.reset);
                }
            });
            coreThread.IsBackground = true;
            coreThread.Start();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            // Thread
            if (coreThread != null && coreThread.IsAlive)
            {
                coreThread.Abort();
                Thread.Sleep(0);

                while (coreThread.ThreadState.HasFlag(ThreadState.Aborted))
                    Thread.Sleep(0);
            }

            coreThread = null;

            SetStatusLabel(WCM.ApplicationReset);

            // Enable buttons
            EnableButtons(true);

            // Core
            path = null;
            graph = null;
            ResetProperty();
            SetStatusLabel("");
            isModifiedGraph = false;
            drawGraphPictureBox.Image = null;
        }

        private void generateGraphButton_Click(object sender, EventArgs e)
        {
            // Variable
            int countOfVertices;
            GenerateGraph.ErdosRenyiModel.ErdosRenyiModel.ErdosRenyiModelProbabilityEnum graphDensity;

            // Get count of vertices
            countOfVertices = (int)countOfVerticesGenerateGraphLabelNumericUpDown.Value;

            // Get graph density
            graphDensity = (GenerateGraph.ErdosRenyiModel.ErdosRenyiModel.ErdosRenyiModelProbabilityEnum)graphDensityGenerateGraphComboBox.SelectedValue;

            // Disable all buttons
            EnableButtons(false);

            path = null;
            isModifiedGraph = false;

            // Status
            SetStatusLabel(WCM.GenerateGraphProgressStatus);

            coreThread = new Thread(() =>
            {
                try
                {
                    GenerateGraph.ErdosRenyiModel.ErdosRenyiModel erdosRenyiModel = new GenerateGraph.ErdosRenyiModel.ErdosRenyiModel(countOfVertices, graphDensity);
                    graph = erdosRenyiModel.GenerateGraph();

                    graph.GetGraphProperty().GetCountComponents();
                    ResetProperty();
                    ShowGraph();

                    // Status
                    SetStatusLabel(WCM.GenerateGraphStatus);

                    // Print to console
                    MyWrite(WCM.GenerateGraphStatus, WriteEnum.generateGraph);

                    // Enable all buttons
                    EnableButtons(true);
                }
                catch (ThreadAbortException)
                {
                    // Print to console
                    MyWrite(WCM.ApplicationReset, WriteEnum.reset);
                }
            });
            coreThread.IsBackground = true;
            coreThread.Start();
        }

        private void newGraphButton_Click(object sender, EventArgs e)
        {
            // Variable
            string graphContent = null;
            isModifiedGraph = false;
            string namedGraph = namedGraphsComboBox.SelectedValue.ToString();

            GalleryOfNamedGraphs.NamedGraphs.NamedGraphsEnum namedGraphEnum;
            
            namedGraphEnum = (GalleryOfNamedGraphs.NamedGraphs.NamedGraphsEnum)Enum.Parse(typeof(GalleryOfNamedGraphs.NamedGraphs.NamedGraphsEnum), namedGraph, true);

            foreach (Dictionary<GalleryOfNamedGraphs.NamedGraphs.NamedGraphsEnum, string> record in GalleryOfNamedGraphs.NamedGraphs.namedGraphsList)
            {
                if (record.TryGetValue(namedGraphEnum, out graphContent))
                    break;
            }

            // Disable all buttons
            EnableButtons(false);

            // Status
            SetStatusLabel(WCM.NewGraphProgressStatus);

            coreThread = new Thread(() =>
            {
                try
                {
                    path = null;

                    // Read a file
                    string pathGraph = ReaderWriter.ReaderWriter.CreateTestFile(graphContent);

                    ReaderWriter.IReaderGraphInterface reader = new ReaderWriter.ReaderGraph(pathGraph, false);
                    graph = reader.ReadFile();

                    ReaderWriter.ReaderWriter.DeleteTestFile();

                    graph.GetGraphProperty().GetCountComponents();
                    ResetProperty();
                    ShowGraph();

                    // Status
                    SetStatusLabel(WCM.NewGraphStatus);

                    // Print to console
                    MyWrite(WCM.NewGraphStatus, WriteEnum.newGraph);
                }
                catch (ThreadAbortException)
                {
                    // Print to console
                    MyWrite(WCM.ApplicationReset, WriteEnum.reset);
                }
                finally
                {
                    // Enable all buttons
                    EnableButtons(true);
                }
            });
            coreThread.IsBackground = true;
            coreThread.Start();
        }

        private void complementGraphGraphOperationButton_Click(object sender, EventArgs e)
        {
            // Disable all buttons
            EnableButtons(false);

            // Status
            SetStatusLabel(WCM.ComplementGraphProgressStatus);

            coreThread = new Thread(() =>
            {
                try
                {
                    path = null;
                    Graph.IGraphInterface complementGraph = Graph.GraphOperation.GraphOperation.ComplementGraph(graph);
                    complementGraph.SetName("Complement graph - " + graph.GetName());
                    graph = complementGraph;

                    graph.GetGraphProperty().GetCountComponents();
                    ResetProperty();
                    ShowGraph();

                    // Status
                    SetStatusLabel(WCM.ComplementGraphStatus);

                    // Print to console
                    MyWrite(WCM.ComplementGraphStatus, WriteEnum.complementGraph);
                }
                catch (ThreadAbortException)
                {
                    // Print to console
                    MyWrite(WCM.ApplicationReset, WriteEnum.reset);
                }
                finally
                {
                    // Enable all buttons
                    EnableButtons(true);
                }
            });
            coreThread.IsBackground = true;
            coreThread.Start();
        }

        private void lineGraphGraphOperationButton_Click(object sender, EventArgs e)
        {
            // Disable all buttons
            EnableButtons(false);

            // Status
            SetStatusLabel(WCM.LineGraphProgressStatus);

            coreThread = new Thread(() =>
            {
                try
                {
                    path = null;
                    Graph.IGraphInterface lineGraph = Graph.GraphOperation.GraphOperation.LineGraph(graph);
                    lineGraph.SetName("Line graph - " + graph.GetName());
                    graph = lineGraph;

                    graph.GetGraphProperty().GetCountComponents();
                    ResetProperty();
                    ShowGraph();

                    // Status
                    SetStatusLabel(WCM.LineGraphStatus);

                    // Print to console
                    MyWrite(WCM.LineGraphStatus, WriteEnum.lineGraph);
                }
                catch (ThreadAbortException)
                {
                    // Print to console
                    MyWrite(WCM.ApplicationReset, WriteEnum.reset);
                }
                finally
                {
                    // Enable all buttons
                    EnableButtons(true);
                }
            });
            coreThread.IsBackground = true;
            coreThread.Start();
        }
        #endregion

        // Property - click
        #region
        // Class click
        private void classPropertiesLabel_Click(object sender, EventArgs e)
        {
            if (graph == null)
                return;

            if (graph.GetGraphProperty().GetIsConnected())
            {
                // Disable all buttons
                EnableButtons(false);

                coreThread = new Thread(() =>
                {
                    string classType = "";

                    // Class type
                    Graph.GraphClass.GraphClass.GraphClassEnum classTypeEnum = graph.GetGraphProperty().GetGraphClass();
                    if (!Graph.GraphClass.GraphClass.WCMClassGraphDictionary.TryGetValue(classTypeEnum, out classType))
                        classType = "";

                    SetClassValuePropertiesLabel(classType);

                    // Enable all buttons
                    EnableButtons(true);
                });
                coreThread.IsBackground = true;
                coreThread.Start();
            }
        }

        // Is regular - click
        private void isRegularGraphPropertiesLabel_Click(object sender, EventArgs e)
        {
            if (graph == null)
                return;

            if (graph.GetGraphProperty().GetIsConnected())
            {
                // Disable all buttons
                EnableButtons(false);

                coreThread = new Thread(() =>
                {
                    string isRegular = "";
                    isRegular = graph.GetGraphProperty().GetIsRegular().ToString();

                    SetIsRegularValueGraphPropertiesLabel(isRegular);

                    // Enable all buttons
                    EnableButtons(true);
                });
                coreThread.IsBackground = true;
                coreThread.Start();
            }
        }

        // Is cyclic - click
        private void isCyclicGraphPropertiesLabel_Click(object sender, EventArgs e)
        {
            if (graph == null)
                return;

            if (graph.GetGraphProperty().GetIsConnected())
            {
                // Disable all buttons
                EnableButtons(false);

                coreThread = new Thread(() =>
                {
                    string isCyclic = "";
                    isCyclic = graph.GetGraphProperty().GetIsCyclic().ToString();

                    SetIsCyclicValueGraphPropertiesLabel(isCyclic);

                    // Enable all buttons
                    EnableButtons(true);
                });
                coreThread.IsBackground = true;
                coreThread.Start();
            }
        }

        // Maximum / minimum / average degree - click
        private void maximumMinimumAverageVertexDegreeGraphPropertiesLabel_Click(object sender, EventArgs e)
        {
            if (graph == null)
                return;

            // Disable all buttons
            EnableButtons(false);

            coreThread = new Thread(() =>
            {
                string maximumVertexDegree = "", minimumVertexDegree = "", averageVertexDegree = "";
                maximumVertexDegree = graph.GetGraphProperty().GetMaximumVertexDegree().ToString();
                minimumVertexDegree = graph.GetGraphProperty().GetMinimumVertexDegree().ToString();
                averageVertexDegree = graph.GetGraphProperty().GetAverageVertexDegree().ToString();

                SetMinimumVertexDegreeValueGraphPropertiesLabel(minimumVertexDegree);
                SetMaximumVertexDegreeValueGraphPropertiesLabel(maximumVertexDegree);
                SetAverageVertexDegreeValueGraphPropertiesLabel(averageVertexDegree);

                // Enable all buttons
                EnableButtons(true);
            });
            coreThread.IsBackground = true;
            coreThread.Start();
        }

        // Bridges / cut vertices - click
        private void countBridgesCutVerticesGraphPropertiesLabel_Click(object sender, EventArgs e)
        {
            if (graph == null)
                return;

            if (graph.GetGraphProperty().GetIsConnected())
            {
                // Disable all buttons
                EnableButtons(false);

                coreThread = new Thread(() =>
                {
                    string countOfCutVertices = "", countOfBridges = "";
                    countOfCutVertices = graph.GetGraphProperty().GetCutVertices().Count.ToString();
                    countOfBridges = graph.GetGraphProperty().GetBridges().Count.ToString();

                    SetCountCutVerticesValueGraphPropertiesLabel(countOfCutVertices);
                    SetCountBridgesValueGraphPropertiesLabel(countOfBridges);

                    // Enable all buttons
                    EnableButtons(true);
                });
                coreThread.IsBackground = true;
                coreThread.Start();
            }
        }

        // Girth - click
        private void girthGraphPropertiesLabel_Click(object sender, EventArgs e)
        {
            if (graph == null)
                return;

            // Disable all buttons
            EnableButtons(false);

            coreThread = new Thread(() =>
            {
                string girth = "";
                girth = graph.GetGraphProperty().GetGirth().ToString();

                SetGirthValueGraphPropertiesLabel(girth);

                // Enable all buttons
                EnableButtons(true);
            });
            coreThread.IsBackground = true;
            coreThread.Start();
        }

        // Cayleys formula - click
        private void cayleysFormulaGraphPropertiesLabel_Click(object sender, EventArgs e)
        {
            if (graph == null)
                return;

            if (graph.GetGraphProperty().GetIsConnected())
            {
                // Disable all buttons
                EnableButtons(false);

                coreThread = new Thread(() =>
                {
                    string cayleysFormula = "";
                    cayleysFormula = graph.GetGraphProperty().GetCayleysFormula().ToString();

                    SetCayleysFormulaValueGraphPropertiesLabel(cayleysFormula);

                    // Enable all buttons
                    EnableButtons(true);
                });
                coreThread.IsBackground = true;
                coreThread.Start();
            }
        }

        // Is eulerian - click
        private void isEulerianGraphPropertiesLabel_Click(object sender, EventArgs e)
        {
            if (graph == null)
                return;

            if (graph.GetGraphProperty().GetIsConnected())
            {
                // Disable all buttons
                EnableButtons(false);

                coreThread = new Thread(() =>
                {
                    string isEulerian = "";
                    Graph.GraphProperty.GraphProperty.EulerianGraphEnum isEulerianEnum = graph.GetGraphProperty().GetIsEulerian();
                    if (!Graph.GraphProperty.GraphProperty.WCMEulerianGraphDictionary.TryGetValue(isEulerianEnum, out isEulerian))
                        isEulerian = "";

                    SetIsEulerianValueGraphPropertiesLabel(isEulerian);

                    // Enable all buttons
                    EnableButtons(true);
                });
                coreThread.IsBackground = true;
                coreThread.Start();
            }
        }

        // Chordal - click
        private void isChordalGraphPropertiesLabel_Click(object sender, EventArgs e)
        {
            if (graph == null)
                return;

            // Disable all buttons
            EnableButtons(false);

            coreThread = new Thread(() =>
            {
                // Variable
                string isChordal;
                string simplicialVertexName = "None";

                isChordal = graph.GetGraphProperty().GetIsChordal().ToString();

                if (graph.GetGraphProperty().GetIsChordal())
                    simplicialVertexName = graph.GetGraphProperty().GetPerfectEliminationOrdering().Last().GetUserName();

                SetIsChordalValueGraphPropertiesLabel(isChordal);
                SetIsSimplicialVertexValueGraphPropertiesLabel(simplicialVertexName);

                // Enable all buttons
                EnableButtons(true);
            });
            coreThread.IsBackground = true;
            coreThread.Start();
        }
        #endregion

        // Graph modification - click
        #region

        private void changeVertexNameGraphModificationVertexButton_Click(object sender, EventArgs e)
        {
            if (!ExistsGraph())
                return;

            string vertexName = vertexNameGraphModificationVertexTextBox.Text;
            string newVertexName = newVertexNameGraphModificationVertexTextBox.Text;

            if (vertexName == "")
            {
                ShowMessageBox("Error | " + WCM.GraphModificationVertexDoesntInsertedTitle, WCM.GraphModificationVertexDoesntInserted);
                return;
            }

            if (newVertexName == "")
            {
                ShowMessageBox("Error | " + WCM.GraphModificationVertexNameDoesntInsertedTitle, WCM.GraphModificationVertexNameDoesntInserted);
                return;
            }

            // Disable all buttons
            EnableButtons(false);

            coreThread = new Thread(() =>
            {
                try
                {
                    graph.RenameVertexUserName(graph.GetVertexByUserName(vertexName), newVertexName);

                    ResetProperty();
                    ShowGraph();

                    path = null;

                    // Status
                    SetStatusLabel(WCM.GraphModificationStatus);

                    // Print to console
                    MyWrite(WCM.GraphModificationStatus, WriteEnum.graphModification);
                }
                catch (MyException.GraphException.GraphVertexUserNameAlreadyExistsException ex)
                {
                    ShowMessageBox("Error | " + WCM.GraphModificationVertexUserNameExistsTitle, WCM.GraphModificationVertexUserNameExists);

                    // Print to console
                    MyWrite(ex.ToString(), WriteEnum.error);
                }
                catch (MyException.GraphException.GraphVertexDoesntExistException ex)
                {
                    ShowMessageBox("Error | " + WCM.GraphModificationVertexDoesntExistTitle, WCM.GraphModificationVertexDoesntExist);

                    // Print to console
                    MyWrite(ex.ToString(), WriteEnum.error);
                }
                catch (ThreadAbortException)
                {
                    // Print to console
                    MyWrite(WCM.ApplicationReset, WriteEnum.reset);
                }
                finally
                {
                    // Enable all buttons
                    EnableButtons(true);

                }
            });
            coreThread.IsBackground = true;
            coreThread.Start();
        }

        private void vertexAddGraphModificationVertexButton_Click(object sender, EventArgs e)
        {
            if (!ExistsGraph())
                return;

            // Variable
            string vertexName = vertexNameGraphModificationVertexTextBox.Text;

            if (vertexName == "")
            {
                ShowMessageBox("Error | " + WCM.GraphModificationVertexDoesntInsertedTitle, WCM.GraphModificationVertexDoesntInserted);
                return;
            }

            // Disable all buttons
            EnableButtons(false);

            // Status
            SetStatusLabel(WCM.GraphModificationProgressStatus);

            // Change graph name
            if (!isModifiedGraph)
            {
                isModifiedGraph = true;
                graph.SetName(MODIFIEDGRAPHNAME + " - " + graph.GetName());
            }

            coreThread = new Thread(() =>
            {
                try
                {
                    graph.VertexAdd(new Graph.Vertex(vertexName));

                    ResetProperty();
                    ShowGraph();

                    path = null;

                    // Status
                    SetStatusLabel(WCM.GraphModificationStatus);

                    // Print to console
                    MyWrite(WCM.GraphModificationStatus, WriteEnum.graphModification);
                }
                catch (MyException.GraphException.GraphVertexAlreadyExistsException ex)
                {
                    ShowMessageBox("Error | " + WCM.GraphModificationVertexAlreadyExistsTitle, WCM.GraphModificationVertexAlreadyExists);

                    // Print to console
                    MyWrite(ex.ToString(), WriteEnum.error);
                }
                catch (ThreadAbortException)
                {
                    // Print to console
                    MyWrite(WCM.ApplicationReset, WriteEnum.reset);
                }
                finally
                {
                    // Enable all buttons
                    EnableButtons(true);
                }
            });
            coreThread.IsBackground = true;
            coreThread.Start();
        }

        private void vertexDeleteGraphModificationVertexButton_Click(object sender, EventArgs e)
        {
            if (!ExistsGraph())
                return;

            // Variable
            string vertexName = vertexNameGraphModificationVertexTextBox.Text;

            if (vertexName == "")
            {
                ShowMessageBox("Error | " + WCM.GraphModificationVertexDoesntInsertedTitle, WCM.GraphModificationVertexDoesntInserted);
                return;
            }

            if (graph.GetGraphProperty().GetCountVertices() == 1)
            {
                ShowMessageBox("Error | " + WCM.GraphModificationDeleteVertexOneTitle, WCM.GraphModificationDeleteVertexOne);
                return;
            }

            // Disable all buttons
            EnableButtons(false);

            // Status
            SetStatusLabel(WCM.GraphModificationProgressStatus);
            
            // Change graph name
            if (!isModifiedGraph)
            {
                isModifiedGraph = true;
                graph.SetName(MODIFIEDGRAPHNAME + " - " + graph.GetName());
            }

            coreThread = new Thread(() =>
            {
                try
                {
                    graph.VertexDelete(graph.GetVertexByUserName(vertexName));
                    ResetProperty();
                    ShowGraph();

                    path = null;

                    // Status
                    SetStatusLabel(WCM.GraphModificationStatus);

                    // Print to console
                    MyWrite(WCM.GraphModificationStatus, WriteEnum.graphModification);
                }
                catch (MyException.GraphException.GraphVertexDoesntExistException ex)
                {
                    ShowMessageBox("Error | " + WCM.GraphModificationVertexDoesntExistTitle, WCM.GraphModificationVertexDoesntExist);

                    // Print to console
                    MyWrite(ex.ToString(), WriteEnum.error);
                }
                catch (ThreadAbortException)
                {
                    // Print to console
                    MyWrite(WCM.ApplicationReset, WriteEnum.reset);
                }
                finally
                {
                    // Enable all buttons
                    EnableButtons(true);
                }
            });
            coreThread.IsBackground = true;
            coreThread.Start();
        }

        private void vertexContractionGraphModificationVertexButton_Click(object sender, EventArgs e)
        {
            if (!ExistsGraph())
                return;

            // Variable
            string vertexName = vertexNameGraphModificationVertexTextBox.Text;

            if (vertexName == "")
            {
                ShowMessageBox("Error | " + WCM.GraphModificationVertexDoesntInsertedTitle, WCM.GraphModificationVertexDoesntInserted);
                return;
            }

            // Disable all buttons
            EnableButtons(false);

            // Status
            SetStatusLabel(WCM.GraphModificationProgressStatus);
            
            // Change graph name
            if (!isModifiedGraph)
            {
                isModifiedGraph = true;
                graph.SetName(MODIFIEDGRAPHNAME + " - " + graph.GetName());
            }

            coreThread = new Thread(() =>
            {
                try
                {
                    graph.VertexContraction(graph.GetVertexByUserName(vertexName));
                    ResetProperty();
                    ShowGraph();

                    path = null;

                    // Status
                    SetStatusLabel(WCM.GraphModificationStatus);

                    // Print to console
                    MyWrite(WCM.GraphModificationStatus, WriteEnum.graphModification);
                }
                catch (MyException.GraphException.GraphVertexDoesntExistException ex)
                {
                    ShowMessageBox("Error | " + WCM.GraphModificationVertexDoesntExistTitle, WCM.GraphModificationVertexDoesntExist);

                    // Print to console
                    MyWrite(ex.ToString(), WriteEnum.error);
                }
                catch (ThreadAbortException)
                {
                    // Print to console
                    MyWrite(WCM.ApplicationReset, WriteEnum.reset);
                }
                finally
                {
                    // Enable all buttons
                    EnableButtons(true);
                }
            });
            coreThread.IsBackground = true;
            coreThread.Start();
        }

        private void vertexSuppressionGraphModificationVertexButton_Click(object sender, EventArgs e)
        {
            if (!ExistsGraph())
                return;

            // Variable
            string vertexName = vertexNameGraphModificationVertexTextBox.Text;

            if (vertexName == "")
            {
                ShowMessageBox("Error | " + WCM.GraphModificationVertexDoesntInsertedTitle, WCM.GraphModificationVertexDoesntInserted);
                return;
            }

            // Disable all buttons
            EnableButtons(false);

            // Status
            SetStatusLabel(WCM.GraphModificationProgressStatus);

            // Change graph name
            if (!isModifiedGraph)
            {
                isModifiedGraph = true;
                graph.SetName(MODIFIEDGRAPHNAME + " - " + graph.GetName());
            }

            coreThread = new Thread(() =>
            {
                try
                {
                    graph.VertexSuppression(graph.GetVertexByUserName(vertexName));
                    ResetProperty();
                    ShowGraph();

                    path = null;

                    // Status
                    SetStatusLabel(WCM.GraphModificationStatus);

                    // Print to console
                    MyWrite(WCM.GraphModificationStatus, WriteEnum.graphModification);
                }
                catch (MyException.GraphException.GraphVertexDoesntExistException ex)
                {
                    ShowMessageBox("Error | " + WCM.GraphModificationVertexDoesntExistTitle, WCM.GraphModificationVertexDoesntExist);

                    // Print to console
                    MyWrite(ex.ToString(), WriteEnum.error);
                }
                catch (MyException.GraphException.GraphInvalidDegreeVertex ex)
                {
                    ShowMessageBox("Error | " + WCM.GraphModificationVertexInvalidVertexTitle, WCM.GraphModificationVertexInvalidVertex);

                    // Print to console
                    MyWrite(ex.ToString(), WriteEnum.error);
                }
                catch (ThreadAbortException)
                {
                    // Print to console
                    MyWrite(WCM.ApplicationReset, WriteEnum.reset);
                }
                finally
                {
                    // Enable all buttons
                    EnableButtons(true);
                }
            });
            coreThread.IsBackground = true;
            coreThread.Start();
        }

        private void vertexExpansionGraphModificationVertexButton_Click(object sender, EventArgs e)
        {
            if (!ExistsGraph())
                return;

            // Variable
            string vertexName = vertexNameGraphModificationVertexTextBox.Text;

            if (vertexName == "")
            {
                ShowMessageBox("Error | " + WCM.GraphModificationVertexDoesntInsertedTitle, WCM.GraphModificationVertexDoesntInserted);
                return;
            }

            // Disable all buttons
            EnableButtons(false);

            // Status
            SetStatusLabel(WCM.GraphModificationProgressStatus);

            // Change graph name
            if (!isModifiedGraph)
            {
                isModifiedGraph = true;
                graph.SetName(MODIFIEDGRAPHNAME + " - " + graph.GetName());
            }

            coreThread = new Thread(() =>
            {
                try
                {
                    graph.VertexExpansion(graph.GetVertexByUserName(vertexName));
                    ResetProperty();
                    ShowGraph();

                    path = null;

                    // Status
                    SetStatusLabel(WCM.GraphModificationStatus);

                    // Print to console
                    MyWrite(WCM.GraphModificationStatus, WriteEnum.graphModification);
                }
                catch (MyException.GraphException.GraphVertexDoesntExistException ex)
                {
                    ShowMessageBox("Error | " + WCM.GraphModificationVertexDoesntExistTitle, WCM.GraphModificationVertexDoesntExist);

                    // Print to console
                    MyWrite(ex.ToString(), WriteEnum.error);
                }
                catch (ThreadAbortException)
                {
                    // Print to console
                    MyWrite(WCM.ApplicationReset, WriteEnum.reset);
                }
                finally
                {
                    // Enable all buttons
                    EnableButtons(true);
                }
            });
            coreThread.IsBackground = true;
            coreThread.Start();
        }

        private void edgeAddGraphModificationEdgeButton_Click(object sender, EventArgs e)
        {
            if (!ExistsGraph())
                return;

            // Variable
            string firstVertexName = firstVertexNameGraphModificationEdgeTextBox.Text;
            string secondVertexName = secondVertexNameGraphModificationEdgeTextBox.Text;

            if (firstVertexName == "" || secondVertexName == "")
            {
                ShowMessageBox("Error | " + WCM.GraphModificationVertexDoesntInsertedTitle, WCM.GraphModificationVertexDoesntInserted);
                return;
            }

            // Disable all buttons
            EnableButtons(false);

            // Status
            SetStatusLabel(WCM.GraphModificationProgressStatus);

            // Change graph name
            if (!isModifiedGraph)
            {
                isModifiedGraph = true;
                graph.SetName(MODIFIEDGRAPHNAME + " - " + graph.GetName());
            }

            coreThread = new Thread(() =>
            {
                try
                {
                    graph.EdgeAdd(new Graph.Edge(graph.GetVertexByUserName(firstVertexName), graph.GetVertexByUserName(secondVertexName)));
                    ResetProperty();
                    ShowGraph();

                    path = null;

                    // Status
                    SetStatusLabel(WCM.GraphModificationStatus);

                    // Print to console
                    MyWrite(WCM.GraphModificationStatus, WriteEnum.graphModification);
                }
                catch (MyException.GraphException.GraphVertexDoesntExistException ex)
                {
                    ShowMessageBox("Error | " + WCM.GraphModificationVertexDoesntExistTitle, WCM.GraphModificationVertexDoesntExist);

                    // Print to console
                    MyWrite(ex.ToString(), WriteEnum.error);
                }
                catch (MyException.GraphException.GraphEdgeAlreadyExistsException ex)
                {
                    ShowMessageBox("Error | " + WCM.GraphModificationEdgeAlreadyExistsTitle, WCM.GraphModificationEdgeAlreadyExists);

                    // Print to console
                    MyWrite(ex.ToString(), WriteEnum.error);
                }
                catch (MyException.GraphException.GraphInvalidVertexException ex)
                {
                    ShowMessageBox("Error | " + WCM.GraphModificationVertexInvalidVertexTitle, WCM.GraphModificationVertexInvalidVertex);

                    // Print to console
                    MyWrite(ex.ToString(), WriteEnum.error);
                }
                catch (ThreadAbortException)
                {
                    // Print to console
                    MyWrite(WCM.ApplicationReset, WriteEnum.reset);
                }
                finally
                {
                    // Enable all buttons
                    EnableButtons(true);
                }
            });
            coreThread.IsBackground = true;
            coreThread.Start();
        }

        private void deleteEdgeGraphModificationEdgeButton_Click(object sender, EventArgs e)
        {
            if (!ExistsGraph())
                return;

            // Variable
            string firstVertexName = firstVertexNameGraphModificationEdgeTextBox.Text;
            string secondVertexName = secondVertexNameGraphModificationEdgeTextBox.Text;

            if (firstVertexName == "" || secondVertexName == "")
            {
                ShowMessageBox("Error | " + WCM.GraphModificationVertexDoesntInsertedTitle, WCM.GraphModificationVertexDoesntInserted);
                return;
            }

            // Disable all buttons
            EnableButtons(false);

            // Status
            SetStatusLabel(WCM.GraphModificationProgressStatus);

            // Change graph name
            if (!isModifiedGraph)
            {
                isModifiedGraph = true;
                graph.SetName(MODIFIEDGRAPHNAME + " - " + graph.GetName());
            }

            coreThread = new Thread(() =>
            {
                try
                {
                    graph.EdgeDelete(new Graph.Edge(graph.GetVertexByUserName(firstVertexName), graph.GetVertexByUserName(secondVertexName)));
                    ResetProperty();
                    ShowGraph();

                    path = null;

                    // Status
                    SetStatusLabel(WCM.GraphModificationStatus);

                    // Print to console
                    MyWrite(WCM.GraphModificationStatus, WriteEnum.graphModification);
                }
                catch (MyException.GraphException.GraphVertexDoesntExistException ex)
                {
                    ShowMessageBox("Error | " + WCM.GraphModificationVertexDoesntExistTitle, WCM.GraphModificationVertexDoesntExist);

                    // Print to console
                    MyWrite(ex.ToString(), WriteEnum.error);
                }
                catch (MyException.GraphException.GraphEdgeDoesntExistException ex)
                {
                    ShowMessageBox("Error | " + WCM.GraphModificationEdgeDoesntExistTitle, WCM.GraphModificationEdgeDoesntExist);

                    // Print to console
                    MyWrite(ex.ToString(), WriteEnum.error);
                }
                catch (ThreadAbortException)
                {
                    // Print to console
                    MyWrite(WCM.ApplicationReset, WriteEnum.reset);
                }
                finally
                {
                    // Enable all buttons
                    EnableButtons(true);
                }
            });
            coreThread.IsBackground = true;
            coreThread.Start();
        }

        private void edgeContractionGraphModificationEdgeButton_Click(object sender, EventArgs e)
        {
            if (!ExistsGraph())
                return;

            // Variable
            string firstVertexName = firstVertexNameGraphModificationEdgeTextBox.Text;
            string secondVertexName = secondVertexNameGraphModificationEdgeTextBox.Text;

            if (firstVertexName == "" || secondVertexName == "")
            {
                ShowMessageBox("Error | " + WCM.GraphModificationVertexDoesntInsertedTitle, WCM.GraphModificationVertexDoesntInserted);
                return;
            }

            // Disable all buttons
            EnableButtons(false);

            // Status
            SetStatusLabel(WCM.GraphModificationProgressStatus);

            // Change graph name
            if (!isModifiedGraph)
            {
                isModifiedGraph = true;
                graph.SetName(MODIFIEDGRAPHNAME + " - " + graph.GetName());
            }

            coreThread = new Thread(() =>
            {
                try
                {
                    graph.EdgeContraction(new Graph.Edge(graph.GetVertexByUserName(firstVertexName), graph.GetVertexByUserName(secondVertexName)));
                    ResetProperty();
                    ShowGraph();

                    path = null;

                    // Status
                    SetStatusLabel(WCM.GraphModificationStatus);

                    // Print to console
                    MyWrite(WCM.GraphModificationStatus, WriteEnum.graphModification);
                }
                catch (MyException.GraphException.GraphVertexDoesntExistException ex)
                {
                    ShowMessageBox("Error | " + WCM.GraphModificationVertexDoesntExistTitle, WCM.GraphModificationVertexDoesntExist);

                    // Print to console
                    MyWrite(ex.ToString(), WriteEnum.error);
                }
                catch (MyException.GraphException.GraphEdgeDoesntExistException ex)
                {
                    ShowMessageBox("Error | " + WCM.GraphModificationEdgeDoesntExistTitle, WCM.GraphModificationEdgeDoesntExist);

                    // Print to console
                    MyWrite(ex.ToString(), WriteEnum.error);
                }
                catch (ThreadAbortException)
                {
                    // Print to console
                    MyWrite(WCM.ApplicationReset, WriteEnum.reset);
                }
                finally
                {
                    // Enable all buttons
                    EnableButtons(true);
                }
            });
            coreThread.IsBackground = true;
            coreThread.Start();
        }

        private void edgeSubdivisionGraphModificationEdgeButton_Click(object sender, EventArgs e)
        {
            if (!ExistsGraph())
                return;

            // Variable
            string firstVertexName = firstVertexNameGraphModificationEdgeTextBox.Text;
            string secondVertexName = secondVertexNameGraphModificationEdgeTextBox.Text;

            if (firstVertexName == "" || secondVertexName == "")
            {
                ShowMessageBox("Error | " + WCM.GraphModificationVertexDoesntInsertedTitle, WCM.GraphModificationVertexDoesntInserted);
                return;
            }

            // Disable all buttons
            EnableButtons(false);

            // Status
            SetStatusLabel(WCM.GraphModificationProgressStatus);

            // Change graph name
            if (!isModifiedGraph)
            {
                isModifiedGraph = true;
                graph.SetName(MODIFIEDGRAPHNAME + " - " + graph.GetName());
            }

            coreThread = new Thread(() =>
            {
                try
                {
                    graph.EdgeSubdivision(new Graph.Edge(graph.GetVertexByUserName(firstVertexName), graph.GetVertexByUserName(secondVertexName)));
                    ResetProperty();
                    ShowGraph();

                    path = null;

                    // Status
                    SetStatusLabel(WCM.GraphModificationStatus);

                    // Print to console
                    MyWrite(WCM.GraphModificationStatus, WriteEnum.graphModification);
                }
                catch (MyException.GraphException.GraphVertexDoesntExistException ex)
                {
                    ShowMessageBox("Error | " + WCM.GraphModificationVertexDoesntExistTitle, WCM.GraphModificationVertexDoesntExist);

                    // Print to console
                    MyWrite(ex.ToString(), WriteEnum.error);
                }
                catch (MyException.GraphException.GraphEdgeDoesntExistException ex)
                {
                    ShowMessageBox("Error | " + WCM.GraphModificationEdgeDoesntExistTitle, WCM.GraphModificationEdgeDoesntExist);

                    // Print to console
                    MyWrite(ex.ToString(), WriteEnum.error);
                }
                catch (ThreadAbortException)
                {
                    // Print to console
                    MyWrite(WCM.ApplicationReset, WriteEnum.reset);
                }
                finally
                {
                    // Enable all buttons
                    EnableButtons(true);
                }
            });
            coreThread.IsBackground = true;
            coreThread.Start();
        }
        #endregion

        // Drag and drop
        #region
        private void GraphColoringForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy; // Okay
            else
                e.Effect = DragDropEffects.None; // Unknown data, ignore it
        }

        private void GraphColoringForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (fileList.Length == 1)
            {
                path = fileList[0];

                LoadGraph();
            }
        }

        private void drawGraphPanel_Click(object sender, EventArgs e)
        {
            if (drawGraphPictureBox.Image == null)
                return;
            
            /*
            if (graphVisualizationForm != null)
            {
                graphVisualizationForm.Close();
            }
            */

            graphVisualizationForm = new GraphVisualizationForm(drawGraphPictureBox.Image, " - " + graph.GetName());
            graphVisualizationForm.Show();
        }

        private void drawGraphPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        #endregion

        // Fill Combobox
        public void FillNamedGraphsComboBox()
        {
            namedGraphsComboBox.DataSource = Enum.GetValues(typeof(GalleryOfNamedGraphs.NamedGraphs.NamedGraphsEnum))
                .Cast<Enum>()
                .Select(value => new
                {
                    (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                    value
                })
                .OrderBy(item => item.value)
                .ToList();
            namedGraphsComboBox.DisplayMember = "Description";
            namedGraphsComboBox.ValueMember = "value";
        }

        public void FillGraphDensityGenerateGraphComboBox()
        {
            graphDensityGenerateGraphComboBox.DataSource = Enum.GetValues(typeof(GenerateGraph.ErdosRenyiModel.ErdosRenyiModel.ErdosRenyiModelProbabilityEnum))
                .Cast<Enum>()
                .Select(value => new
                {
                    (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                    value
                })
                .OrderBy(item => item.value)
                .ToList();
            graphDensityGenerateGraphComboBox.DisplayMember = "Description";
            graphDensityGenerateGraphComboBox.ValueMember = "value";
        }
    }
}