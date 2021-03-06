﻿using System;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GraphColoring.GraphVisualization
{
    partial class GraphVisualization : IGraphVisualizationInterface
    {
        #region Variable
        private Image image;
        private List<Graph.IGraphInterface> graphList;
        private const int MAXALLOWEDVERTICES = 50;
        private const int MAXCOLORS = 14;
        private FileNameExtensionEnum fileNameExtensionEnum;
        private IConvertGraphToDotInterface convertGraphToDot;
        
        private bool isSchedule;
        private bool showSpanningTree;
        private bool showSimplicialVertex;
        private bool showCutVerticesAndBridges;
        private bool showMaximumAndMinimumDegreeVertices;

        private const string LIB_GVC = @".\GraphVisualization\DOT\gvc.dll";
        private const string LIB_GRAPH = @".\GraphVisualization\DOT\cgraph.dll";
        private const int SUCCESS = 0;
        #endregion
        
        #region Constructor
        /// <summary>
        /// Constructor
        /// Default value for fileNameExtensionEnum is .jpg
        /// </summary>
        /// <param name="graph">The graph</param>
        public GraphVisualization(Graph.IGraphInterface graph,
            bool isSchedule, bool showSpanningTree, bool showSimplicialVertex, bool showCutVerticesAndBridges, bool showMaximumAndMinimumDegreeVertices,
            FileNameExtensionEnum fileNameExtensionEnum = FileNameExtensionEnum.jpg)
        {
            graphList = new List<Graph.IGraphInterface>();
            graphList.Add(graph);
            this.fileNameExtensionEnum = fileNameExtensionEnum;

            this.isSchedule = isSchedule;
            this.showSpanningTree = showSpanningTree;
            this.showSimplicialVertex = showSimplicialVertex;
            this.showCutVerticesAndBridges = showCutVerticesAndBridges;
            this.showMaximumAndMinimumDegreeVertices = showMaximumAndMinimumDegreeVertices;
        }

        public GraphVisualization(List<Graph.IGraphInterface> graphList, 
            bool isSchedule, bool showSpanningTree, bool showSimplicialVertex, bool showCutVerticesAndBridges, bool showMaximumAndMinimumDegreeVertices, 
            FileNameExtensionEnum fileNameExtensionEnum = FileNameExtensionEnum.jpg)
        {
            this.graphList = graphList;
            this.fileNameExtensionEnum = fileNameExtensionEnum;

            this.isSchedule = isSchedule;
            this.showSpanningTree = showSpanningTree;
            this.showSimplicialVertex = showSimplicialVertex;
            this.showCutVerticesAndBridges = showCutVerticesAndBridges;
            this.showMaximumAndMinimumDegreeVertices = showMaximumAndMinimumDegreeVertices;
        }
        #endregion

        /// 
        /// Creates a new Graphviz context.
        /// 

        [DllImport(LIB_GVC, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr gvContext();

        /// 
        /// Releases a context's resources.
        /// 
        [DllImport(LIB_GVC, CallingConvention = CallingConvention.Cdecl)]
        private static extern int gvFreeContext(IntPtr gvc);

        /// 
        /// Reads a graph from a string.
        /// 
        [DllImport(LIB_GRAPH, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr agmemread(string data);


        /// 
        /// Releases the resources used by a graph.
        /// 
        [DllImport(LIB_GRAPH, CallingConvention = CallingConvention.Cdecl)]
        private static extern void agclose(IntPtr g);

        /// 
        /// Applies a layout to a graph using the given engine.
        /// 
        [DllImport(LIB_GVC, CallingConvention = CallingConvention.Cdecl)]
        private static extern int gvLayout(IntPtr gvc, IntPtr g, string engine);


        /// 
        /// Releases the resources used by a layout.
        /// 
        [DllImport(LIB_GVC, CallingConvention = CallingConvention.Cdecl)]
        private static extern int gvFreeLayout(IntPtr gvc, IntPtr g);

        /// 
        /// Renders a graph to a file.
        /// 
        [DllImport(LIB_GVC, CallingConvention = CallingConvention.Cdecl)]
        private static extern int gvRenderFilename(IntPtr gvc, IntPtr g,
              string format, string fileName);

        /// 
        /// Renders a graph in memory.
        /// 
        [DllImport(LIB_GVC, CallingConvention = CallingConvention.Cdecl)]
        private static extern int gvRenderData(IntPtr gvc, IntPtr g,
              string format, out IntPtr result, out int length);

        /// 
        /// Release render resources.
        /// 
        [DllImport(LIB_GVC, CallingConvention = CallingConvention.Cdecl)]
        private static extern int gvFreeRenderData(IntPtr result);

        #region Method
        public void CreateGraphVisualization()
        {
            if (image != null)
                return;
            
            // Max count of vertices
            int sumVertices = 0;
            int maxCountColors = 0;

            foreach (Graph.IGraphInterface graph in graphList)
            {
                sumVertices += graph.GetRealCountVertices();
                if (graph.GetColoredGraph().GetIsInitializedColoredGraph())
                {
                    if (maxCountColors < graph.GetColoredGraph().GetCountUsedColors())
                        maxCountColors = graph.GetColoredGraph().GetCountUsedColors();
                }
            }

            if (sumVertices > MAXALLOWEDVERTICES)
            {
                CreateImageGraphWithTest("The graph has so many vertices. \nMaximum " + MAXALLOWEDVERTICES + " vertices are allowed.");
                return;
            }

            if (maxCountColors > MAXCOLORS)
            {
                CreateImageGraphWithTest("The graph has so many colors. \nMaximum " + MAXCOLORS + " colors are allowed.");
                return;
            }

            // Convertor from graph to DOT
            convertGraphToDot = new ConvertGraphToDot(graphList, isSchedule, showSpanningTree, showSimplicialVertex, showCutVerticesAndBridges, showMaximumAndMinimumDegreeVertices);
            string source = convertGraphToDot.Convert();
            string format = fileNameExtensionEnum.ToString();
            
            try
            {
                // Create a Graphviz context
                IntPtr gvc = gvContext();
                if (gvc == IntPtr.Zero)
                    throw new MyException.GraphVisualizationException.GraphVisualizationGeneratingGraphException("Failed to create Graphviz context.");

                // Load the DOT data into a graph
                IntPtr g = agmemread(source);
                if (g == IntPtr.Zero)
                    throw new MyException.GraphVisualizationException.GraphVisualizationGeneratingGraphException("Failed to create graph from source. Check for syntax errors.");

                // Apply a layout
                if (gvLayout(gvc, g, "dot") != SUCCESS)
                    throw new MyException.GraphVisualizationException.GraphVisualizationGeneratingGraphException("Layout failed.");

                IntPtr result;
                int length;

                // Render the graph
                if (gvRenderData(gvc, g, format, out result, out length) != SUCCESS)
                    throw new MyException.GraphVisualizationException.GraphVisualizationGeneratingGraphException("Render failed.");

                // Create an array to hold the rendered graph
                byte[] bytes = new byte[length];

                // Copy the image from the IntPtr
                Marshal.Copy(result, bytes, 0, length);

                // Free up the resources
                gvFreeRenderData(result);
                gvFreeLayout(gvc, g);
                agclose(g);
                gvFreeContext(gvc);
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    image = Image.FromStream(stream);
                }
            }
            catch (Exception e)
            {
                if (e is DllNotFoundException || e is BadImageFormatException || e is MyException.GraphVisualizationException.GraphVisualizationException)
                    CreateImageGraphWithTest("Problem with GraphViz library! \nThe graph can't be visualizated!");
                else
                    throw e;
            }
        }
        #endregion

        #region Property
        public List<Graph.IGraphInterface> GetGraphList()
        {
            return graphList;
        }

        public Image GetImage()
        {
            if (image == null)
                throw new MyException.GraphVisualizationException.GraphVisualizationNotInitializationException();

            return image;
        }

        private void CreateImageGraphWithTest(string text)
        {
            Bitmap bitmap = new Bitmap(500, 500);

            RectangleF rectanglef = new RectangleF(0, 100, 500, 300);

            Graphics g = Graphics.FromImage(bitmap);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawString(text, new Font("Ariel", 20), Brushes.Black, rectanglef);

            g.Flush();

            image = bitmap;
        }
        #endregion
    }
}
