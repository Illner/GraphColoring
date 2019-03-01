using System;

namespace GraphColoring.MyException.ReaderWriterException
{
    /// <summary>
    /// ReaderWriter exceptions
    /// </summary>
    public class ReaderWriterException : Exception
    {
        public ReaderWriterException() { }

        public ReaderWriterException(string message) : base(message) { }

        public ReaderWriterException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// ReaderWriter exception
    /// Invalid file path
    /// </summary>
    public class ReaderWriterInavalidPathException : ReaderWriterException
    {
        public ReaderWriterInavalidPathException() { }

        public ReaderWriterInavalidPathException(string message) : base(message) { }

        public ReaderWriterInavalidPathException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// ReaderWriter exception
    /// No permission for modification
    /// </summary>
    public class ReaderWriterNoAuthorizationException : ReaderWriterException
    {
        public ReaderWriterNoAuthorizationException() { }

        public ReaderWriterNoAuthorizationException(string message) : base(message) { }

        public ReaderWriterNoAuthorizationException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// ReaderWriter exception
    /// Invalid file name extension ".graph"
    /// </summary>
    public class ReaderWriterInvalidFileTypeException : ReaderWriterException
    {
        public ReaderWriterInvalidFileTypeException() { }

        public ReaderWriterInvalidFileTypeException(string message) : base(message) { }

        public ReaderWriterInvalidFileTypeException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// ReaderWriter exception
    /// Invalid header in a file (!= READERWRITERHEADER + representation)
    /// </summary>
    public class ReaderWriterInvalidHeaderException : ReaderWriterException
    {
        public ReaderWriterInvalidHeaderException() { }

        public ReaderWriterInvalidHeaderException(string message) : base(message) { }

        public ReaderWriterInvalidHeaderException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// ReaderWriter exception
    /// Invalid core data in a file
    /// </summary>
    public class ReaderWriterInvalidDataException : ReaderWriterException
    {
        public ReaderWriterInvalidDataException() { }

        public ReaderWriterInvalidDataException(string message) : base(message) { }

        public ReaderWriterInvalidDataException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// ReaderWriter exception
    /// Invalid format of a file
    /// </summary>
    public class ReaderWriterInvalidFormatException : ReaderWriterException
    {
        public ReaderWriterInvalidFormatException() { }

        public ReaderWriterInvalidFormatException(string message) : base(message) { }

        public ReaderWriterInvalidFormatException(string message, Exception inner) : base(message, inner) { }
    }
}