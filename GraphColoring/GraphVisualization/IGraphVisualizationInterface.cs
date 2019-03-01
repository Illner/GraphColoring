using System;
using System.Drawing;
using System.Collections.Generic;


namespace GraphColoring.GraphVisualization
{
    interface IGraphVisualizationInterface
    {
        void CreateGraphVisualization();
        Image GetImage();
        List<Graph.IGraphInterface> GetGraphList();
    }
}
