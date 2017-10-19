using System;
using System.Runtime.Serialization;
using Example.Collection.Infrastructure.Enum;

namespace Example.Collection.Infrastructure.Exceptions
{
    public class BusinessException : BaseException
    {
        public object[] MessageParameters { get; set; }

        public string ResponseMessage { get; set; }

        public BusinessException(string message) : base(message) { }

        public BusinessException(string responseCode, string message) : base(message, responseCode) { }

        public BusinessException(string responseCode, string message, Exception inner) : base(message, responseCode, inner) { }

        protected BusinessException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public BusinessException(ResponseCodeEnum responseCode, string message) : base(message, responseCode.ToString()) { }

        public BusinessException(ResponseCodeEnum responseCode, string message, Exception inner) : base(message, responseCode.ToString(), inner) { }

        public BusinessException(ResponseCodeEnum responseCode, string message, params object[] messageParameters) : base(message, responseCode.ToString())
        {
            MessageParameters = messageParameters;
        }

        public BusinessException(string responseCode, string message, params object[] messageParameters) : base(message, responseCode)
        {
            MessageParameters = messageParameters;
        }
    }
}
