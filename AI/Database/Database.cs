using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.IO;

namespace AI.Database
{
    class Database
    {
        // Variable
        #region
        private const string datasource = "127.0.0.1";
        private const string database = "GraphColoring";
        private const string username = "GraphColoring";
        private const string password = "GraphColoring.";
        
        private SqlConnection connection;
        private Dictionary<int, GraphColoring.Graph.GraphProperty.GraphProperty.EulerianGraphEnum> IDEulerianGraphEnumDictionary;
        private Dictionary<GraphColoring.Graph.GraphProperty.GraphProperty.EulerianGraphEnum, int> EulerianGraphEnumIDDictionary;
        private Dictionary<int, GraphColoring.Graph.GraphClass.GraphClass.GraphClassEnum> IDGraphClassEnumDictionary;
        private Dictionary<GraphColoring.Graph.GraphClass.GraphClass.GraphClassEnum, int> GraphClassEnumIDDictionary;
        private Dictionary<int, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum> IDGraphColoringAlgorithmEnumDictionary;
        private Dictionary<GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum, int> GraphColoringAlgorithmEnumIDDictionary;
        #endregion
        
        // Constructor
        #region
        public Database()
        {
            string connectionString = @"Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;

            connection = new SqlConnection(connectionString);
            connection.Open();

            if (GetConnectionState() != ConnectionState.Open)
                throw new GraphColoring.MyException.DatabaseException.DatabaseNotOpenException();

            FillDictionaries();
        }
        #endregion
        
        // Method
        #region
        private void FillDictionaries()
        {
            int id;
            IDEulerianGraphEnumDictionary = new Dictionary<int, GraphColoring.Graph.GraphProperty.GraphProperty.EulerianGraphEnum>();
            EulerianGraphEnumIDDictionary = new Dictionary<GraphColoring.Graph.GraphProperty.GraphProperty.EulerianGraphEnum, int>();
            IDGraphClassEnumDictionary = new Dictionary<int, GraphColoring.Graph.GraphClass.GraphClass.GraphClassEnum>();
            GraphClassEnumIDDictionary = new Dictionary<GraphColoring.Graph.GraphClass.GraphClass.GraphClassEnum, int>();
            IDGraphColoringAlgorithmEnumDictionary = new Dictionary<int, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum>();
            GraphColoringAlgorithmEnumIDDictionary = new Dictionary<GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum, int>();

            // Fill EulerianGraphEnum
            foreach (GraphColoring.Graph.GraphProperty.GraphProperty.EulerianGraphEnum eulerianGraphEnum in (GraphColoring.Graph.GraphProperty.GraphProperty.EulerianGraphEnum[])Enum.GetValues(typeof(GraphColoring.Graph.GraphProperty.GraphProperty.EulerianGraphEnum)))
            {
                id = GetEulerianGraph_ID(eulerianGraphEnum.ToString());
                IDEulerianGraphEnumDictionary.Add(id, eulerianGraphEnum);
                EulerianGraphEnumIDDictionary.Add(eulerianGraphEnum, id);
            }

            // Fill GraphClassEnum
            foreach (GraphColoring.Graph.GraphClass.GraphClass.GraphClassEnum graphClassEnum in (GraphColoring.Graph.GraphClass.GraphClass.GraphClassEnum[])Enum.GetValues(typeof(GraphColoring.Graph.GraphClass.GraphClass.GraphClassEnum)))
            {
                id = GetGraphClass_ID(graphClassEnum.ToString());
                IDGraphClassEnumDictionary.Add(id, graphClassEnum);
                GraphClassEnumIDDictionary.Add(graphClassEnum, id);
            }

            // Fill GraphColoringAlgorithmEnum
            // Fill GraphClassEnum
            foreach (GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum graphColoringAlgorithmEnum in (GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum[])Enum.GetValues(typeof(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum)))
            {
                id = GetGraphColoringAlgorithm_ID(graphColoringAlgorithmEnum.ToString());
                IDGraphColoringAlgorithmEnumDictionary.Add(id, graphColoringAlgorithmEnum);
                GraphColoringAlgorithmEnumIDDictionary.Add(graphColoringAlgorithmEnum, id);
            }
        }
        
        // Core SQL function / procedure
        #region
        /// <summary>
        /// create function GetEulerianGraph_ID(
        /// @name varchar(30)
        /// ) returns Integer
        /// </summary>
        /// <param name="name">@name</param>
        /// <returns>Integer</returns>
        private int GetEulerianGraph_ID(string name)
        {
            // Check if it is connected
            if (GetConnectionState() != ConnectionState.Open)
                throw new GraphColoring.MyException.DatabaseException.DatabaseNotOpenException();

            // Variable
            int result = -1;

            try
            {
                // Create a Command object to call function.
                SqlCommand cmd = new SqlCommand("GetEulerianGraph_ID", connection);

                // CommandType is StoredProcedure
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameter
                cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;

                // Create a Parameter object, store the return value of the function.
                SqlParameter resultParam = new SqlParameter("@ret", SqlDbType.Int);
                
                resultParam.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(resultParam);

                // Call function.
                cmd.ExecuteNonQuery();

                if (resultParam.Value != DBNull.Value)
                {
                    result = (int)resultParam.Value;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// create function GetEulerianGraph_Name(
        /// @ID integer
        /// ) returns varchar(30)
        /// </summary>
        /// <param name="ID">@ID</param>
        /// <returns>returns varchar(30)</returns>
        private string GetEulerianGraph_Name(int ID)
        {
            // Check if it is connected
            if (GetConnectionState() != ConnectionState.Open)
                throw new GraphColoring.MyException.DatabaseException.DatabaseNotOpenException();

            // Variable
            string result = null;

            try
            {
                // Create a Command object to call function.
                SqlCommand cmd = new SqlCommand("GetEulerianGraph_Name", connection);

                // CommandType is StoredProcedure
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameter
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;

                // Create a Parameter object, store the return value of the function.
                SqlParameter resultParam = new SqlParameter("@ret", SqlDbType.VarChar);

                resultParam.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(resultParam);

                // Call function.
                cmd.ExecuteNonQuery();

                if (resultParam.Value != DBNull.Value)
                {
                    result = (string)resultParam.Value;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// create function GetGraphClass_ID(
        /// @name varchar(30)
        /// ) returns Integer
        /// </summary>
        /// <param name="name">@name</param>
        /// <returns>Integer</returns>
        private int GetGraphClass_ID(string name)
        {
            // Check if it is connected
            if (GetConnectionState() != ConnectionState.Open)
                throw new GraphColoring.MyException.DatabaseException.DatabaseNotOpenException();

            // Variable
            int result = -1;

            try
            {
                // Create a Command object to call function.
                SqlCommand cmd = new SqlCommand("GetGraphClass_ID", connection);

                // CommandType is StoredProcedure
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameter
                cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;

                // Create a Parameter object, store the return value of the function.
                SqlParameter resultParam = new SqlParameter("@ret", SqlDbType.Int);

                resultParam.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(resultParam);

                // Call function.
                cmd.ExecuteNonQuery();

                if (resultParam.Value != DBNull.Value)
                {
                    result = (int)resultParam.Value;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// create function GetGraphClass_Name(
        /// @ID integer
        /// ) returns varchar(30)
        /// </summary>
        /// <param name="ID">@ID</param>
        /// <returns>varchar(30)</returns>
        private string GetGraphClass_Name(int ID)
        {
            // Check if it is connected
            if (GetConnectionState() != ConnectionState.Open)
                throw new GraphColoring.MyException.DatabaseException.DatabaseNotOpenException();

            // Variable
            string result = null;

            try
            {
                // Create a Command object to call function.
                SqlCommand cmd = new SqlCommand("GetGraphClass_Name", connection);

                // CommandType is StoredProcedure
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameter
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;

                // Create a Parameter object, store the return value of the function.
                SqlParameter resultParam = new SqlParameter("@ret", SqlDbType.VarChar);

                resultParam.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(resultParam);

                // Call function.
                cmd.ExecuteNonQuery();

                if (resultParam.Value != DBNull.Value)
                {
                    result = (string)resultParam.Value;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// create function GetGraphColoringAlgorithm_ID(
        /// @name varchar(60)
        /// ) returns Integer
        /// </summary>
        /// <param name="name">@name</param>
        /// <returns>Integer</returns>
        private int GetGraphColoringAlgorithm_ID(string name)
        {
            // Check if it is connected
            if (GetConnectionState() != ConnectionState.Open)
                throw new GraphColoring.MyException.DatabaseException.DatabaseNotOpenException();

            // Variable
            int result = -1;

            try
            {
                // Create a Command object to call function.
                SqlCommand cmd = new SqlCommand("GetGraphColoringAlgorithm_ID", connection);

                // CommandType is StoredProcedure
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameter
                cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name;

                // Create a Parameter object, store the return value of the function.
                SqlParameter resultParam = new SqlParameter("@ret", SqlDbType.Int);

                resultParam.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(resultParam);

                // Call function.
                cmd.ExecuteNonQuery();

                if (resultParam.Value != DBNull.Value)
                {
                    result = (int)resultParam.Value;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// create function GetGraphColoringAlgorithm_Name(
        /// @ID integer
        /// ) returns varchar(60)
        /// </summary>
        /// <param name="ID">@ID</param>
        /// <returns>varchar(60)</returns>
        private string GetGraphColoringAlgorithm_Name(int ID)
        {
            // Check if it is connected
            if (GetConnectionState() != ConnectionState.Open)
                throw new GraphColoring.MyException.DatabaseException.DatabaseNotOpenException();

            // Variable
            string result = null;

            try
            {
                // Create a Command object to call function.
                SqlCommand cmd = new SqlCommand("GetGraphColoringAlgorithm_Name", connection);

                // CommandType is StoredProcedure
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameter
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;

                // Create a Parameter object, store the return value of the function.
                SqlParameter resultParam = new SqlParameter("@ret", SqlDbType.VarChar);

                resultParam.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(resultParam);

                // Call function.
                cmd.ExecuteNonQuery();

                if (resultParam.Value != DBNull.Value)
                {
                    result = (string)resultParam.Value;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// create function ExistsGraph(
        /// @CountVertices Integer,
        /// @CountEdges Integer,
        /// @ID_GraphClass Integer,
        /// @IsChordal bit,
        /// @IsRegular bit,
        /// @IsCyclic bit,
        /// @ID_EulerianGraph Integer,
        /// @MaximumVertexDegree Integer,
        /// @MinimumVertexDegree Integer,
        /// @AverageVertexDegree Integer,
        /// @CountCutVertices Integer,
        /// @CountBridges Integer,
        /// @Girth Integer
        /// ) returns bit
        /// </summary>
        /// <param name="CountVertices">@CountVertices</param>
        /// <param name="CountEdges">@CountEdges</param>
        /// <param name="ID_GraphClass">@ID_GraphClass</param>
        /// <param name="IsChordal">@IsChordal</param>
        /// <param name="IsRegular">@IsRegular</param>
        /// <param name="IsCyclic">@IsCyclic</param>
        /// <param name="ID_EulerianGraph">@ID_EulerianGraph</param>
        /// <param name="MaximumVertexDegree">@MaximumVertexDegree</param>
        /// <param name="MinimumVertexDegree">@MinimumVertexDegree</param>
        /// <param name="AverageVertexDegree">@AverageVertexDegree</param>
        /// <param name="CountCutVertices">@CountCutVertices</param>
        /// <param name="CountBridges">@CountBridges</param>
        /// <param name="Girth">@Girth</param>
        private bool ExistsGraph(int CountVertices, int CountEdges, int ID_GraphClass, bool IsChordal, bool IsRegular, bool IsCyclic, int ID_EulerianGraph,
            int MaximumVertexDegree, int MinimumVertexDegree, double AverageVertexDegree, int CountCutVertices, int CountBridges, int Girth)
        {
            // Check if it is connected
            if (GetConnectionState() != ConnectionState.Open)
                throw new GraphColoring.MyException.DatabaseException.DatabaseNotOpenException();

            // Variable
            bool result = false;

            try
            {
                // Create a Command object to call function.
                SqlCommand cmd = new SqlCommand("ExistsGraph", connection);

                // CommandType is StoredProcedure
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameter
                cmd.Parameters.Add("@CountVertices", SqlDbType.Int).Value = CountVertices;
                cmd.Parameters.Add("@CountEdges", SqlDbType.Int).Value = CountEdges;
                cmd.Parameters.Add("@ID_GraphClass", SqlDbType.Int).Value = ID_GraphClass;
                cmd.Parameters.Add("@IsChordal", SqlDbType.Bit).Value = IsChordal;
                cmd.Parameters.Add("@IsRegular", SqlDbType.Bit).Value = IsRegular;
                cmd.Parameters.Add("@IsCyclic", SqlDbType.Bit).Value = IsCyclic;
                cmd.Parameters.Add("@ID_EulerianGraph", SqlDbType.Int).Value = ID_EulerianGraph;
                cmd.Parameters.Add("@MaximumVertexDegree", SqlDbType.Int).Value = MaximumVertexDegree;
                cmd.Parameters.Add("@MinimumVertexDegree", SqlDbType.Int).Value = MinimumVertexDegree;
                cmd.Parameters.Add("@AverageVertexDegree", SqlDbType.Float).Value = AverageVertexDegree;
                cmd.Parameters.Add("@CountCutVertices", SqlDbType.Int).Value = CountCutVertices;
                cmd.Parameters.Add("@CountBridges", SqlDbType.Int).Value = CountBridges;
                cmd.Parameters.Add("@Girth", SqlDbType.Int).Value = Girth;

                // Create a Parameter object, store the return value of the function.
                SqlParameter resultParam = new SqlParameter("@ret", SqlDbType.Bit);

                resultParam.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(resultParam);

                // Call function.
                cmd.ExecuteNonQuery();

                if (resultParam.Value != DBNull.Value)
                {
                    result = (bool)resultParam.Value;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// create procedure InsertGraph(
        /// @CountVertices Integer,
        /// @CountEdges Integer,
        /// @ID_GraphClass Integer,
        /// @IsChordal bit,
        /// @IsRegular bit,
        /// @IsCyclic bit,
        /// @ID_EulerianGraph Integer,
        /// @MaximumVertexDegree Integer,
        /// @MinimumVertexDegree Integer,
        /// @AverageVertexDegree Integer,
        /// @CountCutVertices Integer,
        /// @CountBridges Integer,
        /// @Girth Integer
        /// ) 
        /// </summary>
        /// <param name="CountVertices">@CountVertices</param>
        /// <param name="CountEdges">@CountEdges</param>
        /// <param name="ID_GraphClass">@ID_GraphClass</param>
        /// <param name="IsChordal">@IsChordal</param>
        /// <param name="IsRegular">@IsRegular</param>
        /// <param name="IsCyclic">@IsCyclic</param>
        /// <param name="ID_EulerianGraph">@ID_EulerianGraph</param>
        /// <param name="MaximumVertexDegree">@MaximumVertexDegree</param>
        /// <param name="MinimumVertexDegree">@MinimumVertexDegree</param>
        /// <param name="AverageVertexDegree">@AverageVertexDegree</param>
        /// <param name="CountCutVertices">@CountCutVertices</param>
        /// <param name="CountBridges">@CountBridges</param>
        /// <param name="Girth">@Girth</param>
        private void InsertGraph(int CountVertices, int CountEdges, int ID_GraphClass, bool IsChordal, bool IsRegular, bool IsCyclic, int ID_EulerianGraph,
            int MaximumVertexDegree, int MinimumVertexDegree, double AverageVertexDegree, int CountCutVertices, int CountBridges, int Girth)
        {
            // Check if it is connected
            if (GetConnectionState() != ConnectionState.Open)
                throw new GraphColoring.MyException.DatabaseException.DatabaseNotOpenException();

            try
            {
                // Create a Command object to call procedure Get_Employee_Info
                SqlCommand cmd = new SqlCommand("InsertGraph", connection);

                // Command Type is StoredProcedure
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameter
                cmd.Parameters.Add("@CountVertices", SqlDbType.Int).Value = CountVertices;
                cmd.Parameters.Add("@CountEdges", SqlDbType.Int).Value = CountEdges;
                cmd.Parameters.Add("@ID_GraphClass", SqlDbType.Int).Value = ID_GraphClass;
                cmd.Parameters.Add("@IsChordal", SqlDbType.Bit).Value = IsChordal;
                cmd.Parameters.Add("@IsRegular", SqlDbType.Bit).Value = IsRegular;
                cmd.Parameters.Add("@IsCyclic", SqlDbType.Bit).Value = IsCyclic;
                cmd.Parameters.Add("@ID_EulerianGraph", SqlDbType.Int).Value = ID_EulerianGraph;
                cmd.Parameters.Add("@MaximumVertexDegree", SqlDbType.Int).Value = MaximumVertexDegree;
                cmd.Parameters.Add("@MinimumVertexDegree", SqlDbType.Int).Value = MinimumVertexDegree;
                cmd.Parameters.Add("@AverageVertexDegree", SqlDbType.Float).Value = AverageVertexDegree;
                cmd.Parameters.Add("@CountCutVertices", SqlDbType.Int).Value = CountCutVertices;
                cmd.Parameters.Add("@CountBridges", SqlDbType.Int).Value = CountBridges;
                cmd.Parameters.Add("@Girth", SqlDbType.Int).Value = Girth;

                // Call function.
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
        }

        /// <summary>
        /// create function GetGraph(
        /// @CountVertices Integer,
        /// @CountEdges Integer,
        /// @ID_GraphClass Integer,
        /// @IsChordal bit,
        /// @IsRegular bit,
        /// @IsCyclic bit,
        /// @ID_EulerianGraph Integer,
        /// @MaximumVertexDegree Integer,
        /// @MinimumVertexDegree Integer,
        /// @AverageVertexDegree Real,
        /// @CountCutVertices Integer,
        /// @CountBridges Integer,
        /// @Girth Integer
        /// ) returns Integer
        /// </summary>
        /// <param name="CountVertices">@CountVertices</param>
        /// <param name="CountEdges">@CountEdges</param>
        /// <param name="ID_GraphClass">@ID_GraphClass</param>
        /// <param name="IsChordal">@IsChordal</param>
        /// <param name="IsRegular">@IsRegular</param>
        /// <param name="IsCyclic">@IsCyclic</param>
        /// <param name="ID_EulerianGraph">@ID_EulerianGraph</param>
        /// <param name="MaximumVertexDegree">@MaximumVertexDegree</param>
        /// <param name="MinimumVertexDegree">@MinimumVertexDegree</param>
        /// <param name="AverageVertexDegree">@AverageVertexDegree</param>
        /// <param name="CountCutVertices">@CountCutVertices</param>
        /// <param name="CountBridges">@CountBridges</param>
        /// <param name="Girth">@Girth</param>
        /// <returns>Integer</returns>
        private int GetGraph(int CountVertices, int CountEdges, int ID_GraphClass, bool IsChordal, bool IsRegular, bool IsCyclic, int ID_EulerianGraph,
            int MaximumVertexDegree, int MinimumVertexDegree, double AverageVertexDegree, int CountCutVertices, int CountBridges, int Girth)
        {
            // Check if it is connected
            if (GetConnectionState() != ConnectionState.Open)
                throw new GraphColoring.MyException.DatabaseException.DatabaseNotOpenException();

            // Variable
            int result = -1;

            try
            {
                // Create a Command object to call function.
                SqlCommand cmd = new SqlCommand("GetGraph", connection);

                // CommandType is StoredProcedure
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameter
                cmd.Parameters.Add("@CountVertices", SqlDbType.Int).Value = CountVertices;
                cmd.Parameters.Add("@CountEdges", SqlDbType.Int).Value = CountEdges;
                cmd.Parameters.Add("@ID_GraphClass", SqlDbType.Int).Value = ID_GraphClass;
                cmd.Parameters.Add("@IsChordal", SqlDbType.Bit).Value = IsChordal;
                cmd.Parameters.Add("@IsRegular", SqlDbType.Bit).Value = IsRegular;
                cmd.Parameters.Add("@IsCyclic", SqlDbType.Bit).Value = IsCyclic;
                cmd.Parameters.Add("@ID_EulerianGraph", SqlDbType.Int).Value = ID_EulerianGraph;
                cmd.Parameters.Add("@MaximumVertexDegree", SqlDbType.Int).Value = MaximumVertexDegree;
                cmd.Parameters.Add("@MinimumVertexDegree", SqlDbType.Int).Value = MinimumVertexDegree;
                cmd.Parameters.Add("@AverageVertexDegree", SqlDbType.Float).Value = AverageVertexDegree;
                cmd.Parameters.Add("@CountCutVertices", SqlDbType.Int).Value = CountCutVertices;
                cmd.Parameters.Add("@CountBridges", SqlDbType.Int).Value = CountBridges;
                cmd.Parameters.Add("@Girth", SqlDbType.Int).Value = Girth;

                // Create a Parameter object, store the return value of the function.
                SqlParameter resultParam = new SqlParameter("@ret", SqlDbType.Int);

                resultParam.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(resultParam);

                // Call function.
                cmd.ExecuteNonQuery();

                if (resultParam.Value != DBNull.Value)
                {
                    result = (int)resultParam.Value;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// create procedure InsertCoreProbability(
        /// @ID_Graph Integer,
        /// @ID_GraphAlgorithm Integer,
        /// @CountOfIteration Integer,
        /// @MinColors Integer,
        /// @MaxColors Integer
        /// )
        /// </summary>
        /// <param name="ID_Graph">@ID_Graph</param>
        /// <param name="ID_GraphAlgorithm">@ID_GraphAlgorithm</param>
        /// <param name="CountOfIteration">@CountOfIteration</param>
        /// <param name="MinColors">@MinColors</param>
        /// <param name="MaxColors">@MaxColors</param>
        private void InsertCoreProbability(int ID_Graph, int ID_GraphAlgorithm, int CountOfIterations, int MinColors, int MaxColors)
        {
            // Check if it is connected
            if (GetConnectionState() != ConnectionState.Open)
                throw new GraphColoring.MyException.DatabaseException.DatabaseNotOpenException();

            try
            {
                // Create a Command object to call procedure Get_Employee_Info
                SqlCommand cmd = new SqlCommand("InsertCoreProbability", connection);

                // Command Type is StoredProcedure
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameter
                cmd.Parameters.Add("@ID_Graph", SqlDbType.Int).Value = ID_Graph;
                cmd.Parameters.Add("@ID_GraphAlgorithm", SqlDbType.Int).Value = ID_GraphAlgorithm;
                cmd.Parameters.Add("@CountOfIteration", SqlDbType.Int).Value = CountOfIterations;
                cmd.Parameters.Add("@MinColors", SqlDbType.Int).Value = MinColors;
                cmd.Parameters.Add("@MaxColors", SqlDbType.Int).Value = MaxColors;
                
                // Execute procedure.
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
        }

        /// <summary>
        /// create procedure InsertCore(
        /// @ID_Graph Integer,
        /// @ID_GraphAlgorithm Integer,
        /// @Colors Integer
        /// )
        /// </summary>
        /// <param name="ID_Graph">@ID_Graph</param>
        /// <param name="ID_GraphAlgorithm">@ID_GraphAlgorithm</param>
        /// <param name="Colors">@Colors</param>
        private void InsertCore(int ID_Graph, int ID_GraphAlgorithm, int Colors)
        {
            // Check if it is connected
            if (GetConnectionState() != ConnectionState.Open)
                throw new GraphColoring.MyException.DatabaseException.DatabaseNotOpenException();
            
            try
            {
                // Create a Command object to call procedure Get_Employee_Info
                SqlCommand cmd = new SqlCommand("InsertCore", connection);

                // Command Type is StoredProcedure
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameter
                cmd.Parameters.Add("@ID_Graph", SqlDbType.Int).Value = ID_Graph;
                cmd.Parameters.Add("@ID_GraphAlgorithm", SqlDbType.Int).Value = ID_GraphAlgorithm;
                cmd.Parameters.Add("@Colors", SqlDbType.Int).Value = Colors;

                // Execute procedure.
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
        }

        /// <summary>
        /// create function GetBest(
        /// @ID_Graph Integer
        /// ) returns Integer
        /// </summary>
        /// <param name="ID_Graph">@ID_Graph</param>
        /// <returns>Integer</returns>
        private int GetBest(int ID_Graph)
        {
            // Check if it is connected
            if (GetConnectionState() != ConnectionState.Open)
                throw new GraphColoring.MyException.DatabaseException.DatabaseNotOpenException();

            // Variable
            int result = -1;

            try
            {
                // Create a Command object to call function.
                SqlCommand cmd = new SqlCommand("GetBest", connection);

                // CommandType is StoredProcedure
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameter
                cmd.Parameters.Add("@ID_Graph", SqlDbType.Int).Value = ID_Graph;

                // Create a Parameter object, store the return value of the function.
                SqlParameter resultParam = new SqlParameter("@ret", SqlDbType.Int);

                resultParam.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(resultParam);

                // Call function.
                cmd.ExecuteNonQuery();

                if (resultParam.Value != DBNull.Value)
                {
                    result = (int)resultParam.Value;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// create function UnUsedAlgorithms(
        /// @ID_Graph Integer
        /// ) returns varchar(1000)
        /// </summary>
        /// <param name="ID_Graph">@ID_Graph</param>
        /// <returns>List<int></int></returns>
        private List<int> UnUsedAlgorithms(int ID_Graph)
        {
            // Check if it is connected
            if (GetConnectionState() != ConnectionState.Open)
                throw new GraphColoring.MyException.DatabaseException.DatabaseNotOpenException();

            // Variable
            string result = "";

            try
            {
                // Create a Command object to call function.
                SqlCommand cmd = new SqlCommand("UnUsedAlgorithms", connection);

                // CommandType is StoredProcedure
                cmd.CommandType = CommandType.StoredProcedure;

                // Add parameter
                cmd.Parameters.Add("@ID_Graph", SqlDbType.Int).Value = ID_Graph;

                // Create a Parameter object, store the return value of the function.
                SqlParameter resultParam = new SqlParameter("@ret", SqlDbType.VarChar);

                resultParam.Direction = ParameterDirection.ReturnValue;

                cmd.Parameters.Add(resultParam);

                // Call function.
                cmd.ExecuteNonQuery();

                if (resultParam.Value != DBNull.Value)
                {
                    result = (string)resultParam.Value;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }

            string[] splitedResultArray = result.Split(';');
            return splitedResultArray.ToList().ConvertAll(int.Parse);
        }

        /// <summary>
        /// create procedure RemoveAllRecors
        /// </summary>
        private void RemoveAllRecors()
        {
            try
            {
                // Create a Command object to call procedure Get_Employee_Info
                SqlCommand cmd = new SqlCommand("RemoveAllRecors", connection);

                // Command Type is StoredProcedure
                cmd.CommandType = CommandType.StoredProcedure;

                // Execute procedure.
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
        }
        #endregion

        // Public function / procedure / query

        /// <summary>
        /// Save data (graph property + ID_Algorithm) to file
        /// train and test data
        /// </summary>
        /// <param name="path">file's path</param>
        public void SaveDataFromDatabaseToFile(string path)
        {
            SqlCommand cmd = new SqlCommand();

            string sql = "Select " +
                "CountVertices, CountEdges, " +
                "ID_GraphClass, ID_EulerianGraph, " +
                "CountCutVertices, CountBridges, " +
                "IsRegular, IsCyclic, Girth,  " +
                "MinimumVertexDegree, MaximumVertexDegree, AverageVertexDegree,  " +
                "Best.ID_GraphColoringAlgorithm " +
                "From GraphColoringBest as Best " +
                "left join GraphColoringGraph as Graph " +
                "on Best.ID_Graph = Graph.ID_Graph; ";

            cmd.Connection = connection; 
            cmd.CommandText = sql;

            int CountVertices, CountEdges;
            int ID_GraphClass, ID_EulerianGraph;
            int CountCutVertices, CountBridges;
            int IsRegular, IsCyclic, Girth;
            int MinimumVertexDegree, MaximumVertexDegree; double AverageVertexDegree;
            int ID_GraphColoringAlgorithm;

            // Delete the file if exists
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (File.Create(path)) { }

            // Read query and save text to the file
            using (DbDataReader reader = cmd.ExecuteReader())
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("CountVertices\tCountEdges\tID_GraphClass\tID_EulerianGraph\tCountCutVertices\tCountBridges\tIsRegular\tIsCyclic\tGirth\tMinimumVertexDegree\tMaximumVertexDegree\tAverageVertexDegree\tID_GraphColoringAlgorithm");

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CountVertices = Convert.ToInt32(reader.GetValue(0));
                        CountEdges = Convert.ToInt32(reader.GetValue(1));
                        ID_GraphClass = Convert.ToInt32(reader.GetValue(2));
                        ID_EulerianGraph = Convert.ToInt32(reader.GetValue(3));
                        CountCutVertices = Convert.ToInt32(reader.GetValue(4));
                        CountBridges = Convert.ToInt32(reader.GetValue(5));
                        IsRegular = Convert.ToInt32(reader.GetValue(6));
                        IsCyclic = Convert.ToInt32(reader.GetValue(7));
                        Girth = Convert.ToInt32(reader.GetValue(8));
                        MinimumVertexDegree = Convert.ToInt32(reader.GetValue(9));
                        MaximumVertexDegree = Convert.ToInt32(reader.GetValue(10));
                        AverageVertexDegree = Convert.ToDouble(reader.GetValue(11));
                        ID_GraphColoringAlgorithm = Convert.ToInt32(reader.GetValue(12));

                        sw.WriteLine(CountVertices + "\t" + CountEdges + "\t" + ID_GraphClass + "\t" + ID_EulerianGraph + "\t" + CountCutVertices +
                            "\t" + CountBridges + "\t" + IsRegular + "\t" + IsCyclic + "\t" + Girth + "\t" + MinimumVertexDegree + "\t" +
                            MaximumVertexDegree + "\t" + AverageVertexDegree + "\t" + ID_GraphColoringAlgorithm);
                    }
                }
            }
        }

        /// <summary>
        /// Return true, if graph exists in DB, otherwise false
        /// If the graph is not connected, throw GraphIsNotConnected
        /// </summary>
        /// <param name="graph">The graph</param>
        /// <returns>true if graph exists in DB</returns>
        public bool ExistsGraph(GraphColoring.Graph.IGraphInterface graph)
        {
            if (graph.GetGraphProperty().GetCountComponents() != 1)
                throw new GraphColoring.MyException.GraphException.GraphIsNotConnected();

            return ExistsGraph(graph.GetGraphProperty().GetCountVertices(), graph.GetGraphProperty().GetCountEdges(), GraphClassEnumIDDictionary[graph.GetGraphProperty().GetGraphClass()], graph.GetGraphProperty().GetIsChordal(),
                graph.GetGraphProperty().GetIsRegular(), graph.GetGraphProperty().GetIsCyclic(), EulerianGraphEnumIDDictionary[graph.GetGraphProperty().GetIsEulerian()], graph.GetGraphProperty().GetMaximumVertexDegree(),
                graph.GetGraphProperty().GetMinimumVertexDegree(), graph.GetGraphProperty().GetAverageVertexDegree(), graph.GetGraphProperty().GetCutVertices().Count, graph.GetGraphProperty().GetBridges().Count, graph.GetGraphProperty().GetGirth());
        }

        /// <summary>
        /// Save the graph to the DB and return graph ID
        /// If the graph is not connected, throw GraphIsNotConnected
        /// </summary>
        /// <param name="graph">The graph</param>
        /// <returns>Graph ID in DB</returns>
        public int InsertGraph(GraphColoring.Graph.IGraphInterface graph)
        {
            if (graph.GetGraphProperty().GetCountComponents() != 1)
                throw new GraphColoring.MyException.GraphException.GraphIsNotConnected();

            InsertGraph(graph.GetGraphProperty().GetCountVertices(), graph.GetGraphProperty().GetCountEdges(), GraphClassEnumIDDictionary[graph.GetGraphProperty().GetGraphClass()], graph.GetGraphProperty().GetIsChordal(),
                graph.GetGraphProperty().GetIsRegular(), graph.GetGraphProperty().GetIsCyclic(), EulerianGraphEnumIDDictionary[graph.GetGraphProperty().GetIsEulerian()], graph.GetGraphProperty().GetMaximumVertexDegree(),
                graph.GetGraphProperty().GetMinimumVertexDegree(), graph.GetGraphProperty().GetAverageVertexDegree(), graph.GetGraphProperty().GetCutVertices().Count, graph.GetGraphProperty().GetBridges().Count, graph.GetGraphProperty().GetGirth());

            return GetGraph(graph.GetGraphProperty().GetCountVertices(), graph.GetGraphProperty().GetCountEdges(), GraphClassEnumIDDictionary[graph.GetGraphProperty().GetGraphClass()], graph.GetGraphProperty().GetIsChordal(),
                graph.GetGraphProperty().GetIsRegular(), graph.GetGraphProperty().GetIsCyclic(), EulerianGraphEnumIDDictionary[graph.GetGraphProperty().GetIsEulerian()], graph.GetGraphProperty().GetMaximumVertexDegree(),
                graph.GetGraphProperty().GetMinimumVertexDegree(), graph.GetGraphProperty().GetAverageVertexDegree(), graph.GetGraphProperty().GetCutVertices().Count, graph.GetGraphProperty().GetBridges().Count, graph.GetGraphProperty().GetGirth());
        }

        /// <summary>
        /// Save graph coloring to DB.
        /// </summary>
        /// <param name="ID_Graph">The ID graph</param>
        /// <param name="graphColoringAlgorithmEnum">Algorithm</param>
        public void InsertGraphColoring(int ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum graphColoringAlgorithmEnum,
            int countColors)
        {
            // Variable
            int ID_GraphColoringAlgorithm = GraphColoringAlgorithmEnumIDDictionary[graphColoringAlgorithmEnum];

            InsertCore(ID_Graph, ID_GraphColoringAlgorithm, countColors);
        }

        /// <summary>
        /// Save graph coloring to DB.
        /// </summary>
        /// <param name="ID_Graph">The ID graph</param>
        /// <param name="graphColoringAlgorithmEnum">Algorithm</param>
        /// <param name="countColors">Count of colors</param>
        /// <param name="countOfIterations">Count of iterations</param>
        /// <param name="minColors">Minimum colors</param>
        /// <param name="maxColors">Maximum colors</param>
        public void InsertGraphColoring(int ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum graphColoringAlgorithmEnum,
            int countOfIterations, int minColors, int maxColors)
        {
            // Variable
            int ID_GraphColoringAlgorithm = GraphColoringAlgorithmEnumIDDictionary[graphColoringAlgorithmEnum];

            InsertCoreProbability(ID_Graph, ID_GraphColoringAlgorithm, countOfIterations, minColors, maxColors);
        }

        public void CleanDB()
        {
            RemoveAllRecors();
        }

        #endregion
        // Property
        #region
        public ConnectionState GetConnectionState()
        {
            return connection.State;
        }
        #endregion
    }
}
