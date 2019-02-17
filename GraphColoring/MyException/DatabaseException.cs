using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.MyException.DatabaseException
{
    public class DatabaseException : Exception
    {
        public DatabaseException() { }

        public DatabaseException(string message) : base(message) { }

        public DatabaseException(string message, Exception inner) : base(message, inner) { }
    }

    public class DatabaseNotOpenException : DatabaseException
    {
        public DatabaseNotOpenException() { }

        public DatabaseNotOpenException(string message) : base(message) { }

        public DatabaseNotOpenException(string message, Exception inner) : base(message, inner) { }
    }

    public class DatabaseInvalidArgumentsMinCountMaxCountException : DatabaseException
    {
        public DatabaseInvalidArgumentsMinCountMaxCountException() { }

        public DatabaseInvalidArgumentsMinCountMaxCountException(string message) : base(message) { }

        public DatabaseInvalidArgumentsMinCountMaxCountException(string message, Exception inner) : base(message, inner) { }
    }
}
