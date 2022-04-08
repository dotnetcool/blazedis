using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazedis.App.Exceptions
{
    public abstract class BlazedisExceptionBase : Exception
    {
        public BlazedisExceptionBase() : base()
        {
        }

        public BlazedisExceptionBase(string? message) : base(message)
        {
        }

        public BlazedisExceptionBase(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
