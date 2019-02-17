using System;
using System.IO;

namespace GraphColoring.ReaderWriter
{
    public interface IReaderWriterInterface
    {
        // Method
        #region
        void DeleteFile();
        void CreateFile();
        Boolean ExistsFile();
        void ClearFile();
        #endregion

        // Property
        #region
        string GetPath();
        #endregion
    }
}
