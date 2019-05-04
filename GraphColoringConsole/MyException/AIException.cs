using System;

namespace GraphColoringConsole.MyException.AIException
{
    class AIException : Exception
    {
        public AIException() { }

        public AIException(string message) : base(message) { }

        public AIException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// AI exception
    /// Data file doesn't exist
    /// </summary>
    class DataFileDoesntExist : AIException
    {
        public DataFileDoesntExist() { }

        public DataFileDoesntExist(string message) : base(message) { }

        public DataFileDoesntExist(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// AI exception
    /// Model doesn't exist
    /// </summary>
    class ModelDoesntExistException : AIException
    {
        public ModelDoesntExistException() { }

        public ModelDoesntExistException(string message) : base(message) { }

        public ModelDoesntExistException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// AI exception
    /// Invalid test fraction
    /// </summary>
    class InvalidTestFractionException : AIException
    {
        public InvalidTestFractionException() { }

        public InvalidTestFractionException(string message) : base(message) { }

        public InvalidTestFractionException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// AI exception
    /// Not enough data to create model
    /// </summary>
    class NotEnoughDataToCreateModel : AIException
    {
        public NotEnoughDataToCreateModel() { }

        public NotEnoughDataToCreateModel(string message) : base(message) { }

        public NotEnoughDataToCreateModel(string message, Exception inner) : base(message, inner) { }
    }
}
