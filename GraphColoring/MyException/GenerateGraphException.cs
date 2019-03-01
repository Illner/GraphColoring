using System;

namespace GraphColoring.MyException.GenerateGraphException
{
    /// <summary>
    /// Generate graph exceptions
    /// </summary>
    public class GenerateGraphException : Exception
    {
        public GenerateGraphException() { }

        public GenerateGraphException(string message) : base(message) { }

        public GenerateGraphException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Enum probability is missing
    /// </summary>
    public class ErdosReneiModelProbabilityEnumMissing : Exception
    {
        public ErdosReneiModelProbabilityEnumMissing() { }

        public ErdosReneiModelProbabilityEnumMissing(string message) : base(message) { }

        public ErdosReneiModelProbabilityEnumMissing(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Not assigned attribute p
    /// </summary>
    public class ErdosReneiModelChoosePNotAssigned : Exception
    {
        public ErdosReneiModelChoosePNotAssigned() { }

        public ErdosReneiModelChoosePNotAssigned(string message) : base(message) { }

        public ErdosReneiModelChoosePNotAssigned(string message, Exception inner) : base(message, inner) { }
    }
}
