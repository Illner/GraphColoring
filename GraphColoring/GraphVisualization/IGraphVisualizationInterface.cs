using System;
using System.Collections.Generic;
using System.Drawing;


namespace GraphColoring.GraphVisualization
{
    interface IGraphVisualizationInterface
    {
        void CreateGraphVisualization();
        Image GetImage();
        List<Graph.IGraphInterface> GetGraphList();
    }
}
