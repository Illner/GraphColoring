using System;

namespace GraphColoring.MyException
{
    /// <summary>
    /// ReaderWriter exceptions
    /// </summary>
    class ReaderWriterException : Exception
    {
        public ReaderWriterException() { }

        public ReaderWriterException(string message) : base(message) { }

        public ReaderWriterException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// ReaderWriter exception
    /// Navalidní cesta k souboru
    /// </summary>
    class ReaderWriterInavalidPathException : ReaderWriterException
    {
        public ReaderWriterInavalidPathException() { }

        ReaderWriterInavalidPathException(string message) : base(message) { }

        ReaderWriterInavalidPathException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// ReaderWriter exception
    /// Uživatel nemá přístup k danému souboru
    /// </summary>
    class ReaderWriterNoAuthorizationException : ReaderWriterException
    {

        public ReaderWriterNoAuthorizationException() { }

        ReaderWriterNoAuthorizationException(string message) : base(message) { }

        ReaderWriterNoAuthorizationException(string message, Exception inner) : base(message, inner) { }
    }
}