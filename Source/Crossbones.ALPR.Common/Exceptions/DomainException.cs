using System;
using System.Net;

namespace Crossbones.ALPR.Common.Exceptions
{
    public abstract class DomainException :Exception
    {
        public HttpStatusCode HttpCode { get; }

        public DomainException(string message, HttpStatusCode code) : base(message) => HttpCode = code;
        public DomainException(string message, Exception innerException, HttpStatusCode code) : base(message, innerException) => HttpCode = code;
    }
}
