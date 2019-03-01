using System;
using Microsoft.ML.Data;

namespace GraphColoring.GraphColoringAlgorithm.AI
{
    public class GraphData
    {
        [LoadColumn(0)]
        [ColumnName("Label")]
        public bool Label { get; set; }
        [LoadColumn(1)]
        public float ID_GraphClass { get; set; }
        [LoadColumn(2)]
        public float ID_EulerianGraph { get; set; }
        [LoadColumn(3)]
        public bool IsRegular { get; set; }
        [LoadColumn(4)]
        public bool IsCyclic { get; set; }
        [LoadColumn(5)]
        public bool IsChordal { get; set; }
        [LoadColumn(6)]
        public float CountVertices { get; set; }
        [LoadColumn(7)]
        public float CountEdges { get; set; }
        [LoadColumn(8)]
        public float CountCutVertices { get; set; }
        [LoadColumn(9)]
        public float CountBridges { get; set; }
        [LoadColumn(10)]
        public float Girth { get; set; }
        [LoadColumn(11)]
        public float Dense { get; set; }
        [LoadColumn(12)]
        public float MinimumVertexDegree { get; set; }
        [LoadColumn(13)]
        public float MaximumVertexDegree { get; set; }
        [LoadColumn(14)]
        public float AverageVertexDegree { get; set; }
        [LoadColumn(15)]
        public float MedianVertexDegree { get; set; }
        [LoadColumn(16)]
        public float VertexDegree0 { get; set; }
        [LoadColumn(17)]
        public float VertexDegree1 { get; set; }
        [LoadColumn(18)]
        public float VertexDegree2 { get; set; }
        [LoadColumn(19)]
        public float VertexDegree3 { get; set; }
        [LoadColumn(20)]
        public float VertexDegree4 { get; set; }
        [LoadColumn(21)]
        public float VertexDegree5 { get; set; }
        [LoadColumn(22)]
        public float VertexDegree6 { get; set; }
        [LoadColumn(23)]
        public float VertexDegree7 { get; set; }
        [LoadColumn(24)]
        public float VertexDegree8 { get; set; }
        [LoadColumn(25)]
        public float VertexDegree9 { get; set; }
        [LoadColumn(26)]
        public float VertexDegree10 { get; set; }
        //[LoadColumn(27,65)]
        //public float[] VertexDegree { get; set; }

        override
        public string ToString()
        {
            return "ID_GraphClass: " + ID_GraphClass + "\n" + 
                " ID_EulerianGraph: " + ID_EulerianGraph + "\n" +
                " IsRegular: " + IsRegular + "\n" +
                " IsCyclic: " + IsCyclic + "\n" +
                " IsChordal: " + IsChordal + "\n" +
                " CountVertices: " + CountVertices + "\n" +
                " CountEdges: " + CountEdges + "\n" +
                " CountCutVertices: " + CountCutVertices + "\n" +
                " CountBridges: " + CountBridges + "\n" +
                " Girth: " + Girth + "\n" +
                " Dense: " + Dense + "\n" +
                " MinimumVertexDegree: " + MinimumVertexDegree + "\n" +
                " MaximumVertexDegree: " + MaximumVertexDegree + "\n" +
                " AverageVertexDegree: " + AverageVertexDegree + "\n" +
                " MedianVertexDegree: " + MedianVertexDegree + "\n" +
                " VertexDegree0: " + VertexDegree0 + "\n" +
                " VertexDegree1: " + VertexDegree1 + "\n" +
                " VertexDegree2: " + VertexDegree2 + "\n" +
                " VertexDegree3: " + VertexDegree3 + "\n" +
                " VertexDegree4: " + VertexDegree4 + "\n" +
                " VertexDegree5: " + VertexDegree5 + "\n" +
                " VertexDegree6: " + VertexDegree6 + "\n" +
                " VertexDegree7: " + VertexDegree7 + "\n" +
                " VertexDegree8: " + VertexDegree8 + "\n" +
                " VertexDegree9: " + VertexDegree9 + "\n" +
                " VertexDegree10: " + VertexDegree10;
        }
    }

    public class AlgorithmPrediction
    {
        [ColumnName("PredictedLabel")]
        public bool Prediction { get; set; }

        [ColumnName("Probability")]
        public float Probability { get; set; }

        [ColumnName("Score")]
        public float Score { get; set; }
    }
}
