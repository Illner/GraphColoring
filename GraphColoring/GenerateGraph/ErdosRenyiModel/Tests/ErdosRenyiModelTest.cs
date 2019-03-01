using System;
using System.Text;

namespace GraphColoring.GenerateGraph.ErdosRenyiModel.Tests
{
    class ErdosRenyiModelTest : GraphColoring.Tests.ITestInterface
    {
        // Variable
        #region
        private Graph.IGraphInterface graph;
        private StringBuilder stringBuilder;
        private ErdosRenyiModel erdosRenyiModel;
        private const int COUNTVERTICES = 1000;

        // Path
        private string testPathErdosRenyiModel = @"Testing\Test\GenerateGraphErdosRenyiModel.txt";
        #endregion

        // Enum
        #region
        public enum ErdosRenyiModelEnum
        {
            erdosRenyiModelCDividedByNLessThanOne,
            erdosRenyiModelCDividedByNMoreThanOne,
            erdosRenyiModelCLogNDividedByN,
            erdosRenyiModelNotAssigned,
            invalidVerticesCount
        }
        #endregion

        // Constructor
        #region
        public ErdosRenyiModelTest()
        {
            stringBuilder = new StringBuilder();
        }
        #endregion

        // Method
        #region
        /// <summary>
        /// Test all values of enum (files)
        /// </summary>
        /// <returns>report</returns>
        public StringBuilder Test()
        {
            stringBuilder.Clear();

            foreach (ErdosRenyiModelEnum erdosRenyiModelEnum in (ErdosRenyiModelEnum[])Enum.GetValues(typeof(ErdosRenyiModelEnum)))
            {
                Testing(erdosRenyiModelEnum);
            }

            return stringBuilder;
        }

        /// <summary>
        /// Test a particular enum (file)
        /// </summary>
        /// <param name="erdosRenyiModelEnum">particular enum (file)</param>
        /// <returns>report</returns>
        public StringBuilder Test(ErdosRenyiModelEnum erdosRenyiModelEnum)
        {
            stringBuilder.Clear();

            Testing(erdosRenyiModelEnum);

            return stringBuilder;
        }

        private void Testing(ErdosRenyiModelEnum erdosRenyiModelEnum)
        {
            try
            {
                switch (erdosRenyiModelEnum)
                {
                    case ErdosRenyiModelEnum.erdosRenyiModelCDividedByNLessThanOne:
                        stringBuilder.AppendLine(erdosRenyiModelEnum.ToString());
                        erdosRenyiModel = new ErdosRenyiModel(COUNTVERTICES, ErdosRenyiModel.ErdosRenyiModelProbabilityEnum.cDividedByNLessThanOne);
                        graph = erdosRenyiModel.GenerateGraph();

                        if (graph.GetRealCountVertices() != COUNTVERTICES)
                            throw new MyException.GraphException.GraphInvalidCountVerticesException("The number of vertices of generated graph is wrong!");

                        stringBuilder.AppendLine("OK");
                        break;
                    case ErdosRenyiModelEnum.erdosRenyiModelCDividedByNMoreThanOne:
                        stringBuilder.AppendLine(erdosRenyiModelEnum.ToString());
                        erdosRenyiModel = new ErdosRenyiModel(COUNTVERTICES, ErdosRenyiModel.ErdosRenyiModelProbabilityEnum.cDividedByNMoreThanOne);
                        graph = erdosRenyiModel.GenerateGraph();

                        if (graph.GetRealCountVertices() != COUNTVERTICES)
                            throw new MyException.GraphException.GraphInvalidCountVerticesException("The number of vertices of generated graph is wrong!");

                        stringBuilder.AppendLine("OK");
                        break;
                    case ErdosRenyiModelEnum.erdosRenyiModelCLogNDividedByN:
                        stringBuilder.AppendLine(erdosRenyiModelEnum.ToString());
                        erdosRenyiModel = new ErdosRenyiModel(COUNTVERTICES, ErdosRenyiModel.ErdosRenyiModelProbabilityEnum.cLogNDividedByN);
                        graph = erdosRenyiModel.GenerateGraph();

                        if (graph.GetRealCountVertices() != COUNTVERTICES)
                            throw new MyException.GraphException.GraphInvalidCountVerticesException("The number of vertices of generated graph is wrong!");

                        stringBuilder.AppendLine("OK");
                        break;
                    case ErdosRenyiModelEnum.erdosRenyiModelNotAssigned:
                        stringBuilder.AppendLine(erdosRenyiModelEnum.ToString());
                        erdosRenyiModel = new ErdosRenyiModel(COUNTVERTICES, ErdosRenyiModel.ErdosRenyiModelProbabilityEnum.notAssigned);
                        graph = erdosRenyiModel.GenerateGraph();

                        if (erdosRenyiModel.GetErdosRenyiModelProbabilityEnum() == ErdosRenyiModel.ErdosRenyiModelProbabilityEnum.notAssigned)
                            throw new MyException.GenerateGraphException.ErdosReneiModelChoosePNotAssigned();

                        if (graph.GetRealCountVertices() != COUNTVERTICES)
                            throw new MyException.GraphException.GraphInvalidCountVerticesException("The number of vertices of generated graph is wrong!");

                        stringBuilder.AppendLine("OK");
                        break;
                    case ErdosRenyiModelEnum.invalidVerticesCount:
                        stringBuilder.AppendLine(erdosRenyiModelEnum.ToString());
                        erdosRenyiModel = new ErdosRenyiModel(0, ErdosRenyiModel.ErdosRenyiModelProbabilityEnum.notAssigned);
                        graph = erdosRenyiModel.GenerateGraph();
                        break;
                    default:
                        throw new MyException.TestsException.TestsMissingTestException();
                }
            }
            catch (Exception e)
            {
                stringBuilder.AppendLine(e.Message);
            }
        }
        #endregion

        // Property
        #region
        public string GetPath()
        {
            return testPathErdosRenyiModel;
        }
        #endregion
    }
}
