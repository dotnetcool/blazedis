using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazedis.App.Exceptions
{
    public class BlazedisFailedConnectedServerException : BlazedisExceptionBase
    {
        public BlazedisFailedConnectedServerException() : base()
        {
        }

        public BlazedisFailedConnectedServerException(string? message) : base(message)
        {
        }

        public BlazedisFailedConnectedServerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
