using System;
using System.IO;
using System.Linq;
using System.Data;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace GraphColoringConsole.Database
{
    class Database
    {
        #region Variables
        private string datasource = "127.0.0.1";
        private string database = "GraphColoring";
        private string username = "GraphColoring";
        private string password = "GraphColoring.";
        
        private SqlConnection connection;
        private Dictionary<int, GraphColoring.Graph.GraphProperty.GraphProperty.EulerianGraphEnum> IDEulerianGraphEnumDictionary;
        private Dictionary<GraphColoring.Graph.GraphProperty.GraphProperty.EulerianGraphEnum, int> EulerianGraphEnumIDDictionary;
        private Dictionary<int, GraphColoring.Graph.GraphClass.GraphClass.GraphClassEnum> IDGraphClassEnumDictionary;
        private Dictionary<GraphColoring.Graph.GraphClass.GraphClass.GraphClassEnum, int> GraphClassEnumIDDictionary;
        private Dictionary<int, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum> IDGraphColoringAlgorithmEnumDictionary;
        private Dictionary<GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum, int> GraphColoringAlgorithmEnumIDDictionary;
        #endregion
        
        #region Constructor
        public Database(string datasource, string database, string username, string password)
        {
            this.datasource = datasource;
            this.database = database;
            this.username = username;
            this.password = password;

            string connectionString = @"Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;

            connection = new SqlConnection(connectionString);
            connection.Open();

            if (GetConnectionState() != ConnectionState.Open)
                throw new MyException.DatabaseException.GenerateGraphsDatabaseNotOpenException();

            FillDictionaries();
        }
        #endregion
        
        #region Method
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

                // EulerianGraphEnum does not exist in the DB
                if (id == -1)
                    continue;

                IDEulerianGraphEnumDictionary.Add(id, eulerianGraphEnum);
                EulerianGraphEnumIDDictionary.Add(eulerianGraphEnum, id);
            }

            // Fill GraphClassEnum
            foreach (GraphColoring.Graph.GraphClass.GraphClass.GraphClassEnum graphClassEnum in (GraphColoring.Graph.GraphClass.GraphClass.GraphClassEnum[])Enum.GetValues(typeof(GraphColoring.Graph.GraphClass.GraphClass.GraphClassEnum)))
            {
                id = GetGraphClass_ID(graphClassEnum.ToString());

                // GraphClassEnum does not exist in the DB
                if (id == -1)
                    continue;

                IDGraphClassEnumDictionary.Add(id, graphClassEnum);
                GraphClassEnumIDDictionary.Add(graphClassEnum, id);
            }

            // Fill GraphColoringAlgorithmEnum
            foreach (GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum graphColoringAlgorithmEnum in (GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum[])Enum.GetValues(typeof(GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum)))
            {
                id = GetGraphColoringAlgorithm_ID(graphColoringAlgorithmEnum.ToString());
                
                // Algorithm does not exist in the DB
                if (id == -1)
                    continue;
                
                IDGraphColoringAlgorithmEnumDictionary.Add(id, graphColoringAlgorithmEnum);
                GraphColoringAlgorithmEnumIDDictionary.Add(graphColoringAlgorithmEnum, id);
            }
        }
        
        #region Core SQL function / procedure
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
                throw new MyException.DatabaseException.GenerateGraphsDatabaseNotOpenException();

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
                Console.WriteLine("Error: " + e.GetType().Name);
                //Console.WriteLine(e.StackTrace);
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
                throw new MyException.DatabaseException.GenerateGraphsDatabaseNotOpenException();

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
                Console.WriteLine("Error: " + e.GetType().Name);
                //Console.WriteLine(e.StackTrace);
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
                throw new MyException.DatabaseException.GenerateGraphsDatabaseNotOpenException();

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
                Console.WriteLine("Error: " + e.GetType().Name);
                //Console.WriteLine(e.StackTrace);
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
                throw new MyException.DatabaseException.GenerateGraphsDatabaseNotOpenException();

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
                Console.WriteLine("Error: " + e.GetType().Name);
                //Console.WriteLine(e.StackTrace);
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
                throw new MyException.DatabaseException.GenerateGraphsDatabaseNotOpenException();

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
                Console.WriteLine("Error: " + e.GetType().Name);
                //Console.WriteLine(e.StackTrace);
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
                throw new MyException.DatabaseException.GenerateGraphsDatabaseNotOpenException();

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
                Console.WriteLine("Error: " + e.GetType().Name);
                //Console.WriteLine(e.StackTrace);
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
        /// @AverageVertexDegree Real,
        /// @MedianVertexDegree Integer,
        /// @CountCutVertices Integer,
        /// @CountBridges Integer,
        /// @Girth Integer
        /// @VertexDegreeList AS MyIDList READONLY
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
        /// <param name="MedianVertexDegree">@MedianVertexDegree</param>
        /// <param name="CountCutVertices">@CountCutVertices</param>
        /// <param name="CountBridges">@CountBridges</param>
        /// <param name="Girth">@Girth</param>
        /// <param name="VertexDegreeArray">@VertexDegreeArray</param>
        private bool ExistsGraphPrivate(int CountVertices, int CountEdges, int ID_GraphClass, bool IsChordal, bool IsRegular, bool IsCyclic, int ID_EulerianGraph,
            int MaximumVertexDegree, int MinimumVertexDegree, double AverageVertexDegree, int MedianVertexDegree, int CountCutVertices, int CountBridges, int Girth, int[] VertexDegreeArray)
        {
            // Check if it is connected
            if (GetConnectionState() != ConnectionState.Open)
                throw new MyException.DatabaseException.GenerateGraphsDatabaseNotOpenException();

            // Variable
            bool result = false;

            // Parse VertexDegreeArray
            DataTable tvp = new DataTable();
            tvp.Columns.Add(new DataColumn("ID", typeof(int)));
            foreach (var id in VertexDegreeArray)
            {
                tvp.Rows.Add(id);
            }

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
                cmd.Parameters.Add("@MedianVertexDegree", SqlDbType.Int).Value = MedianVertexDegree;
                cmd.Parameters.Add("@CountCutVertices", SqlDbType.Int).Value = CountCutVertices;
                cmd.Parameters.Add("@CountBridges", SqlDbType.Int).Value = CountBridges;
                cmd.Parameters.Add("@Girth", SqlDbType.Int).Value = Girth;
                SqlParameter tvparam = cmd.Parameters.AddWithValue("@VertexDegreeList", tvp);

                tvparam.SqlDbType = SqlDbType.Structured;
                tvparam.TypeName = "MyIDList";

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
                Console.WriteLine("Error: " + e.GetType().Name);
                //Console.WriteLine(e.StackTrace);
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
        /// @AverageVertexDegree Real,
        /// @MedianVertexDegree Integer,
        /// @CountCutVertices Integer,
        /// @CountBridges Integer,
        /// @Girth Integer
        /// @VertexDegreeList AS MyIDList READONLY
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
        /// <param name="MedianVertexDegree">@MedianVertexDegree</param>
        /// <param name="CountCutVertices">@CountCutVertices</param>
        /// <param name="CountBridges">@CountBridges</param>
        /// <param name="Girth">@Girth</param>
        /// <param name="VertexDegreeArray">@VertexDegreeArray</param>
        private void InsertGraphPrivate(int CountVertices, int CountEdges, int ID_GraphClass, bool IsChordal, bool IsRegular, bool IsCyclic, int ID_EulerianGraph,
            int MaximumVertexDegree, int MinimumVertexDegree, double AverageVertexDegree, int MedianVertexDegree, int CountCutVertices, int CountBridges, int Girth,
            int[] VertexDegreeArray, int TypeGeneratedGraph = 1)
        {
            // Check if it is connected
            if (GetConnectionState() != ConnectionState.Open)
                throw new MyException.DatabaseException.GenerateGraphsDatabaseNotOpenException();

            // Parse VertexDegreeArray
            DataTable tvp = new DataTable();
            tvp.Columns.Add(new DataColumn("ID", typeof(int)));
            foreach (var id in VertexDegreeArray)
            {
                tvp.Rows.Add(id);
            }

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
                cmd.Parameters.Add("@MedianVertexDegree", SqlDbType.Int).Value = MedianVertexDegree;
                cmd.Parameters.Add("@CountCutVertices", SqlDbType.Int).Value = CountCutVertices;
                cmd.Parameters.Add("@CountBridges", SqlDbType.Int).Value = CountBridges;
                cmd.Parameters.Add("@Girth", SqlDbType.Int).Value = Girth;
                SqlParameter tvparam = cmd.Parameters.AddWithValue("@VertexDegreeList", tvp);
                cmd.Parameters.Add("@TypeGeneratedGraph", SqlDbType.Int).Value = TypeGeneratedGraph;

                tvparam.SqlDbType = SqlDbType.Structured;
                tvparam.TypeName = "MyIDList";

                // Call function.
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.GetType().Name);
                //Console.WriteLine(e.StackTrace);
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
        /// @MedianVertexDegree Integer,
        /// @CountCutVertices Integer,
        /// @CountBridges Integer,
        /// @Girth Integer
        /// @VertexDegreeList AS MyIDList READONLY
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
        /// <param name="MedianVertexDegree">@MedianVertexDegree</param>
        /// <param name="CountCutVertices">@CountCutVertices</param>
        /// <param name="CountBridges">@CountBridges</param>
        /// <param name="Girth">@Girth</param>
        /// <param name="VertexDegreeArray">@VertexDegreeArray</param>
        /// <returns>Integer</returns>
        private int GetGraphPrivate(int CountVertices, int CountEdges, int ID_GraphClass, bool IsChordal, bool IsRegular, bool IsCyclic, int ID_EulerianGraph,
            int MaximumVertexDegree, int MinimumVertexDegree, double AverageVertexDegree, int MedianVertexDegree, int CountCutVertices, int CountBridges, int Girth, int[] VertexDegreeArray)
        {
            // Check if it is connected
            if (GetConnectionState() != ConnectionState.Open)
                throw new MyException.DatabaseException.GenerateGraphsDatabaseNotOpenException();

            // Variable
            int result = -1;

            // Parse VertexDegreeArray
            DataTable tvp = new DataTable();
            tvp.Columns.Add(new DataColumn("ID", typeof(int)));
            foreach (var id in VertexDegreeArray)
            {
                tvp.Rows.Add(id);
            }

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
                cmd.Parameters.Add("@MedianVertexDegree", SqlDbType.Int).Value = MedianVertexDegree;
                cmd.Parameters.Add("@CountCutVertices", SqlDbType.Int).Value = CountCutVertices;
                cmd.Parameters.Add("@CountBridges", SqlDbType.Int).Value = CountBridges;
                cmd.Parameters.Add("@Girth", SqlDbType.Int).Value = Girth;
                SqlParameter tvparam = cmd.Parameters.AddWithValue("@VertexDegreeList", tvp);

                tvparam.SqlDbType = SqlDbType.Structured;
                tvparam.TypeName = "MyIDList";

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
                Console.WriteLine("Error: " + e.GetType().Name);
                //Console.WriteLine(e.StackTrace);
            }

            return result;
        }

        /// <summary>
        /// create procedure AddCoreProbability(
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
        private void AddCoreProbability(int ID_Graph, int ID_GraphAlgorithm, int CountOfIterations, int MinColors, int MaxColors)
        {
            // Check if it is connected
            if (GetConnectionState() != ConnectionState.Open)
                throw new MyException.DatabaseException.GenerateGraphsDatabaseNotOpenException();

            try
            {
                // Create a Command object to call procedure Get_Employee_Info
                SqlCommand cmd = new SqlCommand("AddCoreProbability", connection);

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
                Console.WriteLine("Error: " + e.GetType().Name);
                //Console.WriteLine(e.StackTrace);
            }
        }

        /// <summary>
        /// create procedure AddCore(
        /// @ID_Graph Integer,
        /// @ID_GraphAlgorithm Integer,
        /// @Colors Integer
        /// )
        /// </summary>
        /// <param name="ID_Graph">@ID_Graph</param>
        /// <param name="ID_GraphAlgorithm">@ID_GraphAlgorithm</param>
        /// <param name="Colors">@Colors</param>
        private void AddCore(int ID_Graph, int ID_GraphAlgorithm, int Colors)
        {
            // Check if it is connected
            if (GetConnectionState() != ConnectionState.Open)
                throw new MyException.DatabaseException.GenerateGraphsDatabaseNotOpenException();

            try
            {
                // Create a Command object to call procedure Get_Employee_Info
                SqlCommand cmd = new SqlCommand("AddCore", connection);

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
                Console.WriteLine("Error: " + e.GetType().Name);
                //Console.WriteLine(e.StackTrace);
            }
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
                throw new MyException.DatabaseException.GenerateGraphsDatabaseNotOpenException();

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
                Console.WriteLine("Error: " + e.GetType().Name);
                //Console.WriteLine(e.StackTrace);
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
                throw new MyException.DatabaseException.GenerateGraphsDatabaseNotOpenException();

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
                Console.WriteLine("Error: " + e.GetType().Name);
                //Console.WriteLine(e.StackTrace);
            }
        }

        /// <summary>
        /// create procedure UpdateCoreProbability(
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
        private void UpdateCoreProbability(int ID_Graph, int ID_GraphAlgorithm, int CountOfIterations, int MinColors, int MaxColors)
        {
            // Check if it is connected
            if (GetConnectionState() != ConnectionState.Open)
                throw new MyException.DatabaseException.GenerateGraphsDatabaseNotOpenException();

            try
            {
                // Create a Command object to call procedure Get_Employee_Info
                SqlCommand cmd = new SqlCommand("UpdateCoreProbability", connection);

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
                Console.WriteLine("Error: " + e.GetType().Name);
                //Console.WriteLine(e.StackTrace);
            }
        }

        /// <summary>
        /// create procedure UpdateCore(
        /// @ID_Graph Integer,
        /// @ID_GraphAlgorithm Integer,
        /// @Colors Integer
        /// )
        /// </summary>
        /// <param name="ID_Graph">@ID_Graph</param>
        /// <param name="ID_GraphAlgorithm">@ID_GraphAlgorithm</param>
        /// <param name="Colors">@Colors</param>
        private void UpdateCore(int ID_Graph, int ID_GraphAlgorithm, int Colors)
        {
            // Check if it is connected
            if (GetConnectionState() != ConnectionState.Open)
                throw new MyException.DatabaseException.GenerateGraphsDatabaseNotOpenException();

            try
            {
                // Create a Command object to call procedure Get_Employee_Info
                SqlCommand cmd = new SqlCommand("UpdateCore", connection);

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
                Console.WriteLine("Error: " + e.GetType().Name);
                //Console.WriteLine(e.StackTrace);
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
                throw new MyException.DatabaseException.GenerateGraphsDatabaseNotOpenException();

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
                Console.WriteLine("Error: " + e.GetType().Name);
                //Console.WriteLine(e.StackTrace);
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
                throw new MyException.DatabaseException.GenerateGraphsDatabaseNotOpenException();

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
                Console.WriteLine("Error: " + e.GetType().Name);
                //Console.WriteLine(e.StackTrace);
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
                Console.WriteLine("Error: " + e.GetType().Name);
                //Console.WriteLine(e.StackTrace);
            }
        }
        #endregion

        // Public function / procedure / query

        /// <summary>
        /// Save data (graph property + ID_Algorithm) to file
        /// train and test data
        /// </summary>
        /// <param name="path">file's path</param>
        /// <param name="algorithmEnum">algorithm</param>
        /// <returns>Count of data</returns>
        public int SaveDataFromDatabaseToFile(string path, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum algorithmEnum)
        {
            // Check if it is connected
            if (GetConnectionState() != ConnectionState.Open)
                throw new MyException.DatabaseException.GenerateGraphsDatabaseNotOpenException();

            SqlCommand cmd = new SqlCommand();

            string sql = "SELECT * " +
                         "from CreateML " +
                         "order by CountVertices";

            cmd.Connection = connection;
            cmd.CommandText = sql;

            // Variable
            char delim = ';';
            char delim2 = ',';
            int degreeDegreeSequence;
            int countVerticesDegreeSequence;
            SortedDictionary<int, int> degreeSequenceDictionary;
            StringBuilder degreeSequenceStringBuilder;
            
            string GraphColoringAlgorithms;
            string DegreeSequence;
            
            List<GraphColoring.GraphColoringAlgorithm.AI.GraphData> positiveData = new List<GraphColoring.GraphColoringAlgorithm.AI.GraphData>();
            List<GraphColoring.GraphColoringAlgorithm.AI.GraphData> negativeData = new List<GraphColoring.GraphColoringAlgorithm.AI.GraphData>();
            List<GraphColoring.GraphColoringAlgorithm.AI.GraphData> data = new List<GraphColoring.GraphColoringAlgorithm.AI.GraphData>();

            // Delete the file if exists
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (File.Create(path)) { }

            cmd.CommandTimeout = 3600;

            // Read query
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        GraphColoring.GraphColoringAlgorithm.AI.GraphData graphData = new GraphColoring.GraphColoringAlgorithm.AI.GraphData();

                        graphData.CountVertices = Convert.ToInt32(reader.GetValue(0));
                        graphData.CountEdges = Convert.ToInt32(reader.GetValue(1));
                        graphData.ID_GraphClass = Convert.ToInt32(reader.GetValue(2));
                        graphData.ID_EulerianGraph = Convert.ToInt32(reader.GetValue(3));
                        graphData.CountCutVertices = Convert.ToInt32(reader.GetValue(4));
                        graphData.CountBridges = Convert.ToInt32(reader.GetValue(5));
                        graphData.IsRegular = Convert.ToBoolean(reader.GetValue(6));
                        graphData.IsCyclic = Convert.ToBoolean(reader.GetValue(7));
                        graphData.IsChordal = Convert.ToBoolean(reader.GetValue(8));
                        graphData.Girth = Convert.ToInt32(reader.GetValue(9));
                        graphData.Dense = Convert.ToSingle(reader.GetValue(10));
                        graphData.MinimumVertexDegree = Convert.ToInt32(reader.GetValue(11));
                        graphData.MaximumVertexDegree = Convert.ToInt32(reader.GetValue(12));
                        graphData.AverageVertexDegree = Convert.ToSingle(reader.GetValue(13));
                        graphData.MedianVertexDegree = Convert.ToInt32(reader.GetValue(14));
                        GraphColoringAlgorithms = Convert.ToString(reader.GetValue(15));
                        DegreeSequence = Convert.ToString(reader.GetValue(16));

                        degreeSequenceDictionary = new SortedDictionary<int, int>();

                        // Parse DegreeSequence
                        string[] tupleDegreeSequenceArray = DegreeSequence.Split(delim);

                        foreach (string record in tupleDegreeSequenceArray)
                        {
                            if (record == "")
                                break;

                            string[] valueDegreeSequenceArray = record.Split(delim2);

                            degreeDegreeSequence = int.Parse(valueDegreeSequenceArray[0]);
                            countVerticesDegreeSequence = int.Parse(valueDegreeSequenceArray[1]);

                            degreeSequenceDictionary.Add(degreeDegreeSequence, countVerticesDegreeSequence);
                        }

                        degreeSequenceStringBuilder = new StringBuilder();
                        for (int i = 0; i <= 10; i++)
                        {
                            int value = 0;
                            degreeSequenceDictionary.TryGetValue(i, out value);

                            // Fill degress
                            switch (i)
                            {
                                case 0:
                                    graphData.VertexDegree0 = value;
                                    break;
                                case 1:
                                    graphData.VertexDegree1 = value;
                                    break;
                                case 2:
                                    graphData.VertexDegree2 = value;
                                    break;
                                case 3:
                                    graphData.VertexDegree3 = value;
                                    break;
                                case 4:
                                    graphData.VertexDegree4 = value;
                                    break;
                                case 5:
                                    graphData.VertexDegree5 = value;
                                    break;
                                case 6:
                                    graphData.VertexDegree6 = value;
                                    break;
                                case 7:
                                    graphData.VertexDegree7 = value;
                                    break;
                                case 8:
                                    graphData.VertexDegree8 = value;
                                    break;
                                case 9:
                                    graphData.VertexDegree9 = value;
                                    break;
                                case 10:
                                    graphData.VertexDegree10 = value;
                                    break;
                            }
                        }

                        // Parse ID_GraphColoringAlgorithm
                        string[] algorithmArray = GraphColoringAlgorithms.Split(delim);
                        int IDAlgorithm = GraphColoringAlgorithmEnumIDDictionary[algorithmEnum];
                        graphData.Label = false;

                        foreach (string algorithm in algorithmArray)
                        {
                            if (Int32.Parse(algorithm) == IDAlgorithm)
                            {
                                graphData.Label = true;
                                break;
                            }
                        }

                        if (graphData.Label)
                            positiveData.Add(graphData);
                        else
                            negativeData.Add(graphData);
                    }
                }
            }

            int minLength;
            List<GraphColoring.GraphColoringAlgorithm.AI.GraphData> minData, maxData;

            if (positiveData.Count() < negativeData.Count())
            {
                minData = positiveData;
                maxData = negativeData;
            }
            else
            {
                minData = negativeData;
                maxData = positiveData;
            }
            minLength = minData.Count();

            // Copy the first list
            data = minData.ToList();

            // Permutate the second list
            GraphColoring.MyMath.MyMath.FisherYatesShuffle(maxData);

            // Short the second list
            maxData.RemoveRange(minLength, maxData.Count() - minLength);
            
            data = data.Concat(maxData).ToList();
            GraphColoring.MyMath.MyMath.FisherYatesShuffle(data);

            // Save text to the file
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("ID_GraphColoringAlgorithm\tID_GraphClass\tID_EulerianGraph\tIsRegular\tIsCyclic\tIsChordal\tCountVertices\tCountEdges\tCountCutVertices\tCountBridges\tGirth\tDense\tMinimumVertexDegree\tMaximumVertexDegree\tAverageVertexDegree\tMedianVertexDegree");

                foreach (var record in data)
                {
                    sw.WriteLine(record.Label + "\t" + record.ID_GraphClass + "\t" + record.ID_EulerianGraph + "\t" +
                            record.IsRegular + "\t" + record.IsCyclic + "\t" + record.IsChordal + "\t" + record.CountVertices + "\t" + record.CountEdges + "\t" +
                            record.CountCutVertices + "\t" + record.CountBridges + "\t" + record.Girth + "\t" + record.Dense + "\t" + record.MinimumVertexDegree + "\t" +
                            record.MaximumVertexDegree + "\t" + record.AverageVertexDegree + "\t" + record.MedianVertexDegree + "\t" + record.VertexDegree0 + "\t" + 
                            record.VertexDegree1 + "\t" + record.VertexDegree2 + "\t" + record.VertexDegree3 + "\t" + record.VertexDegree4 + "\t" + record.VertexDegree5 + "\t" + 
                            record.VertexDegree6 + "\t" + record.VertexDegree7 + "\t" + record.VertexDegree8 + "\t" + record.VertexDegree9 + "\t" + record.VertexDegree10);
                    
                }
            }
            
            return data.Count();
        }

        /// <summary>
        /// Return true, if graph exists in DB, otherwise false
        /// If the graph is not connected throws GraphIsNotConnected
        /// </summary>
        /// <param name="graph">graph</param>
        /// <returns>true if the graph exists in DB</returns>
        public bool ExistsGraph(GraphColoring.Graph.IGraphInterface graph)
        {
            if (graph.GetGraphProperty().GetCountComponents() != 1)
                throw new GraphColoring.MyException.GraphException.GraphIsNotConnected();

            // Get EulerianGraphEnum
            int eulerianGraphID;
            if (!EulerianGraphEnumIDDictionary.TryGetValue(graph.GetGraphProperty().GetIsEulerian(), out eulerianGraphID))
                eulerianGraphID = EulerianGraphEnumIDDictionary[GraphColoring.Graph.GraphProperty.GraphProperty.EulerianGraphEnum.undefined];

            // Get GraphClassEnum
            int graphClassID;
            if (!GraphClassEnumIDDictionary.TryGetValue(graph.GetGraphProperty().GetGraphClass(), out graphClassID))
                graphClassID = GraphClassEnumIDDictionary[GraphColoring.Graph.GraphClass.GraphClass.GraphClassEnum.undefined];

            return ExistsGraphPrivate(graph.GetGraphProperty().GetCountVertices(), graph.GetGraphProperty().GetCountEdges(), graphClassID, graph.GetGraphProperty().GetIsChordal(),
                graph.GetGraphProperty().GetIsRegular(), graph.GetGraphProperty().GetIsCyclic(), eulerianGraphID, graph.GetGraphProperty().GetMaximumVertexDegree(),
                graph.GetGraphProperty().GetMinimumVertexDegree(), graph.GetGraphProperty().GetAverageVertexDegree(), graph.GetGraphProperty().GetMedianVertexDegree(), graph.GetGraphProperty().GetCutVertices().Count, 
                graph.GetGraphProperty().GetBridges().Count, graph.GetGraphProperty().GetGirth(), graph.GetGraphProperty().GetDegreeSequenceInt(false).ToArray());
        }

        public bool ExistsGraph(int CountVertices, int CountEdges, GraphColoring.Graph.GraphClass.GraphClass.GraphClassEnum GraphClass, bool IsChordal, bool IsRegular, bool IsCyclic, GraphColoring.Graph.GraphProperty.GraphProperty.EulerianGraphEnum EulerianGraph,
            int MaximumVertexDegree, int MinimumVertexDegree, double AverageVertexDegree, int MedianVertexDegree, int CountCutVertices, int CountBridges, int Girth, int[] VertexDegreeArray)
        {
            // Get EulerianGraphEnum
            int eulerianGraphID;
            if (!EulerianGraphEnumIDDictionary.TryGetValue(EulerianGraph, out eulerianGraphID))
                eulerianGraphID = EulerianGraphEnumIDDictionary[GraphColoring.Graph.GraphProperty.GraphProperty.EulerianGraphEnum.undefined];

            // Get GraphClassEnum
            int graphClassID;
            if (!GraphClassEnumIDDictionary.TryGetValue(GraphClass, out graphClassID))
                graphClassID = GraphClassEnumIDDictionary[GraphColoring.Graph.GraphClass.GraphClass.GraphClassEnum.undefined];

            return ExistsGraphPrivate(CountVertices, CountEdges, graphClassID, IsChordal, IsRegular, IsCyclic, eulerianGraphID,
                               MaximumVertexDegree, MinimumVertexDegree, AverageVertexDegree, MedianVertexDegree, CountCutVertices,
                               CountBridges, Girth, VertexDegreeArray);
        }

        /// <summary>
        /// Save the graph to the DB and return graph ID
        /// If the graph is not connected throws GraphIsNotConnected
        /// </summary>
        /// <param name="graph">graph</param>
        public void InsertGraph(GraphColoring.Graph.IGraphInterface graph)
        {
            if (graph.GetGraphProperty().GetCountComponents() != 1)
                throw new GraphColoring.MyException.GraphException.GraphIsNotConnected();

            // Get EulerianGraphEnum
            int eulerianGraphID;
            if (!EulerianGraphEnumIDDictionary.TryGetValue(graph.GetGraphProperty().GetIsEulerian(), out eulerianGraphID))
                eulerianGraphID = EulerianGraphEnumIDDictionary[GraphColoring.Graph.GraphProperty.GraphProperty.EulerianGraphEnum.undefined];

            // Get GraphClassEnum
            int graphClassID;
            if (!GraphClassEnumIDDictionary.TryGetValue(graph.GetGraphProperty().GetGraphClass(), out graphClassID))
                graphClassID = GraphClassEnumIDDictionary[GraphColoring.Graph.GraphClass.GraphClass.GraphClassEnum.undefined];

            InsertGraphPrivate(graph.GetGraphProperty().GetCountVertices(), graph.GetGraphProperty().GetCountEdges(), graphClassID, graph.GetGraphProperty().GetIsChordal(),
                graph.GetGraphProperty().GetIsRegular(), graph.GetGraphProperty().GetIsCyclic(), eulerianGraphID, graph.GetGraphProperty().GetMaximumVertexDegree(),
                graph.GetGraphProperty().GetMinimumVertexDegree(), graph.GetGraphProperty().GetAverageVertexDegree(), graph.GetGraphProperty().GetMedianVertexDegree(), graph.GetGraphProperty().GetCutVertices().Count, 
                graph.GetGraphProperty().GetBridges().Count, graph.GetGraphProperty().GetGirth(), graph.GetGraphProperty().GetDegreeSequenceInt(false).ToArray());
        }

        public void InsertGraph(int CountVertices, int CountEdges, GraphColoring.Graph.GraphClass.GraphClass.GraphClassEnum GraphClass, bool IsChordal, bool IsRegular, bool IsCyclic, GraphColoring.Graph.GraphProperty.GraphProperty.EulerianGraphEnum EulerianGraph,
            int MaximumVertexDegree, int MinimumVertexDegree, double AverageVertexDegree, int MedianVertexDegree, int CountCutVertices, int CountBridges, int Girth, int[] VertexDegreeArray)
        {
            // Get EulerianGraphEnum
            int eulerianGraphID;
            if (!EulerianGraphEnumIDDictionary.TryGetValue(EulerianGraph, out eulerianGraphID))
                eulerianGraphID = EulerianGraphEnumIDDictionary[GraphColoring.Graph.GraphProperty.GraphProperty.EulerianGraphEnum.undefined];

            // Get GraphClassEnum
            int graphClassID;
            if (!GraphClassEnumIDDictionary.TryGetValue(GraphClass, out graphClassID))
                graphClassID = GraphClassEnumIDDictionary[GraphColoring.Graph.GraphClass.GraphClass.GraphClassEnum.undefined];

            InsertGraphPrivate(CountVertices, CountEdges, graphClassID, IsChordal, IsRegular, IsCyclic, eulerianGraphID,
                               MaximumVertexDegree, MinimumVertexDegree, AverageVertexDegree, MedianVertexDegree, CountCutVertices,
                               CountBridges, Girth, VertexDegreeArray);
        }

        /// <summary>
        /// Get ID of the graphs
        /// If the graph is not connected throws GraphIsNotConnected
        /// </summary>
        /// <param name="graph">graph</param>
        /// <returns>graph ID in DB</returns>
        public int GetGraph(GraphColoring.Graph.IGraphInterface graph)
        {
            if (graph.GetGraphProperty().GetCountComponents() != 1)
                throw new GraphColoring.MyException.GraphException.GraphIsNotConnected();

            // Get EulerianGraphEnum
            int eulerianGraphID;
            if (!EulerianGraphEnumIDDictionary.TryGetValue(graph.GetGraphProperty().GetIsEulerian(), out eulerianGraphID))
                eulerianGraphID = EulerianGraphEnumIDDictionary[GraphColoring.Graph.GraphProperty.GraphProperty.EulerianGraphEnum.undefined];

            // Get GraphClassEnum
            int graphClassID;
            if (!GraphClassEnumIDDictionary.TryGetValue(graph.GetGraphProperty().GetGraphClass(), out graphClassID))
                graphClassID = GraphClassEnumIDDictionary[GraphColoring.Graph.GraphClass.GraphClass.GraphClassEnum.undefined];

            return GetGraphPrivate(graph.GetGraphProperty().GetCountVertices(), graph.GetGraphProperty().GetCountEdges(), graphClassID, graph.GetGraphProperty().GetIsChordal(),
                graph.GetGraphProperty().GetIsRegular(), graph.GetGraphProperty().GetIsCyclic(), eulerianGraphID, graph.GetGraphProperty().GetMaximumVertexDegree(),
                graph.GetGraphProperty().GetMinimumVertexDegree(), graph.GetGraphProperty().GetAverageVertexDegree(), graph.GetGraphProperty().GetMedianVertexDegree(), graph.GetGraphProperty().GetCutVertices().Count,
                graph.GetGraphProperty().GetBridges().Count, graph.GetGraphProperty().GetGirth(), graph.GetGraphProperty().GetDegreeSequenceInt(false).ToArray());
        }

        public int GetGraph(int CountVertices, int CountEdges, GraphColoring.Graph.GraphClass.GraphClass.GraphClassEnum GraphClass, bool IsChordal, bool IsRegular, bool IsCyclic, GraphColoring.Graph.GraphProperty.GraphProperty.EulerianGraphEnum EulerianGraph,
            int MaximumVertexDegree, int MinimumVertexDegree, double AverageVertexDegree, int MedianVertexDegree, int CountCutVertices, int CountBridges, int Girth, int[] VertexDegreeArray)
        {
            // Get EulerianGraphEnum
            int eulerianGraphID;
            if (!EulerianGraphEnumIDDictionary.TryGetValue(EulerianGraph, out eulerianGraphID))
                eulerianGraphID = EulerianGraphEnumIDDictionary[GraphColoring.Graph.GraphProperty.GraphProperty.EulerianGraphEnum.undefined];

            // Get GraphClassEnum
            int graphClassID;
            if (!GraphClassEnumIDDictionary.TryGetValue(GraphClass, out graphClassID))
                graphClassID = GraphClassEnumIDDictionary[GraphColoring.Graph.GraphClass.GraphClass.GraphClassEnum.undefined];

            return GetGraphPrivate(CountVertices, CountEdges, graphClassID, IsChordal, IsRegular, IsCyclic, eulerianGraphID,
                               MaximumVertexDegree, MinimumVertexDegree, AverageVertexDegree, MedianVertexDegree, CountCutVertices,
                               CountBridges, Girth, VertexDegreeArray);
        }

        /// <summary>
        /// Save a graph coloring to the DB.
        /// Should not be used. Better use AddGraphColoring
        /// </summary>
        /// <param name="ID_Graph">ID graph</param>
        /// <param name="graphColoringAlgorithmEnum">algorithm</param>
        public void InsertGraphColoring(int ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum graphColoringAlgorithmEnum,
            int countColors)
        {
            // Variable
            int ID_GraphColoringAlgorithm;

            if (!GraphColoringAlgorithmEnumIDDictionary.TryGetValue(graphColoringAlgorithmEnum, out ID_GraphColoringAlgorithm))
                throw new MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException(graphColoringAlgorithmEnum.ToString());
            
            InsertCore(ID_Graph, ID_GraphColoringAlgorithm, countColors);
        }

        /// <summary>
        /// Save a graph coloring to the DB.
        /// Should not be used. Better use AddGraphColoring
        /// </summary>
        /// <param name="ID_Graph">ID graph</param>
        /// <param name="graphColoringAlgorithmEnum">algorithm</param>
        /// <param name="countColors">Count of colors</param>
        /// <param name="countOfIterations">Count of iterations</param>
        /// <param name="minColors">Minimum colors</param>
        /// <param name="maxColors">Maximum colors</param>
        public void InsertGraphColoring(int ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum graphColoringAlgorithmEnum,
            int countOfIterations, int minColors, int maxColors)
        {
            // Variable
            int ID_GraphColoringAlgorithm;

            if (!GraphColoringAlgorithmEnumIDDictionary.TryGetValue(graphColoringAlgorithmEnum, out ID_GraphColoringAlgorithm))
                throw new MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException(graphColoringAlgorithmEnum.ToString());

            InsertCoreProbability(ID_Graph, ID_GraphColoringAlgorithm, countOfIterations, minColors, maxColors);
        }

        /// <summary>
        /// Update a graph coloring in the DB.
        /// Should not be used. Better use AddGraphColoring
        /// </summary>
        /// <param name="ID_Graph">ID graph</param>
        /// <param name="graphColoringAlgorithmEnum">algorithm</param>
        public void UpdateGraphColoring(int ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum graphColoringAlgorithmEnum,
            int countColors)
        {
            // Variable
            int ID_GraphColoringAlgorithm;

            if (!GraphColoringAlgorithmEnumIDDictionary.TryGetValue(graphColoringAlgorithmEnum, out ID_GraphColoringAlgorithm))
                throw new MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException(graphColoringAlgorithmEnum.ToString());

            UpdateCore(ID_Graph, ID_GraphColoringAlgorithm, countColors);
        }

        /// <summary>
        /// Update a graph coloring in the DB.
        /// Should not be used. Better use AddGraphColoring
        /// </summary>
        /// <param name="ID_Graph">ID graph</param>
        /// <param name="graphColoringAlgorithmEnum">algorithm</param>
        /// <param name="countColors">Count of colors</param>
        /// <param name="countOfIterations">Count of iterations</param>
        /// <param name="minColors">Minimum colors</param>
        /// <param name="maxColors">Maximum colors</param>
        public void UpdateGraphColoring(int ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum graphColoringAlgorithmEnum,
            int countOfIterations, int minColors, int maxColors)
        {
            // Variable
            int ID_GraphColoringAlgorithm;

            if (!GraphColoringAlgorithmEnumIDDictionary.TryGetValue(graphColoringAlgorithmEnum, out ID_GraphColoringAlgorithm))
                throw new MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException(graphColoringAlgorithmEnum.ToString());

            UpdateCoreProbability(ID_Graph, ID_GraphColoringAlgorithm, countOfIterations, minColors, maxColors);
        }

        /// <summary>
        /// Add a graph coloring to the DB.
        /// If the record exists call Update, otherwise Insert
        /// </summary>
        /// <param name="ID_Graph">ID graph</param>
        /// <param name="graphColoringAlgorithmEnum">algorithm</param>
        public void AddGraphColoring(int ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum graphColoringAlgorithmEnum,
            int countColors)
        {
            // Variable
            int ID_GraphColoringAlgorithm;

            if (!GraphColoringAlgorithmEnumIDDictionary.TryGetValue(graphColoringAlgorithmEnum, out ID_GraphColoringAlgorithm))
                throw new MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException(graphColoringAlgorithmEnum.ToString());

            AddCore(ID_Graph, ID_GraphColoringAlgorithm, countColors);
        }

        /// <summary>
        /// Add a graph coloring to the DB.
        /// If the record exists call Update, otherwise Insert
        /// </summary>
        /// <param name="ID_Graph">ID graph</param>
        /// <param name="graphColoringAlgorithmEnum">algorithm</param>
        /// <param name="countColors">Count of colors</param>
        /// <param name="countOfIterations">Count of iterations</param>
        /// <param name="minColors">Minimum colors</param>
        /// <param name="maxColors">Maximum colors</param>
        public void AddGraphColoring(int ID_Graph, GraphColoring.GraphColoringAlgorithm.GraphColoringAlgorithm.GraphColoringAlgorithmEnum graphColoringAlgorithmEnum,
            int countOfIterations, int minColors, int maxColors)
        {
            // Variable
            int ID_GraphColoringAlgorithm;

            if (!GraphColoringAlgorithmEnumIDDictionary.TryGetValue(graphColoringAlgorithmEnum, out ID_GraphColoringAlgorithm))
                throw new MyException.DatabaseException.DatabaseAlgorithmDoesNotExistException(graphColoringAlgorithmEnum.ToString());

            AddCoreProbability(ID_Graph, ID_GraphColoringAlgorithm, countOfIterations, minColors, maxColors);
        }

        public void CleanDB()
        {
            RemoveAllRecors();
        }

        #endregion
        
        #region Property
        public ConnectionState GetConnectionState()
        {
            return connection.State;
        }
        #endregion
    }
}
