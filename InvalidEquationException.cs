using System;
using System.Runtime.Serialization;

namespace Equ
{
    [Serializable]
    internal class InvalidEquationException : Exception
    {
        private Error errorType;
        private string message;

        public InvalidEquationException()
        {
        }

        public InvalidEquationException(string message) : base(message)
        {
        }

        public InvalidEquationException(Error e, string s)
        {
            this.MessageString = s;
            this.ErrorType = e;
        }

        public InvalidEquationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidEquationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public string MessageString { get => message; set => message = value; }
        internal Error ErrorType { get => errorType; set => errorType = value; }
    }
}
