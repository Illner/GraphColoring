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

        public ReaderWriterInavalidPathException(string message) : base(message) { }

        public ReaderWriterInavalidPathException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// ReaderWriter exception
    /// Uživatel nemá přístup k danému souboru
    /// </summary>
    class ReaderWriterNoAuthorizationException : ReaderWriterException
    {
        public ReaderWriterNoAuthorizationException() { }

        public ReaderWriterNoAuthorizationException(string message) : base(message) { }

        public ReaderWriterNoAuthorizationException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// ReaderWriter exception
    /// Špatný typ souboru (!= FILETYPE)
    /// </summary>
    class ReaderWriterInvalidFileTypeException : ReaderWriterException
    {
        public ReaderWriterInvalidFileTypeException() { }

        public ReaderWriterInvalidFileTypeException(string message) : base(message) { }

        public ReaderWriterInvalidFileTypeException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// ReaderWriter exception
    /// Špatná hlavičkou souboru (!= READERWRITERHEADER + representation)
    /// </summary>
    class ReaderWriterInvalidHeaderException : ReaderWriterException
    {
        public ReaderWriterInvalidHeaderException() { }

        public ReaderWriterInvalidHeaderException(string message) : base(message) { }

        public ReaderWriterInvalidHeaderException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// ReaderWriter exception
    /// Špatná core data v souboru.
    /// </summary>
    class ReaderWriterInvalidDataException : ReaderWriterException
    {
        public ReaderWriterInvalidDataException() { }

        public ReaderWriterInvalidDataException(string message) : base(message) { }

        public ReaderWriterInvalidDataException(string message, Exception inner) : base(message, inner) { }
    }
}