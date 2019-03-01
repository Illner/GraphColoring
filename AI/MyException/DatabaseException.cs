using System;

namespace GraphColoring.MyException.DatabaseException
{
    class DatabaseException : Exception
    {
        public DatabaseException() { }

        public DatabaseException(string message) : base(message) { }

        public DatabaseException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Database exception
    /// Unable to connect to the DB
    /// </summary>
    class DatabaseNotOpenException : DatabaseException
    {
        public DatabaseNotOpenException() { }

        public DatabaseNotOpenException(string message) : base(message) { }

        public DatabaseNotOpenException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Database exception
    /// MinCount or MaxCount (represents count of vertices) is invalid
    /// </summary>
    class DatabaseInvalidArgumentsMinCountMaxCountException : DatabaseException
    {
        public DatabaseInvalidArgumentsMinCountMaxCountException() { }

        public DatabaseInvalidArgumentsMinCountMaxCountException(string message) : base(message) { }

        public DatabaseInvalidArgumentsMinCountMaxCountException(string message, Exception inner) : base(message, inner) { }
    }
}
