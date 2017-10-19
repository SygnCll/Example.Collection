using System;
using System.Runtime.Serialization;

namespace Example.Collection.Infrastructure.Exceptions
{
    public abstract class BaseException : Exception
    {
        public readonly string ResponseCode;

        public object[] Parameters;

        protected BaseException(string message) : base(message) { }

        protected BaseException(string message, string responseCode) : base(message)
        {
            ResponseCode = responseCode;
        }

        protected BaseException(string message, string responseCode, Exception inner) : base(message, inner)
        {
            ResponseCode = responseCode;
        }

        protected BaseException(string message, string responseCode, params object[] parameters) : this(message, responseCode)
        {
            Parameters = parameters;
        }

        protected BaseException(string message, string responseCode, Exception inner, params object[] parameters) : this(message, responseCode, inner)
        {
            Parameters = parameters;
        }

        protected BaseException(string message, Exception inner) : base(message, inner) { }

        protected BaseException(string message, params object[] parameters) : base(message)
        {
            Parameters = parameters;
        }

        protected BaseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
