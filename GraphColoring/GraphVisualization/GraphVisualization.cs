using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace GraphColoring.GraphVisualization
{
    partial class GraphVisualization : IGraphVisualizationInterface
    {
        // Variable
        private Image image;
        private List<Graph.IGraphInterface> graphList;
        private const int MAXALLOWEDVERTICES = 100;
        private FileNameExtensionEnum fileNameExtensionEnum;
        private IConvertGraphToDotInterface convertGraphToDot;

        private const string LIB_GVC = @".\GraphVisualization\DOT\gvc.dll";
        private const string LIB_GRAPH = @".\GraphVisualization\DOT\cgraph.dll";
        private const int SUCCESS = 0;

        // Constructor
        /// <summary>
        /// Constructor
        /// Default value for fileNameExtensionEnum is jpg
        /// </summary>
        /// <param name="graph">The graph</param>
        public GraphVisualization(Graph.IGraphInterface graph, FileNameExtensionEnum fileNameExtensionEnum = FileNameExtensionEnum.jpg)
        {
            graphList = new List<Graph.IGraphInterface>();
            graphList.Add(graph);
            this.fileNameExtensionEnum = fileNameExtensionEnum;
        }

        public GraphVisualization(List<Graph.IGraphInterface> graphList, FileNameExtensionEnum fileNameExtensionEnum = FileNameExtensionEnum.jpg)
        {
            this.graphList = graphList;
            this.fileNameExtensionEnum = fileNameExtensionEnum;
        }

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

        // Method
        public void CreateGraphVisualization()
        {
            if (image != null)
                return;
            
            // Max count of vertices
            int sumVertices = 0;
            foreach (Graph.IGraphInterface graph in graphList)
                sumVertices += graph.GetRealCountVertices();

            if (sumVertices > MAXALLOWEDVERTICES)
            {
                CreateImageGraphToLarge();
                return;
            }

            // Convertor from graph to DOT
            convertGraphToDot = new ConvertGraphToDot(graphList);
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
            catch (DllNotFoundException e)
            {
                throw new MyException.GraphVisualizationException.GraphVisualizationDLLDontExistException(e.ToString());
            }
        }

        // Property
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

        private void CreateImageGraphToLarge()
        {
            Bitmap bitmap = new Bitmap(500, 500);

            RectangleF rectanglef = new RectangleF(0, 100, 500, 300);

            Graphics g = Graphics.FromImage(bitmap);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawString("The graph has so many vertices. \nMaximum " + MAXALLOWEDVERTICES + " vertices are allowed.", new Font("Ariel", 20), Brushes.Black, rectanglef);

            g.Flush();

            image = bitmap;
        }
    }
}
