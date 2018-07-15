﻿using System;

namespace GraphColoring.MyException
{
    /// <summary>
    /// Tests exception
    /// </summary>
    class TestsException : Exception
    {
        public TestsException() { }

        public TestsException(string message) : base(message) { }

        public TestsException(string message, Exception inner) : base(message, inner) { }
    }


    /// <summary>
    /// Tests exception
    /// Chybí test
    /// </summary>
    class TestsMissingTestException : TestsException
    {
        public TestsMissingTestException() { }

        public TestsMissingTestException(string message) : base(message) { }

        public TestsMissingTestException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Tests exception
    /// Něco je špatně v testu
    /// </summary>
    class SomethingWrongTestException : TestsException
    {
        public SomethingWrongTestException() { }

        public SomethingWrongTestException(string message) : base(message) { }

        public SomethingWrongTestException(string message, Exception inner) : base(message, inner) { }
    }
}