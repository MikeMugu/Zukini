using System;
using System.Runtime.Serialization;

namespace Zukini
{
    /// <summary>Thrown when an assertion failed.</summary>
    [Serializable]
    public class ZukiniAssertionException : Exception
    {
        /// <param name="message">The error message that explains
        /// the reason for the exception</param>
        public ZukiniAssertionException(string message) : base(message) {}

        /// <param name="message">The error message that explains
        /// the reason for the exception</param>
        /// <param name="inner">The exception that caused the
        /// current exception</param>
        public ZukiniAssertionException(string message, Exception inner) : base(message, inner){}

        /// <summary>Serialization Constructor</summary>
        protected ZukiniAssertionException(SerializationInfo info, StreamingContext context): base(info, context) {}
    }
}
