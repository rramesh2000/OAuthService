using System;

namespace Application.Common.Exceptions
{
    public class InvalidClientException : Exception
    {
        public InvalidClientException()
        {
        }
        public InvalidClientException(string message) : base(message)
        {
        }

        public InvalidClientException(string message, Exception innerException) : base(message, innerException)
        {
        }

      
    }
}
