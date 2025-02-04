using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace VTS.QLNS.CTC.Utility.Exceptions
{
    public class InputPasswordExceedLimitException : Exception
    {
        public string Username { get; set; }

        public InputPasswordExceedLimitException(string username)
        {
            Username = username;
        }

        public InputPasswordExceedLimitException(string message, string username) : base(message)
        {
            Username = username;
        }

        public InputPasswordExceedLimitException(string message, Exception innerException, string username) : base(message, innerException)
        {
            Username = username;
        }
    }
}
