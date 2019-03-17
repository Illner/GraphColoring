﻿using System;
using System.IO;
using System.Collections.Generic;

// ML
using Microsoft.ML;
using Microsoft.ML.Core.Data;

namespace GraphColoring.GraphColoringAlgorithm.AI
{
    public static class AIAlgorithm
    {
        // Variable
        #region
        private static MLContext mlContext;
        private static Dictionary<GraphColoringAlgorithm.GraphColoringAlgorithmEnum, string> modelPathDictionary;
        private static Dictionary<GraphColoringAlgorithm.GraphColoringAlgorithmEnum, ITransformer> modelDictionary;
        private static Dictionary<GraphColoringAlgorithm.GraphColoringAlgorithmEnum, AlgorithmPrediction> predictionDictionary;
        private static Dictionary<GraphColoringAlgorithm.GraphColoringAlgorithmEnum, double> aucDictionary;

        private static string pathModels = @"AIModels/";
        private static string randomSequenceModelPath = pathModels + "model-randomSequence.zip";
        private static string largestFirstSequenceModelPath = pathModels + "model-largestFirstSequence.zip";
        private static string smallestLastSequenceModelPath = pathModels + "model-smallestLastSequence.zip";
        private static string randomSequenceInterchangeModelPath = pathModels + "model-randomSequenceInterchange.zip";
        private static string largestFirstSequenceInterchangeModelPath = pathModels + "model-largestFirstSequenceInterchange.zip";
        private static string smallestLastSequenceInterchangeModelPath = pathModels + "model-smallestLastSequenceInterchange.zip";
        private static string connectedSequentialModelPath = pathModels + "model-connectedSequential.zip";
        private static string saturationLargestFirstSequenceModelPath = pathModels + "model-saturationLargestFirstSequence.zip";
        private static string geneticAlgorithmModelPath = pathModels + "model-geneticAlgortihm.zip";
        private static string greedyIndependentSetModelPath = pathModels + "model-greedyIndependentSet.zip";
        private static string combinationAlgorithmModelPath = pathModels + "model-combinationAlgorithm.zip";
        #endregion

        // Constructor
        #region
        static AIAlgorithm()
        {
            mlContext = new MLContext();
            
            // Fill dictionaries
            modelPathDictionary = new Dictionary<GraphColoringAlgorithm.GraphColoringAlgorithmEnum, string>()
            {
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence, randomSequenceModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequence, largestFirstSequenceModelPath },
                //{ GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequence, smallestLastSequenceModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchange, randomSequenceInterchangeModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchange, largestFirstSequenceInterchangeModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchange, smallestLastSequenceInterchangeModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedSequential, connectedSequentialModelPath },
                //{ GraphColoringAlgorithm.GraphColoringAlgorithmEnum.saturationLargestFirstSequence, saturationLargestFirstSequenceModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.greedyIndependentSet, greedyIndependentSetModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm, geneticAlgorithmModelPath },
                //{ GraphColoringAlgorithm.GraphColoringAlgorithmEnum.combinationAlgorithm, combinationAlgorithmModelPath }
            };

            Console.WriteLine("--------------------------------------------------");
            modelDictionary = new Dictionary<GraphColoringAlgorithm.GraphColoringAlgorithmEnum, ITransformer>();
            foreach (var record in modelPathDictionary)
            {
                if (!File.Exists(record.Value))
                    continue;

                try
                {
                    modelDictionary.Add(record.Key, LoadModel(record.Value));
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error loading model - " + record.Key);
                }
            }
            Console.WriteLine("Loaded models: " + modelDictionary.Count);
            Console.WriteLine("--------------------------------------------------");

            // Fill aucDictionary
            aucDictionary = new Dictionary<GraphColoringAlgorithm.GraphColoringAlgorithmEnum, double>()
            {
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence, 0},
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequence, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequence, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchange, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchange, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchange, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedSequential, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.saturationLargestFirstSequence, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.greedyIndependentSet, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.combinationAlgorithm, 0 }
            };
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Return "the best" algorithm for a graph
        /// </summary>
        /// <param name="graphData">features - graph</param>
        /// <returns>algorithm</returns>
        public static GraphColoringAlgorithm.GraphColoringAlgorithmEnum ChooseAlgorithm(GraphData graphData)
        {
            predictionDictionary = new Dictionary<GraphColoringAlgorithm.GraphColoringAlgorithmEnum, AlgorithmPrediction>();
            AlgorithmPrediction algorithmPrediction;

            foreach(var record in modelDictionary)
            {
                algorithmPrediction = GetPrediction(record.Value, graphData);
                predictionDictionary.Add(record.Key, algorithmPrediction);
            }

            GraphColoringAlgorithm.GraphColoringAlgorithmEnum algorithmEnum = GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm;
            float score = float.MinValue;
            foreach(var record in predictionDictionary)
            {
                if (score < record.Value.Score)
                {
                    score = record.Value.Score;
                    algorithmEnum = record.Key;
                }
            }
            
            return algorithmEnum;
        }
        
        /// <summary>
        /// Use model prediction
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="graphData">features - graph</param>
        /// <returns>label</returns>
        private static AlgorithmPrediction GetPrediction(ITransformer model, GraphData graphData)
        {
            var predictionEngine = model.CreatePredictionEngine<GraphData, AlgorithmPrediction>(mlContext);
            var prediction = predictionEngine.Predict(graphData);

            AlgorithmPrediction algorithmPrediction = new AlgorithmPrediction();
            algorithmPrediction.Prediction = prediction.Prediction;
            algorithmPrediction.Probability = prediction.Probability;
            algorithmPrediction.Score = prediction.Score;

            return algorithmPrediction;
        }

        /// <summary>
        /// Load a model
        /// </summary>
        /// <param name="model">path</param>
        /// <returns>loaded model</returns>
        private static ITransformer LoadModel(string model)
        {
            ITransformer loadedModel;
            using (var stream = new FileStream(model, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                loadedModel = mlContext.Model.Load(stream);
            }

            return loadedModel;
        }
        #endregion

        // Property
        #region

        #endregion
    }
}