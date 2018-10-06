using System;
using System.Runtime.Serialization;

namespace Atarashii.Profile.Lastprof
{
    public partial class Parser
    {
        [Serializable]
        public class ParserException : Exception
        {
            //
            // For guidelines regarding the creation of new exception types, see
            //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
            // and
            //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
            //

            public ParserException()
            {
            }

            public ParserException(string message) : base(message)
            {
            }

            public ParserException(string message, Exception inner) : base(message, inner)
            {
            }

            protected ParserException(
                SerializationInfo info,
                StreamingContext context) : base(info, context)
            {
            }
        }
    }
}