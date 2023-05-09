using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Crossbones.ALPR.Common.Exceptions
{
    public class UnAuthorized :DomainException
    {
        public UnAuthorized(string message) : base(message, HttpStatusCode.Unauthorized) { }
        public UnAuthorized(string message, Exception innerException) : base(message, innerException, HttpStatusCode.Unauthorized) { }
    }
}
