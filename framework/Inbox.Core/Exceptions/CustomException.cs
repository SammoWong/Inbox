using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inbox.Core.Exceptions
{
    public class CustomException : Exception
    {
        public CustomException(int code)
        {
            Code = code;
        }

        public CustomException(int code, string message) : base(message)
        {
            Code = code;
        }

        public int Code { get; set; }
    }
}
