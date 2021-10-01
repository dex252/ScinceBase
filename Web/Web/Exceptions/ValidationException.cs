using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Exceptions
{
    public class ValidationException: Exception
    {
        public string Log { get; }
        public ValidationException(string message, string json): base(message)
        {
            Log = json;
        }
    }
}
