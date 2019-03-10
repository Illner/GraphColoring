using System;
using System.IO;
using System.Linq;
using Microsoft.Data.DataView;

// ML
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Training;
using Microsoft.ML.Core.Data;
using Microsoft.ML.StaticPipe;

namespace AI.ML
{
    partial class CreateAI
    {
        // Variable
        #region
        private AIEnum aiEnum;
        private MLContext mlContext;
        private ITransformer trainedModel;
        private IDataView data, testData, trainingData;
        EstimatorChain<ISingleFeaturePredictionTransformer<IPredictorProducing<float>>> pipeline;
        GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum algorithmEnum;
        private string pathData = @"Data\data.tsv";
        private string pathModel;

        private bool modelExists = false;
        private double testFraction = 0.3;

        // Evaluation
        private double accuracy;
        private double logLoss;
        private double logLossReduction;
        private double F1Score;
        private double auc;
        #endregion

        // Constructor
        #region
        /// <summary>
        /// Initialize CreateAI
        /// </summary>
        /// <param name="aiEnum">ML binary trainer</param>
        /// <param name="algorithmEnum">algorithm</param>
        /// <param name="generateData">download data from the DB?</param>
        public CreateAI(AIEnum aiEnum, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum algorithmEnum, bool generateData = true)
        {
            // Generate data
            if (generateData)
            {
                Database.Database database = new Database.Database();
                database.SaveDataFromDatabaseToFile(GetPathData(), algorithmEnum);
            }

            // Check if data file exists
            if (!File.Exists(GetPathData()))
                throw new MyException.AIException.DataFileDoesntExist();

            this.aiEnum = aiEnum;
            this.algorithmEnum = algorithmEnum;
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Create a binary classification model
        /// </summary>
        public void CreateModel()
        {
            // Reset
            mlContext = new MLContext();
            trainedModel = null;
            modelExists = false;

            accuracy = double.NaN;
            logLoss = double.NaN;
            logLossReduction = double.NaN;
            F1Score = double.NaN;
            auc = double.NaN;

            //Console.WriteLine("=============== Loading the model ===============");
            LoadData();
            
            //Console.WriteLine("=============== Transforming data ===============");
            TransformData();

            //Console.WriteLine("=============== Training the mdoel ===============");
            TrainModel();
            
            //Console.WriteLine("=============== Evaluating the mdoel ===============");
            EvaluateModel();

            //Console.WriteLine("=============== Saving the mdoel ===============");
            SaveModel();
        }
        
        private void LoadData()
        {
            // Load data
            data = mlContext.Data.ReadFromTextFile<GraphColoring.GraphColoringAlgorithm.AI.GraphData>(path: pathData, hasHeader: true, separatorChar: '\t');
            (trainingData, testData) = mlContext.MulticlassClassification.TrainTestSplit(data, testFraction);
        }

        private void TransformData()
        {
            // Select type of trainer
            ITrainerEstimator<ISingleFeaturePredictionTransformer<IPredictorProducing<float>>, IPredictorProducing<float>> trainer = null;
            Console.WriteLine("-------------------");
            switch (aiEnum)
            {
                case AIEnum.fastTree:
                    Console.WriteLine("fastTree");
                    trainer = mlContext.BinaryClassification.Trainers.FastTree();
                    break;
                case AIEnum.generalizedAdditiveModels:
                    Console.WriteLine("generalizedAdditiveModels");
                    trainer = mlContext.BinaryClassification.Trainers.GeneralizedAdditiveModels();
                    break;
                case AIEnum.logisticRegression:
                    Console.WriteLine("logisticRegression");
                    trainer = mlContext.BinaryClassification.Trainers.LogisticRegression();
                    break;
                case AIEnum.stochasticDualCoordinateAscent:
                    Console.WriteLine("stochasticDualCoordinateAscent");
                    trainer = mlContext.BinaryClassification.Trainers.StochasticDualCoordinateAscent();
                    break;
                case AIEnum.stochasticGradientDescent:
                    Console.WriteLine("stochasticGradientDescent");
                    trainer = mlContext.BinaryClassification.Trainers.StochasticGradientDescent();
                    break;
            }
            Console.WriteLine("-------------------");

            // Create a pipeline
            pipeline = mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "IsRegularOneHot", inputColumnName: "IsRegular")
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "IsCyclicOneHot", inputColumnName: "IsCyclic"))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "IsChordalOneHot", inputColumnName: "IsChordal"))
                //.Append(mlContext.Transforms.Conversion.MapValueToKey("Label"))
                /*.Append(mlContext.Transforms.Normalize(
                    new NormalizingEstimator.MinMaxColumn(inputColumnName: "ID_GraphClass", outputColumnName: "ID_GraphClassNormalized", fixZero: true),
                    new NormalizingEstimator.MinMaxColumn(inputColumnName: "ID_EulerianGraph", outputColumnName: "ID_EulerianGraphNormalized", fixZero: true),
                    new NormalizingEstimator.MinMaxColumn(inputColumnName: "IsRegularOneHot", outputColumnName: "IsRegularOneHotNormalized", fixZero: true),
                    new NormalizingEstimator.MinMaxColumn(inputColumnName: "IsCyclicOneHot", outputColumnName: "IsCyclicOneHotNormalized", fixZero: true),
                    new NormalizingEstimator.MinMaxColumn(inputColumnName: "IsChordalOneHot", outputColumnName: "IsChordalOneHotNormalized", fixZero: true),
                    new NormalizingEstimator.MinMaxColumn(inputColumnName: "CountVertices", outputColumnName: "CountVerticesNormalized", fixZero: true),
                    new NormalizingEstimator.MinMaxColumn(inputColumnName: "CountEdges", outputColumnName: "CountEdgesNormalized", fixZero: true),
                    new NormalizingEstimator.MinMaxColumn(inputColumnName: "CountCutVertices", outputColumnName: "CountCutVerticesNormalized", fixZero: true),
                    new NormalizingEstimator.MinMaxColumn(inputColumnName: "CountBridges", outputColumnName: "CountBridgesNormalized", fixZero: true),
                    new NormalizingEstimator.MinMaxColumn(inputColumnName: "Girth", outputColumnName: "GirthNormalized", fixZero: true),
                    new NormalizingEstimator.MinMaxColumn(inputColumnName: "Dense", outputColumnName: "DenseNormalized", fixZero: true),
                    new NormalizingEstimator.MinMaxColumn(inputColumnName: "MinimumVertexDegree", outputColumnName: "MinimumVertexDegreeNormalized", fixZero: true),
                    new NormalizingEstimator.MinMaxColumn(inputColumnName: "MaximumVertexDegree", outputColumnName: "MaximumVertexDegreeNormalized", fixZero: true),
                    new NormalizingEstimator.MinMaxColumn(inputColumnName: "AverageVertexDegree", outputColumnName: "AverageVertexDegreeNormalized", fixZero: true),
                    new NormalizingEstimator.MinMaxColumn(inputColumnName: "MedianVertexDegree", outputColumnName: "MedianVertexDegreeNormalized", fixZero: true)))*/
                /*.Append(mlContext.Transforms.Concatenate(DefaultColumnNames.Features, "ID_GraphClassNormalized", "ID_EulerianGraphNormalized", "IsRegularOneHotNormalized", "IsCyclicOneHotNormalized", "IsChordalOneHotNormalized",
                "CountVerticesNormalized", "CountEdgesNormalized", "CountCutVerticesNormalized", "CountBridgesNormalized", "GirthNormalized", "DenseNormalized", "MinimumVertexDegreeNormalized", "MaximumVertexDegreeNormalized", "AverageVertexDegreeNormalized", "MedianVertexDegreeNormalized",
                "VertexDegree1", "VertexDegree2", "VertexDegree3", "VertexDegree4", "VertexDegree5", "VertexDegree6", "VertexDegree7", "VertexDegree8", "VertexDegree9", "VertexDegree10", "VertexDegree"))*/
                .Append(mlContext.Transforms.Concatenate(DefaultColumnNames.Features, "ID_GraphClass", "ID_EulerianGraph", "IsRegularOneHot", "IsCyclicOneHot", "IsChordalOneHot",
                "CountVertices", "CountEdges", "CountCutVertices", /*"CountBridges",*/ "Girth", "Dense", "MinimumVertexDegree", "MaximumVertexDegree", "AverageVertexDegree", /*"MedianVertexDegree",*/
                "VertexDegree1", "VertexDegree2", "VertexDegree3", "VertexDegree4", "VertexDegree5", "VertexDegree6", "VertexDegree7", "VertexDegree8", "VertexDegree9", "VertexDegree10"))//, "VertexDegree"))
                .AppendCacheCheckpoint(mlContext)
            //.Append(mlContext.BinaryClassification.Trainers.AveragedPerceptron(labelColumn: DefaultColumnNames.Label, featureColumn: DefaultColumnNames.Features));
            .Append(trainer);
        }

        private void TrainModel()
        {
            trainedModel = pipeline.Fit(trainingData);
            modelExists = true;
        }

        private void EvaluateModel()
        {
            var predictions = trainedModel.Transform(testData);
            var metrics = mlContext.BinaryClassification.Evaluate(data: predictions, label: DefaultColumnNames.Label);

            accuracy = metrics.Accuracy;
            logLoss = metrics.LogLoss;
            logLossReduction = metrics.LogLossReduction;
            F1Score = metrics.F1Score;
            auc = metrics.Auc;
        }

        /// <summary>
        /// Save the model
        /// If the model doesn't exist throws ModelDoesntExistException
        /// </summary>
        public void SaveModel()
        {
            pathModel = @"Data\model" + DateTime.Now.ToString("-dd-MM-yyyy-HH-mm-ss-") + algorithmEnum.ToString() + ".zip";

            using (var fileStream = new FileStream(pathModel, FileMode.Create, FileAccess.Write, FileShare.Write))
                mlContext.Model.Save(trainedModel, fileStream);
        }
        #endregion

        // Property
        #region
        /// <summary>
        /// Return a path
        /// </summary>
        /// <returns>path</returns>
        public string GetPathData()
        {
            return pathData;
        }

        /// <summary>
        /// Return path
        /// If the model doesn't exist throws ModelDoesntExistException
        /// </summary>
        /// <returns>path</returns>
        public string GetPathModel()
        {
            if (!modelExists)
                throw new MyException.AIException.ModelDoesntExistException();

            return pathModel;
        }

        /// <summary>
        /// Return accurancy
        /// If the model doesn't exist throws ModelDoesntExistException
        /// </summary>
        /// <returns>accurancy</returns>
        public double GetAccurancy()
        {
            if (!modelExists)
                throw new MyException.AIException.ModelDoesntExistException();

            return accuracy;
        }

        /// <summary>
        /// Return LogLoss
        /// If the model doesn't exist throws ModelDoesntExistException
        /// </summary>
        /// <returns>LogLoss</returns>
        public double GetLogLoss()
        {
            if (!modelExists)
                throw new MyException.AIException.ModelDoesntExistException();

            return logLoss;
        }

        /// <summary>
        /// Return LogLoss reduction
        /// If the model doesn't exist throws ModelDoesntExistException
        /// </summary>
        /// <returns>LogLoss reduction</returns>
        public double GetLogLossReduction()
        {
            if (!modelExists)
                throw new MyException.AIException.ModelDoesntExistException();

            return logLossReduction;
        }

        public double GetF1Score()
        {
            if (!modelExists)
                throw new MyException.AIException.ModelDoesntExistException();

            return F1Score;
        }

        public double GetAuc()
        {
            if (!modelExists)
                throw new MyException.AIException.ModelDoesntExistException();

            return auc;
        }

        /// <summary>
        /// Return a test fraction (0,1)
        /// </summary>
        /// <returns></returns>
        public double GetTestFraction()
        {
            return testFraction;
        }

        /// <summary>
        /// Set test fraction
        /// Test fraction must be greater than 0 and less than 1, otherwise throws InvalidTestFractionException
        /// </summary>
        /// <param name="testFraction">test fraction</param>
        public void SetTestFraction(double testFraction)
        {
            if (testFraction <= 0 || testFraction >= 1)
                throw new MyException.AIException.InvalidTestFractionException(testFraction.ToString());

            this.testFraction = testFraction;
        }
        #endregion
    }
}
