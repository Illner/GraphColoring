using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

// ML
using Microsoft.ML;
using Microsoft.ML.Core.Data;

namespace GraphColoring.GraphColoringAlgorithm.AI
{
    public static class AIAlgorithm
    {
        #region Variable
        private static MLContext mlContext;
        private static Dictionary<GraphColoringAlgorithm.GraphColoringAlgorithmEnum, string> modelPathDictionary;
        private static Dictionary<GraphColoringAlgorithm.GraphColoringAlgorithmEnum, ITransformer> modelDictionary;
        private static Dictionary<GraphColoringAlgorithm.GraphColoringAlgorithmEnum, AlgorithmPrediction> predictionDictionary;
        private static Dictionary<GraphColoringAlgorithm.GraphColoringAlgorithmEnum, float> accuracyDictionary;
        private static Dictionary<GraphColoringAlgorithm.GraphColoringAlgorithmEnum, GraphColoringAlgorithm.TimeComplexityEnum> timeComplexityDictionary;

        private static string pathModels = @"GraphColoringAlgorithm/AIModels/";

        private static string connectedLargestFirstAlgorithmModelPath = pathModels + "model-connectedLargestFirst.zip";
        private static string connectedLargestFirstInterchangeAlgorithmModelPath = pathModels + "model-connectedLargestFirstInterchange.zip";
        private static string connectedLargestFirstInterchangeExtendedAlgorithmModelPath = pathModels + "model-connectedLargestFirstInterchangeExtended.zip";
        private static string connectedLargestFirstInterchangeExtendedK3AlgorithmModelPath = pathModels + "model-connectedLargestFirstInterchangeExtendedK3.zip";

        private static string randomSequenceModelPath = pathModels + "model-randomSequence.zip";
        private static string randomSequenceInterchangeModelPath = pathModels + "model-randomSequenceInterchange.zip";
        private static string randomSequenceInterchangeExtendedModelPath = pathModels + "model-randomSequenceInterchangeExtended.zip";
        private static string randomSequenceInterchangeExtendedK3ModelPath = pathModels + "model-randomSequenceInterchangeExtendedK3.zip";

        private static string largestFirstSequenceModelPath = pathModels + "model-largestFirstSequence.zip";
        private static string largestFirstSequenceInterchangeModelPath = pathModels + "model-largestFirstSequenceInterchange.zip";
        private static string largestFirstSequenceInterchangeExtendedModelPath = pathModels + "model-largestFirstSequenceInterchangeExtended.zip";
        private static string largestFirstSequenceInterchangeExtendedK3ModelPath = pathModels + "model-largestFirstSequenceInterchangeExtendedK3.zip";

        private static string smallestLastSequenceModelPath = pathModels + "model-smallestLastSequence.zip";
        private static string smallestLastSequenceInterchangeModelPath = pathModels + "model-smallestLastSequenceInterchange.zip";
        private static string smallestLastSequenceInterchangeExtendedModelPath = pathModels + "model-smallestLastSequenceInterchangeExtended.zip";
        private static string smallestLastSequenceInterchangeExtendedK3ModelPath = pathModels + "model-smallestLastSequenceInterchangeExtendedK3.zip";

        private static string connectedSequentialModelPath = pathModels + "model-connectedSequential.zip";
        private static string saturationLargestFirstSequenceModelPath = pathModels + "model-saturationLargestFirstSequence.zip";
        private static string greedyIndependentSetModelPath = pathModels + "model-greedyIndependentSet.zip";
        private static string combinationAlgorithmModelPath = pathModels + "model-combinationAlgorithm.zip";

        private static string geneticAlgorithmModelPath = pathModels + "model-geneticAlgorithm.zip";
        private static string geneticAlgorithm2ModelPath = pathModels + "model-geneticAlgorithm2.zip";
        #endregion
        
        #region Constructor
        static AIAlgorithm()
        {
            mlContext = new MLContext();
            
            // Fill dictionaries
            modelPathDictionary = new Dictionary<GraphColoringAlgorithm.GraphColoringAlgorithmEnum, string>()
            {
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirst, connectedLargestFirstAlgorithmModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchange, connectedLargestFirstInterchangeAlgorithmModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchangeExtended, connectedLargestFirstInterchangeExtendedAlgorithmModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchangeExtendedK3, connectedLargestFirstInterchangeExtendedK3AlgorithmModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence, randomSequenceModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchange, randomSequenceInterchangeModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtended, randomSequenceInterchangeExtendedModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtendedK3, randomSequenceInterchangeExtendedK3ModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequence, largestFirstSequenceModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchange, largestFirstSequenceInterchangeModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtended, largestFirstSequenceInterchangeExtendedModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtendedK3, largestFirstSequenceInterchangeExtendedK3ModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequence, smallestLastSequenceModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchange, smallestLastSequenceInterchangeModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtended, smallestLastSequenceInterchangeExtendedModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtendedK3, smallestLastSequenceInterchangeExtendedK3ModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedSequential, connectedSequentialModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.saturationLargestFirstSequence, saturationLargestFirstSequenceModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.greedyIndependentSet, greedyIndependentSetModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.combinationAlgorithm, combinationAlgorithmModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm, geneticAlgorithmModelPath },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm2, geneticAlgorithm2ModelPath }
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

            // Fill accuracyDictionary
            accuracyDictionary = new Dictionary<GraphColoringAlgorithm.GraphColoringAlgorithmEnum, float>()
            {
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirst, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchange, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchangeExtended, 0.737f },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchangeExtendedK3, 0.84f },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchange, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtended, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtendedK3, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequence, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchange, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtended, 0.76f },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtendedK3, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequence, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchange, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtended, 0.79f },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtendedK3, 0.825f },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedSequential, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.saturationLargestFirstSequence, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.greedyIndependentSet, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.combinationAlgorithm, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm, 0 },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm2, 0.956f }
            };

            // Fill timeComplexityDictionary
            timeComplexityDictionary = new Dictionary<GraphColoringAlgorithm.GraphColoringAlgorithmEnum, GraphColoringAlgorithm.TimeComplexityEnum>()
            {
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirst, GraphColoringAlgorithm.TimeComplexityEnum.quadratic },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchange, GraphColoringAlgorithm.TimeComplexityEnum.cubic },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchangeExtended, GraphColoringAlgorithm.TimeComplexityEnum.quartic  },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedLargestFirstInterchangeExtendedK3, GraphColoringAlgorithm.TimeComplexityEnum.quintic  },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequence, GraphColoringAlgorithm.TimeComplexityEnum.linear },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchange, GraphColoringAlgorithm.TimeComplexityEnum.cubic  },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtended, GraphColoringAlgorithm.TimeComplexityEnum.quartic  },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.randomSequenceInterchangeExtendedK3, GraphColoringAlgorithm.TimeComplexityEnum.quintic  },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequence, GraphColoringAlgorithm.TimeComplexityEnum.quadratic  },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchange, GraphColoringAlgorithm.TimeComplexityEnum.cubic },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtended, GraphColoringAlgorithm.TimeComplexityEnum.quartic },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.largestFirstSequenceInterchangeExtendedK3, GraphColoringAlgorithm.TimeComplexityEnum.quintic  },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequence, GraphColoringAlgorithm.TimeComplexityEnum.quadraticPlusMultiply },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchange, GraphColoringAlgorithm.TimeComplexityEnum.cubic  },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtended, GraphColoringAlgorithm.TimeComplexityEnum.quartic  },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtendedK3, GraphColoringAlgorithm.TimeComplexityEnum.quintic  },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.connectedSequential, GraphColoringAlgorithm.TimeComplexityEnum.linear  },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.saturationLargestFirstSequence, GraphColoringAlgorithm.TimeComplexityEnum.cubic  },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.greedyIndependentSet, GraphColoringAlgorithm.TimeComplexityEnum.cubic  },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.combinationAlgorithm, GraphColoringAlgorithm.TimeComplexityEnum.cubic  },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm, GraphColoringAlgorithm.TimeComplexityEnum.quadraticPlusMultiply },
                { GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm2, GraphColoringAlgorithm.TimeComplexityEnum.cubicPlusQuadratic   }
            };
        }
        #endregion
        
        #region Method
        /// <summary>
        /// Return "the best" algorithm for a graph
        /// </summary>
        /// <param name="graphData">features - graph</param>
        /// <returns>algorithm</returns>
        public static GraphColoringAlgorithm.GraphColoringAlgorithmEnum ChooseAlgorithm(GraphData graphData)
        {
            AlgorithmPrediction algorithmPrediction;
            GraphColoringAlgorithm.GraphColoringAlgorithmEnum algorithmEnum;
            predictionDictionary = new Dictionary<GraphColoringAlgorithm.GraphColoringAlgorithmEnum, AlgorithmPrediction>();

            // In case no models loaded
            if (graphData.CountVertices < 50)
                algorithmEnum = GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm2;
            else
                algorithmEnum = GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtended;

            if (modelDictionary.Count == 0)
                return algorithmEnum;

            if (modelDictionary.Count == 1)
                return modelDictionary.First().Key;
            
            foreach (var record in modelDictionary)
            {
                if (graphData.CountVertices > 60 && record.Key == GraphColoringAlgorithm.GraphColoringAlgorithmEnum.geneticAlgorithm2)
                    continue;

                algorithmPrediction = GetPrediction(record.Value, graphData);
                if (!algorithmPrediction.Prediction)
                    continue;

                accuracyDictionary.TryGetValue(record.Key, out var temp);
                algorithmPrediction.Score = algorithmPrediction.Probability + 0.4f * temp;
                predictionDictionary.Add(record.Key, algorithmPrediction);
            }

            if (predictionDictionary.Count == 0)
                return GraphColoringAlgorithm.GraphColoringAlgorithmEnum.smallestLastSequenceInterchangeExtended;

            // Some models exist - sort models
            List<GraphColoringAlgorithm.GraphColoringAlgorithmEnum> sortedAlgorithmsList = (from entry in predictionDictionary
                                                                                            orderby entry.Value.Score
                                                                                            descending
                                                                                            select entry.Key).Take(2).ToList();

            if (sortedAlgorithmsList.Count == 1)
                return sortedAlgorithmsList[0];

            // Variable
            GraphColoringAlgorithm.GraphColoringAlgorithmEnum firstAlgorithm, secondAlgorithm;
            float firstScore, secondScore;

            firstAlgorithm = sortedAlgorithmsList[0];
            secondAlgorithm = sortedAlgorithmsList[1];

            predictionDictionary.TryGetValue(firstAlgorithm, out var temp1);
            firstScore = temp1.Probability;
            predictionDictionary.TryGetValue(secondAlgorithm, out var temp2);
            secondScore = temp2.Probability;

            // Similar scores
            if (Math.Abs(firstScore - secondScore) < 0.01)
            {
                    // Variable
                    GraphColoringAlgorithm.TimeComplexityEnum firstTimeComplexity = GraphColoringAlgorithm.TimeComplexityEnum.undefined, 
                                                              secondTimeComplexity = GraphColoringAlgorithm.TimeComplexityEnum.undefined;

                    timeComplexityDictionary.TryGetValue(firstAlgorithm, out firstTimeComplexity);
                    timeComplexityDictionary.TryGetValue(secondAlgorithm, out secondTimeComplexity);

                    if (GraphColoringAlgorithm.GetOrder(firstTimeComplexity) <= GraphColoringAlgorithm.GetOrder(secondTimeComplexity))
                        return firstAlgorithm;
                    else
                        return secondAlgorithm;
            }

            return firstAlgorithm;
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
    }
}
