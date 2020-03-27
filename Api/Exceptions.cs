using System;
using System.Runtime.Serialization;

namespace Api
{
    [Serializable]
    internal class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    internal class BadParametersException : Exception
    {
        public BadParametersException()
        {
        }

        public BadParametersException(string message) : base(message)
        {
        }

        public BadParametersException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BadParametersException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class ConflictException : Exception
    {
        public ConflictException()
        {
        }

        public ConflictException(string message) : base(message)
        {
        }

        public ConflictException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConflictException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    internal class UnAuthorized : Exception
    {
        public UnAuthorized()
        {
        }

        public UnAuthorized(string message) : base(message)
        {
        }

        public UnAuthorized(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnAuthorized(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
