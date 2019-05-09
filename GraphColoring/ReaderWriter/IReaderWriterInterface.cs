using System;

namespace GraphColoring.ReaderWriter
{
    public interface IReaderWriterInterface
    {
        #region Method
        void DeleteFile();
        void CreateFile();
        Boolean ExistsFile();
        void ClearFile();
        #endregion
        
        #region Property
        string GetPath();
        #endregion
    }
}
