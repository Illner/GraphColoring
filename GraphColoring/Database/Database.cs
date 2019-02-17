using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;

namespace GraphColoring.Database
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
        private Dictionary<int, Graph.GraphProperty.GraphProperty.EulerianGraphEnum> IDEulerianGraphEnumDictionary;
        private Dictionary<Graph.GraphProperty.GraphProperty.EulerianGraphEnum, int> EulerianGraphEnumIDDictionary;
        private Dictionary<int, Graph.GraphClass.GraphClass.GraphClassEnum> IDGraphClassEnumDictionary;
        private Dictionary<Graph.GraphClass.GraphClass.GraphClassEnum, int> GraphClassEnumIDDictionary;
        private Dictionary<int, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum> IDGraphColoringAlgorithmEnumDictionary;
        private Dictionary<GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum, int> GraphColoringAlgorithmEnumIDDictionary;
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
                throw new MyException.DatabaseException.DatabaseNotOpenException();

            FillDictionaries();
        }
        #endregion

        // Method
        #region
        private void FillDictionaries()
        {
            int id;
            IDEulerianGraphEnumDictionary = new Dictionary<int, Graph.GraphProperty.GraphProperty.EulerianGraphEnum>();
            EulerianGraphEnumIDDictionary = new Dictionary<Graph.GraphProperty.GraphProperty.EulerianGraphEnum, int>();
            IDGraphClassEnumDictionary = new Dictionary<int, Graph.GraphClass.GraphClass.GraphClassEnum>();
            GraphClassEnumIDDictionary = new Dictionary<Graph.GraphClass.GraphClass.GraphClassEnum, int>();
            IDGraphColoringAlgorithmEnumDictionary = new Dictionary<int, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum>();
            GraphColoringAlgorithmEnumIDDictionary = new Dictionary<GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum, int>();

            // Fill EulerianGraphEnum
            foreach (Graph.GraphProperty.GraphProperty.EulerianGraphEnum eulerianGraphEnum in (Graph.GraphProperty.GraphProperty.EulerianGraphEnum[])Enum.GetValues(typeof(Graph.GraphProperty.GraphProperty.EulerianGraphEnum)))
            {
                id = GetEulerianGraph_ID(eulerianGraphEnum.ToString());
                IDEulerianGraphEnumDictionary.Add(id, eulerianGraphEnum);
                EulerianGraphEnumIDDictionary.Add(eulerianGraphEnum, id);
            }

            // Fill GraphClassEnum
            foreach (Graph.GraphClass.GraphClass.GraphClassEnum graphClassEnum in (Graph.GraphClass.GraphClass.GraphClassEnum[])Enum.GetValues(typeof(Graph.GraphClass.GraphClass.GraphClassEnum)))
            {
                id = GetGraphClass_ID(graphClassEnum.ToString());
                IDGraphClassEnumDictionary.Add(id, graphClassEnum);
                GraphClassEnumIDDictionary.Add(graphClassEnum, id);
            }

            // Fill GraphColoringAlgorithmEnum
            // Fill GraphClassEnum
            foreach (GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum graphColoringAlgorithmEnum in (GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum[])Enum.GetValues(typeof(GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum)))
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
                throw new MyException.DatabaseException.DatabaseNotOpenException();

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
                throw new MyException.DatabaseException.DatabaseNotOpenException();

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
                throw new MyException.DatabaseException.DatabaseNotOpenException();

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
                throw new MyException.DatabaseException.DatabaseNotOpenException();

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
                throw new MyException.DatabaseException.DatabaseNotOpenException();

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
                throw new MyException.DatabaseException.DatabaseNotOpenException();

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
                throw new MyException.DatabaseException.DatabaseNotOpenException();

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
                throw new MyException.DatabaseException.DatabaseNotOpenException();

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
                throw new MyException.DatabaseException.DatabaseNotOpenException();

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
                throw new MyException.DatabaseException.DatabaseNotOpenException();

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
                throw new MyException.DatabaseException.DatabaseNotOpenException();
            
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
                throw new MyException.DatabaseException.DatabaseNotOpenException();

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
                throw new MyException.DatabaseException.DatabaseNotOpenException();

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

        // Public function / procedure
        /// <summary>
        /// Return true, if graph exists in DB, otherwise false
        /// If the graph is not connected, throw GraphIsNotConnected
        /// </summary>
        /// <param name="graph">The graph</param>
        /// <returns>true if graph exists in DB</returns>
        public bool ExistsGraph(Graph.IGraphInterface graph)
        {
            if (graph.GetGraphProperty().GetCountComponents() != 1)
                throw new MyException.GraphException.GraphIsNotConnected();

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
        public int InsertGraph(Graph.IGraphInterface graph)
        {
            if (graph.GetGraphProperty().GetCountComponents() != 1)
                throw new MyException.GraphException.GraphIsNotConnected();

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
        public void InsertGraphColoring(int ID_Graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum graphColoringAlgorithmEnum,
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
        public void InsertGraphColoring(int ID_Graph, GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum graphColoringAlgorithmEnum,
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
