using System;

namespace GraphColoringConsole.MyException.DatabaseException
{
    class DatabaseException : Exception
    {
        public DatabaseException() { }

        public DatabaseException(string message) : base(message) { }

        public DatabaseException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Database exception
    /// Unable to connect to DB
    /// </summary>
    class GenerateGraphsDatabaseNotOpenException : DatabaseException
    {
        public GenerateGraphsDatabaseNotOpenException() { }

        public GenerateGraphsDatabaseNotOpenException(string message) : base(message) { }

        public GenerateGraphsDatabaseNotOpenException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Database exception
    /// Algorithm does not exist
    /// </summary>
    class DatabaseAlgorithmDoesNotExistException : DatabaseException
    {
        public DatabaseAlgorithmDoesNotExistException() { }

        public DatabaseAlgorithmDoesNotExistException(string message) : base(message) { }

        public DatabaseAlgorithmDoesNotExistException(string message, Exception inner) : base(message, inner) { }
    }
}
