using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphColoring.MyException
{
    /// <summary>
    /// Generate graph exceptions
    /// </summary>
    class GenerateGraphException : Exception
    {
        public GenerateGraphException() { }

        public GenerateGraphException(string message) : base(message) { }

        public GenerateGraphException(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Není ošetřen záznam v ErdosReneiModelProbabilityEnum
    /// </summary>
    class ErdosReneiModelProbabilityEnumMissing : Exception
    {
        public ErdosReneiModelProbabilityEnumMissing() { }

        public ErdosReneiModelProbabilityEnumMissing(string message) : base(message) { }

        public ErdosReneiModelProbabilityEnumMissing(string message, Exception inner) : base(message, inner) { }
    }

    /// <summary>
    /// Není vybrána volba p
    /// </summary>
    class ErdosReneiModelChoosePNotAssigned : Exception
    {
        public ErdosReneiModelChoosePNotAssigned() { }

        public ErdosReneiModelChoosePNotAssigned(string message) : base(message) { }

        public ErdosReneiModelChoosePNotAssigned(string message, Exception inner) : base(message, inner) { }
    }
}
