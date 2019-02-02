using System;
using System.Runtime.Serialization;

namespace SPV3.Loader
{
    /// <summary>
    ///     Exception thrown when the HCE data is deemed invalid.
    /// </summary>
    [Serializable]
    public class HceDataException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public HceDataException()
        {
        }

        public HceDataException(string message) : base(message)
        {
        }

        public HceDataException(string message, Exception inner) : base(message, inner)
        {
        }

        protected HceDataException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}